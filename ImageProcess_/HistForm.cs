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
    public partial class HistForm : Form
    {
        private System.Drawing.Bitmap bmpHist;
        private int[] countPixel;
        private int maxPixel;

        public HistForm(Bitmap bmp)
        {
            InitializeComponent();
            bmpHist = bmp;
            countPixel = new int[256];
        }
        

        public HistForm()
        {
            InitializeComponent();
        }

        private void close_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void close_Paint(object sender, PaintEventArgs e)
        {
            
        }

        private void HistForm_Load(object sender, EventArgs e)
        {
            Rectangle rect = new Rectangle(0, 0, bmpHist.Width, bmpHist.Height);
            System.Drawing.Imaging.BitmapData bmpData = bmpHist.LockBits(rect, System.Drawing.Imaging.ImageLockMode.ReadWrite, bmpHist.PixelFormat);
            IntPtr ptr = bmpData.Scan0;
            int bytes = bmpHist.Width * bmpHist.Height;
            byte[] grayValue = new byte[bytes];
            System.Runtime.InteropServices.Marshal.Copy(ptr, grayValue, 0, bytes);

            byte temp = 0;
            maxPixel = 0;

            Array.Clear(countPixel, 0, 256);
            for (int i = 0; i < bytes; i++)
            {
                temp = grayValue[i];
                countPixel[temp]++;
                if (countPixel[temp] > maxPixel)
                {
                    maxPixel = countPixel[temp];
                }
            }

            System.Runtime.InteropServices.Marshal.Copy(grayValue, 0, ptr, bytes);
            bmpHist.UnlockBits(bmpData);

        }

        private void HistForm_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            Pen curPen = new Pen(Brushes.Black, 1);

            //绘制坐标轴
            g.DrawLine(curPen, 50, 240, 320, 240);
            g.DrawLine(curPen, 50, 240, 50, 30);

            g.DrawLine(curPen, 100, 240, 100, 242);
            g.DrawLine(curPen, 150, 240, 150, 242);
            g.DrawLine(curPen, 200, 240, 200, 242);
            g.DrawLine(curPen, 250, 240, 300, 242);
            g.DrawLine(curPen, 300, 240, 300, 242);
            g.DrawString("0", new Font("new Timer", 8), Brushes.Black, new PointF(46, 242));
            g.DrawString("50", new Font("new Timer", 8), Brushes.Black, new PointF(92, 242));
            g.DrawString("100", new Font("new Timer", 8), Brushes.Black, new PointF(139, 242));
            g.DrawString("150", new Font("new Timer", 8), Brushes.Black, new PointF(189, 242));
            g.DrawString("200", new Font("new Timer", 8), Brushes.Black, new PointF(239, 242));
            g.DrawString("250", new Font("new Timer", 8), Brushes.Black, new PointF(289, 242));

            g.DrawLine(curPen, 48, 40, 50, 40);

            double temp = 0;
            for (int i = 0; i < 256; i++)
            {
                temp = 200.0 * countPixel[i] / maxPixel;
                g.DrawLine(curPen, 50 + i, 240, 50 + i, 240 - (int)temp);
            }
            curPen.Dispose();
        }
    }
}
