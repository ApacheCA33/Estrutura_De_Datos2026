using System;
using System.Diagnostics;

/// <summary>
/// Modelo de datos para el experimento de benchmarking.
/// Reutilizado de la Fase 1 sin modificaciones.
/// </summary>
public struct RegistroDatos
{
    public int Id { get; }
    public string HashValidacion { get; }
    public double PesoBytes { get; }

    /// <summary>
    /// Constructor con validación de dominio.
    /// </summary>
    public RegistroDatos(int id, string hashValidacion, double pesoBytes)
    {
        if (id <= 0)
            throw new ArgumentException("El Id debe ser un entero positivo mayor que cero.", nameof(id));
        if (string.IsNullOrEmpty(hashValidacion))
            throw new ArgumentNullException(nameof(hashValidacion), "HashValidacion no puede ser null ni vacío.");
        if (pesoBytes <= 0.0)
            throw new ArgumentOutOfRangeException(nameof(pesoBytes), "PesoBytes debe ser un valor positivo mayor que cero.");

        Id = id;
        HashValidacion = hashValidacion;
        PesoBytes = pesoBytes;
    }

    public override string ToString() =>
        $"[Id={Id,6} | Hash={HashValidacion[..8]}... | Peso={PesoBytes:F2}B]";
}

class Program
{
    // Contadores para Selection Sort
    static int contadorComparaciones = 0;
    static int contadorIntercambios  = 0;

    // Contador para QuickSort
    static int contadorLlamadas = 0;

    // ─── SELECTION SORT (Fase 1) ────────────────────────────────────────────
    /// <summary>
    /// Ordena el arreglo por Id usando Selection Sort instrumentado.
    /// </summary>
    static void OrdenarPorSeleccion(RegistroDatos[] arr)
    {
        int n = arr.Length;
        for (int i = 0; i < n - 1; i++)
        {
            int indiceMinimo = i;
            for (int j = i + 1; j < n; j++)
            {
                contadorComparaciones++;
                if (arr[j].Id < arr[indiceMinimo].Id)
                    indiceMinimo = j;
            }
            if (indiceMinimo != i)
            {
                (arr[i], arr[indiceMinimo]) = (arr[indiceMinimo], arr[i]);
                contadorIntercambios++;
            }
        }
    }

    // ─── QUICKSORT (Fase 2) ─────────────────────────────────────────────────
    /// <summary>
    /// Método de control recursivo de QuickSort.
    /// </summary>
    static void QuickSort(RegistroDatos[] arr, int bajo, int alto)
    {
        contadorLlamadas++;

        if (bajo < alto)
        {
            int indicePivote = Particionar(arr, bajo, alto);
            QuickSort(arr, bajo, indicePivote - 1);
            QuickSort(arr, indicePivote + 1, alto);
        }
    }

    /// <summary>
    /// Método de particionado con esquema Lomuto.
    /// Pivote = último elemento del rango.
    /// </summary>
    static int Particionar(RegistroDatos[] arr, int bajo, int alto)
    {
        RegistroDatos pivote = arr[alto];
        int i = bajo - 1;

        for (int j = bajo; j < alto; j++)
        {
            if (arr[j].Id <= pivote.Id)
            {
                i++;
                (arr[i], arr[j]) = (arr[j], arr[i]); // Tupla moderna C# 7+
            }
        }

        (arr[i + 1], arr[alto]) = (arr[alto], arr[i + 1]);
        return i + 1;
    }

    // ─── GENERADOR DE DATOS ─────────────────────────────────────────────────
    /// <summary>
    /// Genera un arreglo de registros aleatorios con semilla fija para reproducibilidad.
    /// </summary>
    static RegistroDatos[] GenerarArregloAleatorio(int cantidad)
    {
        Random rnd = new Random(42); // Semilla fija para reproducibilidad
        RegistroDatos[] arreglo = new RegistroDatos[cantidad];

        for (int i = 0; i < cantidad; i++)
        {
            arreglo[i] = new RegistroDatos(
                id:             rnd.Next(1, 100_001),
                hashValidacion: Guid.NewGuid().ToString(),
                pesoBytes:      1.0 + rnd.NextDouble() * 9999
            );
        }
        return arreglo;
    }

    // ─── VERIFICACIÓN ───────────────────────────────────────────────────────
    static bool EstaOrdenado(RegistroDatos[] arr)
    {
        for (int i = 0; i < arr.Length - 1; i++)
            if (arr[i].Id > arr[i + 1].Id)
                return false;
        return true;
    }

