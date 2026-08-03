using System;

class Program
{
    // Módulo 2: Bubble Sort tradicional con contador
    static void OrdenarPorBurbuja(int[] arr)
    {
        int n = arr.Length;
        int contadorIntercambios = 0;

        for (int i = 0; i < n - 1; i++)
        {
            for (int j = 0; j < n - i - 1; j++)
            {
                if (arr[j] > arr[j + 1])
                {
                    // Intercambio con sintaxis de tuplas moderna de C#
                    (arr[j], arr[j + 1]) = (arr[j + 1], arr[j]);
                    contadorIntercambios++;
                }
            }
        }
        Console.WriteLine($"\nTotal de intercambios realizados: {contadorIntercambios}");
    }

    static void ImprimirArreglo(int[] arr)
    {
        Console.WriteLine(string.Join(", ", arr));
    }

    // Módulo 1: Inicialización aleatoria + Módulo 3: Manejo de excepciones
    static void Main(string[] args)
    {
        try
        {
            int[] calificaciones = new int[100];
            Random rng = new Random();
            for (int i = 0; i < calificaciones.Length; i++)
                calificaciones[i] = rng.Next(0, 101); // 0 a 100 inclusive

            Console.WriteLine("=== Estado inicial: calificaciones desordenadas ===");
            ImprimirArreglo(calificaciones);

            // Llamada al algoritmo de ordenamiento
            OrdenarPorBurbuja(calificaciones);

            Console.WriteLine("\n=== Estado final: calificaciones ordenadas (menor a mayor) ===");
            ImprimirArreglo(calificaciones);
        }
        catch (IndexOutOfRangeException ex)
        {
            Console.WriteLine($"[ERROR] Índice fuera de rango detectado: {ex.Message}");
            Console.WriteLine("Revisa los límites de tus ciclos for anidados.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"[ERROR inesperado]: {ex.Message}");
        }

        Console.WriteLine("\nPresiona cualquier tecla para salir...");
        Console.ReadKey();
    }
}
