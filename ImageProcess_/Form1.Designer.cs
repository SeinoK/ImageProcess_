namespace ImageProcess_
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
            this.Open = new System.Windows.Forms.Button();
            this.Save = new System.Windows.Forms.Button();
            this.Close = new System.Windows.Forms.Button();
            this.runTime = new System.Windows.Forms.Label();
            this.pixel = new System.Windows.Forms.Button();
            this.memory = new System.Windows.Forms.Button();
            this.pointer = new System.Windows.Forms.Button();
            this.normalConvert = new System.Windows.Forms.Button();
            this.histogram = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // Open
            // 
            this.Open.Location = new System.Drawing.Point(45, 43);
            this.Open.Name = "Open";
            this.Open.Size = new System.Drawing.Size(75, 23);
            this.Open.TabIndex = 0;
            this.Open.Text = "Open Image";
            this.Open.UseVisualStyleBackColor = true;
            this.Open.Click += new System.EventHandler(this.Open_Click);
            // 
            // Save
            // 
            this.Save.Location = new System.Drawing.Point(45, 96);
            this.Save.Name = "Save";
            this.Save.Size = new System.Drawing.Size(75, 23);
            this.Save.TabIndex = 1;
            this.Save.Text = "Save Image";
            this.Save.UseVisualStyleBackColor = true;
            this.Save.Click += new System.EventHandler(this.Save_Click);
            // 
            // Close
            // 
            this.Close.Location = new System.Drawing.Point(45, 153);
            this.Close.Name = "Close";
            this.Close.Size = new System.Drawing.Size(75, 23);
            this.Close.TabIndex = 2;
            this.Close.Text = "Close Image";
            this.Close.UseVisualStyleBackColor = true;
            this.Close.Click += new System.EventHandler(this.Close_Click);
            // 
            // runTime
            // 
            this.runTime.AutoSize = true;
            this.runTime.Location = new System.Drawing.Point(42, 596);
            this.runTime.Name = "runTime";
            this.runTime.Size = new System.Drawing.Size(59, 13);
            this.runTime.TabIndex = 3;
            this.runTime.Text = "Run Time: ";
            // 
            // pixel
            // 
            this.pixel.Location = new System.Drawing.Point(45, 208);
            this.pixel.Name = "pixel";
            this.pixel.Size = new System.Drawing.Size(75, 23);
            this.pixel.TabIndex = 4;
            this.pixel.Text = "Get Pixel";
            this.pixel.UseVisualStyleBackColor = true;
            this.pixel.Click += new System.EventHandler(this.pixel_Click);
            // 
            // memory
            // 
            this.memory.Location = new System.Drawing.Point(45, 259);
            this.memory.Name = "memory";
            this.memory.Size = new System.Drawing.Size(75, 23);
            this.memory.TabIndex = 5;
            this.memory.Text = "Memory Get";
            this.memory.UseVisualStyleBackColor = true;
            this.memory.Click += new System.EventHandler(this.memory_Click);
            // 
            // pointer
            // 
            this.pointer.Location = new System.Drawing.Point(45, 315);
            this.pointer.Name = "pointer";
            this.pointer.Size = new System.Drawing.Size(75, 23);
            this.pointer.TabIndex = 6;
            this.pointer.Text = "Pointer Get";
            this.pointer.UseVisualStyleBackColor = true;
            this.pointer.Click += new System.EventHandler(this.pointer_Click);
            // 
            // normalConvert
            // 
            this.normalConvert.Location = new System.Drawing.Point(45, 367);
            this.normalConvert.Name = "normalConvert";
            this.normalConvert.Size = new System.Drawing.Size(75, 23);
            this.normalConvert.TabIndex = 7;
            this.normalConvert.Text = "Normal";
            this.normalConvert.UseVisualStyleBackColor = true;
            this.normalConvert.Click += new System.EventHandler(this.normalConvert_Click);
            // 
            // histogram
            // 
            this.histogram.Location = new System.Drawing.Point(45, 422);
            this.histogram.Name = "histogram";
            this.histogram.Size = new System.Drawing.Size(99, 23);
            this.histogram.TabIndex = 8;
            this.histogram.Text = "Draw Histogram";
            this.histogram.UseVisualStyleBackColor = true;
            this.histogram.Click += new System.EventHandler(this.histogram_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 761);
            this.Controls.Add(this.histogram);
            this.Controls.Add(this.normalConvert);
            this.Controls.Add(this.pointer);
            this.Controls.Add(this.memory);
            this.Controls.Add(this.pixel);
            this.Controls.Add(this.runTime);
            this.Controls.Add(this.Close);
            this.Controls.Add(this.Save);
            this.Controls.Add(this.Open);
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.Text = "Form1";
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.Form1_Paint);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button Open;
        private System.Windows.Forms.Button Save;
        private System.Windows.Forms.Button Close;
        private System.Windows.Forms.Label runTime;
        private System.Windows.Forms.Button pixel;
        private System.Windows.Forms.Button memory;
        private System.Windows.Forms.Button pointer;
        private System.Windows.Forms.Button normalConvert;
        private System.Windows.Forms.Button histogram;
    }
}

