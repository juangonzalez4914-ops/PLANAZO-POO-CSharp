PLANAZO-POO-CSharp

Descripcion

Aplicacion de consola en C# para PLANAZO, una empresa que organiza conciertos, funciones de cine y talleres. Permite consultar eventos, reservar entradas, controlar los cupos y cancelar reservas

Funcionalidades

- Ver y consultar eventos
- Reservar entradas
- Ver reservas realizadas
- Cancelar una reserva y devolver los cupos
- Ver el resumen de reservas activas

Como ejecutarlo

Abrir una terminal en la carpeta del proyecto y escribir dotnet run

Clases

- Evento: clase abstracta con los datos comunes de todo evento
- Cine, Concierto y Taller: heredan de Evento y calculan su propio precio
- Reserva: asocia a una persona con un Evento
- GestorReservas: guarda las listas de eventos y reservas y realiza las operaciones

Conceptos de POO

- Encapsulamiento: los cupos y el estado de la reserva usan private set y solo cambian mediante metodos de su clase
- Abstraccion: Evento es abstracta y no se puede crear directamente
- Herencia: Cine, Concierto y Taller heredan de Evento
- Polimorfismo: CalcularPrecioFinal se llama igual en todos los eventos, pero cada uno calcula un precio distinto

Pregunta de sustentacion

¿Por que utilizaste una clase abstracta Evento en lugar de crear clases completamente independientes para Cine, Concierto y Taller? ¿Que ventajas tiene esta decision cuando necesitamos agregar un nuevo tipo de evento?

Porque los tres eventos comparten los mismos datos y el mismo manejo de cupos, y solo cambia como calculan el precio. Con una clase abstracta ese codigo comun se escribe una sola vez y cada clase hija solo define su precio. Por ejemplo, Concierto solo implementa CalcularPrecioFinal multiplicando el precio base por 1.15

Para agregar un nuevo tipo de evento solo hay que crear una clase que herede de Evento e implemente CalcularPrecioFinal. No hay que modificar Cine, Concierto, Taller, Reserva ni GestorReservas, porque Reserva funciona con cualquier evento gracias al polimorfismo
