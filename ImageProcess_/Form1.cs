using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ImageProcess_
{
    public partial class Form1 : Form
    {
        private HighPerfTimer myTimer;
        private string curFileName;
        private Bitmap curBitmap;
        public Form1()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
            myTimer = new HighPerfTimer();
        }

        private void Form1_Load_1(object sender, EventArgs e)
        {
            
        }

        private void Open_Click(object sender, EventArgs e)
        {
            OpenFileDialog opnDiag = new OpenFileDialog();
            //opnDiag.Filter = "All Image| *.bmp; *.png; *.gif; *.jpg; " + "*.tif; *.ico";
            opnDiag.Filter = "All Image Files|*.bmp;*.ico;*.gif;*.jpeg;*.jpg;*.png;*.tif;*.tiff|" +
                             "Windows Bitmap(*.bmp)|*.bmp|" +
                             "Windows Icon(*.ico)|*.ico|" +
                             "Graphics Interchange Format (*.gif)|(*.gif)|" +
                             "JPEG File Interchange Format (*.jpg)|*.jpg;*.jpeg|" +
                             "Portable Network Graphics (*.png)|*.png|" +
                             "Tag Image File Format (*.tif)|*.tif;*.tiff";
            opnDiag.Title = "Open Image File";
            opnDiag.ShowHelp = true;
            if (opnDiag.ShowDialog() == DialogResult.OK)
            {
                curFileName = opnDiag.FileName;
                try
                {
                    curBitmap = (Bitmap)Image.FromFile(curFileName);
                    //MessageBox.Show(curFileName);
                    
                    this.Text = curFileName;
                }
                catch (Exception exp)
                {
                    MessageBox.Show(exp.Message + curFileName);
                }
            }
            Invalidate();
        }

        private void Save_Click(object sender, EventArgs e)
        {
            if (curBitmap == null)
                return;
            SaveFileDialog saveDiag = new SaveFileDialog();
            saveDiag.Title = "Save as";
            saveDiag.OverwritePrompt = true;
            // saveDiag.Filter = "bmp file(*.bmp)|*.bmp|" + "Gif file(*.gif)|*.gif|" + "Jpeg file(*.jpg)|*.jpg|" + "Png file(*.png)|*.png|";
            saveDiag.Filter = "All Image Files|*.bmp;*.ico;*.gif;*.jpeg;*.jpg;*.png;*.tif;*.tiff|" +
                             "Windows Bitmap(*.bmp)|*.bmp|" +
                             "Windows Icon(*.ico)|*.ico|" +
                             "Graphics Interchange Format (*.gif)|(*.gif)|" +
                             "JPEG File Interchange Format (*.jpg)|*.jpg;*.jpeg|" +
                             "Portable Network Graphics (*.png)|*.png|" +
                             "Tag Image File Format (*.tif)|*.tif;*.tiff";
            saveDiag.ShowHelp = true;
            if (saveDiag.ShowDialog() == DialogResult.OK)
            {
                string fileName = saveDiag.FileName;
                string strFilExtn = fileName.Remove(0, fileName.Length - 3);
                switch (strFilExtn)
                {
                    case "bmp":
                        curBitmap.Save(fileName, System.Drawing.Imaging.ImageFormat.Bmp);
                        break;
                    case "gif":
                        curBitmap.Save(fileName, System.Drawing.Imaging.ImageFormat.Gif);
                        break;
                    case "jpg":
                        curBitmap.Save(fileName, System.Drawing.Imaging.ImageFormat.Jpeg);
                        break;
                    case "png":
                        curBitmap.Save(fileName, System.Drawing.Imaging.ImageFormat.Png);
                        break;
                    default:
                        break;
                }
            }
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            if (curBitmap != null)
            {
                g.DrawImage(curBitmap, 160, 50, curBitmap.Width, curBitmap.Height);
            }
        }

        private void Close_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void pixel_Click(object sender, EventArgs e)
        {
            if (curBitmap != null)
            {
                myTimer.Start();
                Color curColor;
                int ret;
                for (int i = 0; i < curBitmap.Width; i++)
                {
                    for (int j = 0; j < curBitmap.Height; j++)
                    {
                        curColor = curBitmap.GetPixel(i, j);
                        ret = (int)(curColor.R * 0.299 + curColor.G * 0.587 + curColor.B * 0.114);
                        curBitmap.SetPixel(i, j, Color.FromArgb(ret, ret, ret));
                    }
                }
                myTimer.Stop();
                runTime.Text += myTimer.Duration.ToString("####.##") + "ms";
                Invalidate();
            }
        }

        private void memory_Click(object sender, EventArgs e)
        {

            //这里因为图像的尺寸是512x512，所以不用考虑未使用的空间
            //if (curBitmap != null)
            //{
            //    Rectangle rect = new Rectangle(0, 0, curBitmap.Width, curBitmap.Height);
            //    //注意，PixelFormat返回图像的像素格式，颜色值顺序是蓝、绿、红
            //    System.Drawing.Imaging.BitmapData bmpData = curBitmap.LockBits(rect, System.Drawing.Imaging.ImageLockMode.ReadWrite, curBitmap.PixelFormat);
            //    IntPtr ptr = bmpData.Scan0;
            //    int bytes = curBitmap.Width * curBitmap.Height * 3;
            //    byte[] rgbValues = new byte[bytes];
            //    System.Runtime.InteropServices.Marshal.Copy(ptr, rgbValues, 0, bytes);
            //    double colorTemp = 0;
            //    for (int i = 0; i < rgbValues.Length; i += 3)
            //    {
            //        colorTemp = rgbValues[i + 2] * 0.299 + rgbValues[i + 1] * 0.587 + rgbValues[i] * 0.114;
            //        rgbValues[i] = rgbValues[i + 1] = rgbValues[i + 2] = (byte)colorTemp;
            //    }
            //    System.Runtime.InteropServices.Marshal.Copy(rgbValues, 0, ptr, bytes);
            //    curBitmap.UnlockBits(bmpData);
            //    Invalidate();
            //}

            //这里如果图像尺寸是任意的，
            if (curBitmap != null)
            {
                myTimer.Start();
                Rectangle rect = new Rectangle(0, 0, curBitmap.Width, curBitmap.Height);
                System.Drawing.Imaging.BitmapData bmpData = curBitmap.LockBits(rect, System.Drawing.Imaging.ImageLockMode.ReadWrite, curBitmap.PixelFormat);
                IntPtr ptr = bmpData.Scan0;
                int bytes = bmpData.Stride * bmpData.Height;
                byte[] rgbValues = new byte[bytes];
                System.Runtime.InteropServices.Marshal.Copy(ptr, rgbValues, 0, bytes);
                double colorTemp = 0;
                for (int i = 0; i < bmpData.Height; i++)
                {
                    for (int j = 0; j < bmpData.Width * 3; j += 3)
                    {
                        int offSet = i * bmpData.Stride + j;
                        colorTemp = rgbValues[offSet + 2] * 0.299 + rgbValues[offSet + 1] * 0.587 + rgbValues[offSet] * 0.114;
                        rgbValues[offSet + 2] = rgbValues[offSet + 1] = rgbValues[offSet] = (byte)colorTemp;
                    }
                }
                System.Runtime.InteropServices.Marshal.Copy(rgbValues, 0, ptr, bytes);
                curBitmap.UnlockBits(bmpData);
                myTimer.Stop();
                runTime.Text += myTimer.Duration.ToString("####.##") + "ms";
                Invalidate();
            }
        }



        private void pointer_Click(object sender, EventArgs e)
        {
            if (curBitmap != null)
            {
                myTimer.Start();
                Rectangle rect = new Rectangle(0, 0, curBitmap.Width, curBitmap.Height);
                System.Drawing.Imaging.BitmapData bmpData = curBitmap.LockBits(rect, System.Drawing.Imaging.ImageLockMode.ReadWrite, curBitmap.PixelFormat);
                byte temp = 0;
                unsafe
                {
                    byte* ptr = (byte*)(bmpData.Scan0);
                    for (int i = 0; i < bmpData.Height; i++)
                    {
                        for (int j = 0; j < bmpData.Width; j++)
                        {
                            temp = (byte)(ptr[2] * 0.299 + ptr[1] * 0.587 + ptr[0] * 0.114);
                            ptr[2] = ptr[1] = ptr[0] = temp;
                            ptr += 3;
                        }
                        ptr += bmpData.Stride - bmpData.Width * 3;
                    }
                }
                curBitmap.UnlockBits(bmpData);

                myTimer.Stop();
                runTime.Text += myTimer.Duration.ToString("####.##") + "ms ";

                Invalidate();

            }
        }



        private void normalConvert_Click(object sender, EventArgs e)
        {
            if (curBitmap != null)
            {
                Rectangle rect = new Rectangle(0, 0, curBitmap.Width, curBitmap.Height);
                //注意，PixelFormat返回图像的像素格式，颜色值顺序是红、绿、蓝
                System.Drawing.Imaging.BitmapData bmpData = curBitmap.LockBits(rect, System.Drawing.Imaging.ImageLockMode.ReadWrite, curBitmap.PixelFormat);
                IntPtr ptr = bmpData.Scan0;
                int bytes = curBitmap.Width * curBitmap.Height * 3;
                byte[] rgbValues = new byte[bytes];
                System.Runtime.InteropServices.Marshal.Copy(ptr, rgbValues, 0, bytes);
                double colorTemp = 0;
                for (int i = 0; i < bmpData.Height; i++)
                {
                    for (int j = 0; j < bmpData.Width * 3; j += 3)
                    {
                        int offSet = i * bmpData.Stride + j;
                        colorTemp = rgbValues[offSet + 2] * 0.299 + rgbValues[offSet + 1] * 0.587 + rgbValues[offSet] * 0.114;
                        rgbValues[offSet + 2] = rgbValues[offSet + 1] = rgbValues[offSet] = (byte)colorTemp;
                    }
                }
                System.Runtime.InteropServices.Marshal.Copy(rgbValues, 0, ptr, bytes);
                curBitmap.UnlockBits(bmpData);
                Invalidate();
            }


        }

        private void histogram_Click(object sender, EventArgs e)
        {
            if (curBitmap != null)
            {
                HistForm histogram = new HistForm(curBitmap);
                histogram.ShowDialog();
            }
        }
    }
}
