using System;

// Struct para representar una transacción financiera
struct Transaccion
{
    public int Id;
    public double Monto;
    public long Timestamp;

    public Transaccion(int id, double monto, long timestamp)
    {
        Id = id;
        Monto = monto;
        Timestamp = timestamp;
    }

    public override string ToString()
    {
        return $"ID: {Id,4} | Monto: {Monto,10:F2} | Timestamp: {Timestamp}";
    }
}

class Program
{
    // Módulo de ordenamiento por inserción
    static int OrdenarPorInsercion(Transaccion[] arr)
    {
        int contadorDesplazamientos = 0;
        int n = arr.Length;

        for (int i = 1; i < n; i++)
        {
            Transaccion clave = arr[i];
            int j = i - 1;

            while (j >= 0 && arr[j].Id > clave.Id)
            {
                arr[j + 1] = arr[j];
                contadorDesplazamientos++;
                j--;
            }
            arr[j + 1] = clave;
        }
        return contadorDesplazamientos;
    }

    static void Main(string[] args)
    {
        try
        {
            Transaccion[] bitacora = new Transaccion[50];
            Random rng = new Random();

            // Primeros 45 elementos: IDs en orden ascendente
            for (int i = 0; i < 45; i++)
            {
                bitacora[i] = new Transaccion(
                    id: i + 1,
                    monto: Math.Round(rng.NextDouble() * 9999.99 + 0.01, 2),
                    timestamp: DateTimeOffset.UtcNow.ToUnixTimeMilliseconds() + i * 100
                );
            }

            // Últimos 5 elementos: IDs aleatorios fuera de rango
            int[] idsAleatorios = { 78, 3, 99, 12, 55 };
            for (int i = 0; i < 5; i++)
            {
                bitacora[45 + i] = new Transaccion(
                    id: idsAleatorios[i],
                    monto: Math.Round(rng.NextDouble() * 9999.99 + 0.01, 2),
                    timestamp: DateTimeOffset.UtcNow.ToUnixTimeMilliseconds() + (45 + i) * 100
                );
            }

            Console.WriteLine("=== OPTIMIZADOR DE BITÁCORAS DE TRANSACCIONES ===\n");

            int totalDesplazamientos = OrdenarPorInsercion(bitacora);

            Console.WriteLine("Transacciones ordenadas por ID:");
            foreach (var t in bitacora)
                Console.WriteLine(t);

            Console.WriteLine($"\nTotal de desplazamientos realizados: {totalDesplazamientos}");
            Console.WriteLine($"Eficiencia: {((1 - (double)totalDesplazamientos / (50 * 49 / 2)) * 100):F1}% mejor que el peor caso.");
        }
        catch (OverflowException ex)
        {
            Console.WriteLine($"[ERROR] Desbordamiento de datos: {ex.Message}");
        }
        catch (FormatException ex)
        {
            Console.WriteLine($"[ERROR] Formato de entrada inválido: {ex.Message}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"[ERROR] Excepción inesperada: {ex.Message}");
        }

        Console.WriteLine("\nPresiona cualquier tecla para salir...");
        Console.ReadKey();
    }
}  
