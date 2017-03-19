using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySearch
{
    class Program
    {
        static void Main(string[] args)
        {
            System.Diagnostics.Stopwatch sw = new Stopwatch();

            // Создаём список для поиска
            var searchedList = new List<int>();
            for (var i = -100000; i < 100000; i++)
            {
                searchedList.Add(i);
            }

            #region Простым перебором (очень долго)
            //sw.Start();
            //for (var i = 0; i < 1; i++)
            //{
            //    var tpl = MyTask.MySearch.Search(searchedList, 3);
            //}
            //sw.Stop();
            //Console.WriteLine("Простой метод: " + (sw.ElapsedMilliseconds / 1000.0).ToString() + " с.");

            //sw.Reset();
            #endregion

            // Хэш с сортировкой
            sw.Start();
            for (var i = 0; i < 100; i++)
            {
                var tpl = MyTask.MySearch.SearchHashSort(searchedList, 3);
            }
            sw.Stop();
            Console.WriteLine("Хэш с сортировкой: " + (sw.ElapsedMilliseconds / 1000.0).ToString() + " с.");

            sw.Reset();

            // Хэш без сортировки
            sw.Start();
            for (var i = 0; i < 100; i++)
            {
                var tpl = MyTask.MySearch.SearchHashUnsort(searchedList, 3);
            }
            sw.Stop();
            Console.WriteLine("Хэш без сортировки: " + (sw.ElapsedMilliseconds / 1000.0).ToString() + " с.");

            sw.Reset();

            // Хэш с сортировкой Dictionary
            sw.Start();
            for (var i = 0; i < 100; i++)
            {
                var tpl = MyTask.MySearch.SearchHashSortDictionary(searchedList, 3);
            }
            sw.Stop();
            Console.WriteLine("Хэш с сортировкой Dictionary: " + (sw.ElapsedMilliseconds / 1000.0).ToString() + " с.");

            sw.Reset();

            // Хэш без сортировки Dictionary
            sw.Start();
            for (var i = 0; i < 100; i++)
            {
                var tpl = MyTask.MySearch.SearchHashUnsortDictionary(searchedList, 3);
            }
            sw.Stop();
            Console.WriteLine("Хэш без сортировки Dictionary: " + (sw.ElapsedMilliseconds / 1000.0).ToString() + " с.");
            

            // Пример
            Console.WriteLine("\nПример:");

            var searchedList2 = new List<int> { -11, -3, -2, -1, 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 };
            var tpl2 = MyTask.MySearch.SearchHashUnsortDictionary(searchedList2, -3);

            Console.WriteLine("Исходный список: " + string.Join(", ", searchedList2));
            Console.WriteLine("Пары значений:");

            foreach (var item in tpl2)
            {
                Console.WriteLine(item.Item1 + " " + item.Item2);
            }
            
            Console.WriteLine("Для завершения нажмите любую клавишу.");
            Console.ReadKey();
        }
    }
}