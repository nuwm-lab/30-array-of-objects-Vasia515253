using System;
using System.Linq;

public class ArithmeticProgression
{
    public double FirstTerm { get; }
    public double Difference { get; }
    public int Count { get; }

    public ArithmeticProgression(double firstTerm, double difference, int count)
    {
        if (count <= 0)
            throw new ArgumentException("Count must be positive.", nameof(count));

        FirstTerm = firstTerm;
        Difference = difference;
        Count = count;
    }

    public double Sum()
    {
        // Формула: S = n*(2*a0 + (n-1)*d)/2
        return Count * (2 * FirstTerm + (Count - 1) * Difference) / 2.0;
    }

    public override string ToString()
    {
        return $"a0 = {FirstTerm}, d = {Difference}, n = {Count}, S = {Sum()}";
    }
}

public class Program
{
    public static void Main()
    {
        Console.Write("Введіть кількість прогресій: ");
        int n = int.Parse(Console.ReadLine());

        var progressions = new ArithmeticProgression[n];

        for (int i = 0; i < n; i++)
        {
            Console.WriteLine($"\nПрогресія №{i + 1}");

            Console.Write("Перший член (a0): ");
            double a0 = double.Parse(Console.ReadLine());

            Console.Write("Різниця (d): ");
            double d = double.Parse(Console.ReadLine());

            Console.Write("Кількість членів (n): ");
            int count = int.Parse(Console.ReadLine());

            progressions[i] = new ArithmeticProgression(a0, d, count);
        }

        // Пошук за максимальною сумою
        var maxProgression = progressions
            .OrderByDescending(p => p.Sum())
            .First();

        Console.WriteLine("\nПрогресія з найбільшою сумою:");
        Console.WriteLine(maxProgression);
    }
}
