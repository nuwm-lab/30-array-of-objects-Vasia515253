using System;

namespace LabWork
{
    public class ArithmeticProgression
    {
        private double _firstTerm;
        private double _difference;
        private int _count;

        public double FirstTerm
        {
            get => _firstTerm;
            set => _firstTerm = value;
        }

        public double Difference
        {
            get => _difference;
            set => _difference = value;
        }

        public int Count
        {
            get => _count;
            set
            {
                if (value <= 0)
                {
                    throw new ArgumentException("Count (n) must be greater than 0.");
                }
                _count = value;
            }
        }

        public ArithmeticProgression(double firstTerm, double difference, int count)
        {
            FirstTerm = firstTerm;
            Difference = difference;
            Count = count;
        }

        public double Sum()
        {
            // Формула: S = n * (2*a0 + (n - 1) * d) / 2
            return Count * (2 * FirstTerm + (Count - 1) * Difference) / 2.0;
        }

        public override string ToString()
        {
            return $"a0 = {FirstTerm}, d = {Difference}, n = {Count}, Sum = {Sum()}";
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Console.Write("Enter number of progressions: ");
            int size = int.Parse(Console.ReadLine());

            ArithmeticProgression[] arr = new ArithmeticProgression[size];

            for (int i = 0; i < size; i++)
            {
                Console.WriteLine($"\nProgression #{i + 1}:");

                Console.Write("Enter first term (a0): ");
                double a0 = double.Parse(Console.ReadLine());

                Console.Write("Enter difference (d): ");
                double d = double.Parse(Console.ReadLine());

                Console.Write("Enter number of terms (n): ");
                int n = int.Parse(Console.ReadLine());

                arr[i] = new ArithmeticProgression(a0, d, n);
            }

            // Пошук прогресії з максимальною сумою
            int maxIndex = 0;
            double maxSum = arr[0].Sum();

            for (int i = 1; i < size; i++)
            {
                double currentSum = arr[i].Sum();
                if (currentSum > maxSum)
                {
                    maxSum = currentSum;
                    maxIndex = i;
                }
            }

            Console.WriteLine("\nProgression with the largest sum:");
            Console.WriteLine(arr[maxIndex]);
        }
    }
}

