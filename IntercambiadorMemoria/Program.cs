using System;

// Clase para el Módulo 3
public class Alumno
{
    public string Nombre { get; set; }

    public Alumno(string nombre)
    {
        Nombre = nombre;
    }
}

class Program
{
    // Módulo 1: Intercambiar dos enteros por referencia SIN variable auxiliar
    static void Intercambiar(ref int a, ref int b)
    {
        a = a + b;
        b = a - b;
        a = a - b;
    }

    // Módulo 2: Devuelve cociente y residuo con out
    static int CalcularYValidar(int dividendo, int divisor, out int residuo)
    {
        if (divisor == 0)
        {
            residuo = 0;
            Console.WriteLine("Error: No se puede dividir entre cero.");
            return 0;
        }

        residuo = dividendo % divisor;
        return dividendo / divisor;
    }

    static void Main(string[] args)
    {
        Console.WriteLine("=== El Intercambiador de Memoria ===\n");

        // --- Módulo 1: Intercambiar ---
        Console.WriteLine("--- Módulo 1: Intercambiar con ref ---");
        int x = 10, y = 20;
        Console.WriteLine($"Antes:  x = {x}, y = {y}");
        Intercambiar(ref x, ref y);
        Console.WriteLine($"Después: x = {x}, y = {y}");

        Console.WriteLine();

        // --- Módulo 2: Calcular y Validar ---
        Console.WriteLine("--- Módulo 2: CalcularYValidar con out ---");
        int residuo;
        int cociente = CalcularYValidar(17, 5, out residuo);
        Console.WriteLine($"17 / 5 = {cociente} con residuo {residuo}");

        // Prueba con divisor 0
        int residuo2;
        CalcularYValidar(10, 0, out residuo2);

        Console.WriteLine();

        // --- Módulo 3: Referencias de Objetos ---
        Console.WriteLine("--- Módulo 3: Demostración de Referencias ---");
        Alumno alumno1 = new Alumno("Dany");
        Console.WriteLine($"alumno1 antes: {alumno1.Nombre}");

        // alumno2 apunta al MISMO objeto en memoria
        Alumno alumno2 = alumno1;
        alumno2.Nombre = "3Treum";

        Console.WriteLine($"alumno2 después: {alumno2.Nombre}");
        Console.WriteLine($"alumno1 también cambió: {alumno1.Nombre}");
        Console.WriteLine("\n¿Por qué? Porque alumno1 y alumno2 apuntan");
        Console.WriteLine("al MISMO objeto en memoria. No son copias,");
        Console.WriteLine("son dos variables que referencian el mismo espacio.");

        Console.WriteLine("\nPresiona cualquier tecla para salir...");
        Console.ReadKey();
    }
}
