using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Drawing.Imaging;
using AForge;
using AForge.Video;
using AForge.Video.DirectShow;


namespace ImageProcess_
{
    public partial class Form1 : Form
    {
        private HighPerfTimer myTimer;
        private string curFileName;
        private Bitmap curBitmap;
        private Bitmap bufferBitmap;
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
                    bufferBitmap = (Bitmap)Image.FromFile(curFileName);
                    curBitmap = bufferBitmap;
                    //MessageBox.Show(curFileName);
                    pixelFormat.Text = "Current format: " + curBitmap.PixelFormat.ToString();
                    this.Text = curFileName;
                }
                catch (Exception exp)
                {
                    MessageBox.Show(exp.Message + curFileName);
                }
            }

            Invalidate();


            //if (curBitmap.PixelFormat == System.Drawing.Imaging.PixelFormat.Format24bppRgb)
            //{
            //    // MessageBox.Show("The pixel contains 3 channel");
            //}
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
            //Graphics g = e.Graphics;
            //if (curBitmap != null)
            //{
            //    g.DrawImage(curBitmap, 160, 50, curBitmap.Width, curBitmap.Height);
            //}
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
                bufferBitmap = new Bitmap(curBitmap.Width, curBitmap.Height);
                int ret;
                for (int i = 0; i < curBitmap.Width; i++)
                {
                    for (int j = 0; j < curBitmap.Height; j++)
                    {
                        curColor = curBitmap.GetPixel(i, j);
                        ret = (int)(curColor.R * 0.299 + curColor.G * 0.587 + curColor.B * 0.114);
                        bufferBitmap.SetPixel(i, j, Color.FromArgb(ret, ret, ret));
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
                BitmapData bmpData = curBitmap.LockBits(rect, ImageLockMode.ReadWrite, curBitmap.PixelFormat);
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
                //toGrayscale(curBitmap);
                //grayToNormal(curBitmap);

                Rectangle rect = new Rectangle(0, 0, curBitmap.Width, curBitmap.Height);
                BitmapData bmpData = curBitmap.LockBits(rect, ImageLockMode.ReadWrite, curBitmap.PixelFormat);
                IntPtr ptr = bmpData.Scan0;
                int bytes = bmpData.Stride * bmpData.Height;
                byte[] rgbValue = new byte[bytes];
                Marshal.Copy(ptr, rgbValue, 0, bytes);
                double tempColor = 0;

                //灰度化当前图像
                for (int h = 0; h < bmpData.Height; h++)
                {
                    for (int w = 0; w < bmpData.Width * 3; w += 3)
                    {
                        int offSet = h * bmpData.Stride + w;
                        tempColor = rgbValue[offSet + 2] * 0.299 + rgbValue[offSet + 1] * 0.587 + rgbValue[offSet] * 0.114;
                        rgbValue[offSet + 2] = rgbValue[offSet + 1] = rgbValue[offSet] = (byte)tempColor;
                    }
                }


                //转法线
                for (int h = 1; h < bmpData.Height - 1; h++)
                {
                    for (int w = 1; w < bmpData.Width - 1; w++)
                    {
                        //byte left = rgbValue[(w + bmpData.Width * h - 1) * 4 + 1];
                        //byte right = rgbValue[(w + bmpData.Width * h + 1) * 4 + 1];
                        //byte up = rgbValue[(w + bmpData.Width * (h - 1) + 1) * 4 + 1];
                        //byte down = rgbValue[(w + bmpData.Width * (h + 1) + 1) * 4 + 1];

                        byte left = rgbValue[(w - 1) * 3 + bmpData.Stride * h];
                        byte right = rgbValue[(w + 1) * 3 + bmpData.Stride * h];
                        byte up = rgbValue[w * 3 + bmpData.Stride * (h - 1)];
                        byte down = rgbValue[w * 3 + bmpData.Stride * (h + 1)];

                        double horizonVector = (right - left) / 255.0;
                        double verticalVector = (down - up) / 255.0;

                        double magnitude = Math.Sqrt(horizonVector * horizonVector + verticalVector * verticalVector + 1);

                        double x = horizonVector / magnitude;
                        double y = verticalVector / magnitude;
                        double z = 1 / magnitude;

                        x = x / 2 + 0.5;
                        y = y / 2 + 0.5;
                        z = z / 2 + 0.5;

                        rgbValue[w * 3 + bmpData.Stride * h] = (byte)z;
                        rgbValue[w * 3 + bmpData.Stride * h + 1] = (byte)y;
                        rgbValue[w * 3 + bmpData.Stride * h + 2] = (byte)x;
                    }
                }

                //for (int h = 0; h < bmpData.Height; h++)
                //{
                //    for (int w = 0; w < bmpData.Stride - bmpData.Stride % 3; w+=3)
                //    {

                //    }
                //}

                //MessageBox.Show("Image Width:" + curBitmap.Width.ToString() + "  Image Height:" + curBitmap.Height.ToString() + "  Image Stride:" + bmpData.Stride );

                Marshal.Copy(rgbValue, 0, ptr, bytes);
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

        private void toGrayscale(Bitmap bmp)
        {
            //使用内存法将图像处理成灰度图
            //注意，PixelFormat返回图像的像素格式，颜色值顺序是红、绿、蓝
            //gray = r * 0.299 + g * 0.587 + b * 0.114
            //MessageBox.Show(bmp.PixelFormat.ToString());

            Rectangle rect = new Rectangle(0, 0, bmp.Width, bmp.Height);
            BitmapData bmpData = bmp.LockBits(rect, ImageLockMode.ReadWrite, bmp.PixelFormat);
            IntPtr ptr = bmpData.Scan0;
            int bytes = bmpData.Width * bmpData.Height * 3;
            byte[] rgbValue = new byte[bytes];
            Marshal.Copy(ptr, rgbValue, 0, bytes);
            double tempColor = 0;
            for (int i = 0; i < bmpData.Height; i++)
            {
                for (int j = 0; j < bmpData.Width * 3; j += 3)
                {
                    int offSet = i * bmpData.Stride + j;
                    tempColor = rgbValue[offSet + 2] * 0.299 + rgbValue[offSet + 1] * 0.587 + rgbValue[offSet] * 0.114;
                    rgbValue[offSet + 2] = rgbValue[offSet + 1] = rgbValue[offSet] = (byte)tempColor;
                }
            }
            Marshal.Copy(rgbValue, 0, ptr, bytes);
            bmp.UnlockBits(bmpData);
        }

        private void grayToNormal(Bitmap bmp)
        {
            Bitmap normal;
            int depth = 4;
            Rectangle rect = new Rectangle(0, 0, bmp.Width, bmp.Height);
            BitmapData bmpData = bmp.LockBits(rect, ImageLockMode.ReadWrite, bmp.PixelFormat);
            IntPtr ptr = bmpData.Scan0;
            byte[] bytes = new byte[bmpData.Stride * bmpData.Height * depth];
            Marshal.Copy(ptr, bytes, 0, bytes.Length);

            normal = new Bitmap(bmp.Width, bmp.Height, PixelFormat.Format24bppRgb);
            BitmapData normalData = normal.LockBits(rect, ImageLockMode.ReadWrite, normal.PixelFormat);
            IntPtr normalPtr = normalData.Scan0;
            byte[] normalByte = new byte[bmp.Width * bmp.Height * 3];
            Marshal.Copy(ptr, normalByte, 0, normalByte.Length);

            for (int h = 1; h < bmp.Height - 1; h++)
            {
                for (int w = 1; w < bmp.Width - 1; w++)
                {
                    byte leftPixel = bytes[(w + bmp.Width * h - 1) * depth + 1];
                    byte rightPixel = bytes[(w + bmp.Width * h + 1) * depth + 1];
                    byte upPixel = bytes[w + bmp.Width * (h - 1) * depth + 1];
                    byte downPixel = bytes[w + bmp.Width * (h + 1) * depth + 1];

                    double horizonVector = (leftPixel - rightPixel) / 255.0;
                    double verticalVector = (downPixel - upPixel) / 255.0;

                    double magnitude = Math.Sqrt(horizonVector * horizonVector + verticalVector * verticalVector + 1);

                    double x = horizonVector / magnitude;
                    double y = verticalVector / magnitude;
                    double z = 1 / magnitude;

                    x = x / 2 + 0.5;
                    y = y / 2 + 0.5;
                    z = z / 2 + 0.5;

                    bytes[(w + bmp.Width * h) * 3] = (byte)z;
                    bytes[(w + bmp.Width * h + 1) * 3 + 1] = (byte)y;
                    bytes[(w + bmp.Width * h + 2) * 3 + 2] = (byte)x;
                }
            }
            Marshal.Copy(normalByte, 0, normalPtr, normalByte.Length);
            normal.UnlockBits(normalData);
            bmp.UnlockBits(bmpData);

            //return normal;
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            if (bufferBitmap != null)
            {
                pictureBox1.Image = bufferBitmap;
            }
            Invalidate();
            //Graphics g = e.Graphics;

            //if (curBitmap != null)
            //{
            //    g.DrawImage(curBitmap, 0, 0, curBitmap.Width, curBitmap.Height);
            //}
        }

        private void pictureBox2_Paint(object sender, PaintEventArgs e)
        {
            pictureBox2.Image = curBitmap;
            Invalidate();
        }
    }
}
