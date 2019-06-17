using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace WindowsFormsApp2
{
   public class Snake
    {
        public enum Direct
        {
            Up,
            Down,
            Left,
            Right
        }
        private  int speed =1;
        private string addPoint = "";

        public int Speed
        {
            get
            {
                return speed;
            }
            set
            {
                if(!(value <=0 || value > 10))
                {
                    speed = value;
                }
            }
        }
        public Direct Dir { get; set; }
        public string AddPoint
        {
            get
            {
                return addPoint;
            }
        } 
        public bool isRun { get; set; }


        /// <summary>
        /// 当前占据点
        /// </summary>
       public List<string> listSnakePoint;
        /// <summary>
        /// 所有点
        /// </summary>
       private Dictionary<string, Point> dicAllPoint ;
        /// <summary>
        /// 未被占据的点
        /// </summary>
       public List<string> listUnSnakePoint;


        public Snake(Dictionary<string,Point> pairs)
        {
            dicAllPoint = new Dictionary<string, Point>(pairs);
            listUnSnakePoint = new List<string>(dicAllPoint.Keys);
            listSnakePoint = new List<string>();
            listSnakePoint.Add("P0_0");
            listUnSnakePoint.Remove("P0_0");
            Dir = Direct.Right;
        }
        /// <summary>
        /// 下一步的状态
        /// </summary>
        /// <returns>是否结束</returns>
        public bool Next()
        {
            bool flag= true;
            flag = NextAddPoint()& flag;
            flag = NextSnakeState() & flag;
            flag = NextAddPoint() & flag;
            return flag;
        }

        public bool NextSnakeState()
        {
            string point = listSnakePoint[listSnakePoint.Count - 1];
            int x = int.Parse(point.Split('_')[0].Substring(1).ToString());
            int y = int.Parse(point.Split('_')[1].ToString());
            //获取下一步状态
            switch (Dir)
            {
                case Direct.Left:
                    x -= 1;
                    break;
                case Direct.Right:
                    x += 1;
                    break;
                case Direct.Up:
                    y -= 1;
                    break;
                case Direct.Down:
                    y += 1;
                    break;
            }
            string nextPoint = "P" + x.ToString() + "_" + y.ToString();
            if (!dicAllPoint.Keys.Contains(nextPoint) || listSnakePoint.Contains(nextPoint))
            {
                return false;
            }
            else if (nextPoint.Equals(addPoint))
            {
                listSnakePoint.Add(addPoint);
                listUnSnakePoint.Remove(addPoint);
                addPoint = "";
            }
            else
            {
                //非snake更新数据
                listUnSnakePoint.Add(listSnakePoint[0]);
                listUnSnakePoint.Remove(addPoint);
                //snake更新数据
                listSnakePoint.Add(nextPoint);
                listSnakePoint.RemoveAt(0);
            }
            return true;
        }

        public bool NextAddPoint()
        {
            if(listUnSnakePoint.Count ==0)
            {
                return false;
            }
            if(addPoint.Equals(""))
            {
                Random random = new Random();
                int index = random.Next(0, listUnSnakePoint.Count);
                addPoint = listUnSnakePoint[index];
            }
            return true;
        }
    }
}
