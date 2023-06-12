namespace DoAnLapTrinhMang.WindowsFormChess
{
    partial class ScoreHistoryForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ScoreHistoryForm));
            this.lstSavedFiles = new System.Windows.Forms.ListBox();
            this.rtResult = new System.Windows.Forms.RichTextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lstSavedFiles
            // 
            this.lstSavedFiles.FormattingEnabled = true;
            this.lstSavedFiles.ItemHeight = 16;
            this.lstSavedFiles.Location = new System.Drawing.Point(12, 38);
            this.lstSavedFiles.Name = "lstSavedFiles";
            this.lstSavedFiles.Size = new System.Drawing.Size(257, 452);
            this.lstSavedFiles.TabIndex = 0;
            this.lstSavedFiles.SelectedIndexChanged += new System.EventHandler(this.lstSavedFiles_SelectedIndexChanged);
            // 
            // rtResult
            // 
            this.rtResult.Location = new System.Drawing.Point(292, 38);
            this.rtResult.Name = "rtResult";
            this.rtResult.Size = new System.Drawing.Size(551, 453);
            this.rtResult.TabIndex = 1;
            this.rtResult.Text = "";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(79, 16);
            this.label1.TabIndex = 2;
            this.label1.Text = "Saved Files";
            // 
            // ScoreHistoryForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(863, 510);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.rtResult);
            this.Controls.Add(this.lstSavedFiles);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "ScoreHistoryForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Score History Form";
            this.Load += new System.EventHandler(this.ScoreHistoryForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox lstSavedFiles;
        private System.Windows.Forms.RichTextBox rtResult;
        private System.Windows.Forms.Label label1;
    }
}