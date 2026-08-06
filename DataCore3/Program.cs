using System;

// ─── STRUCT REGISTRODATOS (Fase 1 - sin modificaciones) ─────────────────────
/// <summary>
/// Modelo de datos reutilizado de la Fase 1. No se modifica.
/// </summary>
public struct RegistroDatos
{
    public int Id { get; }
    public string Nombre { get; }
    public double Monto { get; }

    public RegistroDatos(int id, string nombre, double monto)
    {
        if (id <= 0)
            throw new ArgumentException("El Id debe ser positivo.", nameof(id));
        if (string.IsNullOrEmpty(nombre))
            throw new ArgumentNullException(nameof(nombre));
        if (monto <= 0)
            throw new ArgumentOutOfRangeException(nameof(monto));

        Id = id;
        Nombre = nombre;
        Monto = monto;
    }

    public override string ToString() =>
        $"Id: {Id,3} | Nombre: {Nombre,-20} | Monto: ${Monto:F2}";
}

// ─── CLASE NODOREGISTRO ──────────────────────────────────────────────────────
/// <summary>
/// Nodo individual de la lista enlazada. Vive en el Heap.
/// </summary>
public class NodoRegistro
{
    public RegistroDatos Dato { get; set; }
    public NodoRegistro? Siguiente { get; set; }

    public NodoRegistro(RegistroDatos dato)
    {
        Dato = dato;
        Siguiente = null;
    }
}

// ─── CLASE TABLADINAMICA ─────────────────────────────────────────────────────
/// <summary>
/// Lista simplemente enlazada que administra los nodos en el Heap.
/// </summary>
public class TablaDinamica
{
    private NodoRegistro? cabeza;
    private int contadorRegistros;

    public TablaDinamica()
    {
        cabeza = null;
        contadorRegistros = 0;
    }

    /// <summary>
    /// Inserta al inicio de la lista. O(1).
    /// </summary>
    public void InsertarInicio(RegistroDatos nuevoRegistro)
    {
        NodoRegistro nuevoNodo = new NodoRegistro(nuevoRegistro);
        nuevoNodo.Siguiente = cabeza;
        cabeza = nuevoNodo;
        contadorRegistros++;
    }

    /// <summary>
    /// Inserta al final de la lista. O(n).
    /// </summary>
    public void InsertarFinal(RegistroDatos nuevoRegistro)
    {
        NodoRegistro nuevoNodo = new NodoRegistro(nuevoRegistro);

        if (cabeza == null)
        {
            cabeza = nuevoNodo;
        }
        else
        {
            NodoRegistro actual = cabeza;
            while (actual.Siguiente != null)
                actual = actual.Siguiente;
            actual.Siguiente = nuevoNodo;
        }
        contadorRegistros++;
    }

    /// <summary>
    /// Elimina el nodo con el Id indicado. O(n).
    /// </summary>
    public void EliminarPorId(int idTarget)
    {
        if (cabeza == null) return;

        // Caso especial: eliminar la cabeza
        if (cabeza.Dato.Id == idTarget)
        {
            cabeza = cabeza.Siguiente;
            contadorRegistros--;
            return;
        }

        NodoRegistro anterior = cabeza;
        NodoRegistro? actual = cabeza.Siguiente;

        while (actual != null)
        {
            if (actual.Dato.Id == idTarget)
            {
                anterior.Siguiente = actual.Siguiente;
                contadorRegistros--;
                return;
            }
            anterior = actual;
            actual = actual.Siguiente;
        }
    }

    /// <summary>
    /// Convierte la lista a un arreglo para interoperabilidad con QuickSort. O(n).
    /// </summary>
    public RegistroDatos[] ObtenerComoArreglo()
    {
        RegistroDatos[] resultado = new RegistroDatos[contadorRegistros];
        NodoRegistro? actual = cabeza;
        int i = 0;

        while (actual != null)
        {
            resultado[i] = actual.Dato;
            actual = actual.Siguiente;
            i++;
        }
        return resultado;
    }

    public int Conteo => contadorRegistros;
}

// ─── PROGRAMA PRINCIPAL ──────────────────────────────────────────────────────
class Program
{
    // ─── QUICKSORT (Fase 2 - heredado) ──────────────────────────────────────
    static void QuickSort(RegistroDatos[] arr, int bajo, int alto)
    {
        if (bajo < alto)
        {
            int indicePivote = Particionar(arr, bajo, alto);
            QuickSort(arr, bajo, indicePivote - 1);
            QuickSort(arr, indicePivote + 1, alto);
        }
    }

    static int Particionar(RegistroDatos[] arr, int bajo, int alto)
    {
        RegistroDatos pivote = arr[alto];
        int i = bajo - 1;

        for (int j = bajo; j < alto; j++)
        {
            if (arr[j].Id <= pivote.Id)
            {
                i++;
                (arr[i], arr[j]) = (arr[j], arr[i]);
            }
        }
        (arr[i + 1], arr[alto]) = (arr[alto], arr[i + 1]);
        return i + 1;
    }

    // ─── MAIN ────────────────────────────────────────────────────────────────
    static void Main(string[] args)
    {
        Console.WriteLine("╔══════════════════════════════════════════════════════╗");
        Console.WriteLine("║     DATACORE — FASE 3: LISTA SIMPLEMENTE ENLAZADA    ║");
        Console.WriteLine("╚══════════════════════════════════════════════════════╝\n");

        TablaDinamica dataCore = new TablaDinamica();

        // Paso 1: Insertar 15 registros dinámicos
        for (int i = 1; i <= 15; i++)
        {
            RegistroDatos reg = new RegistroDatos(i, $"Transacción-{i}", i * 100.0);
            dataCore.InsertarFinal(reg);
            Console.WriteLine($"[INSERT] Registro {i} añadido a la cadena.");
        }

        // Paso 2: Eliminar 2 registros específicos
        Console.WriteLine("\n--- Eliminando registros con Id 5 y Id 11 ---");
        dataCore.EliminarPorId(5);
        dataCore.EliminarPorId(11);
        Console.WriteLine("Cadena reestructurada exitosamente. Sin NullReferenceException.");

        // Paso 3: Convertir a arreglo y ordenar con QuickSort (Fase 2)
        RegistroDatos[] arreglo = dataCore.ObtenerComoArreglo();
        Console.WriteLine($"\nRegistros en arreglo: {arreglo.Length} (esperado: 13)");

        QuickSort(arreglo, 0, arreglo.Length - 1);

        Console.WriteLine("\n--- Arreglo ordenado por Id (QuickSort) ---");
        foreach (var r in arreglo)
            Console.WriteLine($"  {r}");

        Console.WriteLine("\nPresiona cualquier tecla para salir...");
        Console.ReadKey();
    }
}
