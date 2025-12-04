using System;
using System.Linq; // Використовується для OrderByDescending
using System.Collections.Generic; // Використовується для List<T>

namespace LabWork
{
    // Даний проект є шаблоном для виконання лабораторних робіт

    /// <summary>
    /// Клас, що представляє Арифметичну Прогресію.
    /// Характеризується першим членом (a0), різницею (d) та кількістю членів (n).
    /// </summary>
    public class ArithmeticProgression // PascalCase
    {
        // Приватні поля (інкапсуляція)
        private readonly double _firstTerm;
        private readonly double _difference;
        private readonly int _count;

        // Властивості (PascalCase) лише для читання
        public double FirstTerm => _firstTerm;
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
            // Обробка граничних випадків: n<=0
            if (n <= 0)
            {
                // Повідомлення про помилкові дані
                throw new ArgumentException("Кількість членів (Count) повинна бути більшою за нуль.", nameof(n));
            }

            _firstTerm = a0;
            _difference = d;
            _count = n;
        }

        /// <summary>
        /// Обчислює суму арифметичної прогресії за формулою: S = n*(2*a0 + (n-1)*d)/2.
        /// </summary>
        /// <returns>Сума n членів прогресії (double).</returns>
        public double Sum() // Метод Sum()
        {
            // Використовуємо формулу: sum = n*(2*a0 + (n-1)*d)/2
            double sum = (double)_count * (2 * _firstTerm + (_count - 1) * _difference) / 2;
            return sum;
        }

        /// <summary>
        /// Перевизначений метод для читабельного виводу параметрів прогресії та її суми.
        /// </summary>
        /// <returns>Рядок із параметрами прогресії та обчисленою сумою.</returns>
        public override string ToString()
        {
            return $"A.P.: a0={FirstTerm:F2}, d={Difference:F2}, n={Count}, Sum={Sum():F2}";
        }
    }


    class Result
    {
        // Клас залишаємо як заглушку, як у початковому шаблоні
    }

    class Program
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
        /// Допоміжний метод для безпечного зчитування int з консолі.
        /// </summary>
        private static int ReadInt(string prompt)
        {
            int value;
            Console.Write(prompt);
            while (!int.TryParse(Console.ReadLine(), out value) || value <= 0)
            {
                Console.WriteLine("Помилка вводу. Будь ласка, введіть ціле число, більше за 0.");
                Console.Write(prompt);
            }
            return value;
        }


        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            Console.WriteLine("--- 🚀 Лабораторна робота: Арифметична прогресія (Пошук Max Суми) ---");
            Console.WriteLine("-------------------------------------------------------------------");
            
            // 1. Запит у користувача кількості прогресій m
            int m = ReadInt("Введіть кількість прогресій (m > 0): ");
            
            // Створення List для динамічного зберігання об'єктів
            var progressions = new List<ArithmeticProgression>();
            
            // 2. Зчитування параметрів та створення об'єктів
            for (int i = 0; i < m; i++)
            {
                Console.WriteLine($"\n--- Введення параметрів для прогресії #{i + 1} ---");
                try
                {
                    double a0 = ReadDouble($"Введіть перший член a0 (Прогресія #{i + 1}): ");
                    double d = ReadDouble($"Введіть різницю d (Прогресія #{i + 1}): ");
                    int n = ReadInt($"Введіть кількість членів n (n > 0, Прогресія #{i + 1}): ");
                    
                    // Створення та додавання об'єкта
                    progressions.Add(new ArithmeticProgression(a0, d, n));
                }
                catch (ArgumentException ex)
                {
                    // Обробка помилок (хоча ReadInt вже перевіряє n>0)
                    Console.WriteLine($"Помилка: {ex.Message}. Прогресія #{i + 1} пропущена.");
                    i--; // Повторити ітерацію для коректної кількості
                }
            }
            
            Console.WriteLine("\n-------------------------------------------------------------------");
            Console.WriteLine($"Введено {progressions.Count} коректних прогресій. Обчислення сум...");

            if (progressions.Count == 0)
            {
                Console.WriteLine("Немає прогресій для аналізу.");
                return;
            }

            // 3. Знаходження прогресії з найбільшою сумою (традиційний спосіб)
            ArithmeticProgression maxSumProgression = progressions[0];
            double maxSum = maxSumProgression.Sum();
            
            Console.WriteLine("\nДетальний вивід прогресій та пошук максимуму:");

            for (int i = 0; i < progressions.Count; i++)
            {
                var currentProgression = progressions[i];
                double currentSum = currentProgression.Sum();
                
                // Вивід поточної прогресії (ToString)
                Console.WriteLine($"Прогресія #{i + 1}: {currentProgression.ToString()}");

                // Порівняння для знаходження найбільшої суми
                if (currentSum > maxSum)
                {
                    maxSum = currentSum;
                    maxSumProgression = currentProgression;
                }
            }
            
            Console.WriteLine("\n-------------------------------------------------------------------");

            // 4. Виведення результату
            Console.WriteLine("🏆 ПРОГРЕСІЯ З НАЙБІЛЬШОЮ СУМОЮ:");
            Console.WriteLine(maxSumProgression.ToString());
            
            Console.WriteLine("\n--- Завершення роботи програми ---");
        }
    }
}
