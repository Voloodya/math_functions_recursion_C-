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
    public partial class Form1 : Form
    {
        public class calkuliytor
        {
            static public double eval(string s, double x2)
            {
                int ppm = 1, pud = 2, step=3, trig=4, minp = 100, pos = -1;

                for (int i = 0; i < s.Length; i++)
                {
                    if (s[i] == '+' || s[i] == '-')
                    {
                        if (ppm <= minp) { minp = ppm; pos = i; }
                    }
                    
                    if (s[i] == '*' || s[i] == '/')
                    {
                        if (pud <= minp) { minp = pud; pos = i; }
                    }
                    if (s[i] == '^')
                    {
                        if (step <= minp) { minp = step; pos = i; }
                    }
                    if (s[i] == 'L')
                    {
                        if (trig < minp) { minp = trig; pos = i; i += 2; }
                    }
                    if (s[i] == 's' || s[i]=='c' || s[i]=='t' || s[i] == 'k')
                    {
                        if (trig < minp) { minp = trig; pos = i; i += 2; }
                    }
                    if ((s[i] == 'A' && s[i + 1] == 's') || (s[i] == 'A' && s[i + 1] == 'c') || (s[i] == 'A' && s[i + 1] == 't') || (s[i] == 'A' && s[i + 1] == 'k'))
                    {
                        if (trig < minp) { minp = trig; pos = i; i += 3; }
                    }

                    if (s[i] == '(')
                    {
                        ppm += 5; pud += 5;
                    }
                    if (s[i] == ')')
                    {
                        ppm -= 5; pud -= 5;
                    }
                    
                }

                if (pos == -1)
                {

                    string r = "";
                    bool x=false;
                    for(int i=0;i<s.Length;i++)
                    {
                        if (s[i] >= 'a' && s[i] <= 'z') x = true;
                    }

                    for (int i = 0; i < s.Length; i++)
                    {
                        if (s[i] >= '0' && s[i] <= '9')
                            r += s[i];

                        else if (x == false) r = "0"; //???
                    }
                        
                            if (r == "") return x2;
                            return double.Parse(r);
                        
                    }
                
                    
                

                double y = 0;
                if (s[pos] == 'l')
                {
                    y = eval(s.Substring(pos + 3), x2);

                    return Math.Log(y);
                }
                    if (s[pos] == 's' || s[pos] == 'c' || s[pos] == 't' || s[pos] == 'k')
                {
                                        
                    y = eval(s.Substring(pos + 3), x2);

                    if (s[pos] == 's') return Math.Sin(y);
                    if (s[pos] == 'c') return Math.Cos(y);
                    if (s[pos] == 't') return Math.Tan(y);
                    if (s[pos] == 'k') return (1 / Math.Tan(y));
                    return pos;
                }
                if ((s[pos] == 'A' && s[pos + 1] == 's') || (s[pos] == 'A' && s[pos + 1] == 'c') || (s[pos] == 'A' && s[pos + 1] == 't') || (s[pos] == 'A' && s[pos + 1] == 'k'))
                {
                    
                    y = eval(s.Substring(pos + 4, 1), x2);
                    if (s[pos + 1] == 's') return Math.Asin(y);
                    if (s[pos + 1] == 'c') return Math.Acos(y);
                    if (s[pos + 1] == 't') return Math.Atan(y);
                    if (s[pos + 1] == 'k') return (1 / Math.Atan(y));
                    return pos;
                }
                else
                {
                    double x;
                    if (s.Substring(0, pos) == "") x = 0;//???
                    else x = eval(s.Substring(0, pos), x2);
                    y = eval(s.Substring(pos + 1), x2);
                    if (s[pos] == '+')
                        return x + y;
                    if (s[pos] == '-')
                        return x - y;
                    if (s[pos] == '*')
                        return x * y;
                    if (s[pos] == '/')
                        return x / y;
                    if (s[pos] == '^')
                    {
                        return Math.Pow(x, y);
                    }
                    return pos;
                }

            }
          
        };
        
        static int ex, ey, nx, ny;
        Pen p = new Pen(Color.Green, 3);
        public Pen p2 = new Pen(Color.Black, 3);
        public Pen p3 = new Pen(Color.LightBlue, 1);
        public Form1()
        {
            InitializeComponent();
            
        }
        static void koord(double x, double y, out int x0, out int y0)
        {
            x0 = (int)(nx + x * ex);
            y0 = (int)(ny - y * ey);
        }


        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                string varajenie = textBox1_Function.Text;

                double rezult = 0;
                double a = double.Parse(textBox1_Diapazon1.Text);
                double b = double.Parse(textBox1_Diapazon2.Text);
                double jump = double.Parse(textBox1_Hag.Text);
                for (double x = a; x <= b; x += jump)
                {
                    rezult = calkuliytor.eval(varajenie, x);

                    listBox1.Items.Add(x);
                    listBox2.Items.Add(rezult);
                }
            }
            catch (FormatException)
            {
                MessageBox.Show("Введите выражение. Неверный формат выражения");
            }
            
        }

        private void label5_Click(object sender, EventArgs e)
        {

        }
       
        public void Form1_Load(object sender, EventArgs e)
        {
            ex = int.Parse(textBox1.Text);
            ey = int.Parse(textBox2.Text);
            nx = (panel1.Width / 2);
            ny = (panel1.Height / 2);
        }


        public void textBox1_Function_TextChanged(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            panel1.Refresh();
            //panel1.Controls.Clear();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Form2 f = new Form2();
            f.Owner=this;
            f.Show();
            
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            if (colorDialog1.ShowDialog() == DialogResult.OK)
                p.Color = colorDialog1.Color;
            panel1.Refresh();

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            ex = int.Parse(textBox1.Text);
            ey = int.Parse(textBox2.Text);
            Graphics g = e.Graphics;
            Pen p1 = new Pen(Color.Black, 3);
            p1.EndCap = System.Drawing.Drawing2D.LineCap.ArrowAnchor;
            g.DrawLine(p1, 0, ny, panel1.Width, ny);
            g.DrawLine(p1, nx, panel1.Height, nx, 0);
            for (int i = 1; nx + (ex * i) < panel1.Width; i++)
            {
                g.DrawLine(p3,
                    (nx + (ex * i)), 0, (nx + (ex * i)), panel1.Height);
                g.DrawLine(p3,
                    (nx - (ex * i)), 0, (nx - (ex * i)), panel1.Height);
                g.DrawLine(p2,
                    (nx + (ex * i)), ny - 5, (nx + (ex * i)), ny + 5);
                g.DrawLine(p2,
                    (nx - (ex * i)), ny - 5, (nx - (ex * i)), ny + 5);
                if (ex < 25)
                {
                    if (i % 5 == 0)
                    {
                        g.DrawString(i.ToString(), SystemFonts.CaptionFont, Brushes.Black, (nx + (ex * i)), ny + 5);
                        g.DrawString('-' + i.ToString(), SystemFonts.CaptionFont, Brushes.Black, (nx - (ex * i)), ny + 5);
                    }
                }
                else {
                    g.DrawString(i.ToString(), SystemFonts.CaptionFont, Brushes.Black, (nx + (ex * i)), ny + 5);
                    g.DrawString('-' + i.ToString(), SystemFonts.CaptionFont, Brushes.Black, (nx - (ex * i)), ny + 5);
                }
            }
            for (int i = 1; ny + (ex * i) < panel1.Height; i++)
            {
                g.DrawLine(p3,
                    0, (ny - (ey * i)), panel1.Width, ny - (ey * i));
                g.DrawLine(p3,
                    0, (ny + (ey * i)), panel1.Width, ny + (ey * i));
                g.DrawLine(p2,
                    nx - 5, (ny - (ey * i)), nx + 5, ny - (ey * i));
                g.DrawLine(p2,
                    nx - 5, (ny + (ey * i)), nx + 5, ny + (ey * i));
                if (ey < 25)
                {
                    if (i % 5 == 0)
                    {
                        g.DrawString(i.ToString(), SystemFonts.CaptionFont, Brushes.Black, nx + 5, ny - (ex * i));
                        g.DrawString('-' + i.ToString(), SystemFonts.CaptionFont, Brushes.Black, nx + 5, ny + (ex * i));
                    }
                }
                else
                {
                    g.DrawString(i.ToString(), SystemFonts.CaptionFont, Brushes.Black, nx + 5, ny - (ex * i));
                    g.DrawString('-' + i.ToString(), SystemFonts.CaptionFont, Brushes.Black, nx + 5, ny + (ex * i));
                }
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            ex = int.Parse(textBox1.Text);
            ey = int.Parse(textBox2.Text);
            panel1.Refresh();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
            listBox2.Items.Clear();
            
        }
        private void button3_Click(object sender, EventArgs e)
        {
            string varajenie = textBox1_Function.Text;
            double a = double.Parse(textBox1_Diapazon1.Text);
            double b = double.Parse(textBox1_Diapazon2.Text);
            double jump = double.Parse(textBox1_Hag.Text);
            ex = int.Parse(textBox1.Text);
            ey = int.Parse(textBox1.Text);
            Graphics g2 = panel1.CreateGraphics();

           
                for (double x=a; x<b;x+=jump)
            {
                double y= calkuliytor.eval(varajenie, x);
                double y2 = calkuliytor.eval(varajenie, x+jump);
                int x0, y0, x1,y1; double x2;
                x2 = x + jump;
                koord(x, y, out x0, out y0);
                koord(x2, y2, out x1, out y1);
                
                g2.DrawLine(p, x0, y0,x1,y1);
           }
           
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }
    }
}
