using System;

// ─── STRUCT REGISTRODATOS (Reutilizado de fases anteriores) ─────────────────
/// <summary>
/// Modelo de datos inmutable reutilizado de las fases anteriores.
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
        $"Id: {Id,4} | Nombre: {Nombre,-20} | Monto: ${Monto:F2}";
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

    public int Conteo => contadorRegistros;

    /// <summary>Inserta al final de la lista. O(n).</summary>
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

    /// <summary>Elimina el nodo con el Id indicado. O(n).</summary>
    public bool EliminarPorId(int idTarget)
    {
        if (cabeza == null) return false;

        if (cabeza.Dato.Id == idTarget)
        {
            cabeza = cabeza.Siguiente;
            contadorRegistros--;
            return true;
        }

        NodoRegistro anterior = cabeza;
        NodoRegistro? actual = cabeza.Siguiente;

        while (actual != null)
        {
            if (actual.Dato.Id == idTarget)
            {
                anterior.Siguiente = actual.Siguiente;
                contadorRegistros--;
                return true;
            }
            anterior = actual;
            actual = actual.Siguiente;
        }
        return false;
    }

    /// <summary>Muestra todos los registros en consola. O(n).</summary>
    public void MostrarTodos()
    {
        if (cabeza == null)
        {
            Console.WriteLine("  [Lista vacía]");
            return;
        }

        NodoRegistro? actual = cabeza;
        int i = 1;
        while (actual != null)
        {
            Console.WriteLine($"  {i}. {actual.Dato}");
            actual = actual.Siguiente;
            i++;
        }
    }

    /// <summary>Convierte la lista a un arreglo para búsqueda binaria. O(n).</summary>
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
}

// ─── PROGRAMA PRINCIPAL ──────────────────────────────────────────────────────
class Program
{
    // Arreglo ordenado para búsqueda binaria (se actualiza al indexar)
    static RegistroDatos[]? indiceOrdenado = null;

    // ─── QUICKSORT (heredado de Fase 2) ─────────────────────────────────────
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

    // ─── BÚSQUEDA BINARIA INDEXADA O(log n) ──────────────────────────────────
    /// <summary>
    /// Busca un registro por Id en el arreglo ordenado.
    /// Complejidad: O(log n).
    /// </summary>
    static (RegistroDatos? registro, int comparaciones) BuscarRegistroIndexado(
        RegistroDatos[] arrOrdenado, int idBuscado)
    {
        if (arrOrdenado == null || arrOrdenado.Length == 0)
            return (null, 0);

        int izq = 0;
        int der = arrOrdenado.Length - 1;
        int comparaciones = 0;

        while (izq <= der)
        {
            int medio = izq + (der - izq) / 2;
            comparaciones++;

            if (arrOrdenado[medio].Id == idBuscado)
                return (arrOrdenado[medio], comparaciones);
            else if (arrOrdenado[medio].Id < idBuscado)
                izq = medio + 1;
            else
                der = medio - 1;
        }
        return (null, comparaciones);
    }

    // ─── MOSTRAR MENÚ ────────────────────────────────────────────────────────
    static void MostrarMenu(int totalRegistros)
    {
        Console.Clear();
        Console.WriteLine("╔══════════════════════════════════════════════════════╗");
        Console.WriteLine("║         DATACORE v4.0 — MENÚ MAESTRO                 ║");
        Console.WriteLine("╠══════════════════════════════════════════════════════╣");
        Console.WriteLine($"║  Registros en memoria: {totalRegistros,-29}║");
        Console.WriteLine("╠══════════════════════════════════════════════════════╣");
        Console.WriteLine("║  [1] Insertar nuevo registro                          ║");
        Console.WriteLine("║  [2] Eliminar registro por ID                         ║");
        Console.WriteLine("║  [3] Mostrar todos los registros                      ║");
        Console.WriteLine("║  [4] Indexar y Ordenar                                ║");
        Console.WriteLine("║  [5] Búsqueda Binaria Indexada O(log n)               ║");
        Console.WriteLine("║  [0] Salir del sistema                                ║");
        Console.WriteLine("╚══════════════════════════════════════════════════════╝");
        Console.Write("\n  Seleccione una opción: ");
    }

