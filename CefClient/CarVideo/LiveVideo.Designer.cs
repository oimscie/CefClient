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
            this.CloseAudio = new System.Windows.Forms.Button();
            this.imageBox1 = new Emgu.CV.UI.ImageBox();
            ((System.ComponentModel.ISupportInitialize)(this.imageBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // OpenAudio
            // 
            this.OpenAudio.Location = new System.Drawing.Point(872, 731);
            this.OpenAudio.Name = "OpenAudio";
            this.OpenAudio.Size = new System.Drawing.Size(98, 30);
            this.OpenAudio.TabIndex = 1;
            this.OpenAudio.Text = "语音通话";
            this.OpenAudio.UseVisualStyleBackColor = true;
            this.OpenAudio.Click += new System.EventHandler(this.button_Click);
            // 
            // CloseAudio
            // 
            this.CloseAudio.Location = new System.Drawing.Point(976, 731);
            this.CloseAudio.Name = "CloseAudio";
            this.CloseAudio.Size = new System.Drawing.Size(104, 30);
            this.CloseAudio.TabIndex = 1;
            this.CloseAudio.Text = "关闭语音";
            this.CloseAudio.UseVisualStyleBackColor = true;
            this.CloseAudio.Click += new System.EventHandler(this.CloseAudio_Click);
            // 
            // imageBox1
            // 
            this.imageBox1.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.imageBox1.FunctionalMode = Emgu.CV.UI.ImageBox.FunctionalModeOption.Minimum;
            this.imageBox1.InitialImage = global::CefClient.Properties.Resources.load;
            this.imageBox1.Location = new System.Drawing.Point(0, 5);
            this.imageBox1.Name = "imageBox1";
            this.imageBox1.Size = new System.Drawing.Size(1080, 720);
            this.imageBox1.TabIndex = 2;
            this.imageBox1.TabStop = false;
            // 
            // LiveWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(1082, 762);
            this.Controls.Add(this.imageBox1);
            this.Controls.Add(this.CloseAudio);
            this.Controls.Add(this.OpenAudio);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "LiveWindow";
            this.Text = "实况";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.LiveWindow_FormClosing);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.LiveWindow_FormClosed);
            this.Load += new System.EventHandler(this.LiveWindow_Load);
            ((System.ComponentModel.ISupportInitialize)(this.imageBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        public System.Windows.Forms.Button CloseAudio;
        public System.Windows.Forms.Button OpenAudio;
        public Emgu.CV.UI.ImageBox imageBox1;
    }
}