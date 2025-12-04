using System;
using System.Linq;
using System.Collections.Generic;

namespace LabWork
{
    // Даний проект є шаблоном для виконання лабораторних робіт

    /// <summary>
    /// Клас, що представляє Арифметичну Прогресію.
    /// Характеризується першим членом (a0), різницею (d) та кількістю членів (n).
    /// </summary>
    public class ArithmeticProgression
    {
        // Приватні поля (інкапсуляція)
        private readonly double _first;
        private readonly double _difference;
        private readonly int _count;

        // Публічні властивості (PascalCase) лише для читання
        public double First => _first;
        public double Difference => _difference;
        public int Count => _count;

        /// <summary>
        /// Конструктор для ініціалізації арифметичної прогресії.
        /// </summary>
        /// <param name="a0">Перший член прогресії (double).</param>
        /// <param name="d">Різниця прогресії (double).</param>
        /// <param name="n">Кількість членів прогресії (int). Має бути > 0.</param>
        public ArithmeticProgression(double a0, double d, int n)
        {
            // Валідація входу: перевірка, що Count > 0
            if (n <= 0)
            {
                throw new ArgumentException("Кількість членів (Count) повинна бути більшою за нуль.", nameof(n));
            }

            _first = a0;
            _difference = d;
            _count = n;
        }

        /// <summary>
        /// Обчислює суму арифметичної прогресії за формулою: S = n*(2*a0 + (n-1)*d)/2.
        /// </summary>
        /// <returns>Сума n членів прогресії (double).</returns>
        public double Sum()
        {
            // Формула суми арифметичної прогресії
            double sum = (double)_count * (2 * _first + (_count - 1) * _difference) / 2;
            return sum;
        }

        /// <summary>
        /// Перевизначений метод для читабельного виводу параметрів прогресії та її суми.
        /// </summary>
        /// <returns>Рядок із параметрами прогресії та обчисленою сумою.</returns>
        public override string ToString()
        {
            return $"A.P.: First (a0)={First:F2}, Difference (d)={Difference:F2}, Count (n)={Count}, Sum={Sum():F2}";
        }
    }

    /// <summary>
    /// Заглушка для можливих майбутніх результатів лабораторної роботи.
    /// </summary>
    class Result
    {
        // TODO: do it !
    }

    // Клас Program зроблено статичним, оскільки він містить лише статичні члени.
    public static class Program 
    {
        /// <summary>
        /// Допоміжний метод для безпечного зчитування double з консолі.
        /// </summary>
        private static double ReadDouble(string prompt)
        {
            double value;
            Console.Write(prompt);
            while (!double.TryParse(Console.ReadLine(), out value))
            {
                Console.WriteLine("Помилка вводу. Будь ласка, введіть дійсне число.");
                Console.Write(prompt);
            }
            return value;
        }

        /// <summary>
        /// Допоміжний метод для безпечного зчитування int з консолі, перевіряючи, що n > 0.
        /// </summary>
        private static int ReadInt(string prompt)
        {
            int value;
            Console.Write(prompt);
            while (!int.TryParse(Console.ReadLine(), out value) || value <= 0)
            {
                Console.WriteLine("Помилка вводу. Будь ласка, введіть ціле число, БІЛЬШЕ за 0.");
                Console.Write(prompt);
            }
            return value;
        }

        /// <summary>
        /// Точка входу в програму.
        /// </summary>
        public static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            Console.WriteLine("--- 🚀 Лабораторна робота: Арифметична прогресія (Пошук Max Суми) ---");
            Console.WriteLine("-------------------------------------------------------------------");

            // 1. Прочитати розмір масиву (кількість прогресій)
            int totalProgressionsToCreate = 0;
            try
            {
                totalProgressionsToCreate = ReadInt("Введіть кількість прогресій (m > 0), які ви хочете створити: ");
            }
            catch (Exception)
            {
                 Console.WriteLine("Кількість прогресій не була введена коректно. Завершення.");
                 return;
            }
            
            var progressions = new List<ArithmeticProgression>();
            
            // 2. Наповнення масиву/списку
            for (int i = 0; i < totalProgressionsToCreate; i++)
            {
                Console.WriteLine($"\n--- Введення параметрів для прогресії #{i + 1} ---");
                try
                {
                    double a0 = ReadDouble($"Введіть перший член a0: ");
                    double d = ReadDouble($"Введіть різницю d: ");
                    // Тут викликаємо ReadInt, який гарантує n > 0
                    int n = ReadInt($"Введіть кількість членів n (n > 0): "); 
                    
                    progressions.Add(new ArithmeticProgression(a0, d, n));
                }
                catch (ArgumentException ex)
                {
                    // Обробка помилок валідації конструктора
                    Console.WriteLine($"Помилка валідації: {ex.Message}. Створення прогресії #{i + 1} пропущено.");
                    // НЕ ЗМЕНШУЄМО totalProgressionsToCreate, але об'єкт не додається в progressions.
                }
            }
            
            Console.WriteLine("\n-------------------------------------------------------------------");
            
            // ВИПРАВЛЕНО: Виводимо реальну кількість успішно доданих об'єктів
            Console.WriteLine($"Створено та збережено {progressions.Count} прогресій з {totalProgressionsToCreate} спроб. Обчислення сум...");

            if (progressions.Count == 0)
            {
                Console.WriteLine("Немає прогресій для аналізу.");
                return;
            }

            // Вивід усіх прогресій для перевірки
            Console.WriteLine("\nСтворені прогресії:");
            int counter = 1;
            foreach (var p in progressions)
            {
                Console.WriteLine($"Прогресія #{counter++}: {p.ToString()}");
            }

            // 3. Пошук прогресії з максимальною сумою (використовуємо LINQ)
            ArithmeticProgression maxSumProgression = progressions
                .OrderByDescending(p => p.Sum()) 
                .FirstOrDefault();

            // 4. Виведення результату
            Console.WriteLine("\n-------------------------------------------------------------------");
            if (maxSumProgression != null)
            {
                Console.WriteLine("🏆 ПРОГРЕСІЯ З НАЙБІЛЬШОЮ СУМОЮ:");
                Console.WriteLine(maxSumProgression.ToString());
            }
            
            Console.WriteLine("\n--- Завершення роботи програми ---");
        }
    }
}
