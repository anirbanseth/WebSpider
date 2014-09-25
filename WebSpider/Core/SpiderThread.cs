using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace WebSpider.Core
{
    public class SpiderThread
    {
        private List<Thread> activeThreads;

        public int MaxThreads { get; set; }
        private int CurrentActiveThreads {get; set; }

        SpiderThread(){
            activeThreads = new List<Thread>();
            MaxThreads = 10;
        }



        public void CancelAllThreads()
        {
            foreach (Thread thread in activeThreads)
                thread.Abort();
        }

        public void PauseAllThread()
        {
            foreach (Thread thread in activeThreads)
                thread.Suspend();
        }

        public void ResumeAllThread()
        {
            foreach (Thread thread in activeThreads)
                thread.Resume();
        }
    }
}
