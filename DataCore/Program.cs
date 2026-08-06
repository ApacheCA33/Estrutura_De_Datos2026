using System;

// Struct RegistroDatos — tipo de valor inmutable que representa un registro del sistema
public struct RegistroDatos
{
    public int Id;               // Llave primaria, campo de ordenamiento
    public long HashValidacion;  // Código de verificación de integridad (simula checksum)
    public int PesoBytes;        // Tamaño físico del registro en bytes (10 a 5000)

    /// <summary>
    /// Constructor con validación de contrato.
    /// </summary>
    /// <param name="id">Identificador único del registro.</param>
    /// <param name="hash">Código de verificación de integridad.</param>
    /// <param name="pesoBytes">Tamaño físico del registro. Debe ser mayor a 0.</param>
    public RegistroDatos(int id, long hash, int pesoBytes)
    {
        // Contrato de integridad: un registro con peso <= 0 es físicamente imposible
        if (pesoBytes <= 0)
            throw new ArgumentException(
                "PesoBytes debe ser mayor a 0. Un registro no puede tener tamaño nulo o negativo.",
                nameof(pesoBytes));

        Id = id;
        HashValidacion = hash;
        PesoBytes = pesoBytes;
    }

    /// <summary>
    /// Representación legible del registro para consola.
    /// </summary>
    public override string ToString()
    {
        return $"Id: {Id,4} | Hash: {HashValidacion,20} | Peso: {PesoBytes,4} bytes";
    }
}

class Program
{
    /// <summary>
    /// Ordena el arreglo por Id usando Selection Sort instrumentado.
    /// Reporta comparaciones e intercambios reales realizados.
    /// </summary>
    /// <param name="arr">Arreglo de RegistroDatos a ordenar.</param>
    static void OrdenarPorSeleccion(RegistroDatos[] arr)
    {
        int comparaciones = 0;
        int intercambios = 0;

        for (int i = 0; i < arr.Length - 1; i++)
        {
            int indiceMinimo = i;

            for (int j = i + 1; j < arr.Length; j++)
            {
                comparaciones++;
                if (arr[j].Id < arr[indiceMinimo].Id)
                    indiceMinimo = j;
            }

            // Solo intercambia si el mínimo no es el elemento actual
            if (indiceMinimo != i)
            {
                (arr[i], arr[indiceMinimo]) = (arr[indiceMinimo], arr[i]); // Tupla moderna C# 7+
                intercambios++;
            }
        }

        Console.WriteLine($"\nComparaciones realizadas : {comparaciones}");
        Console.WriteLine($"Intercambios reales      : {intercambios}");
    }

    static void Main(string[] args)
    {
        Console.WriteLine("╔══════════════════════════════════════════════╗");
        Console.WriteLine("║         DATACORE — MOTOR DE ORDENACIÓN       ║");
        Console.WriteLine("║     Fase 1: Selection Sort Instrumentado      ║");
        Console.WriteLine("╚══════════════════════════════════════════════╝\n");

        var rng = new Random();
        var arreglo = new RegistroDatos[40];

        // Generación de 40 registros aleatorios con bloque try-catch
        try
        {
            for (int i = 0; i < arreglo.Length; i++)
            {
                arreglo[i] = new RegistroDatos(
                    id:        rng.Next(1, 1001),
                    hash:      rng.NextInt64(),
                    pesoBytes: rng.Next(10, 5001)
                );
            }
        }
        catch (ArgumentException ex)
        {
            Console.WriteLine($"Error al crear registro: {ex.Message}");
        }

        // Estado inicial
        Console.WriteLine("=== ESTADO INICIAL (desordenado) ===");
        foreach (var r in arreglo)
            Console.WriteLine(r);

        // Ejecutar ordenamiento
        OrdenarPorSeleccion(arreglo);

        // Estado final
        Console.WriteLine("\n=== ESTADO FINAL (ordenado por Id) ===");
        foreach (var r in arreglo)
            Console.WriteLine(r);

        Console.WriteLine("\nPresiona cualquier tecla para salir...");
        Console.ReadKey();
    }
}
