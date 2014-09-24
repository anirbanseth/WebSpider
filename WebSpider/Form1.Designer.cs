namespace WebSpider
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.toolBarWeb = new System.Windows.Forms.ToolBar();
            this.toolBarButtonBrowse = new System.Windows.Forms.ToolBarButton();
            this.imageList4 = new System.Windows.Forms.ImageList(this.components);
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.comboBoxWeb = new System.Windows.Forms.ComboBox();
            this.buttonGo = new System.Windows.Forms.Button();
            this.imageList2 = new System.Windows.Forms.ImageList(this.components);
            this.toolBarMain = new System.Windows.Forms.ToolBar();
            this.toolBarButtonContinue = new System.Windows.Forms.ToolBarButton();
            this.toolBarButtonPause = new System.Windows.Forms.ToolBarButton();
            this.toolBarButtonStop = new System.Windows.Forms.ToolBarButton();
            this.toolBarButton1 = new System.Windows.Forms.ToolBarButton();
            this.toolBarButtonDeleteAll = new System.Windows.Forms.ToolBarButton();
            this.toolBarButton2 = new System.Windows.Forms.ToolBarButton();
            this.toolBarButtonSettings = new System.Windows.Forms.ToolBarButton();
            this.tabShopProduct = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.treeCatagory = new System.Windows.Forms.TreeView();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.listViewThreads = new System.Windows.Forms.ListView();
            this.columnHeaderTHreadID = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeaderThreadURL = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeaderThreadAction = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.ListLog = new System.Windows.Forms.ListBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.btnStart = new System.Windows.Forms.Button();
            this.tabShopProduct.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolBarWeb
            // 
            this.toolBarWeb.Appearance = System.Windows.Forms.ToolBarAppearance.Flat;
            this.toolBarWeb.Buttons.AddRange(new System.Windows.Forms.ToolBarButton[] {
            this.toolBarButtonBrowse});
            this.toolBarWeb.ButtonSize = new System.Drawing.Size(50, 26);
            this.toolBarWeb.DropDownArrows = true;
            this.toolBarWeb.ImageList = this.imageList4;
            this.toolBarWeb.Location = new System.Drawing.Point(0, 0);
            this.toolBarWeb.Name = "toolBarWeb";
            this.toolBarWeb.ShowToolTips = true;
            this.toolBarWeb.Size = new System.Drawing.Size(714, 35);
            this.toolBarWeb.TabIndex = 3;
            // 
            // toolBarButtonBrowse
            // 
            this.toolBarButtonBrowse.ImageIndex = 0;
            this.toolBarButtonBrowse.Name = "toolBarButtonBrowse";
            this.toolBarButtonBrowse.Style = System.Windows.Forms.ToolBarButtonStyle.DropDownButton;
            this.toolBarButtonBrowse.ToolTipText = "Browse text sources";
            // 
            // imageList4
            // 
            this.imageList4.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList4.ImageStream")));
            this.imageList4.TransparentColor = System.Drawing.Color.Teal;
            this.imageList4.Images.SetKeyName(0, "");
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Teal;
            this.imageList1.Images.SetKeyName(0, "");
            // 
            // comboBoxWeb
            // 
            this.comboBoxWeb.AllowDrop = true;
            this.comboBoxWeb.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.comboBoxWeb.BackColor = System.Drawing.Color.WhiteSmoke;
            this.comboBoxWeb.ItemHeight = 13;
            this.comboBoxWeb.Location = new System.Drawing.Point(90, 5);
            this.comboBoxWeb.MaxDropDownItems = 20;
            this.comboBoxWeb.Name = "comboBoxWeb";
            this.comboBoxWeb.Size = new System.Drawing.Size(547, 21);
            this.comboBoxWeb.TabIndex = 10;
            this.comboBoxWeb.Tag = "Settings";
            this.comboBoxWeb.Text = "http://";
            // 
            // buttonGo
            // 
            this.buttonGo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonGo.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonGo.ImageIndex = 0;
            this.buttonGo.ImageList = this.imageList1;
            this.buttonGo.Location = new System.Drawing.Point(652, 3);
            this.buttonGo.Name = "buttonGo";
            this.buttonGo.Size = new System.Drawing.Size(40, 22);
            this.buttonGo.TabIndex = 11;
            this.buttonGo.Click += new System.EventHandler(this.buttonGo_Click);
            // 
            // imageList2
            // 
            this.imageList2.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList2.ImageStream")));
            this.imageList2.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList2.Images.SetKeyName(0, "");
            this.imageList2.Images.SetKeyName(1, "");
            this.imageList2.Images.SetKeyName(2, "");
            this.imageList2.Images.SetKeyName(3, "");
            this.imageList2.Images.SetKeyName(4, "");
            // 
            // toolBarMain
            // 
            this.toolBarMain.Appearance = System.Windows.Forms.ToolBarAppearance.Flat;
            this.toolBarMain.Buttons.AddRange(new System.Windows.Forms.ToolBarButton[] {
            this.toolBarButtonContinue,
            this.toolBarButtonPause,
            this.toolBarButtonStop,
            this.toolBarButton1,
            this.toolBarButtonDeleteAll,
            this.toolBarButton2,
            this.toolBarButtonSettings});
            this.toolBarMain.ButtonSize = new System.Drawing.Size(16, 16);
            this.toolBarMain.DropDownArrows = true;
            this.toolBarMain.ImageList = this.imageList2;
            this.toolBarMain.Location = new System.Drawing.Point(0, 35);
            this.toolBarMain.Name = "toolBarMain";
            this.toolBarMain.ShowToolTips = true;
            this.toolBarMain.Size = new System.Drawing.Size(714, 28);
            this.toolBarMain.TabIndex = 12;
            // 
            // toolBarButtonContinue
            // 
            this.toolBarButtonContinue.Enabled = false;
            this.toolBarButtonContinue.ImageIndex = 0;
            this.toolBarButtonContinue.Name = "toolBarButtonContinue";
            this.toolBarButtonContinue.ToolTipText = "Coninue parsing process";
            // 
            // toolBarButtonPause
            // 
            this.toolBarButtonPause.Enabled = false;
            this.toolBarButtonPause.ImageIndex = 1;
            this.toolBarButtonPause.Name = "toolBarButtonPause";
            this.toolBarButtonPause.ToolTipText = "Pause parsing process";
            // 
            // toolBarButtonStop
            // 
            this.toolBarButtonStop.ImageIndex = 2;
            this.toolBarButtonStop.Name = "toolBarButtonStop";
            this.toolBarButtonStop.ToolTipText = "Stop parsing process";
            // 
            // toolBarButton1
            // 
            this.toolBarButton1.Name = "toolBarButton1";
            this.toolBarButton1.Style = System.Windows.Forms.ToolBarButtonStyle.Separator;
            // 
            // toolBarButtonDeleteAll
            // 
            this.toolBarButtonDeleteAll.ImageIndex = 3;
            this.toolBarButtonDeleteAll.Name = "toolBarButtonDeleteAll";
            this.toolBarButtonDeleteAll.ToolTipText = "Delete all results";
            // 
            // toolBarButton2
            // 
            this.toolBarButton2.Name = "toolBarButton2";
            this.toolBarButton2.Style = System.Windows.Forms.ToolBarButtonStyle.Separator;
            // 
            // toolBarButtonSettings
            // 
            this.toolBarButtonSettings.ImageIndex = 4;
            this.toolBarButtonSettings.Name = "toolBarButtonSettings";
            this.toolBarButtonSettings.Style = System.Windows.Forms.ToolBarButtonStyle.DropDownButton;
            this.toolBarButtonSettings.ToolTipText = "Show settings form";
            // 
            // tabShopProduct
            // 
            this.tabShopProduct.Controls.Add(this.tabPage1);
            this.tabShopProduct.Controls.Add(this.tabPage2);
            this.tabShopProduct.Location = new System.Drawing.Point(0, 62);
            this.tabShopProduct.Name = "tabShopProduct";
            this.tabShopProduct.SelectedIndex = 0;
            this.tabShopProduct.Size = new System.Drawing.Size(392, 259);
            this.tabShopProduct.TabIndex = 13;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.treeCatagory);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(384, 233);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Catagory";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // treeCatagory
            // 
            this.treeCatagory.CheckBoxes = true;
            this.treeCatagory.Location = new System.Drawing.Point(3, 3);
            this.treeCatagory.Name = "treeCatagory";
            this.treeCatagory.Size = new System.Drawing.Size(375, 227);
            this.treeCatagory.TabIndex = 0;
            this.treeCatagory.AfterCheck += new System.Windows.Forms.TreeViewEventHandler(this.treeCatagory_AfterCheck);
            // 
            // tabPage2
            // 
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(384, 233);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Brand";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // listViewThreads
            // 
            this.listViewThreads.BackColor = System.Drawing.Color.WhiteSmoke;
            this.listViewThreads.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeaderTHreadID,
            this.columnHeaderThreadURL,
            this.columnHeaderThreadAction});
            this.listViewThreads.FullRowSelect = true;
            this.listViewThreads.GridLines = true;
            this.listViewThreads.HideSelection = false;
            this.listViewThreads.Location = new System.Drawing.Point(388, 84);
            this.listViewThreads.MultiSelect = false;
            this.listViewThreads.Name = "listViewThreads";
            this.listViewThreads.Size = new System.Drawing.Size(323, 237);
            this.listViewThreads.TabIndex = 14;
            this.listViewThreads.UseCompatibleStateImageBehavior = false;
            this.listViewThreads.View = System.Windows.Forms.View.Details;
            // 
            // columnHeaderTHreadID
            // 
            this.columnHeaderTHreadID.Text = "Item";
            this.columnHeaderTHreadID.Width = 100;
            // 
            // columnHeaderThreadURL
            // 
            this.columnHeaderThreadURL.Text = "Uri";
            this.columnHeaderThreadURL.Width = 165;
            // 
            // columnHeaderThreadAction
            // 
            this.columnHeaderThreadAction.Text = "Action";
            this.columnHeaderThreadAction.Width = 52;
            // 
            // ListLog
            // 
            this.ListLog.FormattingEnabled = true;
            this.ListLog.Location = new System.Drawing.Point(0, 343);
            this.ListLog.Name = "ListLog";
            this.ListLog.Size = new System.Drawing.Size(714, 95);
            this.ListLog.TabIndex = 15;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(4, 324);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(25, 13);
            this.label1.TabIndex = 16;
            this.label1.Text = "Log";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(392, 64);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(39, 13);
            this.label2.TabIndex = 17;
            this.label2.Text = "Queue";
            // 
            // btnStart
            // 
            this.btnStart.Location = new System.Drawing.Point(616, 60);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(95, 23);
            this.btnStart.TabIndex = 18;
            this.btnStart.Text = "Start";
            this.btnStart.UseVisualStyleBackColor = true;
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(714, 440);
            this.Controls.Add(this.btnStart);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.ListLog);
            this.Controls.Add(this.listViewThreads);
            this.Controls.Add(this.tabShopProduct);
            this.Controls.Add(this.toolBarMain);
            this.Controls.Add(this.buttonGo);
            this.Controls.Add(this.comboBoxWeb);
            this.Controls.Add(this.toolBarWeb);
            this.Name = "Form1";
            this.Text = "Web Spider";
            this.tabShopProduct.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolBar toolBarWeb;
        private System.Windows.Forms.ToolBarButton toolBarButtonBrowse;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.ImageList imageList4;
        private System.Windows.Forms.ComboBox comboBoxWeb;
        private System.Windows.Forms.Button buttonGo;
        private System.Windows.Forms.ImageList imageList2;
        private System.Windows.Forms.ToolBar toolBarMain;
        private System.Windows.Forms.ToolBarButton toolBarButtonContinue;
        private System.Windows.Forms.ToolBarButton toolBarButtonPause;
        private System.Windows.Forms.ToolBarButton toolBarButtonStop;
        private System.Windows.Forms.ToolBarButton toolBarButton1;
        private System.Windows.Forms.ToolBarButton toolBarButtonDeleteAll;
        private System.Windows.Forms.ToolBarButton toolBarButton2;
        private System.Windows.Forms.ToolBarButton toolBarButtonSettings;
        private System.Windows.Forms.TabControl tabShopProduct;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.TreeView treeCatagory;
        private System.Windows.Forms.ListView listViewThreads;
        private System.Windows.Forms.ColumnHeader columnHeaderTHreadID;
        private System.Windows.Forms.ColumnHeader columnHeaderThreadURL;
        private System.Windows.Forms.ColumnHeader columnHeaderThreadAction;
        private System.Windows.Forms.ListBox ListLog;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnStart;
    }
}

