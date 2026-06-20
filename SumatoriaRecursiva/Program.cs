using System;

class Program
{
    // Función recursiva para calcular la sumatoria
    static int Sumatoria(int n)
    {
        // Caso base
        if (n <= 0)
            return 0;

        // Llamada recursiva: n + suma de todos los anteriores
        return n + Sumatoria(n - 1);
    }

    static void Main(string[] args)
    {
        Console.WriteLine("=== Sumatoria Recursiva con Validación ===\n");

        int numero = 0;
        bool entradaValida = false;

        // Validación profesional: ciclo hasta que el usuario ingrese un valor correcto
        while (!entradaValida)
        {
            Console.Write("Ingresa un número entero positivo: ");
            string entrada = Console.ReadLine();

            if (!int.TryParse(entrada, out numero))
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("❌ Error: Debes ingresar un número entero válido.");
                Console.ResetColor();
            }
            else if (numero <= 0)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("❌ Error: El número debe ser mayor que cero.");
                Console.ResetColor();
            }
            else
            {
                entradaValida = true;
            }
        }

        int resultado = Sumatoria(numero);

        Console.WriteLine($"\nLa sumatoria de 1 hasta {numero} es: {resultado}");
        Console.WriteLine($"Es decir: 1 + 2 + ... + {numero} = {resultado}");

        Console.WriteLine("\nPresiona cualquier tecla para salir...");
        Console.ReadKey();
    }
}
