using System;

namespace LabWork
{
    // Клас для геометричної прогресії
    class GeometricProgression
    {
        private double a0; // перший член
        private double q;  // знаменник
        private int n;     // кількість членів

        public GeometricProgression(double a0, double q, int n)
        {
            this.a0 = a0;
            this.q = q;
            this.n = n;
        }

        // Властивості (інкапсуляція)
        public double A0 => a0;
        public double Q => q;
        public int N => n;

        // Метод для знаходження останнього члена прогресії
        public double LastTerm()
        {
            return a0 * Math.Pow(q, n - 1);
        }

        // Метод для знаходження суми прогресії
        public double Sum()
        {
            if (q == 1)
                return a0 * n;
            else
                return a0 * (Math.Pow(q, n) - 1) / (q - 1);
        }

        // Метод перевіряє, чи є останній член найбільшим у прогресії
        public bool HasLargestLastTerm()
        {
            if (q > 1 && a0 > 0) return true;   // зростаюча прогресія
            if (q < 1 && a0 < 0) return true;   // спадна з від’ємними членами
            return false;
        }

        // Метод для гарного виведення інформації
        public override string ToString()
        {
            return $"a₀ = {a0}, q = {q}, n = {n}, Last = {LastTerm():F2}";
        }
    }

    class Result
    {
        public static void Run()
        {
            Console.Write("Введіть кількість прогресій n: ");
            int n = int.Parse(Console.ReadLine());

            GeometricProgression[] arr = new GeometricProgression[n];

            for (int i = 0; i < n; i++)
            {
                Console.WriteLine($"\nПрогресія #{i + 1}:");
                Console.Write("  Введіть a₀: ");
                double a0 = double.Parse(Console.ReadLine());
                Console.Write("  Введіть q: ");
                double q = double.Parse(Console.ReadLine());
                Console.Write("  Введіть n: ");
                int count = int.Parse(Console.ReadLine());

                arr[i] = new GeometricProgression(a0, q, count);
            }

            // Знаходимо прогресію з найбільшим останнім членом
            GeometricProgression maxProg = arr[0];
            foreach (var gp in arr)
            {
                if (gp.LastTerm() > maxProg.LastTerm())
                    maxProg = gp;
            }

            Console.WriteLine("\n--- Результати ---");
            for (int i = 0; i < arr.Length; i++)
            {
                Console.WriteLine($"Прогресія #{i + 1}: {arr[i]}");
                Console.WriteLine($"  Чи найбільший останній член: {arr[i].HasLargestLastTerm()}");
            }

            Console.WriteLine($"\nПрогресія з найбільшим останнім членом:\n{maxProg}");
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Result.Run();
            Console.WriteLine("\nНатисніть будь-яку клавішу для виходу...");
            Console.ReadKey();
        }
    }
}

