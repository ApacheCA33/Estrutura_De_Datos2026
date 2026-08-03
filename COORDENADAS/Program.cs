using System;

// Módulo A: Struct CoordenadaGPS (Stack - copia por valor)
readonly struct CoordenadaGPS
{
    public double Latitud { get; }
    public double Longitud { get; }

    public CoordenadaGPS(double lat, double lon)
    {
        // Módulo C: Validación en el constructor
        if (lat < -90 || lat > 90)
            throw new ArgumentOutOfRangeException(nameof(lat), "Latitud fuera de rango [-90, 90]");
        if (lon < -180 || lon > 180)
            throw new ArgumentOutOfRangeException(nameof(lon), "Longitud fuera de rango [-180, 180]");

        Latitud = lat;
        Longitud = lon;
    }

    public void ImprimirUbicacion()
    {
        Console.WriteLine($"  Latitud:  {Latitud}");
        Console.WriteLine($"  Longitud: {Longitud}");
    }
}

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("=== Stack vs Heap: CoordenadaGPS ===\n");

        // Módulo B: Comprobando la Copia por Valor
        Console.WriteLine("--- Módulo B: Copia por Valor (struct) ---");

        // Ciudad de México
        CoordenadaGPS c1 = new CoordenadaGPS(19.4326, -99.1332);

        // Copia por valor en el Stack
        CoordenadaGPS c2 = c1;

        // Reasignamos c2 a Berlín
        c2 = new CoordenadaGPS(52.5200, 13.4050);

        Console.WriteLine("c1 (Ciudad de México):");
        c1.ImprimirUbicacion();
        Console.WriteLine("c2 (Berlín):");
        c2.ImprimirUbicacion();
        Console.WriteLine("✅ c1 no cambió porque struct copia por valor.\n");

        // Módulo C: Control de Excepciones y Robustez
        Console.WriteLine("--- Módulo C: Ingresa una coordenada ---");
        try
        {
            Console.Write("Latitud:  ");
            double lat = double.Parse(Console.ReadLine());
            Console.Write("Longitud: ");
            double lon = double.Parse(Console.ReadLine());

            var coord = new CoordenadaGPS(lat, lon);
            Console.WriteLine("\n✅ Coordenada válida:");
            coord.ImprimirUbicacion();
        }
        catch (ArgumentOutOfRangeException ex)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"\n❌ Error de validación: {ex.Message}");
            Console.ResetColor();
        }
        catch (FormatException)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("\n❌ Error: Ingresa un número válido.");
            Console.ResetColor();
        }

        Console.WriteLine("\nPresiona cualquier tecla para salir...");
        Console.ReadKey();
    }
}

