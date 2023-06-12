
namespace DoAnLapTrinhMang.WindowsFormChess
{
    partial class InGameForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(InGameForm));
            this.button1 = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.lblBlackResult = new System.Windows.Forms.Label();
            this.lblBlackPoint = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.lblWhiteResult = new System.Windows.Forms.Label();
            this.lblWhitePoint = new System.Windows.Forms.Label();
            this.lblCountDownTime = new System.Windows.Forms.Label();
            this.btnDauHang = new System.Windows.Forms.Button();
            this.txtTableHistoryLog = new System.Windows.Forms.TextBox();
            this.btnLoadHistory = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.button1.Location = new System.Drawing.Point(743, 446);
            this.button1.Margin = new System.Windows.Forms.Padding(4);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(235, 41);
            this.button1.TabIndex = 0;
            this.button1.Text = "Đi lại";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.panel1);
            this.groupBox1.Controls.Add(this.lblBlackResult);
            this.groupBox1.Controls.Add(this.lblBlackPoint);
            this.groupBox1.Location = new System.Drawing.Point(743, 95);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(235, 108);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Điểm Quân Đen";
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Black;
            this.panel1.Location = new System.Drawing.Point(16, 42);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(46, 28);
            this.panel1.TabIndex = 1;
            // 
            // lblBlackResult
            // 
            this.lblBlackResult.AutoSize = true;
            this.lblBlackResult.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblBlackResult.Location = new System.Drawing.Point(114, 68);
            this.lblBlackResult.Name = "lblBlackResult";
            this.lblBlackResult.Size = new System.Drawing.Size(0, 20);
            this.lblBlackResult.TabIndex = 0;
            // 
            // lblBlackPoint
            // 
            this.lblBlackPoint.AutoSize = true;
            this.lblBlackPoint.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblBlackPoint.Location = new System.Drawing.Point(114, 29);
            this.lblBlackPoint.Name = "lblBlackPoint";
            this.lblBlackPoint.Size = new System.Drawing.Size(19, 20);
            this.lblBlackPoint.TabIndex = 0;
            this.lblBlackPoint.Text = "0";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.panel2);
            this.groupBox2.Controls.Add(this.lblWhiteResult);
            this.groupBox2.Controls.Add(this.lblWhitePoint);
            this.groupBox2.Location = new System.Drawing.Point(743, 224);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(235, 118);
            this.groupBox2.TabIndex = 2;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Điểm Quân Trắng";
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.White;
            this.panel2.Location = new System.Drawing.Point(16, 38);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(46, 28);
            this.panel2.TabIndex = 1;
            // 
            // lblWhiteResult
            // 
            this.lblWhiteResult.AutoSize = true;
            this.lblWhiteResult.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblWhiteResult.Location = new System.Drawing.Point(114, 78);
            this.lblWhiteResult.Name = "lblWhiteResult";
            this.lblWhiteResult.Size = new System.Drawing.Size(19, 20);
            this.lblWhiteResult.TabIndex = 0;
            this.lblWhiteResult.Text = "0";
            // 
            // lblWhitePoint
            // 
            this.lblWhitePoint.AutoSize = true;
            this.lblWhitePoint.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblWhitePoint.Location = new System.Drawing.Point(114, 29);
            this.lblWhitePoint.Name = "lblWhitePoint";
            this.lblWhitePoint.Size = new System.Drawing.Size(19, 20);
            this.lblWhitePoint.TabIndex = 0;
            this.lblWhitePoint.Text = "0";
            // 
            // lblCountDownTime
            // 
            this.lblCountDownTime.AutoSize = true;
            this.lblCountDownTime.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCountDownTime.Location = new System.Drawing.Point(837, 47);
            this.lblCountDownTime.Name = "lblCountDownTime";
            this.lblCountDownTime.Size = new System.Drawing.Size(55, 20);
            this.lblCountDownTime.TabIndex = 0;
            this.lblCountDownTime.Text = "00:00";
            // 
            // btnDauHang
            // 
            this.btnDauHang.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnDauHang.Location = new System.Drawing.Point(743, 370);
            this.btnDauHang.Margin = new System.Windows.Forms.Padding(4);
            this.btnDauHang.Name = "btnDauHang";
            this.btnDauHang.Size = new System.Drawing.Size(235, 45);
            this.btnDauHang.TabIndex = 0;
            this.btnDauHang.Text = "Đầu hàng";
            this.btnDauHang.UseVisualStyleBackColor = true;
            this.btnDauHang.Click += new System.EventHandler(this.btnDauHang_Click);
            // 
            // txtTableHistoryLog
            // 
            this.txtTableHistoryLog.Location = new System.Drawing.Point(1016, 28);
            this.txtTableHistoryLog.Multiline = true;
            this.txtTableHistoryLog.Name = "txtTableHistoryLog";
            this.txtTableHistoryLog.Size = new System.Drawing.Size(264, 528);
            this.txtTableHistoryLog.TabIndex = 3;
            // 
            // btnLoadHistory
            // 
            this.btnLoadHistory.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnLoadHistory.Location = new System.Drawing.Point(743, 515);
            this.btnLoadHistory.Margin = new System.Windows.Forms.Padding(4);
            this.btnLoadHistory.Name = "btnLoadHistory";
            this.btnLoadHistory.Size = new System.Drawing.Size(235, 41);
            this.btnLoadHistory.TabIndex = 0;
            this.btnLoadHistory.Text = "Hiện lịch sử";
            this.btnLoadHistory.UseVisualStyleBackColor = true;
            this.btnLoadHistory.Click += new System.EventHandler(this.btnLoadHistory_Click);
            // 
            // InGameForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1306, 591);
            this.Controls.Add(this.txtTableHistoryLog);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.lblCountDownTime);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btnDauHang);
            this.Controls.Add(this.btnLoadHistory);
            this.Controls.Add(this.button1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "InGameForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Chess Board";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.InGameForm_FormClosing);
            this.Load += new System.EventHandler(this.InGameForm_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label lblBlackPoint;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label lblWhitePoint;
        private System.Windows.Forms.Label lblCountDownTime;
        private System.Windows.Forms.Button btnDauHang;
        private System.Windows.Forms.Label lblBlackResult;
        private System.Windows.Forms.Label lblWhiteResult;
        private System.Windows.Forms.TextBox txtTableHistoryLog;
        private System.Windows.Forms.Button btnLoadHistory;
    }
}

