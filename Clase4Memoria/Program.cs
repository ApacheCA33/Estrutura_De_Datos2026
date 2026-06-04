using System;

class Program
{
    // Función recursiva para calcular el factorial
    static int Factorial(int n)
    {
        if (n < 0)
            throw new ArgumentException("El número no puede ser negativo.");

        if (n == 0 || n == 1)
        {
            Console.WriteLine("Factorial(1) = 1");
            return 1;
        }

        int resultado = n * Factorial(n - 1);
        Console.WriteLine($"Factorial({n}) = {resultado}");
        return resultado;
    }

    static void Main(string[] args)
    {
        Console.Write("Ingresa un número entero positivo: ");
        int numero = int.Parse(Console.ReadLine());

        try
        {
            int resultado = Factorial(numero);
            Console.WriteLine($"\n{numero}! = {resultado}");
        }
        catch (ArgumentException e)
        {
            Console.WriteLine("Error: " + e.Message);
        }
    }
}