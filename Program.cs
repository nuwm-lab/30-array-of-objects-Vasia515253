using System;
using System.Linq;
using System.Collections.Generic; // Для використання List<T>

namespace LabWork
{
    // Даний проект є шаблоном для виконання лабораторних робіт

    /// <summary>
    /// Клас, що представляє Арифметичну Прогресію.
    /// Характеризується першим членом (a0), різницею (d) та кількістю членів (n).
    /// </summary>
    public class ArithmeticProgression // PascalCase та явний public
    {
        // Приватні поля для зберігання даних. Забезпечення інкапсуляції.
        private readonly double _firstTerm;
        private readonly double _difference;
        private readonly int _length;

        // Властивості (PascalCase) лише для читання
        public double FirstTerm => _firstTerm;
        public double Difference => _difference;
        public int Length => _length; // Використовуємо Length замість TermsCount/n

        /// <summary>
        /// Конструктор для ініціалізації арифметичної прогресії.
        /// </summary>
        /// <param name="a0">Перший член прогресії (double).</param>
        /// <param name="d">Різниця прогресії (double).</param>
        /// <param name="n">Кількість членів прогресії (int). Має бути > 0.</param>
        public ArithmeticProgression(double a0, double d, int n)
        {
            // Перевірка граничних значень та обробка помилок
            if (n <= 0)
            {
                // Кидаємо ArgumentException з ясним повідомленням
                throw new ArgumentException("Кількість членів прогресії (Length) повинна бути більшою за нуль.", nameof(n));
            }

            _firstTerm = a0;
            _difference = d;
            _length = n;
        }

        /// <summary>
        /// Обчислює суму арифметичної прогресії за формулою: S = n*(2*a0 + (n-1)*d)/2.
        /// </summary>
        /// <returns>Сума n членів прогресії (double).</returns>
        public double CalculateSum() // PascalCase
        {
            // Формула суми арифметичної прогресії: S = n*(2*a0 + (n-1)*d)/2
            double sum = (double)_length * (2 * _firstTerm + (_length - 1) * _difference) / 2;
            return sum;
        }

        /// <summary>
        /// Перевизначений метод для читабельного виводу параметрів прогресії та її суми.
        /// </summary>
        /// <returns>Рядок із параметрами прогресії та її сумою.</returns>
        public override string ToString()
        {
            return $"A.P.: a0={FirstTerm:F2}, d={Difference:F2}, n={Length}, Sum={CalculateSum():F2}";
        }
    }


    class Result
    {
        // TODO: do it ! (Логіка реалізована в Program.Main)
    }

    class Program
    {
        /// <summary>
        /// Допоміжний метод для генерації списку прогресій із випадковими значеннями.
        /// </summary>
        /// <param name="count">Кількість прогресій.</param>
        /// <returns>Список об'єктів ArithmeticProgression.</returns>
        private static List<ArithmeticProgression> CreateRandomProgressions(int count)
        {
            var progressions = new List<ArithmeticProgression>();
            var random = new Random();

            for (int i = 0; i < count; i++)
            {
                // Генеруємо випадкові параметри
                double a0 = random.Next(-10, 11) + random.NextDouble();
                double d = random.Next(-5, 6) + random.NextDouble();
                int n = random.Next(3, 15); // Кількість членів від 3 до 14

                // Додаємо новий об'єкт
                progressions.Add(new ArithmeticProgression(a0, d, n));
            }

            // Додамо одну прогресію з великою сумою для гарантованого результату
            if (count > 0)
            {
                progressions[0] = new ArithmeticProgression(200.0, 5.0, 10); // S_10 = 2125.00
            }

            return progressions;
        }


        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            Console.WriteLine("--- 🚀 Лабораторна робота: Арифметична прогресія (Пошук Max Суми) ---");
            Console.WriteLine("-------------------------------------------------------------------");
            
            // 1. Створити масив/List заданого розміру
            const int progressionCount = 6;
            Console.WriteLine($"Створення списку з {progressionCount} арифметичних прогресій (випадкові дані):");
            
            // Заповнення списку екземплярами
            List<ArithmeticProgression> progressions = CreateRandomProgressions(progressionCount);
            
            Console.WriteLine("\nСтворені прогресії (a0, d, n та обчислена Sum):");
            int counter = 1;
            foreach (var p in progressions)
            {
                Console.WriteLine($"Прогресія #{counter++}: {p.ToString()}");
            }
            
            Console.WriteLine("\n-------------------------------------------------------------------");

            // 2. Знайти прогресію з максимальною сумою, порівнюючи значення Sum()
            
            // Використовуємо Linq OrderByDescending для .NET 5.0 або нижче.
            ArithmeticProgression maxSumProgression = progressions
                .OrderByDescending(p => p.CalculateSum()) // Сортуємо за спаданням результату методу CalculateSum()
                .FirstOrDefault();                         // Беремо перший (найбільший) елемент
            
            if (maxSumProgression != null)
            {
                Console.WriteLine("🏆 ЗНАЙДЕНО ПРОГРЕСІЮ З НАЙБІЛЬШОЮ СУМОЮ:");
                Console.WriteLine(maxSumProgression.ToString());
            }
            else
            {
                Console.WriteLine("Список прогресій порожній.");
            }
            
            Console.WriteLine("\n--- Завершення роботи програми ---");
        }
    }
}
