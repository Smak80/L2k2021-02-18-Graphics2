using System;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace L2k2021_02_18_Graphics2
{
    // Класс для прорисовки изображений
    class Painter
    {
        // Толщина рисуемых линий
        private int lineSize = 3;

        // Цвет рисуемых линий
        public Color VertexColor = Color.BlueViolet;

        // Размер кружков
        private int vertexSize = 30;

        // Количество выводимых кружков
        // (по умолчанию 1, но будет изменено в классе формы)
        private int vertexCount = 1;
        
        // Картинка, в которой формируется изображение
        public Image Img = null;

        // Свойство, используемое для получения и задания количества кружков
        public int VertexCount
        {
            get { return vertexCount; }
            set { vertexCount = (value <= 0) ? 1 : value; }
        }

        // Кружки (вершины) будут располагаться по окружности
        // Предварительно необходимо найти квадратную область, в которой
        // будет располагаться эта большая окружность 
        // (по центру панели)
        // Параметр r - исходная прямоугольная область, содержащая размеры панели
        private RectangleF GetSquareRect(RectangleF r)
        {
            // Вычисление границ квадратной области
            // Размер - это минимальное значение ширины и высоты
            // Также вычитается двойная толщина линии, которой ободятся вершины
            // Чтобы вершинки сверху и снизу не уехали за края
            var minSz = Math.Min(r.Width, r.Height) - 2 * lineSize;
            // Задаем квадратную область
            RectangleF res = new RectangleF(
                (r.Width - minSz) / 2,
                (r.Height - minSz) / 2,
                minSz,
                minSz
                );
            return res;
        }
        
        // Основной метод класса, используемый для вывода
        public void Paint(Graphics g)
        {
            // Если изображение (картинка) еще не создано
            if (Img == null)
            {
                // ... создаем его
                Img = new Bitmap(
                    (int) g.VisibleClipBounds.Width,
                    (int) g.VisibleClipBounds.Height);
                // получаем Graphics для этой картинки
                var ig = Graphics.FromImage(Img);
                // Устанавливаем сглаживание
                ig.SmoothingMode = SmoothingMode.AntiAlias;
                //Рисуем кружки (вершины)
                DrawVertices(ig);
            }
            // Прорисовываем картинку на панели (основной Graphics - g)
            g.Clear(Color.White);

            // Массив точек во втором параметре
            // позволяет растягивать и сжимать картинку в
            // зависимости от фактических размеров панели.
            g.DrawImage(Img, 
                new PointF[]
                {
                    new PointF(0, 0), 
                    new PointF(g.VisibleClipBounds.Width, 0),
                    new PointF(0, g.VisibleClipBounds.Height)
                });
        }

        // Прорисовка вершин (кружков)
        private void DrawVertices(Graphics g)
        {
            // Получаем размеры квадратной области в центре панели,
            // где будем выводить кружки
            var sr = GetSquareRect(g.VisibleClipBounds);

            // находим центр
            var center = new PointF(sr.X + sr.Width / 2, sr.Y + sr.Height / 2);

            // смещаем центр системы координат в найденную точку
            g.TranslateTransform(center.X, center.Y);

            // Подготавливаем инструменты для рисования (ручка, шрифт и т.п.)
            Pen p = new Pen(VertexColor, lineSize);
            var fnt = new Font("Times New Roman", 14, FontStyle.Bold | FontStyle.Underline);
            var b = new SolidBrush(VertexColor);

            // Рисуем заданное число вершин
            for (int i = 1; i <= VertexCount; i++)
            {
                // Для того, чтобы текст (номера вершин) располагались по центру в кружке
                // вычисляем размеры, которые будут занимать строки (номера),
                // написанные указанным шрифтом
                var text_sz = g.MeasureString(i.ToString(), fnt);

                // и далее находим позицию, в которой следует разместить надпись
                var pos = new PointF(-text_sz.Width/2, (-sr.Height + vertexSize - text_sz.Height) / 2);

                // Вершины всегда рисуем сверху, при этом вращаем холст на нужное число градусов
                g.RotateTransform(360 / VertexCount);

                // Собственно выполняем прорисовку
                g.DrawEllipse(p, -vertexSize / 2, -sr.Height / 2, vertexSize, vertexSize);
                g.DrawString(i.ToString(), fnt, b, pos);
            }
            // Возвращаем параметры графики (начало координат и поворот) в 
            // исходное состояние
            g.ResetTransform();

            // Справочно: выводим границу окружности в рамках которой нарисованы вершины
            g.DrawEllipse(new Pen(Color.Black), sr.X, sr.Y, sr.Width, sr.Height);
        }
    }
}
