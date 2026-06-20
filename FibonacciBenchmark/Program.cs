using System;
using System.Diagnostics;

class Program
{
    // Módulo A: Fibonacci Recursivo Tradicional (Fuerza Bruta)
    public static long FibonacciInseguro(int n)
    {
        if (n == 0) return 0;
        if (n == 1) return 1;
        return FibonacciInseguro(n - 1) + FibonacciInseguro(n - 2);
    }

    // Módulo B: Fibonacci con Memoization (Estrategia Pro)
    public static long FibonacciPro(int n, long[] cache)
    {
        if (n == 0) return 0;
        if (n == 1) return 1;

        // ¿Ya lo calculamos antes?
        if (cache[n] != -1)
            return cache[n]; // Retorno inmediato

        // Calcular, guardar y retornar
        cache[n] = FibonacciPro(n - 1, cache) + FibonacciPro(n - 2, cache);
        return cache[n];
    }

    // Módulo C: Banco de Pruebas con Stopwatch
    static void Main(string[] args)
    {
        Console.WriteLine("=== Fibonacci: Fuerza Bruta vs Memoization ===\n");

        Console.Write("Ingresa un número (35-43): ");
        string input = Console.ReadLine();

        // Validación de entrada
        if (!int.TryParse(input, out int n) || n < 0)
        {
            Console.WriteLine("Error: ingresa un número positivo.");
            return;
        }

        Stopwatch sw = new Stopwatch();

        // --- Método Inseguro ---
        Console.WriteLine("\n⏳ Calculando con Fuerza Bruta...");
        sw.Restart();
        long r1 = FibonacciInseguro(n);
        sw.Stop();
        Console.WriteLine($"Inseguro: F({n}) = {r1}");
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine($"Tiempo:   {sw.ElapsedMilliseconds} ms");
        Console.ResetColor();

        // --- Método Pro ---
        long[] cache = new long[n + 1];
        for (int i = 0; i <= n; i++)
            cache[i] = -1;

        Console.WriteLine("\n⚡ Calculando con Memoization...");
        sw.Restart();
        long r2 = FibonacciPro(n, cache);
        sw.Stop();
        Console.WriteLine($"Pro:      F({n}) = {r2}");
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine($"Tiempo:   {sw.ElapsedMilliseconds} ms");
        Console.ResetColor();

        Console.WriteLine("\n📌 Conclusión: Memoization evita recalcular");
        Console.WriteLine("   los mismos valores, reduciendo el tiempo");
        Console.WriteLine("   de O(2^n) a O(n).");

        Console.WriteLine("\nPresiona cualquier tecla para salir...");
        Console.ReadKey();
    }
}
