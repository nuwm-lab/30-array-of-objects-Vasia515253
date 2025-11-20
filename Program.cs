using System;

namespace LabWork
{
    class ArithmeticProgression
    {
        // Поля закриті — інкапсуляція
        private double a0;
        private double d;
        private int n;

        public double A0
        {
            get { return a0; }
            private set { a0 = value; }
        }

        public double D
        {
            get { return d; }
            private set { d = value; }
        }

        public int N
        {
            get { return n; }
            private set 
            { 
                if (value <= 0)
                    throw new ArgumentException("Кількість членів має бути > 0");
                n = value; 
            }
        }

        // Конструктор
        public ArithmeticProgression(double a0, double d, int n)
        {
            A0 = a0;
            D = d;
            N = n;
        }

        // Метод для суми прогресії
        public double GetSum()
        {
            return (2 * a0 + d * (n - 1)) * n / 2.0;
        }

        public override string ToString()
        {
            return $"a0={a0}, d={d}, n={n}, Sum={GetSum()}";
        }
    }

    class Result
    {
        public void Run()
        {
            Console.Write("Введіть кількість прогресій: ");
            int count = int.Parse(Console.ReadLine());

            ArithmeticProgression[] arr = new ArithmeticProgression[count];

            for (int i = 0; i < count; i++)
            {
                Console.WriteLine($"\nПрогресія #{i + 1}");

                Console.Write("Введіть a0: ");
                double a0 = double.Parse(Console.ReadLine());

                Console.Write("Введіть d: ");
                double d = double.Parse(Console.ReadLine());

                Console.Write("Введіть n: ");
                int n = int.Parse(Console.ReadLine());

                arr[i] = new ArithmeticProgression(a0, d, n);
            }

            // Пошук прогресії з найбільшою сумою
            ArithmeticProgression maxProg = arr[0];

            for (int i = 1; i < arr.Length; i++)
            {
                if (arr[i].GetSum() > maxProg.GetSum())
                {
                    maxProg = arr[i];
                }
            }

            Console.WriteLine("\nПрогресія з найбільшою сумою:");
            Console.WriteLine(maxProg);
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Result result = new Result();
            result.Run();

            Console.WriteLine("\nНатисніть будь-яку клавішу для виходу...");
            Console.ReadKey();
        }
    }
}
