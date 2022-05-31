using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ZedGraph;

namespace SummerPr2022
{
    public partial class Form1 : Form
    {
        
        public int a = 1, b = 2;
        const double x0 = 1, y0 = 0, minX = 1, maxX = 2;
        ZedGraphControl zedGrapgControl1 = new ZedGraphControl();
        public Form1()
        {
            InitializeComponent();
        }
        protected override void OnLoad(EventArgs e)
        {
            zedGrapgControl1.Location = new Point(8, 30);
            zedGrapgControl1.Name = "text";
            zedGrapgControl1.Size = new Size(500, 500);
            Controls.Add(zedGrapgControl1);
            GraphPane myPlane = zedGrapgControl1.GraphPane;
            myPlane.Title.Text = "Графики:";
            myPlane.XAxis.Title.Text = "X";
            myPlane.YAxis.Title.Text = "Y";
        }
        private void GetSize()
        {
            zedGrapgControl1.Location = new Point(10, 10);
            zedGrapgControl1.Size = new Size(ClientRectangle.Width - 20, ClientRectangle.Height - 20);
        }
        protected override void OnSizeChanged(EventArgs e)
        {
            GetSize();
        }

        private void Clear()
        {
            zedGrapgControl1.GraphPane.CurveList.Clear();
            zedGrapgControl1.GraphPane.GraphObjList.Clear();

            zedGrapgControl1.GraphPane.XAxis.Type = AxisType.Linear;
            zedGrapgControl1.GraphPane.XAxis.Scale.TextLabels = null;
            zedGrapgControl1.GraphPane.XAxis.MajorGrid.IsVisible = false;
            zedGrapgControl1.GraphPane.YAxis.MajorGrid.IsVisible = false;
            zedGrapgControl1.GraphPane.YAxis.MinorGrid.IsVisible = false;
            zedGrapgControl1.GraphPane.XAxis.MinorGrid.IsVisible = false;
            zedGrapgControl1.RestoreScale(zedGrapgControl1.GraphPane);

            zedGrapgControl1.AxisChange();
            zedGrapgControl1.Invalidate();
        }

        static double f(double x, double y)
        {
            return (x*x*x*Math.Sin(y) - x)/(-2*y);
        }
        static double f0(double y)
        {
            return Math.Sqrt(y / (-Math.Cos(y) + Math.PI - 1));
        }

        private void Eiler()
        {
            GraphPane myPlane = zedGrapgControl1.GraphPane;

            PointPairList list1 = new PointPairList();
            PointPairList list2 = new PointPairList();
            int n = 0;

            if (!int.TryParse(textBox1.Text, out n))
            {
                MessageBox.Show("Некорректное N.");
                
            }
            
            double maxNev = 0, h, x, y, nev;

            const int maxY = 12;

            x = 1; y = Math.PI;

            h = 7.46/n;

            bool b = true;

            for (;y < maxY;)
            {
                

                nev = 0;

                if ((x >= 1 && x <= 2) && b)
                {
                    nev = Math.Abs(x - f0(y));
                    list1.Add(x, y);
                }
                    
                else if (x >= 1 && x <= 2)
                {
                    nev = Math.Abs(x - f0(y));
                    list2.Add(x, y);
                } 
                else
                    b = false;

                if (nev > maxNev)
                    maxNev = nev;

                y += h;
                x += h * f(x, y);

            }

            myPlane.AddCurve("Решение методом Эйлера", list1, Color.Blue, SymbolType.None);
            myPlane.AddCurve("", list2, Color.Blue, SymbolType.None);
            label3.Text = maxNev.ToString();
            zedGrapgControl1.AxisChange();
            zedGrapgControl1.Invalidate();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void Origin()
        {
            const int maxY = 12;

            GraphPane myPlane = zedGrapgControl1.GraphPane;

            PointPairList list1 = new PointPairList();
            PointPairList list2 = new PointPairList();

            bool b = true;
            int i = 0;
            double x, y;

            for (double k = Math.PI; k < maxY; k+=0.001, i++)
            {
                y = k;
                x = f0(y);

                if((x >= 1 && x <= 2) && b)
                    list1.Add(x, y);
                else if (x >= 1 && x <= 2)
                    list2.Add(x, y);
                else
                    b = false;
                
                
            }

            myPlane.AddCurve("Точное решение", list1, Color.Red, SymbolType.None);
            myPlane.AddCurve("",list2, Color.Red, SymbolType.None);

            zedGrapgControl1.AxisChange();
            zedGrapgControl1.Invalidate();
        }

        private void button1_Click(object sender, EventArgs e)//Эйлер
        {

            
        }
        private void button3_Click_1(object sender, EventArgs e)//точное
        {
            Clear();
            Eiler();
            Origin();
        }

    }
}

