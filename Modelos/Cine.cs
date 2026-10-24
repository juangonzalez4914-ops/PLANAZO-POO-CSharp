namespace PlanazoPOO.Modelos;

public class Cine : Evento
{
    public override string Tipo => "Cine";

    public Cine(int id, string nombre, DateTime fecha, decimal precioBase, int capacidadMaxima)
        : base(id, nombre, fecha, precioBase, capacidadMaxima)
    {
    }

    public override decimal CalcularPrecioFinal()
    {
        return PrecioBase;
    }
}
