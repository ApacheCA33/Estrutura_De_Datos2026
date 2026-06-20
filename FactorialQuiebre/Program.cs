using System;

class Program
{
    // Función Recursiva
    static int FactorialInt(int n)
    {
        if (n == 0 || n == 1)
            return 1;
        return n * FactorialInt(n - 1);
    }

    // Función Iterativa
    static int FactorialIterativo(int n)
    {
        int resultado = 1;
        for (int i = 2; i <= n; i++)
            resultado *= i;
        return resultado;
    }

    static void Main(string[] args)
    {
        Console.WriteLine("=== Factorial: Recursivo vs Iterativo ===\n");
        Console.WriteLine($"{"n",-5} | {"Recursivo",25} | {"Iterativo",25}");
        Console.WriteLine(new string('-', 62));

        for (int i = 1; i <= 20; i++)
        {
            int recursivo   = FactorialInt(i);
            int iterativo   = FactorialIterativo(i);

            // Punto de quiebre: a partir de n=13 el tipo int (32 bits)
            // ya no puede almacenar el resultado y produce valores negativos
            // o incorrectos por desbordamiento (overflow).
            // n=13 produce: 1932053504 (incorrecto, el valor real es 6227020800)
            // n=14 en adelante produce valores negativos o incorrectos.

            if (recursivo < 0 || iterativo < 0)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"n={i:D2} | {recursivo,25} | {iterativo,25} ⚠️ OVERFLOW");
                Console.ResetColor();
            }
            else
            {
                Console.WriteLine($"n={i:D2} | {recursivo,25} | {iterativo,25}");
            }
        }

        Console.WriteLine("\n📌 Conclusión: int (32 bits) se desborda a partir de n=13.");
        Console.WriteLine("   Solución: usar 'long' (64 bits) para soportar hasta n=20.");

        Console.WriteLine("\nPresiona cualquier tecla para salir...");
        Console.ReadKey();
    }
}

