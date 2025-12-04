using System;
using System.Linq;
using System.Collections.Generic;

namespace LabWork
{
    /// <summary>
    /// Клас, що представляє Арифметичну Прогресію.
    /// Характеризується першим членом (First), різницею (Difference) та кількістю членів (Count).
    /// </summary>
    public class ArithmeticProgression
    {
        // Приватні поля з модифікатором readonly для імутабельності
        private readonly double _first;
        private readonly double _difference;
        private readonly int _count;

        /// <summary>
        /// Перший член прогресії (a₀).
        /// </summary>
        public double First => _first;

        /// <summary>
        /// Різниця прогресії (d).
        /// </summary>
        public double Difference => _difference;

        /// <summary>
        /// Кількість членів прогресії (n).
        /// </summary>
        public int Count => _count;

        /// <summary>
        /// Конструктор для ініціалізації арифметичної прогресії.
        /// </summary>
        /// <param name="first">Перший член прогресії (a₀).</param>
        /// <param name="difference">Різниця прогресії (d).</param>
        /// <param name="count">Кількість членів прогресії (n). Має бути > 0.</param>
        /// <exception cref="ArgumentException">Викидається, якщо count ≤ 0.</exception>
        public ArithmeticProgression(double first, double difference, int count)
        {
            // Валідація вхідних даних
            if (count <= 0)
            {
                throw new ArgumentException("Кількість членів повинна бути більшою за нуль.", nameof(count));
            }

            if (double.IsInfinity(first) || double.IsNaN(first))
            {
                throw new ArgumentException("Перший член має бути дійсним числом.", nameof(first));
            }

            if (double.IsInfinity(difference) || double.IsNaN(difference))
            {
                throw new ArgumentException("Різниця має бути дійсним числом.", nameof(difference));
            }

            _first = first;
            _difference = difference;
            _count = count;
        }

        /// <summary>
        /// Обчислює суму арифметичної прогресії.
        /// Формула: S = n × (2 × a₀ + (n - 1) × d) / 2
        /// </summary>
        /// <returns>Сума n членів прогресії.</returns>
        public double Sum()
        {
            // Формула суми арифметичної прогресії
            return _count * (2 * _first + (_count - 1) * _difference) / 2;
        }

        /// <summary>
        /// Повертає рядкове представлення прогресії.
        /// </summary>
        /// <returns>Рядок із параметрами прогресії та обчисленою сумою.</returns>
        public override string ToString()
        {
            return $"Арифметична прогресія: a₀={First:F2}, d={Difference:F2}, n={Count}, S={Sum():F2}";
        }
    }

    /// <summary>
    /// Точка входу в програму для роботи з арифметичними прогресіями.
    /// </summary>
    public static class Program
    {
        /// <summary>
        /// Безпечно зчитує число типу double з консолі.
        /// </summary>
        /// <param name="prompt">Повідомлення для користувача.</param>
        /// <returns>Коректне значення double.</returns>
        private static double ReadDouble(string prompt)
        {
            Console.Write(prompt);
            
            while (true)
            {
                if (double.TryParse(Console.ReadLine(), out double value) && 
                    !double.IsInfinity(value) && 
                    !double.IsNaN(value))
                {
                    return value;
                }
                
                Console.WriteLine("Помилка вводу. Будь ласка, введіть дійсне число.");
                Console.Write(prompt);
            }
        }

        /// <summary>
        /// Безпечно зчитує додатне ціле число з консолі.
        /// </summary>
        /// <param name="prompt">Повідомлення для користувача.</param>
        /// <returns>Коректне додатне значення int.</returns>
        private static int ReadPositiveInt(string prompt)
        {
            Console.Write(prompt);
            
            while (true)
            {
                if (int.TryParse(Console.ReadLine(), out int value) && value > 0)
                {
                    return value;
                }
                
                Console.WriteLine("Помилка вводу. Будь ласка, введіть ціле число, більше за 0.");
                Console.Write(prompt);
            }
        }

        /// <summary>
        /// Створює колекцію арифметичних прогресій на основі введення користувача.
        /// </summary>
        /// <param name="count">Кількість прогресій для створення.</param>
        /// <returns>Колекція створених прогресій.</returns>
        private static List<ArithmeticProgression> CreateProgressions(int count)
        {
            var progressions = new List<ArithmeticProgression>();
            
            for (int i = 0; i < count; i++)
            {
                Console.WriteLine($"\n--- Введення параметрів для прогресії #{i + 1} ---");
                
                try
                {
                    double first = ReadDouble("Введіть перший член (a₀): ");
                    double difference = ReadDouble("Введіть різницю (d): ");
                    int memberCount = ReadPositiveInt("Введіть кількість членів (n > 0): ");
                    
                    progressions.Add(new ArithmeticProgression(first, difference, memberCount));
                    Console.WriteLine($"Прогресія #{i + 1} успішно створена.");
                }
                catch (ArgumentException ex)
                {
                    Console.WriteLine($"Помилка створення прогресії: {ex.Message}");
                    Console.WriteLine("Спробуйте ще раз.");
                    i--; // Повторюємо цю ітерацію
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Неочікувана помилка: {ex.Message}");
                    Console.WriteLine("Спробуйте ще раз.");
                    i--;
                }
            }
            
            return progressions;
        }

        /// <summary>
        /// Знаходить прогресію з найбільшою сумою.
        /// </summary>
        /// <param name="progressions">Колекція прогресій для пошуку.</param>
        /// <returns>Прогресія з максимальною сумою або null, якщо колекція порожня.</returns>
        private static ArithmeticProgression FindProgressionWithMaxSum(IEnumerable<ArithmeticProgression> progressions)
        {
            if (progressions == null || !progressions.Any())
                return null;

            // Використання агрегації для пошуку максимального елемента
            return progressions.Aggregate((max, current) => 
                current.Sum() > max.Sum() ? current : max);
        }

        /// <summary>
        /// Головний метод програми.
        /// </summary>
        public static void Main()
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            Console.WriteLine("=== Лабораторна робота: Арифметичні прогресії ===");
            Console.WriteLine("Ціль: знайти прогресію з найбільшою сумою серед заданих.");
            
            try
            {
                // 1. Зчитування кількості прогресій
                int progressionCount = ReadPositiveInt("\nВведіть кількість прогресій для аналізу: ");
                
                // 2. Створення колекції прогресій
                var progressions = CreateProgressions(progressionCount);
                
                if (progressions.Count == 0)
                {
                    Console.WriteLine("\nНе створено жодної прогресії для аналізу.");
                    return;
                }
                
                // 3. Пошук прогресії з максимальною сумою
                var maxSumProgression = FindProgressionWithMaxSum(progressions);
                
                // 4. Виведення результатів
                Console.WriteLine("\n=== РЕЗУЛЬТАТИ ===");
                Console.WriteLine($"Загальна кількість створених прогресій: {progressions.Count}");
                
                Console.WriteLine("\nВсі прогресії:");
                foreach (var progression in progressions)
                {
                    Console.WriteLine($"  • {progression}");
                }
                
                if (maxSumProgression != null)
                {
                    Console.WriteLine("\n🎯 ПРОГРЕСІЯ З НАЙБІЛЬШОЮ СУМОЮ:");
                    Console.WriteLine($"   {maxSumProgression}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"\n❌ Сталася неочікувана помилка: {ex.Message}");
                Console.WriteLine("Програма завершує роботу.");
            }
            finally
            {
                Console.WriteLine("\n=== Завершення роботи програми ===");
                Console.WriteLine("Натисніть будь-яку клавішу для виходу...");
                Console.ReadKey();
            }
        }
    }
}
