using System;

class Program
{
    // Función recursiva que visualiza el Call Stack LIFO
    static void ImprimirCuentaRegresiva(int numero)
    {
        // Caso base
        if (numero < 1)
        {
            Console.WriteLine("🏁 Fondo del Stack alcanzado!\n");
            return;
        }

        // FASE DE APILADO
        Console.WriteLine($"⬆️  APILANDO  - Marco {numero} en el Stack");

        // Llamada recursiva
        ImprimirCuentaRegresiva(numero - 1);

        // FASE DE RETORNO (LIFO: último en entrar, primero en salir)
        Console.WriteLine($"⬇️  LIBERANDO - Marco {numero} del Stack");
    }

    static void Main(string[] args)
    {
        Console.WriteLine("=== Cuenta Regresiva de Memoria (LIFO) ===\n");

        Console.Write("¿Desde qué número quieres la cuenta regresiva? ");
        int numero = int.Parse(Console.ReadLine());

        Console.WriteLine();
        ImprimirCuentaRegresiva(numero);

        Console.WriteLine("\n✅ El Stack quedó vacío.");
        Console.WriteLine("\nPresiona cualquier tecla para salir...");
        Console.ReadKey();
    }
}
