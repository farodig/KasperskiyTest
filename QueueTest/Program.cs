using MyTask;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace QueueTest
{
    class Program
    {
        static void Main(string[] args)
        {
            // Инициируем очередь
            var qe = new MyQueue<int>();

            // Поток на создание элементов
            Thread myThread = new Thread(() =>
            {
                for(var i = 0; i < 20; i++)
                {
                    Thread.Sleep(10);
                    qe.Push(i);
                    Console.WriteLine($"добавлен элемент {i} :" + string.Join(", ", qe.ToList()));
                }
            });

            // Поток на удаление элементов 1
            Thread myThread2 = new Thread(() =>
            {
                for (var i = 0; i < 10; i++)
                {
                    Thread.Sleep(10);
                    var pop = qe.Pop();
                    Console.WriteLine("создана задача на удаление");
                    var item = pop.Result;
                    Console.WriteLine($"удалён {item} после добавления: " + string.Join(", ", qe.ToList()));
                }
            });

            // Поток на удаление элементов 2
            Thread myThread3 = new Thread(() =>
            {
                for (var i = 0; i < 10; i++)
                {
                    Thread.Sleep(10);
                    var pop = qe.Pop();
                    Console.WriteLine("создана задача на удаление");
                    var item = pop.Result;
                    Console.WriteLine($"удалён {item} после добавления: " + string.Join(", ", qe.ToList()));
                }
            });

            // Запускаем потоки
            myThread.Start();
            Thread.Sleep(50);
            myThread2.Start();
            myThread3.Start();

            // Возвращаемся
            myThread.Join();
            myThread2.Join();
            myThread3.Join();
            
            Console.ReadKey();
        }


    }
}
