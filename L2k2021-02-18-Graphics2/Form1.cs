using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace L2k2021_02_18_Graphics2
{
    public partial class Form1 : Form
    {
        private Painter p;
        public Form1()
        {
            InitializeComponent();
            p = new Painter();
            p.VertexCount = 12;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            mainPanel.Refresh();
            if (sender == button2)
            {
                var gr = Graphics.FromImage(p.Img);
                gr.FillRectangle(new SolidBrush(Color.Black), 0, 0, 200, 200);
                p.Img.Save(@"C:\Smak\Desktop\1.jpg");
            }
        }

        private void mainPanel_Paint(object sender, PaintEventArgs e)
        {
            p.Paint(mainPanel.CreateGraphics());
        }

    }
}
