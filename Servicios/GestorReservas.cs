using PlanazoPOO.Modelos;

namespace PlanazoPOO.Servicios;

public class GestorReservas
{
    private readonly List<Evento> _eventos = new List<Evento>();
    private readonly List<Reserva> _reservas = new List<Reserva>();
    private int _siguienteIdReserva = 1;

    public IReadOnlyList<Evento> Eventos => _eventos;
    public IReadOnlyList<Reserva> Reservas => _reservas;

    public void AgregarEvento(Evento evento)
    {
        _eventos.Add(evento);
    }

    public Evento ConsultarEvento(int id)
    {
        Evento? evento = _eventos.FirstOrDefault(e => e.Id == id);
        if (evento is null)
            throw new KeyNotFoundException($"No existe un evento con el identificador {id}.");

        return evento;
    }

    public Reserva Reservar(int idEvento, string nombrePersona, int cantidad)
    {
        Evento evento = ConsultarEvento(idEvento);

        Reserva reserva = new Reserva(_siguienteIdReserva, nombrePersona, evento, cantidad);
        _siguienteIdReserva++;
        _reservas.Add(reserva);

        return reserva;
    }

    public Reserva Cancelar(int idReserva)
    {
        Reserva? reserva = _reservas.FirstOrDefault(r => r.Id == idReserva);
        if (reserva is null)
            throw new KeyNotFoundException($"No existe una reserva con el identificador {idReserva}.");

        reserva.Cancelar();
        return reserva;
    }

    public (int ReservasActivas, int EntradasReservadas, decimal ValorTotal) ObtenerResumen()
    {
        List<Reserva> activas = _reservas.Where(r => r.Estado == EstadoReserva.Activa).ToList();

        return (activas.Count, activas.Sum(r => r.Cantidad), activas.Sum(r => r.ValorTotal));
    }
}
