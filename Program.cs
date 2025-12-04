using System;
using System.Linq;

namespace LabWork
{
    /// <summary>
    /// Клас, що представляє арифметичну прогресію.
    /// Характеризується першим членом (a0), різницею (d) та кількістю членів (n).
    /// </summary>
    public class ArithmeticProgression
    {
        // Приватні поля для зберігання стану об'єкта
        private readonly double _firstTerm; // a0
        private readonly double _difference; // d
        private readonly int _numberOfTerms; // n

        /// <summary>
        /// Конструктор для ініціалізації арифметичної прогресії.
        /// </summary>
        /// <param name="a0">Перший член прогресії.</param>
        /// <param name="d">Різниця прогресії.</param>
        /// <param name="n">Кількість членів прогресії (має бути > 0).</param>
        public ArithmeticProgression(double a0, double d, int n)
        {
            if (n <= 0)
            {
                throw new ArgumentException("Кількість членів (n) повинна бути більшою за нуль.", nameof(n));
            }

            _firstTerm = a0;
            _difference = d;
            _numberOfTerms = n;
        }

        // Властивості лише для читання, що забезпечують інкапсуляцію
        public double FirstTerm => _firstTerm;
        public double Difference => _difference;
        public int NumberOfTerms => _numberOfTerms;

        /// <summary>
        /// Обчислює суму арифметичної прогресії за формулою:
        /// S_n = n/2 * (2*a0 + (n-1)*d)
        /// </summary>
        /// <returns>Сума n членів прогресії.</returns>
        public double CalculateSum()
        {
            // Формула для n-го члена: a_n = a0 + (n-1) * d
            // Формула для суми: S_n = n/2 * (a0 + a_n)
            // Або: S_n = n/2 * (2*a0 + (n-1)*d)
            double sum = (double)_numberOfTerms / 2 * (2 * _firstTerm + (_numberOfTerms - 1) * _difference);
            return sum;
        }

        /// <summary>
        /// Повертає текстове представлення об'єкта.
        /// </summary>
        /// <returns>Рядок із параметрами прогресії та її сумою.</returns>
        public override string ToString()
        {
            return $"A.P.: a0={_firstTerm}, d={_difference}, n={_numberOfTerms}, Sum={CalculateSum():F2}";
        }
    }
    
    // Результат - тепер цей клас містить логіку знаходження найбільшої суми.
    class Result
    {
        /// <summary>
        /// Створює масив об'єктів ArithmeticProgression з випадковими або заданими параметрами.
        /// </summary>
        /// <param name="count">Кількість прогресій для створення.</param>
        /// <returns>Масив об'єктів ArithmeticProgression.</returns>
        public static ArithmeticProgression[] CreateProgressionsArray(int count)
        {
            // Це лише приклад для демонстрації.
            // У реальній лабораторній роботі ви можете читати дані з файлу або вводу користувача.
            
            var progressions = new ArithmeticProgression[count];
            var random = new Random();

            for (int i = 0; i < count; i++)
            {
                // Генеруємо випадкові параметри
                double a0 = random.Next(-10, 10) + random.NextDouble();
                double d = random.Next(-5, 5) + random.NextDouble();
                int n = random.Next(3, 15); // Кількість членів від 3 до 14

                progressions[i] = new ArithmeticProgression(a0, d, n);
            }

            // Додамо одну прогресію з великою сумою для гарантованого результату
            if (count > 0)
            {
                progressions[0] = new ArithmeticProgression(100, 10, 10); // a0=100, d=10, n=10. S_10 = 10/2 * (2*100 + 9*10) = 5 * (200 + 90) = 1450
            }


            return progressions;
        }

        /// <summary>
        /// Знаходить прогресію з найбільшою сумою в масиві.
        /// </summary>
        /// <param name="progressions">Масив арифметичних прогресій.</param>
        /// <returns>Об'єкт ArithmeticProgression з найбільшою сумою, або null, якщо масив порожній.</returns>
        public static ArithmeticProgression FindProgressionWithMaxSum(ArithmeticProgression[] progressions)
        {
            if (progressions == null || progressions.Length == 0)
            {
                return null;
            }
            
            // Використовуємо Linq для знаходження елемента з максимальним значенням функції (CalculateSum)
            // Це ефективний та лаконічний спосіб
            ArithmeticProgression maxSumProgression = progressions
                .OrderByDescending(p => p.CalculateSum())
                .FirstOrDefault();

            return maxSumProgression;

            /* Альтернативний (класичний) спосіб без Linq:
            
            ArithmeticProgression maxSumProgression = progressions[0];
            double maxSum = maxSumProgression.CalculateSum();

            for (int i = 1; i < progressions.Length; i++)
            {
                double currentSum = progressions[i].CalculateSum();
                if (currentSum > maxSum)
                {
                    maxSum = currentSum;
                    maxSumProgression = progressions[i];
                }
            }
            return maxSumProgression;
            */
        }
    }
    
    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            Console.WriteLine("🚀 Запуск лабораторної роботи: Арифметична прогресія");
            Console.WriteLine("---");

            // 1. Створити масив з n об’єктів класу "Арифметична прогресія".
            int n = 5; // Кількість прогресій у масиві
            ArithmeticProgression[] progressions = Result.CreateProgressionsArray(n);
            
            Console.WriteLine($"Створено {n} арифметичних прогресій:");
            
            int counter = 1;
            foreach (var p in progressions)
            {
                Console.WriteLine($"Прогресія #{counter++}: {p.ToString()}");
            }
            
            Console.WriteLine("---");

            // 2. Знайти прогресію з найбільшою сумою.
            ArithmeticProgression maxSumProgression = Result.FindProgressionWithMaxSum(progressions);

            if (maxSumProgression != null)
            {
                Console.WriteLine("🏆 Прогресія з найбільшою сумою:");
                Console.WriteLine(maxSumProgression.ToString());
            }
            else
            {
                Console.WriteLine("Масив прогресій порожній.");
            }
            
            Console.WriteLine("---");
            Console.WriteLine("Завершення програми.");
        }
    }
}
