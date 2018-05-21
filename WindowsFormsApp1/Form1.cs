using Pensum;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class MyForm : Form
    {
        public MyForm()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            var count = 0;
            var file = ImageReader.ReadBin("../../../Pensum/apple1000.bin");
            int h = 28, w = 28*6;
            var bmp = new Bitmap(h, w);

            for (var x = 0; x < w; x++)
            {
                for (var y = 0; y < h; y++)
                {
                    bmp.SetPixel(y, x, Color.FromArgb(file[count], 0, 150, 10));
                    count++;
                }
            }

            pictureBox1.Image = bmp;
        }

        public void pictureBox1_Click(object sender, EventArgs e) { }
    }
}
