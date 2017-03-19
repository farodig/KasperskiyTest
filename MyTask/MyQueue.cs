using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyTask
{
    /// <summary>
    /// 1
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class MyQueue<T>
    {
        /// <summary>
        /// Контейнер для данных
        /// </summary>
        private Queue<T> container = new Queue<T>();

        /// <summary>
        /// Список задач на удаление элемента
        /// </summary>
        private Queue<Task<T>> removeList = new Queue<Task<T>>();
        
        /// <summary>
        /// Вставляет и выходит
        /// </summary>
        /// <param name="item">Вставляемый элемент</param>
        public async void Push(T item)
        {
            var th = new Task(() =>
            {
                lock (container)
                {
                    // Добавляем объект в конец коллекции
                    container.Enqueue(item);
                }

                // Запускаем задачу удаления из списка
                if (removeList.Any())
                    removeList.Dequeue()?.Start();
            });
            th.Start();
            await th;
        }

        /// <summary>
        /// Ждёт пока не появится новый элемент и возвращает элемент из очереди
        /// </summary>
        /// <returns>Удалённый из очереди элемент</returns>
        public async Task<T> Pop()
        {
            // Создаём задачу на удаление из списка
            var th = new Task<T>(() =>
            {
                lock (container)
                {
                    // Удаляем и возвращаем объект находящийся в начале очереди
                    return container.Dequeue();
                }
            });

            // Добавляем задачу на удаление (следующий вызов Push запустит её)
            removeList.Enqueue(th);

            // Дожидаемся окончания задачи и возвращаем результат
            return await th;
        }

        /// <summary>
        /// Привести к обычному списку (для вывода результатов)
        /// </summary>
        /// <returns>List</returns>
        public List<T> ToList()
        {
            lock(container)
            {
                return container.ToList();
            }
        }
    }
}
