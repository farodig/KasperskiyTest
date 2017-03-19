using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyTask
{
    /// <summary>
    /// 2
    /// </summary>
    public static class MySearch
    {
        /// <summary>
        /// Методом перебора (Простой вариант, не подходит для больших объёмов данных, можно даже не смотреть)
        /// </summary>
        /// <param name="lst">Обрабатываемый список</param>
        /// <param name="x">Значение для вычисления</param>
        /// <returns>Список найденых пар</returns>
        public static List<Tuple<int, int>> Search(List<int> lst, int x)
        {
            // Инициируем результат
            var result = new List<Tuple<int, int>>();

            // Проходимся по всем значениям
            foreach (var item in lst)
            {
                // Ищем нужные элементы
                if (lst.Contains(x - item)
                    // Исключаем дубликаты
                    && !result.Any(a => a.Item1 == (x - item) && a.Item2 == item))
                        result.Add(new Tuple<int, int>(item, x - item));
            }
            return result;
        }

        /// <summary>
        /// Метод хэш таблицы (Существенно быстрее)
        /// Вариант с сортировкой (один лишний проход)
        /// </summary>
        /// <param name="lst">Обрабатываемый список</param>
        /// <param name="x">Значение для вычисления</param>
        /// <returns>Список найденых пар</returns>
        public static List<Tuple<int, int>> SearchHashSort(List<int> lst, int x)
        {
            // Инициируем результат
            var result = new List<Tuple<int, int>>();

            // Минимально, максимальное значение
            var minValue = lst.Min();
            var maxValue = lst.Max();

            // Смещение (индекс не может быть меньше 0)
            var offset = -minValue > -(x - maxValue) ? -minValue : -(x - maxValue);

            // Вычисляем размер хэш таблицы
            var count = Math.Abs(maxValue - minValue) + offset;

            // Хэш таблица
            int?[] dict = new int?[count];
            
            // Записываем значения в хэш
            foreach (var item in lst)
            {
                dict[offset + item] = x - item;
            }

            // Проходимся по хэш таблице, совпавшие значения записываем в результирующий список
            for (var index = 0; index < count; index++)
            {
                if (dict[index].HasValue)
                {
                    var valueIndex = offset + dict[index].Value;

                    if (dict[valueIndex].HasValue)
                    {
                        dict[valueIndex] = null;
                        result.Add(new Tuple<int, int>(index - offset, dict[index].Value));
                    }
                    else
                    {
                        dict[index] = null;
                    }
                }
            }

            return result;
        }

        /// <summary>
        /// Метод хэш таблицы (Существенно быстрее)
        /// Без сортировки, чуть быстрее
        /// </summary>
        /// <param name="lst">Обрабатываемый список</param>
        /// <param name="x">Значение для вычисления</param>
        /// <returns>Список найденых пар</returns>
        public static List<Tuple<int, int>> SearchHashUnsort(List<int> lst, int x)
        {
            // Инициируем результат
            var result = new List<Tuple<int, int>>();

            // Минимально, максимальное значение
            var minValue = lst.Min();
            var maxValue = lst.Max();

            // Смещение (индекс не может быть меньше 0)
            var offset = -minValue > -(x - maxValue) ? -minValue : -(x - maxValue);

            // Вычисляем размер хэш таблицы
            var count = Math.Abs(maxValue - minValue) + offset;

            // Хэш таблица
            int?[] dict = new int?[count];
            
            // Записываем значения в хэш
            foreach (var item in lst)
            {
                var index = offset + item;
                var value = offset + (x - item);
                if (dict[value].HasValue)
                {
                    result.Add(new Tuple<int, int>(x - item, item));
                }
                else
                {
                    dict[index] = x - item;
                }
            }

            return result;
        }

        /// <summary>
        /// Метод хэш таблицы с использованием Dictionary
        /// Вариант с сортировкой (один лишний проход)
        /// </summary>
        /// <param name="lst">Обрабатываемый список</param>
        /// <param name="x">Значение для вычисления</param>
        /// <returns>Список найденых пар</returns>
        public static List<Tuple<int, int>> SearchHashSortDictionary(List<int> lst, int x)
        {
            // Инициируем результат
            var result = new List<Tuple<int, int>>();
            
            // Инициируем хэш таблицу
            var dict = new Dictionary<int, int>();

            //Записываем значения в хэш
            foreach (var item in lst)
            {
                dict.Add(item, x - item);
            }

            // Проходимся по хэш таблице, записываем значение в результат
            foreach (var item in dict)
            {
                // Если находим совпадение по хэшу + доп условие для исключения дублей
                if (dict.ContainsKey(item.Value) && item.Key < item.Value)
                {
                    result.Add(new Tuple<int, int>(item.Key, item.Value));
                }
            }

            return result;
        }

        /// <summary>
        /// Метод хэш таблицы с использованием Dictionary
        /// Без сортировки, чуть быстрее
        /// </summary>
        /// <param name="lst">Обрабатываемый список</param>
        /// <param name="x">Значение для вычисления</param>
        /// <returns>Список найденых пар</returns>
        public static List<Tuple<int, int>> SearchHashUnsortDictionary(List<int> lst, int x)
        {
            // Инициируем результат
            var result = new List<Tuple<int, int>>();
            
            // Инициируем хэш таблицу
            var dict = new Dictionary<int, int?>();

            // Хэш таблица
            foreach (var item in lst)
            {
                var index = x - item;
                if (dict.ContainsKey(index))
                {
                    result.Add(new Tuple<int, int>(index, item));
                }
                else
                    dict.Add(item, index);
            }

            return result;
        }
    }
}