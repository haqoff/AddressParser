using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace FiasParserGUI
{
    public class ProgressTimer
    {
        public int MaxDone { get; set; }
        public int CurrentDone { get; private set; }

        public TimeSpan TimeRemaining { get; private set; }
        public TimeSpan TimeElapsed { get; private set; }

        private int countPerUpdate;

        private Timer timer;

        public ProgressTimer(int countPerUpdate)
        {
            this.countPerUpdate = countPerUpdate;
            timer = new Timer()
            {
                Enabled = true,
                Interval = 1000
            };
            timer.Elapsed += Tick;
        }

        private void Tick(object sender, ElapsedEventArgs e)
        {
            if (TimeRemaining == null || TimeRemaining.TotalMilliseconds <= timer.Interval) TimeRemaining = new TimeSpan();
            else
            {
                TimeRemaining.Subtract(TimeSpan.FromMilliseconds(timer.Interval));
            }          
        }

        public void Start()
        {
            TimeRemaining = new TimeSpan();
            timer.Start();
        }


        public void Iterate(int n)
        {

        }

    }
}
