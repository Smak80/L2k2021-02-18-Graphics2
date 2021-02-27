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
        // Объявление переменной класса Painter,
        // который используется для выполнения отрисовки изображений
        private Painter p;
        public Form1()
        {
            InitializeComponent();
            // Создаем в памяти объект класса.
            p = new Painter();
            // Устанавливаем параметры (количество кружков для вывода)
            p.VertexCount = 12;
        }

        // Нажатие кнопки "Сохранить"
        private void button1_Click(object sender, EventArgs e)
        {
            // Получаем имя файла для записи в него картинки

            // Будем требовать подтверждения пользователя при перезаписи файла
            savePictureDialog.OverwritePrompt = true;
            // Показываем диалог сохранения и убеждаемся, что имя файла выбрано
            // (пользователь не передумал сохранять)
            if (savePictureDialog.ShowDialog() == DialogResult.OK)
            {
                // Получаем выбранное имя файла
                var filename = savePictureDialog.FileName;
                // Сохраняем изображение
                p.Img.Save(filename);
            }
        }

        // Метод вызывается при необходимости перерисовки содержимого панели
        private void mainPanel_Paint(object sender, PaintEventArgs e)
        {
            // Выполняется метод отрисовки изображения класса Painter
            // В качестве параметра передается объект класса Graphics,
            // ассоциированный с панелью
            p.Paint(mainPanel.CreateGraphics());
        }

        // Нажатие кнопки "Открыть"
        private void btnLoad_Click(object sender, EventArgs e)
        {
            // Открываем диалог открытия файла
            if (openPictureDialog.ShowDialog() == DialogResult.OK)
            {
                // Если пользователь выбрал файл (не передумал),
                // получаем указанное имя файла
                var filename = openPictureDialog.FileName;

                // Создаем изображение из указанного файла
                // и записываем его в свойство Img объекта p класса Painter
                p.Img = Image.FromFile(filename);
                
                // Требуем перерисовать панель,
                // чтобы на ней нарисовалось новое изобрбажение
                mainPanel.Refresh();
            }
        }

        // Метод вызывается при изменении размеров панели
        private void mainPanel_Resize(object sender, EventArgs e)
        {
            // При изменении размеров панели требуем обновить 
            // ее содержимое. 
            mainPanel.Refresh(); // Следом будет вызван метод mainPanel_Paint
        }
    }
}
