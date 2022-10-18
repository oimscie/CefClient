namespace CefClient.CarVideo
{
    partial class LiveWindow
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
            this.OpenAudio = new System.Windows.Forms.Button();
            this.RecordVideo = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.imageBox1 = new Emgu.CV.UI.ImageBox();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.imageBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // OpenAudio
            // 
            this.OpenAudio.BackColor = System.Drawing.Color.Transparent;
            this.OpenAudio.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.OpenAudio.Location = new System.Drawing.Point(0, 0);
            this.OpenAudio.Name = "OpenAudio";
            this.OpenAudio.Size = new System.Drawing.Size(75, 25);
            this.OpenAudio.TabIndex = 1;
            this.OpenAudio.TabStop = false;
            this.OpenAudio.Text = "语音通话";
            this.OpenAudio.UseVisualStyleBackColor = false;
            this.OpenAudio.Click += new System.EventHandler(this.button_Click);
            // 
            // RecordVideo
            // 
            this.RecordVideo.Location = new System.Drawing.Point(80, 0);
            this.RecordVideo.Name = "RecordVideo";
            this.RecordVideo.Size = new System.Drawing.Size(75, 25);
            this.RecordVideo.TabIndex = 1;
            this.RecordVideo.TabStop = false;
            this.RecordVideo.Text = "录制视频";
            this.RecordVideo.UseVisualStyleBackColor = true;
            this.RecordVideo.Click += new System.EventHandler(this.RecordVideo_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.OpenAudio);
            this.panel1.Controls.Add(this.RecordVideo);
            this.panel1.Location = new System.Drawing.Point(197, 368);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(155, 25);
            this.panel1.TabIndex = 3;
            // 
            // imageBox1
            // 
            this.imageBox1.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.imageBox1.BackgroundImage = global::CefClient.Properties.Resources.monitorError;
            this.imageBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.imageBox1.FunctionalMode = Emgu.CV.UI.ImageBox.FunctionalModeOption.Minimum;
            this.imageBox1.Location = new System.Drawing.Point(1, 1);
            this.imageBox1.Name = "imageBox1";
            this.imageBox1.Size = new System.Drawing.Size(565, 392);
            this.imageBox1.TabIndex = 2;
            this.imageBox1.TabStop = false;
            // 
            // LiveWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(567, 395);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.imageBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "LiveWindow";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "实况";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.LiveWindow_FormClosing);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.LiveWindow_FormClosed);
            this.Load += new System.EventHandler(this.LiveWindow_Load);
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.imageBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        public System.Windows.Forms.Button RecordVideo;
        public Emgu.CV.UI.ImageBox imageBox1;
        public System.Windows.Forms.Button OpenAudio;
        public System.Windows.Forms.Panel panel1;
    }
}