    // ─── MAIN ───────────────────────────────────────────────────────────────
    static void Main(string[] args)
    {
        int tamaño = 10_000;

        Console.WriteLine("╔══════════════════════════════════════════════════════╗");
        Console.WriteLine("║     DATACORE — BENCHMARK COMPARATIVO DE ORDENACIÓN   ║");
        Console.WriteLine("║         Fase 2: Selection Sort vs. QuickSort          ║");
        Console.WriteLine("╚══════════════════════════════════════════════════════╝\n");

        // Generar datos originales
        RegistroDatos[] arregloOriginal = GenerarArregloAleatorio(tamaño);

        // Clonar para condiciones idénticas
        RegistroDatos[] copiaSeleccion = (RegistroDatos[])arregloOriginal.Clone();
        RegistroDatos[] copiaQuickSort = (RegistroDatos[])arregloOriginal.Clone();

        // ── BENCHMARK 1: Selection Sort ──────────────────────────────────────
        contadorComparaciones = 0;
        contadorIntercambios  = 0;

        Stopwatch swSeleccion = Stopwatch.StartNew();
        OrdenarPorSeleccion(copiaSeleccion);
        swSeleccion.Stop();

        long msSeleccion   = swSeleccion.ElapsedMilliseconds;
        long opSeleccion   = contadorComparaciones + contadorIntercambios;

        // ── BENCHMARK 2: QuickSort ───────────────────────────────────────────
        contadorLlamadas = 0;

        Stopwatch swQuickSort = Stopwatch.StartNew();
        QuickSort(copiaQuickSort, 0, copiaQuickSort.Length - 1);
        swQuickSort.Stop();

        long msQuickSort = swQuickSort.ElapsedMilliseconds;

        // ── VERIFICACIÓN ─────────────────────────────────────────────────────
        string estadoSeleccion = EstaOrdenado(copiaSeleccion) ? "✅ Correcto" : "❌ Error";
        string estadoQuickSort = EstaOrdenado(copiaQuickSort) ? "✅ Correcto" : "❌ Error";

        // ── REPORTE COMPARATIVO ──────────────────────────────────────────────
        Console.WriteLine("╔══════════════════════════════════════════════════════╗");
        Console.WriteLine($"║    REPORTE COMPARATIVO DE ORDENAMIENTO (n = {tamaño:N0})  ║");
        Console.WriteLine("╠══════════════════════════════════════════════════════╣");
        Console.WriteLine($"║ Algoritmo          : Selección Directa               ║");
        Console.WriteLine($"║ Registros          : {tamaño:N0}                          ║");
        Console.WriteLine($"║ Comparaciones      : {contadorComparaciones:N0}                   ║");
        Console.WriteLine($"║ Intercambios       : {contadorIntercambios:N0}                        ║");
        Console.WriteLine($"║ Operaciones totales: {opSeleccion:N0}                   ║");
        Console.WriteLine($"║ Tiempo de ejecución: {msSeleccion} ms                          ║");
        Console.WriteLine($"║ Resultado          : {estadoSeleccion}                        ║");
        Console.WriteLine("╠══════════════════════════════════════════════════════╣");
        Console.WriteLine($"║ Algoritmo          : QuickSort                        ║");
        Console.WriteLine($"║ Registros          : {tamaño:N0}                          ║");
        Console.WriteLine($"║ Llamadas recursivas: {contadorLlamadas:N0}                       ║");
        Console.WriteLine($"║ Tiempo de ejecución: {msQuickSort} ms                            ║");
        Console.WriteLine($"║ Resultado          : {estadoQuickSort}                        ║");
        Console.WriteLine("╠══════════════════════════════════════════════════════╣");

        if (msSeleccion > 0 && msQuickSort > 0)
        {
            long ratio = msSeleccion / msQuickSort;
            Console.WriteLine($"║ Ratio de velocidad : QuickSort fue {ratio}x más rápido  ║");
        }
        else
        {
            Console.WriteLine("║ Ratio de velocidad : QuickSort fue significativamente  ║");
            Console.WriteLine("║                      más rápido (< 1ms)                ║");
        }

        Console.WriteLine("╚══════════════════════════════════════════════════════╝");

        Console.WriteLine("\nPresiona cualquier tecla para salir...");
        Console.ReadKey();
    }
}

