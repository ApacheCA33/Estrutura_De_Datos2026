using System;

class CAL
{
    
    static int SeleccionarPoligono()
    {
        Console.WriteLine("Selecciona un polígono:");
        Console.WriteLine("1. Pentágono  (5 lados)");
        Console.WriteLine("2. Hexágono   (6 lados)");
        Console.WriteLine("3. Heptágono  (7 lados)");
        Console.WriteLine("4. Octágono   (8 lados)");
        Console.Write("Elige una opción: ");

        int opcion = int.Parse(Console.ReadLine());

        if (opcion == 1) return 5;
        else if (opcion == 2) return 6;
        else if (opcion == 3) return 7;
        else return 8;
    }

    
    static void PedirDatos(out double lado, out double apotema)
    {
        Console.Write("Ingresa el lado: ");
        lado = double.Parse(Console.ReadLine());

        Console.Write("Ingresa la apotema: ");
        apotema = double.Parse(Console.ReadLine());
    }

    
    static double CalcularArea(int lados, double lado, double apotema)
    {
        double perimetro = lados * lado;
        double area = (perimetro * apotema) / 2;
        return area;
    }

    static void Main(string[] args)
    {
        int lados = SeleccionarPoligono();

        double lado, apotema;
        PedirDatos(out lado, out apotema);

        double area = CalcularArea(lados, lado, apotema);

        Console.WriteLine("El área es: " + area);
    }
}