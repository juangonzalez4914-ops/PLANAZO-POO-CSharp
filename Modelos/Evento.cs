namespace PlanazoPOO.Modelos;

public abstract class Evento
{
    public int Id { get; }
    public string Nombre { get; }
    public DateTime Fecha { get; }
    public decimal PrecioBase { get; }
    public int CapacidadMaxima { get; }
    public int CuposDisponibles { get; private set; }

    public abstract string Tipo { get; }

    protected Evento(int id, string nombre, DateTime fecha, decimal precioBase, int capacidadMaxima)
    {
        if (id <= 0)
            throw new ArgumentException("El identificador debe ser mayor que cero.");
        if (string.IsNullOrWhiteSpace(nombre))
            throw new ArgumentException("El nombre del evento no puede estar vacío.");
        if (precioBase <= 0)
            throw new ArgumentException("El precio base debe ser mayor que cero.");
        if (capacidadMaxima <= 0)
            throw new ArgumentException("La capacidad máxima debe ser mayor que cero.");

        Id = id;
        Nombre = nombre.Trim();
        Fecha = fecha;
        PrecioBase = precioBase;
        CapacidadMaxima = capacidadMaxima;
        CuposDisponibles = capacidadMaxima;
    }

    public abstract decimal CalcularPrecioFinal();

    public void ReservarCupos(int cantidad)
    {
        if (cantidad <= 0)
            throw new ArgumentException("La cantidad de entradas debe ser mayor que cero.");
        if (cantidad > CuposDisponibles)
            throw new InvalidOperationException(
                $"No hay cupos suficientes. Solicitados: {cantidad}, disponibles: {CuposDisponibles}.");

        CuposDisponibles -= cantidad;
    }

    public void LiberarCupos(int cantidad)
    {
        if (cantidad <= 0)
            throw new ArgumentException("La cantidad a devolver debe ser mayor que cero.");
        if (CuposDisponibles + cantidad > CapacidadMaxima)
            throw new InvalidOperationException("No se pueden devolver más cupos que la capacidad máxima.");

        CuposDisponibles += cantidad;
    }

    public override string ToString()
    {
        return $"[{Id}] {Tipo,-10} | {Nombre,-28} | {Fecha:dd/MM/yyyy HH:mm} | " +
               $"{Moneda.Formato(CalcularPrecioFinal()),12} | Cupos: {CuposDisponibles}/{CapacidadMaxima}";
    }
}
