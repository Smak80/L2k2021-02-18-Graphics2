using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace L2k2021_02_18_Graphics2
{
    class Painter
    {
        private int lineSize = 3;
        public Color VertexColor = Color.BlueViolet;
        private int vertexSize = 30;
        private int vertexCount = 1;
        //private RectangleF sr;
        private Image img;
        public Image Img
        {
            get => img;
            private set => img = value;
        }

        public int VertexCount
        {
            get { return vertexCount; }
            set { vertexCount = (value <= 0) ? 1 : value; }
        }

        private RectangleF GetSquareRect(RectangleF r)
        {
            var minSz = Math.Min(r.Width, r.Height) - 2 * lineSize;
            RectangleF res = new RectangleF(
                (r.Width - minSz) / 2,
                (r.Height - minSz) / 2,
                minSz,
                minSz
                );
            return res;
        }

        public void Paint(Graphics g)
        {
            Img = new Bitmap((int)g.VisibleClipBounds.Width, (int)g.VisibleClipBounds.Height);
            Graphics ig = Graphics.FromImage(Img);
            ig.SmoothingMode = SmoothingMode.AntiAlias;
            DrawVertices(ig);
            g.Clear(Color.White);
            g.DrawImage(Img, 0, 0);
        }

        private void DrawVertices(Graphics g)
        {
            var sr = GetSquareRect(g.VisibleClipBounds);
            var center = new PointF(sr.X + sr.Width / 2, sr.Y + sr.Height / 2);
            g.TranslateTransform(center.X, center.Y);
            Pen p = new Pen(VertexColor, lineSize);
            var fnt = new Font("Times New Roman", 14, FontStyle.Bold | FontStyle.Underline);
            var b = new SolidBrush(VertexColor);
            for (int i = 1; i <= VertexCount; i++)
            {
                var text_sz = g.MeasureString(i.ToString(), fnt);
                var pos = new PointF(-text_sz.Width/2, (-sr.Height + vertexSize - text_sz.Height) / 2);
                g.RotateTransform(360 / VertexCount);
                g.DrawEllipse(p, -vertexSize / 2, -sr.Height / 2, vertexSize, vertexSize);
                g.DrawString(i.ToString(), fnt, b, pos);
            }
            g.ResetTransform();
            g.DrawEllipse(new Pen(Color.Black), sr.X, sr.Y, sr.Width, sr.Height);
        }
    }
}
