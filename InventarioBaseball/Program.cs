using System;
using System.Collections.Generic;
using System.Linq;

// Modelo de datos: Clase Producto
public class Producto
{
    public int ID { get; set; }
    public string Nombre { get; set; }
    public double Precio { get; set; }
    public int Cantidad { get; set; }

    public Producto(int id, string nombre, double precio, int cantidad)
    {
        ID = id;
        Nombre = nombre;
        Precio = precio;
        Cantidad = cantidad;
    }

    public override string ToString()
    {
        return $"[{ID}] {Nombre} - ${Precio:F2} | Stock: {Cantidad}";
    }
}

class Program
{
    // Búsqueda rápida por ID usando Dictionary
    static void BuscarPorID(Dictionary<int, Producto> catalogo)
    {
        Console.Write("\nIngresa el ID del producto a buscar: ");
        if (int.TryParse(Console.ReadLine(), out int idBuscado))
        {
            if (catalogo.TryGetValue(idBuscado, out Producto encontrado))
                Console.WriteLine($"Producto encontrado: {encontrado}");
            else
                Console.WriteLine("Producto no encontrado.");
        }
        else
        {
            Console.WriteLine("ID inválido.");
        }
    }

    static void Main(string[] args)
    {
        Console.WriteLine("⚾ === INVENTARIO DE ARTÍCULOS DE BASEBALL === ⚾\n");

        // Sintaxis 1: Inicializador de colección
        List<Producto> inventario = new List<Producto>
        {
            new Producto(1, "Bate Louisville Slugger",  2500.00, 10),
            new Producto(2, "Guante Rawlings Pro",      3200.00, 8),
            new Producto(3, "Casco Batting Easton",      850.00, 0),
            new Producto(4, "Pelota Oficial MLB",        120.00, 50),
            new Producto(5, "Spike de Baseball Nike",   1800.00, 0),
            new Producto(6, "Uniforme Completo",        1500.00, 15)
        };

        // Sintaxis 2: Agregar elementos después
        inventario.Add(new Producto(7, "Protector de Codo", 450.00, 20));

        // Sintaxis 3: Con var (inferencia de tipo)
        var nuevoProducto = new Producto(8, "Bolsa de Equipo", 750.00, 5);
        inventario.Add(nuevoProducto);

        Console.WriteLine($"Total en inventario: {inventario.Count} productos\n");

        // LINQ: Ordenar por precio descendente
        var porPrecio = inventario.OrderByDescending(p => p.Precio).ToList();
        Console.WriteLine("=== Productos por Precio (Mayor a Menor) ===");
        foreach (var p in porPrecio)
            Console.WriteLine(p);

        // LINQ: Filtrar productos agotados
        var agotados = inventario.Where(p => p.Cantidad == 0).ToList();
        Console.WriteLine("\n=== Productos Agotados ===");
        if (agotados.Count == 0)
            Console.WriteLine("Sin productos agotados.");
        else
            agotados.ForEach(p => Console.WriteLine(p));

        // Dictionary: Búsqueda rápida por ID
        Dictionary<int, Producto> catalogo = inventario.ToDictionary(p => p.ID, p => p);
        Console.WriteLine("\n=== Búsqueda Rápida por ID ===");
        BuscarPorID(catalogo);

        Console.WriteLine("\nPresiona cualquier tecla para salir...");
        Console.ReadKey();
    }
}
