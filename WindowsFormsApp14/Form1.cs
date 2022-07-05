using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp14
{
    public partial class Form1 : Form
    {
        public int iterator = 0;
        public double zoom = 0.001;

        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            fractal();
        }
        public void fractal()
        {
            var bmp = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            for (int i = 0; i < bmp.Width; i++)
            {
                for (int j = 0; j < bmp.Height; j++)
                {
                    var z = new complex(0, 0);

                    double x = (i - 50) * zoom;
                    double y = (j - 200) * zoom;

                    var c = new complex(x, y);

                    iterator = 0;

                    while ((z.x * z.x + z.y * z.y) <= 4 && iterator < 255)
                    {
                        iterator++;
                        z = z * z + c;
                    }

                    bmp.SetPixel(i, j, Color.FromArgb(iterator, iterator, iterator));
                }
                pictureBox1.Image = bmp;

            }
        }

        public class complex
        {
            public double x;
            public double y;

            public complex(double x, double y)
            {
                this.x = x;
                this.y = y;
            }

            public static complex operator +(complex a, complex b)
            {
                var temp = new complex(0, 0);
                temp.x = a.x + b.y;
                temp.y = a.y + b.x;
                return temp;
            }
            public static complex operator *(complex a, complex b)
            {
                var temp = new complex(0, 0);
                temp.x = a.x * b.x - a.y * b.y;
                temp.y = a.x * b.y + a.y * b.x;
                return temp;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (pictureBox1.Image != null)
            {
                SaveFileDialog sfd = new SaveFileDialog();
                sfd.Title = " Сохранить";
                sfd.OverwritePrompt = true;
                sfd.CheckPathExists = true;
                sfd.Filter = " Image Files(*.BMP)|*.BMP";
                sfd.ShowHelp = true;
                if(sfd.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        pictureBox1.Image.Save(sfd.FileName);
                    }
                    catch 
                    {
                        MessageBox.Show("Ошибка");
                    }   
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter= " Image Files(*.BMP)|*.BMP";
            if(ofd.ShowDialog()==DialogResult.OK)
            {
                try
                {
                    pictureBox1.Image = new Bitmap(ofd.FileName);
                }
                catch
                {
                    MessageBox.Show("Oшибка");
                }
            }    
        }
    }  
    
}

