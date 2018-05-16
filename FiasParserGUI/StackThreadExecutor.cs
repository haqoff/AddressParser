using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading;

namespace FiasParserGUI
{
    public class StackThreadExecutor
    {
        private List<ThreadStart> threadsStarts;
        private ConcurrentStack<Thread> threads;
        private Object forLock;

        public StackThreadExecutor()
        {
            threadsStarts = new List<ThreadStart>();
            threads = new ConcurrentStack<Thread>();
            forLock = new object();
        }

        public void Bind(ThreadStart threadStart)
        {
            lock (forLock)
            {
                threadsStarts.Add(threadStart);

                if (threads.Count == 0)
                {
                    var th = new Thread(ExecuteLast);
                    threads.Push(th);
                    th.Start();
                }
            }
        }

        private void ExecuteLast()
        {
            ThreadStart topThreadStart = null;
            lock (forLock)
            {
                if (threadsStarts.Count == 0)
                {
                    threads.Clear();
                    return;
                }

                topThreadStart = threadsStarts[threadsStarts.Count - 1];
                threadsStarts.Clear();
            }

            topThreadStart?.Invoke();

            ExecuteLast();
        }

    }
}
