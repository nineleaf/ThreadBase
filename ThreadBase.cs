using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Class_Ning
{
    public class ThreadBase
    {

        bool Flag = false;
        Thread mythread;
        private readonly object Mylock = new object();

        public void Start()
        {
            lock(Mylock)
            {
                if (Flag != false)
                {
                    Flag = true;
                    mythread = new Thread(new ThreadStart(Run));
                    mythread.Start();
                }
            }
            
        }
        public void Stop()
        {
            lock(Mylock)
            {
                Flag = false;
            }
        }
        protected virtual void Run()
        {
           
        }

    }
}
