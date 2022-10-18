namespace CefClient.CarVideo
{
    partial class PlayBack
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
            this.StartTime = new System.Windows.Forms.DateTimePicker();
            this.OverTime = new System.Windows.Forms.DateTimePicker();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.videoPlay = new System.Windows.Forms.Button();
            this.TipText = new System.Windows.Forms.Label();
            this.recordVideo = new System.Windows.Forms.Button();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.imageBox2 = new Emgu.CV.UI.ImageBox();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.imageBox2)).BeginInit();
            this.SuspendLayout();
            // 
            // StartTime
            // 
            this.StartTime.CalendarForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.StartTime.CalendarTrailingForeColor = System.Drawing.SystemColors.Desktop;
            this.StartTime.Format = System.Windows.Forms.DateTimePickerFormat.Time;
            this.StartTime.Location = new System.Drawing.Point(35, 1);
            this.StartTime.MinDate = new System.DateTime(2020, 12, 25, 0, 0, 0, 0);
            this.StartTime.Name = "StartTime";
            this.StartTime.Size = new System.Drawing.Size(101, 21);
            this.StartTime.TabIndex = 1;
            // 
            // OverTime
            // 
            this.OverTime.CalendarForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.OverTime.CalendarTrailingForeColor = System.Drawing.SystemColors.Desktop;
            this.OverTime.Format = System.Windows.Forms.DateTimePickerFormat.Time;
            this.OverTime.Location = new System.Drawing.Point(174, 1);
            this.OverTime.Name = "OverTime";
            this.OverTime.Size = new System.Drawing.Size(105, 21);
            this.OverTime.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(4, 5);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(29, 12);
            this.label1.TabIndex = 2;
            this.label1.Text = "开始";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(141, 5);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(29, 12);
            this.label2.TabIndex = 2;
            this.label2.Text = "结束";
            // 
            // videoPlay
            // 
            this.videoPlay.Location = new System.Drawing.Point(371, 0);
            this.videoPlay.Name = "videoPlay";
            this.videoPlay.Size = new System.Drawing.Size(60, 23);
            this.videoPlay.TabIndex = 3;
            this.videoPlay.Text = "播放";
            this.videoPlay.UseVisualStyleBackColor = true;
            this.videoPlay.Click += new System.EventHandler(this.button1_Click);
            // 
            // TipText
            // 
            this.TipText.AutoSize = true;
            this.TipText.BackColor = System.Drawing.Color.White;
            this.TipText.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.TipText.ForeColor = System.Drawing.Color.Red;
            this.TipText.Location = new System.Drawing.Point(12, 586);
            this.TipText.Name = "TipText";
            this.TipText.Size = new System.Drawing.Size(0, 14);
            this.TipText.TabIndex = 4;
            // 
            // recordVideo
            // 
            this.recordVideo.Location = new System.Drawing.Point(440, 0);
            this.recordVideo.Name = "recordVideo";
            this.recordVideo.Size = new System.Drawing.Size(60, 23);
            this.recordVideo.TabIndex = 3;
            this.recordVideo.Text = "录制";
            this.recordVideo.UseVisualStyleBackColor = true;
            this.recordVideo.Click += new System.EventHandler(this.button2_Click);
            // 
            // comboBox1
            // 
            this.comboBox1.Enabled = false;
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Items.AddRange(new object[] {
            "1",
            "2",
            "4",
            "8",
            "16"});
            this.comboBox1.Location = new System.Drawing.Point(319, 1);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(42, 20);
            this.comboBox1.TabIndex = 8;
            this.comboBox1.Text = "1";
            this.comboBox1.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(286, 5);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(29, 12);
            this.label3.TabIndex = 2;
            this.label3.Text = "倍速";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.comboBox1);
            this.panel1.Controls.Add(this.StartTime);
            this.panel1.Controls.Add(this.OverTime);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.recordVideo);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.videoPlay);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Location = new System.Drawing.Point(29, 369);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(509, 24);
            this.panel1.TabIndex = 9;
            // 
            // imageBox2
            // 
            this.imageBox2.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.imageBox2.BackgroundImage = global::CefClient.Properties.Resources.monitorError;
            this.imageBox2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.imageBox2.FunctionalMode = Emgu.CV.UI.ImageBox.FunctionalModeOption.Minimum;
            this.imageBox2.Location = new System.Drawing.Point(1, 0);
            this.imageBox2.Name = "imageBox2";
            this.imageBox2.Size = new System.Drawing.Size(565, 367);
            this.imageBox2.TabIndex = 7;
            this.imageBox2.TabStop = false;
            // 
            // PlayBack
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(567, 395);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.imageBox2);
            this.Controls.Add(this.TipText);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "PlayBack";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "录像回放（选择时间后确认播放）";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form2_FormClosing);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Form2_FormClosed);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.imageBox2)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        public System.Windows.Forms.DateTimePicker StartTime;
        public System.Windows.Forms.DateTimePicker OverTime;
        public System.Windows.Forms.Label TipText;
        public System.Windows.Forms.Button recordVideo;
        public System.Windows.Forms.Button videoPlay;
        public Emgu.CV.UI.ImageBox imageBox2;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Panel panel1;
    }
}