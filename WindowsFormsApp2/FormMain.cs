using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;
namespace WindowsFormsApp2
{
    public partial class FormMain : Form
    {
        Dictionary<string, Point> dicAllPoint = new Dictionary<string, Point>();
        Snake snake;
        Thread Run;
        Graphics graphics;
        bool isRun = true;
        public FormMain()
        {
            InitializeComponent();
        }
        
        private void Form1_Load(object sender, EventArgs e)
        {
            Init();
        }

        private void Init()
        {
            //纵向
            for(int i=0;i<20;i++)
            {
                //横向
                for (int j = 0; j < 20; j++)
                {
                    string P = "P" + j.ToString() +"_"+ i.ToString();
                    Point point = new Point(j * 28, i * 28);
                    dicAllPoint.Add(P, point);
                }
            }
        }
        //画矩形
        public void DrawRen()
        {
            graphics = PanMain.CreateGraphics();
            Pen pen = new Pen(Color.Lime, 1);
            Point startPoint = PanMain.Location;
        
            for (int i=1;i<20;i++)
            {
                graphics.DrawLine(pen, new Point(0, i * 28 ), new Point(560, i * 28));
                graphics.DrawLine(pen, new Point( i * 28, 0), new Point(i * 28, 560));
            }
        }

        private void FormMain_Paint(object sender, PaintEventArgs e)
        {
            DrawRen();
        }

        private void NewStart_Click(object sender, EventArgs e)
        {
            Run = new Thread(() =>
            {
                snake = new Snake(dicAllPoint);
                snake.isRun = true;

                while (snake.isRun)
                {

                    Thread.Sleep(1000 / snake.Speed);
                    if (!snake.Next())
                    {
                        MessageBox.Show("OVER");
                        return;
                    }
                    //画snake
                    foreach (string point in snake.listUnSnakePoint)
                    {
                        Point p = dicAllPoint[point];
                        SolidBrush bru = new SolidBrush(Color.Black);
                        graphics.FillRectangle(bru, p.X+1, p.Y+1, 27, 27);
                    }
                    foreach (string point in snake.listSnakePoint)
                    {
                        Point p = dicAllPoint[point];
                        SolidBrush bru = new SolidBrush(Color.Yellow);
                        graphics.FillRectangle(bru, p.X+1, p.Y+1, 27, 27);
                    }
                    SolidBrush brush = new SolidBrush(Color.Red);

                    graphics.FillRectangle(brush, dicAllPoint[snake.AddPoint].X+1, dicAllPoint[snake.AddPoint].Y+1, 27, 27);

                }
            });
            Run.Start();
            Run.IsBackground = true;
        }

     

        private void FormMain_KeyDown(object sender, KeyEventArgs e)
        {

            if (Run == null)
            {
                TexMsg.AppendText(Run.ThreadState.ToString());
                return;
            }
            TexMsg.AppendText(e.KeyCode.ToString() + "按键按下\r\n");
            if (e.KeyCode == Keys.A)
            {
                if(snake.Dir!= Snake.Direct.Right)
                {
                    snake.Dir = Snake.Direct.Left;
                }
            }
            else if (e.KeyCode == Keys.D)
            {
                if (snake.Dir != Snake.Direct.Left)
                {
                    snake.Dir = Snake.Direct.Right;
                }
            }
            else if (e.KeyCode == Keys.W)
            {
                if (snake.Dir != Snake.Direct.Down)
                {
                    snake.Dir = Snake.Direct.Up;
                }
            }
            else if(e.KeyCode == Keys.S)
            {
                if (snake.Dir != Snake.Direct.Up)
                {
                    snake.Dir = Snake.Direct.Down;
                }
            }
        }

        private void Stop_Click(object sender, EventArgs e)
        {
            snake.isRun = false;
            Run = null;
        }
    }
}
