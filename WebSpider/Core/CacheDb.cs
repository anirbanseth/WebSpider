﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WebSpider.Core
{
    public class CacheDb
    {
        #region [Constants]
        private const String _dsName = "UrlCache";
        private const String _dtName = "Cache";
        #endregion

        #region [Private Variables]
        private static String CacheDBPath;
        private static String CacheDBName;
        private static DataSet CacheDB;
        protected static int _cacheValidity = 3600; // in minutes
        #endregion

        #region [Constructor]
        static CacheDb()
        {
            _cacheValidity = Convert.ToInt32(ConfigurationSettings.AppSettings["CacheTimeout"].ToString());

            CacheDBPath = String.Format("{0}\\Cache", Application.StartupPath);
            CacheDBName = String.Format("{0}\\Cache.xml", CacheDBPath);
            CacheDB = new DataSet(_dsName);
            CacheDB.Tables.Add(new DataTable(_dtName));
            CacheDB.Tables[_dtName].Columns.Add("Url", typeof(String));
            CacheDB.Tables[_dtName].Columns.Add("FileName", typeof(String));
            CacheDB.Tables[_dtName].Columns.Add("LastUpdated", typeof(DateTime));
            CacheDB.Tables[_dtName].Columns.Add("ValidTill", typeof(DateTime));

            CacheDB.Tables[_dtName].Columns["LastUpdated"].DefaultValue = DateTime.Now;
            CacheDB.Tables[_dtName].Columns["ValidTill"].DefaultValue = DateTime.Now.AddMinutes(_cacheValidity);
            LoadCache();
        }
        #endregion

        #region [Load/Save Cache]
        /// <summary>
        /// Loads cache from disk
        /// </summary>
        private static void LoadCache()
        {
            try
            {
                CacheDB.ReadXml(CacheDBName);
                ValidateCache();

                List<String> CacheFiles = CacheDB.Tables[_dtName].AsEnumerable().Select(x => GetFullFileName(x.Field<String>("FileName"))).ToList();
                List<String> DirFiles = Directory.GetFiles(CacheDBPath).ToList();

                DirFiles.Remove(GetFullFileName("Cache.xml"));
                DirFiles.Remove(GetFullFileName("Readme.txt"));

                foreach (String file in DirFiles.Except(CacheFiles))
                {
                    new FileInfo(file).Delete();
                }
            }
            catch { }
        }

        /// <summary>
        /// Saves cache to disk
        /// </summary>
        protected static void SaveCache()
        {
            try
            {
                CacheDB.WriteXml(CacheDBName);
            }
            catch
            {
                throw new Exception("Saving cache failed");
            }
        }
        #endregion

        #region [Generate new FileName]
        /// <summary>
        /// Generates a new filename for cache
        /// </summary>
        /// <returns></returns>
        private static String GenerateFileName()
        {
            List<String> filenames = CacheDB.Tables[_dtName].AsEnumerable().Select(x => x.Field<String>("FileName")).ToList();
            String FileName;
            do
            {
                FileName = Guid.NewGuid().ToString();
            } while (filenames.Contains(FileName));
            return FileName;
        }
        #endregion

        #region [Get Full Filename Including Path]
        /// <summary>
        /// GEt Full Filename including path
        /// </summary>
        /// <param name="FileName"></param>
        /// <returns></returns>
        private static string GetFullFileName(String FileName)
        {
            return String.Format("{0}\\{1}", CacheDBPath, FileName);
        }
        #endregion

        #region [Validate Cache]
        protected static void ValidateCache()
        {
            //var rows = CacheDB.Tables[_dtName].AsEnumerable().Where(x => x.Field<DateTime>("ValidTill") <= DateTime.Now);
            // delete outdated files
            // delete invalid data from cache db
            try
            {
                if (!ReferenceEquals(CacheDB, null))
                {
                    foreach (DataRow dRow in CacheDB.Tables[_dtName].Rows)
                    {
                        if ((DateTime)dRow["ValidTill"] < DateTime.Now)
                        {
                            File.Delete(GetFullFileName(dRow["FileName"].ToString()));
                            dRow.Delete();
                        }
                        else if (!File.Exists(GetFullFileName(dRow["FileName"].ToString())))
                        {
                            dRow.Delete();
                        }
                    }
                }
            }
            catch { }
        }
        #endregion

        #region [Get Cached Url]
        /// <summary>
        /// Get Cached Url
        /// </summary>
        /// <param name="Url"></param>
        /// <returns></returns>
        //private static HtmlAgilityPack.HtmlDocument GetCachedUrl(String Url)
        //{
        //    ValidateCache();
        //    var CacheDBRow = CacheDB.Tables[_dtName].AsEnumerable().Where(x => x.Field<String>("Url") == Url);
        //    if (CacheDBRow.Count() == 1)
        //    {
        //        String FileName = CacheDBRow.Select(x => x.Field<String>("FileName")).FirstOrDefault();
        //        DateTime LastUpdated = CacheDBRow.Select(x => x.Field<DateTime>("LastUpdated")).FirstOrDefault();
        //        DateTime ValidTill = CacheDBRow.Select(x => x.Field<DateTime>("ValidTill")).FirstOrDefault();
        //        //String FileData = File.ReadAllText(FileName);
        //        //return FileData;

        //        FileStream fStream = new FileStream(GetFullFileName(FileName), FileMode.Open);
        //        //XDocument document = XDocument.Load(fStream);
        //        HtmlAgilityPack.HtmlDocument document = new HtmlAgilityPack.HtmlDocument();
        //        document.Load(fStream);
        //        fStream.Close();
        //        return document;
        //    }
        //    return null;
        //}

        public byte[] GetCachedUrl(String Url)
        {
            var CacheDBRow = CacheDB.Tables[_dtName].AsEnumerable().Where(x => x.Field<String>("Url") == Url);
            if (CacheDBRow.Count() == 1)
            {
                String FileName = CacheDBRow.Select(x => x.Field<String>("FileName")).FirstOrDefault();
                DateTime LastUpdated = CacheDBRow.Select(x => x.Field<DateTime>("LastUpdated")).FirstOrDefault();
                DateTime ValidTill = CacheDBRow.Select(x => x.Field<DateTime>("ValidTill")).FirstOrDefault();
                
                byte[] responseBytes = File.ReadAllBytes(GetFullFileName(FileName));
                return responseBytes;
            }
            return null;
        }
        #endregion

        #region [Save Cache]
        public static void SaveCache(String Url, byte[] urlData)
        {
            try
            {
                String FileName = GenerateFileName();
                File.WriteAllBytes(GetFullFileName(FileName), urlData);
                DataRow dRow = CacheDB.Tables[_dtName].NewRow();
                dRow["Url"] = Url;
                dRow["FileName"] = FileName;
                dRow["LastUpdated"] = DateTime.Now;
                dRow["ValidTill"] = DateTime.Now.AddMinutes(_cacheValidity);
                CacheDB.Tables[_dtName].Rows.Add(dRow);
                CacheDB.WriteXml("Cache.xml");
            }
            catch { }
        }
        #endregion

        #region [Is Cached Url]
        /// <summary>
        /// Checks whether URL is cached
        /// </summary>
        /// <param name="Url"></param>
        /// <returns></returns>
        public Boolean IsCachedUrl(String Url)
        {
            //return false;
            if (!ReferenceEquals(CacheDB, null))
            {
                ValidateCache();
                int rowsFound = CacheDB.Tables[_dtName].AsEnumerable().Where(x => x.Field<String>("Url").Equals(Url)).Count();
                return rowsFound == 1 ? true : false;
            }
            return false;
        }
        #endregion
        
    }
}
