using System;
using System.Linq;
using System.Collections.Generic;

namespace LabWork
{
    /// <summary>
    /// Представляє арифметичну прогресію з першим членом, різницею та кількістю членів.
    /// </summary>
    public class ArithmeticProgression
    {
        // Приватні поля для імутабельності
        private readonly double _firstTerm;
        private readonly double _difference;
        private readonly int _count;

        /// <summary>
        /// Перший член арифметичної прогресії (a₁).
        /// </summary>
        public double FirstTerm => _firstTerm;

        /// <summary>
        /// Різниця арифметичної прогресії (d).
        /// </summary>
        public double Difference => _difference;

        /// <summary>
        /// Кількість членів арифметичної прогресії (n).
        /// </summary>
        public int Count => _count;

        /// <summary>
        /// Ініціалізує новий екземпляр арифметичної прогресії.
        /// </summary>
        /// <param name="firstTerm">Перший член прогресії.</param>
        /// <param name="difference">Різниця прогресії.</param>
        /// <param name="count">Кількість членів прогресії (має бути > 0).</param>
        /// <exception cref="ArgumentOutOfRangeException">Викидається, якщо count ≤ 0.</exception>
        public ArithmeticProgression(double firstTerm, double difference, int count)
        {
            // Валідація вхідних даних
            if (count <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(count), "Кількість членів повинна бути більшою за нуль.");
            }

            // Перевірка на коректність числових значень
            if (double.IsInfinity(firstTerm) || double.IsNaN(firstTerm))
            {
                throw new ArgumentException("Перший член має бути дійсним числом.", nameof(firstTerm));
            }

            if (double.IsInfinity(difference) || double.IsNaN(difference))
            {
                throw new ArgumentException("Різниця має бути дійсним числом.", nameof(difference));
            }

            _firstTerm = firstTerm;
            _difference = difference;
            _count = count;
        }

        /// <summary>
        /// Обчислює n-ний член арифметичної прогресії.
        /// Формула: aₙ = a₁ + (n - 1) × d
        /// </summary>
        /// <param name="n">Порядковий номер члена (починаючи з 1).</param>
        /// <returns>Значення n-ного члена.</returns>
        /// <exception cref="ArgumentOutOfRangeException">Викидається, якщо n ≤ 0 або n > Count.</exception>
        public double GetNthTerm(int n)
        {
            if (n <= 0 || n > _count)
            {
                throw new ArgumentOutOfRangeException(nameof(n), "Номер члена повинен бути в межах від 1 до Count.");
            }

            return _firstTerm + (n - 1) * _difference;
        }

        /// <summary>
        /// Обчислює суму перших n членів арифметичної прогресії.
        /// Формула: Sₙ = n × (2 × a₁ + (n - 1) × d) / 2
        /// </summary>
        /// <returns>Сума прогресії.</returns>
        /// <exception cref="OverflowException">Викидається при переповненні під час обчислень.</exception>
        public double Sum()
        {
            try
            {
                // Перевірка на переповнення для проміжних обчислень
                checked
                {
                    // Обчислюємо 2 × a₁
                    double twoTimesFirst = 2 * _firstTerm;
                    
                    // Обчислюємо (n - 1) × d
                    double nMinusOneTimesDiff = (_count - 1) * _difference;
                    
                    // Обчислюємо чисельник: 2 × a₁ + (n - 1) × d
                    double numerator = twoTimesFirst + nMinusOneTimesDiff;
                    
                    // Обчислюємо добуток: n × чисельник
                    double product = _count * numerator;
                    
                    // Повертаємо результат ділення на 2
                    return product / 2;
                }
            }
            catch (OverflowException)
            {
                // Якщо виникло переповнення, намагаємося обчислити іншим способом
                return CalculateSumSafe();
            }
        }

        /// <summary>
        /// Безпечне обчислення суми для великих значень.
        /// Використовує формулу через середнє арифметичне: Sₙ = n × (a₁ + aₙ) / 2
        /// </summary>
        private double CalculateSumSafe()
        {
            // Використовуємо альтернативну формулу
            double lastTerm = GetNthTerm(_count); // aₙ
            double average = (_firstTerm + lastTerm) / 2;
            return _count * average;
        }

        /// <summary>
        /// Повертає рядкове представлення прогресії.
        /// </summary>
        public override string ToString()
        {
            return $"Арифметична прогресія: a₁={FirstTerm:F2}, d={Difference:F2}, n={Count}, S={Sum():F2}";
        }

