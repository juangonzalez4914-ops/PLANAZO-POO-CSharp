using System.Globalization;

namespace PlanazoPOO.Modelos;

public static class Moneda
{
    private static readonly CultureInfo Cultura = new CultureInfo("es-CO");

    public static string Formato(decimal valor)
    {
        return valor.ToString("C0", Cultura);
    }
}
