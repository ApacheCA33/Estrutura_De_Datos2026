using System;

class Nodo
{
    public int Valor;
    public Nodo Izquierda;
    public Nodo Derecha;

    public Nodo(int valor)
    {
        Valor = valor;
        Izquierda = null;
        Derecha = null;
    }
}

class ArbolBST
{
    private Nodo raiz;

    // Insertar nodo
    public void Insertar(int valor)
    {
        raiz = InsertarRecursivo(raiz, valor);
    }

    private Nodo InsertarRecursivo(Nodo nodo, int valor)
    {
        if (nodo == null)
            return new Nodo(valor);

        if (valor < nodo.Valor)
            nodo.Izquierda = InsertarRecursivo(nodo.Izquierda, valor);
        else if (valor > nodo.Valor)
            nodo.Derecha = InsertarRecursivo(nodo.Derecha, valor);

        return nodo;
    }

    // BuscarNodo recursivo
    public Nodo BuscarNodo(int idTarget)
    {
        return BuscarRecursivo(raiz, idTarget);
    }

    private Nodo BuscarRecursivo(Nodo nodo, int idTarget)
    {
        // Caso base: no existe o lo encontramos
        if (nodo == null)
        {
            Console.WriteLine($"❌ Nodo {idTarget} no encontrado.");
            return null;
        }

        if (nodo.Valor == idTarget)
        {
            Console.WriteLine($"✅ Nodo {idTarget} encontrado!");
            return nodo;
        }

        // Si es menor busca a la izquierda, si es mayor a la derecha
        if (idTarget < nodo.Valor)
        {
            Console.WriteLine($"  {nodo.Valor} → izquierda");
            return BuscarRecursivo(nodo.Izquierda, idTarget);
        }
        else
        {
            Console.WriteLine($"  {nodo.Valor} → derecha");
            return BuscarRecursivo(nodo.Derecha, idTarget);
        }
    }
}

class Program
{
    static void Main(string[] args)
    {
        ArbolBST arbol = new ArbolBST();

        // Insertamos los 7 nodos del ejemplo balanceado
        int[] valores = { 4, 2, 6, 1, 3, 5, 7 };
        foreach (int v in valores)
            arbol.Insertar(v);

        Console.WriteLine("=== Árbol de Búsqueda Binaria ===\n");
        Console.Write("¿Qué nodo quieres buscar? ");
        int objetivo = int.Parse(Console.ReadLine());

        Console.WriteLine($"\nBuscando {objetivo}:");
        arbol.BuscarNodo(objetivo);
    }
}
