using System;

namespace LabWork
{
    class ArithmeticProgression
    {
        // --- Поля (приватні для інкапсуляції) ---
        private double firstTerm;
        private double difference;
        private int numberOfTerms;

        // --- Властивості ---
        public double FirstTerm
        {
            get => firstTerm;
            set => firstTerm = value;
        }

        public double Difference
        {
            get => difference;
            set => difference = value;
        }

        public int NumberOfTerms
        {
            get => numberOfTerms;
            set
            {
                if (value <= 0)
                    throw new ArgumentException("Number of terms must be > 0");
                numberOfTerms = value;
            }
        }

        // --- Конструктор ---
        public ArithmeticProgression(double a0, double d, int n)
        {
            FirstTerm = a0;
            Difference = d;
            NumberOfTerms = n;
        }

        // --- Метод для обчислення суми прогресії ---
        public double GetSum()
        {
            // S = n/2 * (2a0 + (n - 1)*d)
            return numberOfTerms / 2.0 * (2 * firstTerm + (numberOfTerms - 1) * difference);
        }

        // Для виводу
        public override string ToString()
        {
            return $"a0 = {firstTerm}, d = {difference}, n = {numberOfTerms}, sum = {GetSum()}";
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Console.Write("Enter number of progressions: ");
            int n = int.Parse(Console.ReadLine());

            ArithmeticProgression[] arr = new ArithmeticProgression[n];

            for (int i = 0; i < n; i++)
            {
                Console.WriteLine($"\nProgression {i + 1}:");

                Console.Write("a0 = ");
                double a0 = double.Parse(Console.ReadLine());

                Console.Write("d = ");
                double d = double.Parse(Console.ReadLine());

                Console.Write("n = ");
                int terms = int.Parse(Console.ReadLine());

                arr[i] = new ArithmeticProgression(a0, d, terms);
            }

            // Знаходження з максимальною сумою
            ArithmeticProgression maxProg = arr[0];

            foreach (var prog in arr)
            {
                if (prog.GetSum() > maxProg.GetSum())
                    maxProg = prog;
            }

            Console.WriteLine("\nProgression with MAX sum:");
            Console.WriteLine(maxProg);
        }
    }
}