    // ─── MAIN ────────────────────────────────────────────────────────────────
    static void Main(string[] args)
    {
        TablaDinamica tabla = new TablaDinamica();
        int opcion = -1;

        do
        {
            MostrarMenu(tabla.Conteo);

            try
            {
                string input = Console.ReadLine() ?? "";
                opcion = int.Parse(input);

                switch (opcion)
                {
                    // ── INSERTAR ─────────────────────────────────────────────
                    case 1:
                        Console.WriteLine("\n  --- Insertar Registro ---");
                        try
                        {
                            Console.Write("  ID: ");
                            int id = int.Parse(Console.ReadLine() ?? "");

                            Console.Write("  Nombre: ");
                            string nombre = Console.ReadLine() ?? "";

                            Console.Write("  Monto: ");
                            double monto = double.Parse(Console.ReadLine() ?? "");

                            tabla.InsertarFinal(new RegistroDatos(id, nombre, monto));
                            indiceOrdenado = null; // índice desactualizado
                            Console.WriteLine($"\n  ✅ Registro con ID {id} insertado correctamente.");
                        }
                        catch (FormatException)
                        {
                            Console.WriteLine("\n  ❌ Error: Ingresa valores numéricos válidos.");
                        }
                        catch (ArgumentException ex)
                        {
                            Console.WriteLine($"\n  ❌ Error: {ex.Message}");
                        }
                        break;

                    // ── ELIMINAR ─────────────────────────────────────────────
                    case 2:
                        Console.WriteLine("\n  --- Eliminar Registro ---");
                        try
                        {
                            Console.Write("  ID a eliminar: ");
                            int idEliminar = int.Parse(Console.ReadLine() ?? "");

                            bool eliminado = tabla.EliminarPorId(idEliminar);
                            if (eliminado)
                            {
                                indiceOrdenado = null;
                                Console.WriteLine($"\n  ✅ Registro con ID {idEliminar} eliminado.");
                            }
                            else
                            {
                                Console.WriteLine($"\n  ⚠️  ID {idEliminar} no encontrado en la lista.");
                            }
                        }
                        catch (FormatException)
                        {
                            Console.WriteLine("\n  ❌ Error: Ingresa un ID numérico válido.");
                        }
                        break;

                    // ── MOSTRAR ──────────────────────────────────────────────
                    case 3:
                        Console.WriteLine("\n  --- Registros en Memoria ---");
                        tabla.MostrarTodos();
                        break;

                    // ── INDEXAR Y ORDENAR ────────────────────────────────────
                    case 4:
                        Console.WriteLine("\n  --- Indexar y Ordenar ---");
                        try
                        {
                            if (tabla.Conteo == 0)
                                throw new InvalidOperationException("La lista está vacía.");

                            indiceOrdenado = tabla.ObtenerComoArreglo();
                            QuickSort(indiceOrdenado, 0, indiceOrdenado.Length - 1);
                            Console.WriteLine($"\n  ✅ Índice generado y ordenado con {indiceOrdenado.Length} registros.");
                            Console.WriteLine("  Listo para Búsqueda Binaria.");
                        }
                        catch (InvalidOperationException ex)
                        {
                            Console.WriteLine($"\n  ❌ Error: {ex.Message}");
                        }
                        break;

                    // ── BÚSQUEDA BINARIA ─────────────────────────────────────
                    case 5:
                        Console.WriteLine("\n  --- Búsqueda Binaria Indexada ---");
                        try
                        {
                            if (indiceOrdenado == null)
                                throw new InvalidOperationException("Primero debes indexar (opción 4).");

                            Console.Write("  ID a buscar: ");
                            int idBuscar = int.Parse(Console.ReadLine() ?? "");

                            var (registro, comparaciones) =
                                BuscarRegistroIndexado(indiceOrdenado, idBuscar);

                            if (registro != null)
                            {
                                Console.WriteLine($"\n  ✅ Registro encontrado:");
                                Console.WriteLine($"     {registro}");
                            }
                            else
                            {
                                Console.WriteLine($"\n  ⚠️  ID {idBuscar} no encontrado.");
                            }
                            Console.WriteLine($"  Comparaciones realizadas: {comparaciones} (O(log n))");
                        }
                        catch (FormatException)
                        {
                            Console.WriteLine("\n  ❌ Error: Ingresa un ID numérico válido.");
                        }
                        catch (InvalidOperationException ex)
                        {
                            Console.WriteLine($"\n  ❌ Error: {ex.Message}");
                        }
                        break;

                    // ── SALIR ────────────────────────────────────────────────
                    case 0:
                        Console.WriteLine("\n  ¡Hasta luego! DataCore v4.0 cerrado correctamente.");
                        break;

                    default:
                        Console.WriteLine("\n  ⚠️  Opción inválida. Elige entre 0 y 5.");
                        break;
                }
            }
            catch (FormatException)
            {
                Console.WriteLine("\n  ❌ Error: Ingresa un número válido.");
            }

            if (opcion != 0)
            {
                Console.Write("\n  Presiona Enter para continuar...");
                Console.ReadLine();
            }

        } while (opcion != 0);
    }
}
