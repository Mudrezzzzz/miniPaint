using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DoublePaint
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            SetSize();
        }
        private class ArrayPoints
        {
            private int index = 0;
            private Point[] points;

            public ArrayPoints(int size)
            { if (size <= 0) { size = 2; }
                points = new Point[size];
            }
            // Массив для рисования отдельными точками

            public void SetPoint(int x, int y)
            {
                if (index >= points.Length) { index = 0; }
                points[index] = new Point(x, y);
                index++;
            } // Установка курсова в начальную позицию

            public void ResetPoints()
            {
                index = 0;
            } // Обнуление индекса

            public int GetCountPoint()
            {
                return index;
            }// Получение размера массива

            public Point[] GetPoints()
            { 
                return points; 
            }
            //Возвращение массива точек
        }  

        private ArrayPoints arrayPoints = new ArrayPoints(2); // Экземпляр класса ArrayPoints

        Bitmap map = new Bitmap(100, 100); //Сохранение рисунка
        Graphics graphics;

        private void SetSize ()
        {
            Rectangle rectangle = Screen.PrimaryScreen.Bounds;
            map = new Bitmap (rectangle.Width, rectangle.Height);
            graphics = Graphics.FromImage (map);

            pen.StartCap = System.Drawing.Drawing2D.LineCap.Round;
            pen.EndCap = System.Drawing.Drawing2D.LineCap.Round;
        }//установка размера map,разрешение экрана пользователя

        private bool isMouseClickinng = false; 

        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            isMouseClickinng = true;
        }// состояние когда левая кнопка мыши нажата

        private void pictureBox1_MouseUp(object sender, MouseEventArgs e)
        {
            isMouseClickinng = false;
            arrayPoints.ResetPoints();
        }// состояние когда левая кнопка мыши поднята

        Pen pen = new Pen(Color.Black, 3f); // Экземпляр карандаша

        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            if (!isMouseClickinng) { return; } // проверка того что клавиша мыши опущена


            arrayPoints.SetPoint(e.X, e.Y);
            if (arrayPoints.GetCountPoint() >= 2) //рисование линии
            {
                graphics.DrawLines(pen,arrayPoints.GetPoints());
                pictureBox1.Image = map;
                arrayPoints.SetPoint (e.X, e.Y);
            }

        }

        private void button3_Click(object sender, EventArgs e)
        {
            pen.Color = ((Button)sender).BackColor;
        }//изменение цвета


        private void button28_Click(object sender, EventArgs e)
        {
            if (colorDialog1.ShowDialog() == DialogResult.OK) 
            {
                pen.Color = colorDialog1.Color;
                ((Button)sender ).BackColor = colorDialog1.Color;
            }
        }// Добавление палитры

        private void button2_Click(object sender, EventArgs e)
        {
            graphics.Clear(pictureBox1.BackColor);
            pictureBox1.Image = map;
        }//очистка экрана

        private void trackBar1_ValueChanged(object sender, EventArgs e)
        {
            pen.Width = trackBar1.Value;
        }//толщина кисти

        private void button1_Click(object sender, EventArgs e)
        {
            saveFileDialog1.Filter = "JPG(*.JPG)|*.jpg";
            if(saveFileDialog1.ShowDialog()== DialogResult.OK)
            {if (pictureBox1.Image == null)
                {
                    pictureBox1.Image.Save(saveFileDialog1.FileName);
                }
            }
        }//Сохранение
    }
}
