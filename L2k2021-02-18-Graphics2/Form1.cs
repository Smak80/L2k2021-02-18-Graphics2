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
            savePictureDialog.OverwritePrompt = true;
            if (savePictureDialog.ShowDialog() == DialogResult.OK)
            {
                var filename = savePictureDialog.FileName;
                p.Img.Save(filename);
            }
        }

        private void mainPanel_Paint(object sender, PaintEventArgs e)
        {
            p.Paint(mainPanel.CreateGraphics());
        }

        private void btnLoad_Click(object sender, EventArgs e)
        {
            if (openPictureDialog.ShowDialog() == DialogResult.OK)
            {
                var filename = openPictureDialog.FileName;
                p.Img = Image.FromFile(filename);
                mainPanel.Refresh();
            }
        }

        private void mainPanel_Resize(object sender, EventArgs e)
        {
            mainPanel.Refresh();
        }
    }
}
