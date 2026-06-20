using System;
using System.Numerics;

class Program
{
    // Parte B: Factorial con BigInteger (precisión arbitraria)
    static BigInteger FactorialProfesional(BigInteger n)
    {
        // Caso Base
        if (n == 0 || n == 1)
            return BigInteger.One;

        // Caso Recursivo
        return n * FactorialProfesional(n - 1);
    }

    static void Main(string[] args)
    {
        Console.WriteLine("=== Factorial Profesional con BigInteger ===\n");

        // Prueba con valores pequeños para comparar
        Console.WriteLine("Verificación con valores conocidos:");
        Console.WriteLine($"5!  = {FactorialProfesional(5)}");
        Console.WriteLine($"10! = {FactorialProfesional(10)}");
        Console.WriteLine($"13! = {FactorialProfesional(13)} (aquí int fallaba)");
        Console.WriteLine($"20! = {FactorialProfesional(20)} (aquí long fallaba)");

        // Prueba principal con n=100
        Console.WriteLine("\n📌 Prueba con n=100:");
        BigInteger resultado = FactorialProfesional(100);
        Console.WriteLine($"100! = {resultado}");

        Console.WriteLine("\n✅ BigInteger no tiene desbordamiento.");
        Console.WriteLine("   Crece dinámicamente en el Heap según lo necesite.");

        Console.WriteLine("\nPresiona cualquier tecla para salir...");
        Console.ReadKey();
    }
}

