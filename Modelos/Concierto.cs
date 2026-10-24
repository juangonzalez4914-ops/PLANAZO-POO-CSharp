namespace PlanazoPOO.Modelos;

public class Concierto : Evento
{
    private const decimal Recargo = 0.15m;

    public override string Tipo => "Concierto";

    public Concierto(int id, string nombre, DateTime fecha, decimal precioBase, int capacidadMaxima)
        : base(id, nombre, fecha, precioBase, capacidadMaxima)
    {
    }

    public override decimal CalcularPrecioFinal()
    {
        return PrecioBase * (1 + Recargo);
    }
}