        /// <summary>
        /// Повертає детальну інформацію про прогресію.
        /// </summary>
        public string GetDetailedInfo()
        {
            return $"Прогресія: a₁ = {FirstTerm:F4}, d = {Difference:F4}, n = {Count}\n" +
                   $"Останній член: aₙ = {GetNthTerm(Count):F4}\n" +
                   $"Сума: S = {Sum():F4}";
        }
    }

    /// <summary>
    /// Допоміжний клас для роботи з колекціями арифметичних прогресій.
    /// </summary>
    public static class ProgressionCollectionHelper
    {
        /// <summary>
        /// Знаходить прогресію з найбільшою сумою серед колекції.
        /// </summary>
        /// <param name="progressions">Колекція арифметичних прогресій.</param>
        /// <returns>Прогресія з максимальною сумою або null, якщо колекція порожня.</returns>
        public static ArithmeticProgression FindProgressionWithMaxSum(IEnumerable<ArithmeticProgression> progressions)
        {
            if (progressions == null)
                throw new ArgumentNullException(nameof(progressions));

            ArithmeticProgression maxProgression = null;
            double maxSum = double.MinValue;

            foreach (var progression in progressions)
            {
                double currentSum = progression.Sum();
                if (currentSum > maxSum)
                {
                    maxSum = currentSum;
                    maxProgression = progression;
                }
            }

            return maxProgression;
        }

        /// <summary>
        /// Обчислює загальну суму всіх прогресій у колекції.
        /// </summary>
        public static double CalculateTotalSum(IEnumerable<ArithmeticProgression> progressions)
        {
            if (progressions == null)
                throw new ArgumentNullException(nameof(progressions));

            double totalSum = 0;
            foreach (var progression in progressions)
            {
                totalSum += progression.Sum();
            }

            return totalSum;
        }
    }

    /// <summary>
    /// Головний клас програми.
    /// </summary>
    public static class Program
    {
        /// <summary>
        /// Точка входу в програму.
        /// </summary>
        public static void Main()
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            Console.WriteLine("=== Робота з арифметичними прогресіями ===");

            try
            {
                // Створення колекції прогресій
                var progressions = new List<ArithmeticProgression>
                {
                    new ArithmeticProgression(1, 2, 5),     // 1, 3, 5, 7, 9
                    new ArithmeticProgression(2.5, 1.5, 4), // 2.5, 4, 5.5, 7
                    new ArithmeticProgression(0, 3, 6),     // 0, 3, 6, 9, 12, 15
                    new ArithmeticProgression(-2, 4, 3),    // -2, 2, 6
                    new ArithmeticProgression(10, -2, 5)    // 10, 8, 6, 4, 2
                };

                // Виведення інформації про всі прогресії
                Console.WriteLine("\n📊 Створені арифметичні прогресії:");
                for (int i = 0; i < progressions.Count; i++)
                {
                    Console.WriteLine($"\nПрогресія #{i + 1}:");
                    Console.WriteLine(progressions[i].GetDetailedInfo());
                }

                // Пошук прогресії з найбільшою сумою
                var maxSumProgression = ProgressionCollectionHelper.FindProgressionWithMaxSum(progressions);
                
                if (maxSumProgression != null)
                {
                    Console.WriteLine("\n🏆 Прогресія з найбільшою сумою:");
                    Console.WriteLine(maxSumProgression.GetDetailedInfo());
                }

                // Обчислення загальної суми
                double totalSum = ProgressionCollectionHelper.CalculateTotalSum(progressions);
                Console.WriteLine($"\n📈 Загальна сума всіх прогресій: {totalSum:F4}");

                // Тестування валідації
                Console.WriteLine("\n🔍 Тестування валідації:");
                try
                {
                    // Це має викликати помилку
                    var invalid = new ArithmeticProgression(1, 2, 0);
                }
                catch (ArgumentOutOfRangeException ex)
                {
                    Console.WriteLine($"Очікувана помилка валідації: {ex.Message}");
                }

                // Тестування великих значень для перевірки переповнення
                Console.WriteLine("\n🧪 Тестування обробки великих значень:");
                try
                {
                    var largeProgression = new ArithmeticProgression(1e100, 1e100, 1000);
                    Console.WriteLine($"Сума великої прогресії: {largeProgression.Sum():E}");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Помилка при роботі з великими значеннями: {ex.Message}");
                }

                // Демонстрація роботи з окремими членами
                Console.WriteLine("\n🔢 Демонстрація роботи з окремими членами:");
                var demoProgression = new ArithmeticProgression(2, 3, 5);
                Console.WriteLine($"Прогресія: {demoProgression}");
                Console.WriteLine("Члени прогресії:");
                for (int i = 1; i <= demoProgression.Count; i++)
                {
                    Console.WriteLine($"  a{i} = {demoProgression.GetNthTerm(i):F2}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"\n❌ Неочікувана помилка: {ex.Message}");
            }
            finally
            {
                Console.WriteLine("\n=== Роботу завершено ===");
            }
        }
    }
}
