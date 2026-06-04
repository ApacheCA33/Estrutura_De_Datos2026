using System;

class Program
{
    // 1. CambiarValor: recibe un entero (copia) e intenta cambiarlo
    static void CambiarValor(int x)
    {
        x = 100;
        Console.WriteLine("Dentro de CambiarValor: " + x);
    }

    // 2. CambiarReferencia: recibe un arreglo (referencia) y cambia el primer elemento
    static void CambiarReferencia(int[] arr)
    {
        arr[0] = 100;
        Console.WriteLine("Dentro de CambiarReferencia: " + arr[0]);
    }

    static void Main(string[] args)
    {
        // Prueba con valor
        int numero = 10;
        Console.WriteLine("Antes de CambiarValor: " + numero);
        CambiarValor(numero);
        Console.WriteLine("Después de CambiarValor: " + numero); // Sigue siendo 10

        Console.WriteLine();

        // Prueba con referencia
        int[] arreglo = { 10, 20, 30 };
        Console.WriteLine("Antes de CambiarReferencia: " + arreglo[0]);
        CambiarReferencia(arreglo);
        Console.WriteLine("Después de CambiarReferencia: " + arreglo[0]); // Cambia a 100
    }
}
