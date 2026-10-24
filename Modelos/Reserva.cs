namespace PlanazoPOO.Modelos;

public class Reserva
{
    public int Id { get; }
    public string NombrePersona { get; }
    public Evento Evento { get; }
    public int Cantidad { get; }
    public decimal ValorTotal { get; }
    public EstadoReserva Estado { get; private set; }

    public Reserva(int id, string nombrePersona, Evento evento, int cantidad)
    {
        if (string.IsNullOrWhiteSpace(nombrePersona))
            throw new ArgumentException("El nombre de la persona no puede estar vacío.");
        if (cantidad <= 0)
            throw new ArgumentException("La cantidad de entradas debe ser mayor que cero.");

        evento.ReservarCupos(cantidad);

        Id = id;
        NombrePersona = nombrePersona.Trim();
        Evento = evento;
        Cantidad = cantidad;
        ValorTotal = evento.CalcularPrecioFinal() * cantidad;
        Estado = EstadoReserva.Activa;
    }

    public void Cancelar()
    {
        if (Estado == EstadoReserva.Cancelada)
            throw new InvalidOperationException($"La reserva {Id} ya fue cancelada anteriormente.");

        Evento.LiberarCupos(Cantidad);
        Estado = EstadoReserva.Cancelada;
    }

    public override string ToString()
    {
        return $"Reserva #{Id} | {NombrePersona,-15} | {Evento.Nombre,-28} | " +
               $"Entradas: {Cantidad,2} | Total: {Moneda.Formato(ValorTotal),13} | {Estado}";
    }
}
