
namespace DoAnLapTrinhMang.WindowsFormChess
{
    partial class MainMenu
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainMenu));
            this.Start_btn = new System.Windows.Forms.Button();
            this.Connection_btn = new System.Windows.Forms.Button();
            this.btnViewSaveHistory = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // Start_btn
            // 
            this.Start_btn.BackColor = System.Drawing.Color.Pink;
            this.Start_btn.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Start_btn.Location = new System.Drawing.Point(136, 51);
            this.Start_btn.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Start_btn.Name = "Start_btn";
            this.Start_btn.Size = new System.Drawing.Size(176, 66);
            this.Start_btn.TabIndex = 0;
            this.Start_btn.Text = "Single Game";
            this.Start_btn.UseVisualStyleBackColor = false;
            this.Start_btn.Click += new System.EventHandler(this.Start_btn_Click);
            // 
            // Connection_btn
            // 
            this.Connection_btn.BackColor = System.Drawing.Color.SlateBlue;
            this.Connection_btn.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Connection_btn.Location = new System.Drawing.Point(136, 177);
            this.Connection_btn.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Connection_btn.Name = "Connection_btn";
            this.Connection_btn.Size = new System.Drawing.Size(176, 64);
            this.Connection_btn.TabIndex = 1;
            this.Connection_btn.Text = "Lan Game";
            this.Connection_btn.UseVisualStyleBackColor = false;
            this.Connection_btn.Click += new System.EventHandler(this.Connection_btn_Click);
            // 
            // btnViewSaveHistory
            // 
            this.btnViewSaveHistory.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.btnViewSaveHistory.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnViewSaveHistory.Location = new System.Drawing.Point(136, 434);
            this.btnViewSaveHistory.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnViewSaveHistory.Name = "btnViewSaveHistory";
            this.btnViewSaveHistory.Size = new System.Drawing.Size(176, 64);
            this.btnViewSaveHistory.TabIndex = 1;
            this.btnViewSaveHistory.Text = "View Score History";
            this.btnViewSaveHistory.UseVisualStyleBackColor = false;
            this.btnViewSaveHistory.Click += new System.EventHandler(this.btnViewSaveHistory_Click);
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.SystemColors.Highlight;
            this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.Location = new System.Drawing.Point(136, 298);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(176, 64);
            this.button1.TabIndex = 2;
            this.button1.Text = "ONLINE GAME";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // MainMenu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::DoAnLapTrinhMang.WindowsFormChess.Properties.Resources.ronaldodanhcocucchill;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.ClientSize = new System.Drawing.Size(480, 602);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.btnViewSaveHistory);
            this.Controls.Add(this.Connection_btn);
            this.Controls.Add(this.Start_btn);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "MainMenu";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Main Menu";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button Start_btn;
        private System.Windows.Forms.Button Connection_btn;
        private System.Windows.Forms.Button btnViewSaveHistory;
        private System.Windows.Forms.Button button1;
    }
}