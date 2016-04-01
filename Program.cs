using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
 using System.Threading;

namespace lab7
{
   
        class Sum
        {
            static readonly object countLock = new object();
            int[] numbers = new int[] { 1, 2, 3, 4, 5, 7, 9, 10};
            int count;
            public Sum()
            {
                count = numbers.Length;
            }

            private void add(int i)
            {
                int sum = numbers[i];
                if ((i + 1) < count)
                {
                    sum += numbers[i + 1];
                }
                numbers[i] = sum;
            }

            public void master()
            {
                Console.WriteLine("Vectorul initial este :");
                for (int i = 0; i < count; i++)
                {
                    Console.Write(numbers[i] + " ");
                }
                Console.WriteLine("\n");

                while (count > 1)
                {
                    List<Thread> threaduri = new List<Thread>();
                 
                    for (int i = 0; i < count; i += 2)
                    {
                        Thread aux = new Thread(() => add(i));
                        threaduri.Add(aux);
                        aux.Start();
                        aux.Join();

                        Monitor.Enter(countLock);
                    }

                    for (int i = 0; i < count; i = i + 2)
                    {
                        numbers[i / 2] = numbers[i];
                        Console.WriteLine("suma elementelor vecine:" + numbers[i]);
                    }
                    Console.WriteLine("\n");
                    if (count % 2 == 0) 
                    {
                        count /= 2;
                    }
                    Monitor.Exit(countLock);
                }
              

            }
        }
        class Program
        {
            static void Main(string[] args)
            {
                Sum sv = new Sum();
                sv.master();
                Console.Read();
            }
        }
    }

    
