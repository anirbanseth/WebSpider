namespace WebSpider
{
    partial class WebBrowser
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
            this.oIE = new System.Windows.Forms.WebBrowser();
            this.button1 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // oIE
            // 
            this.oIE.Location = new System.Drawing.Point(-2, -3);
            this.oIE.MinimumSize = new System.Drawing.Size(20, 20);
            this.oIE.Name = "oIE";
            this.oIE.Size = new System.Drawing.Size(652, 250);
            this.oIE.TabIndex = 0;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(535, 309);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 1;
            this.button1.Text = "Test";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // WebBrowser
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(652, 404);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.oIE);
            this.Name = "WebBrowser";
            this.Text = "WebBrowser";
            this.Load += new System.EventHandler(this.WebBrowser_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.WebBrowser oIE;
        private System.Windows.Forms.Button button1;
    }
}