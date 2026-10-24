using PlanazoPOO.Modelos;
using PlanazoPOO.Servicios;

Console.OutputEncoding = System.Text.Encoding.UTF8;

GestorReservas gestor = new GestorReservas();
CargarEventosIniciales(gestor);

int opcion = -1;
while (opcion != 0)
{
    MostrarMenu();

    if (!int.TryParse(Console.ReadLine(), out opcion))
    {
        opcion = -1;
        Console.WriteLine("\nOpción inválida. Escriba un número del menú.");
        Pausa();
        continue;
    }

    try
    {
        switch (opcion)
        {
            case 1: VerEventos(gestor); break;
            case 2: ConsultarEvento(gestor); break;
            case 3: ReservarEntradas(gestor); break;
            case 4: VerReservas(gestor); break;
            case 5: CancelarReserva(gestor); break;
            case 6: VerResumen(gestor); break;
            case 0: Console.WriteLine("\nGracias por usar PLANAZO."); break;
            default: Console.WriteLine("\nOpción inválida. Escriba un número del menú."); break;
        }
    }
    catch (Exception ex) when (ex is ArgumentException or InvalidOperationException or KeyNotFoundException or FormatException)
    {
        Console.WriteLine($"\nError: {ex.Message}");
    }

    if (opcion != 0)
        Pausa();
}

static void CargarEventosIniciales(GestorReservas gestor)
{
    gestor.AgregarEvento(new Cine(1, "Dune: Parte Tres", new DateTime(2026, 11, 20, 19, 0, 0), 18000m, 50));
    gestor.AgregarEvento(new Cine(2, "Toy Story 5", new DateTime(2026, 11, 21, 16, 0, 0), 16000m, 40));
    gestor.AgregarEvento(new Concierto(3, "Karol G en Barranquilla", new DateTime(2026, 12, 5, 20, 0, 0), 50000m, 100));
    gestor.AgregarEvento(new Concierto(4, "Noche de Salsa", new DateTime(2026, 12, 12, 21, 0, 0), 40000m, 80));
    gestor.AgregarEvento(new Taller(5, "Taller de Fotografía", new DateTime(2026, 11, 28, 9, 0, 0), 60000m, 20));
    gestor.AgregarEvento(new Taller(6, "Taller de Cocina Caribe", new DateTime(2026, 11, 29, 10, 0, 0), 80000m, 15));
}

static void MostrarMenu()
{
    Console.Clear();
    Console.WriteLine("================================");
    Console.WriteLine("        PLANAZO - EVENTOS");
    Console.WriteLine("================================");
    Console.WriteLine();
    Console.WriteLine("1. Ver eventos disponibles");
    Console.WriteLine("2. Consultar evento");
    Console.WriteLine("3. Reservar entradas");
    Console.WriteLine("4. Ver reservas realizadas");
    Console.WriteLine("5. Cancelar una reserva");
    Console.WriteLine("6. Ver resumen de reservas");
    Console.WriteLine("0. Salir");
    Console.WriteLine();
    Console.Write("Seleccione una opción: ");
}

static void Pausa()
{
    Console.WriteLine("\nPresione Enter para continuar...");
    Console.ReadLine();
}

static int LeerEntero(string mensaje)
{
    Console.Write(mensaje);
    if (!int.TryParse(Console.ReadLine(), out int valor))
        throw new FormatException("Debe ingresar un número entero válido.");
    return valor;
}

static void VerEventos(GestorReservas gestor)
{
    Console.WriteLine("\n--- EVENTOS DISPONIBLES ---");
    foreach (Evento evento in gestor.Eventos)
        Console.WriteLine(evento);
}

static void ConsultarEvento(GestorReservas gestor)
{
    int id = LeerEntero("\nIdentificador del evento: ");
    Evento evento = gestor.ConsultarEvento(id);

    Console.WriteLine("\n--- DETALLE DEL EVENTO ---");
    Console.WriteLine($"Id:                {evento.Id}");
    Console.WriteLine($"Tipo:              {evento.Tipo}");
    Console.WriteLine($"Nombre:            {evento.Nombre}");
    Console.WriteLine($"Fecha:             {evento.Fecha:dd/MM/yyyy HH:mm}");
    Console.WriteLine($"Precio base:       {Moneda.Formato(evento.PrecioBase)}");
    Console.WriteLine($"Precio final:      {Moneda.Formato(evento.CalcularPrecioFinal())}");
    Console.WriteLine($"Capacidad máxima:  {evento.CapacidadMaxima}");
    Console.WriteLine($"Cupos disponibles: {evento.CuposDisponibles}");
}

static void ReservarEntradas(GestorReservas gestor)
{
    int idEvento = LeerEntero("\nIdentificador del evento: ");
    Console.Write("Nombre de la persona: ");
    string nombre = Console.ReadLine() ?? "";
    int cantidad = LeerEntero("Cantidad de entradas: ");

    Reserva reserva = gestor.Reservar(idEvento, nombre, cantidad);

    Console.WriteLine("\nReserva registrada correctamente:");
    Console.WriteLine(reserva);
}

static void VerReservas(GestorReservas gestor)
{
    Console.WriteLine("\n--- RESERVAS REALIZADAS ---");
    if (gestor.Reservas.Count == 0)
    {
        Console.WriteLine("Aún no hay reservas registradas.");
        return;
    }

    foreach (Reserva reserva in gestor.Reservas)
        Console.WriteLine(reserva);
}

static void CancelarReserva(GestorReservas gestor)
{
    int id = LeerEntero("\nIdentificador de la reserva a cancelar: ");
    Reserva reserva = gestor.Cancelar(id);

    Console.WriteLine("\nReserva cancelada. Se devolvieron los cupos al evento:");
    Console.WriteLine(reserva);
    Console.WriteLine($"Cupos disponibles de '{reserva.Evento.Nombre}': {reserva.Evento.CuposDisponibles}");
}

static void VerResumen(GestorReservas gestor)
{
    var resumen = gestor.ObtenerResumen();

    Console.WriteLine("\n--- RESUMEN DE RESERVAS ACTIVAS ---");
    Console.WriteLine($"Reservas activas:    {resumen.ReservasActivas}");
    Console.WriteLine($"Entradas reservadas: {resumen.EntradasReservadas}");
    Console.WriteLine($"Valor total:         {Moneda.Formato(resumen.ValorTotal)}");
}
