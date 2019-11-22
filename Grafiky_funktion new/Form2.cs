using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Grafiky_funktion_new
{
    
    public partial class Form2 : Form
    {
        Pen p = new Pen(Color.Green, 3);
        public Form2()
        {
            InitializeComponent();
          
        }
        
        static int ex = 50, ey = 50, nx2, ny2;

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            
        }

        private void toolStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            if (colorDialog1.ShowDialog() == DialogResult.OK)
                p.Color = colorDialog1.Color;
            pictureBox1.Refresh();

        }

        private void pictureBox1_Click_1(object sender, EventArgs e)
        {

        }

        public void Form2_Load(object sender, EventArgs e)
        {
            nx2 = (pictureBox1.Width / 2);
            ny2 = (pictureBox1.Height / 2);
            
            pictureBox1.Paint += pictureBox1_Paint;// Подключает обработчик
            //pictureBox1.Paint += pictureBox1_Paint;// Отключает обработчик
        }
        static void koord(double x, double y, out int x0, out int y0)
        {
            x0 = (int)(nx2 + x * ex);
            y0 = (int)(ny2 - y * ey);
        }

        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            Form1 main = this.Owner as Form1;
            string varajenie = main.textBox1_Function.Text; //
            double a = double.Parse(main.textBox1_Diapazon1.Text); //
            double b = double.Parse(main.textBox1_Diapazon2.Text); //
            double jump = double.Parse(main.textBox1_Hag.Text); //
            ex = (int.Parse(main.textBox1.Text))*2; //
            ey = (int.Parse(main.textBox1.Text))*2;
            Graphics g = e.Graphics;
            Pen p1 = new Pen(Color.Black, 3);
            p1.EndCap = System.Drawing.Drawing2D.LineCap.ArrowAnchor;
            g.DrawLine(p1, 0, ny2, pictureBox1.Width, ny2);
            g.DrawLine(p1, nx2, pictureBox1.Height, nx2, 0);
            for (int i = 1; nx2 + (ex * i) <= pictureBox1.Width; i++)
            {
                g.DrawLine(main.p3,
                    (nx2 + (ex * i)), 0, (nx2 + (ex * i)), pictureBox1.Height);
                g.DrawLine(main.p3,
                    (nx2 - (ex * i)), 0, (nx2 - (ex * i)), pictureBox1.Height);
                g.DrawLine(main.p2,
                    (nx2 + (ex * i)), ny2 - 5, (nx2 + (ex * i)), ny2 + 5);
                g.DrawLine(main.p2,
                    (nx2 - (ex * i)), ny2 - 5, (nx2 - (ex * i)), ny2 + 5);
                
                if (i % 1 == 0)
                {
                    g.DrawString(i.ToString(), SystemFonts.CaptionFont, Brushes.Black, (nx2 + (ex * i)), ny2 + 5);
                    g.DrawString('-' + i.ToString(), SystemFonts.CaptionFont, Brushes.Black, (nx2 - (ex * i)), ny2 + 5);
                }
            }
            for (int i = 1; ny2 + (ex * i) < pictureBox1.Height; i++)
            {
                g.DrawLine(main.p3,
                    0, (ny2 - (ey * i)), pictureBox1.Width, ny2 - (ey * i));
                g.DrawLine(main.p3,
                    0, (ny2 + (ey * i)), pictureBox1.Width, ny2 + (ey * i));
                g.DrawLine(main.p2,
                    nx2 - 5, (ny2 - (ey * i)), nx2 + 5, ny2 - (ey * i));
                g.DrawLine(main.p2,
                    nx2 - 5, (ny2 + (ey * i)), nx2 + 5, ny2 + (ey * i));
                if (i % 1 == 0)
                {
                    g.DrawString(i.ToString(), SystemFonts.CaptionFont, Brushes.Black, nx2 + 5, ny2 - (ex * i));
                    g.DrawString('-' + i.ToString(), SystemFonts.CaptionFont, Brushes.Black, nx2 + 5, ny2 + (ex * i));
                }
            }

            for (double x = a; x < b; x += jump)
            {
                double y = Form1.calkuliytor.eval(varajenie, x);
                double y2 = Form1.calkuliytor.eval(varajenie, x + jump);
                int x0, y0, x1, y1; double x2;
                x2 = x + jump;
                koord(x, y, out x0, out y0);
                koord(x2, y2, out x1, out y1);

                g.DrawLine(p, x0, y0, x1, y1);
            }
        }

    }
}
