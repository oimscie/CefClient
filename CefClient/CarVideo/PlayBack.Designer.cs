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
            this.button1 = new System.Windows.Forms.Button();
            this.TipText = new System.Windows.Forms.Label();
            this.button2 = new System.Windows.Forms.Button();
            this.imageBox2 = new Emgu.CV.UI.ImageBox();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.imageBox2)).BeginInit();
            this.SuspendLayout();
            // 
            // StartTime
            // 
            this.StartTime.CalendarForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.StartTime.CalendarTrailingForeColor = System.Drawing.SystemColors.Desktop;
            this.StartTime.Format = System.Windows.Forms.DateTimePickerFormat.Time;
            this.StartTime.Location = new System.Drawing.Point(352, 722);
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
            this.OverTime.Location = new System.Drawing.Point(531, 722);
            this.OverTime.Name = "OverTime";
            this.OverTime.Size = new System.Drawing.Size(105, 21);
            this.OverTime.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(289, 726);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 2;
            this.label1.Text = "开始时间";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(466, 726);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 12);
            this.label2.TabIndex = 2;
            this.label2.Text = "结束时间";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(800, 721);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(137, 23);
            this.button1.TabIndex = 3;
            this.button1.Text = "播放";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
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
            // button2
            // 
            this.button2.Enabled = false;
            this.button2.Location = new System.Drawing.Point(942, 721);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(139, 23);
            this.button2.TabIndex = 3;
            this.button2.Text = "停止";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // imageBox2
            // 
            this.imageBox2.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.imageBox2.FunctionalMode = Emgu.CV.UI.ImageBox.FunctionalModeOption.Minimum;
            this.imageBox2.InitialImage = global::CefClient.Properties.Resources.load;
            this.imageBox2.Location = new System.Drawing.Point(1, 0);
            this.imageBox2.Name = "imageBox2";
            this.imageBox2.Size = new System.Drawing.Size(1080, 720);
            this.imageBox2.TabIndex = 7;
            this.imageBox2.TabStop = false;
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
            this.comboBox1.Location = new System.Drawing.Point(707, 722);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(87, 20);
            this.comboBox1.TabIndex = 8;
            this.comboBox1.Text = "1";
            this.comboBox1.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(648, 726);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 12);
            this.label3.TabIndex = 2;
            this.label3.Text = "倍速播放";
            // 
            // PlayBack
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1082, 744);
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(this.imageBox2);
            this.Controls.Add(this.TipText);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.OverTime);
            this.Controls.Add(this.StartTime);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "PlayBack";
            this.Text = "录像回放（选择时间后确认播放）";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form2_FormClosing);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Form2_FormClosed);
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
        public System.Windows.Forms.Button button2;
        public System.Windows.Forms.Button button1;
        public Emgu.CV.UI.ImageBox imageBox2;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.Label label3;
    }
}