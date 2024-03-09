using System;

Random random = new Random();
Console.CursorVisible = false;
int altura = Console.WindowHeight - 1;
int ancho = Console.WindowWidth - 5;
bool deberiaSalir = false;

// Posición en la consola del jugador:
int jugadorPosicionX = 0;
int jugadorPosicionY = 0;

// Posición en la consola de la comida:
int comidaX = 0;
int comidaY = 0;

// Strings disponibles para el jugador y la comida:
string[] estados = {"('-')", "(^-^)", "(X_X)"};
string[] comidas = {"@@@@@", "$$$$$", "#####"};

// String actual del juagador mostrada en la consola
string aparienciaJugador = estados[0];

// Indice de la apariencia actual de la comida:
int indiceComidaActual = 0;

IniciarJuego();

// Bucle principal del juego. Se ejecutará continuamente mientras `deberiaSalir == false`
while (!deberiaSalir)
{
    if (TerminalCambiada())
    {
        Console.Clear();
        Console.WriteLine("La consola fué cambiada de tamaño. Saliendo...");
        deberiaSalir = true;
    }
    else
    {
        if(JugadorEsRapido())
        {
            Moverse(1, false);
        }
        else if (JugadorEstaCongelado())
        {
            CongelarJugador();
        }
        else
        {
            Moverse(salirConOtraTecla: false);
        }

        if (jugadorHaConsumidoComida())
        {
            CambiarAparienciaJugador();
            MostrarComida();
        }
    }
}


// Devuelve true si la terminal cambia de tamaño. 
bool TerminalCambiada() 
{
    return altura != (Console.WindowHeight - 1) || ancho != (Console.WindowWidth - 5);
}

// Mostrar una comida aleatoria en una posición aleatoria
void MostrarComida()
{
     // Actualizar la comida a un índice aleatoria.
    indiceComidaActual = random.Next(0, comidas.Length);

    // Actualizar la posición de la comida a una localizazión aleatoria.
    comidaX = random.Next(0, ancho - aparienciaJugador.Length);
    comidaY = random.Next(0, altura - 1);

    // Mostrar la comida en la ubicación.
    Console.SetCursorPosition(comidaX, comidaY);
    Console.Write(comidas[indiceComidaActual]);
}


// Devuelve `true` si la ubicación del jugador coincide con la de la comida
bool jugadorHaConsumidoComida() 
{
    return jugadorPosicionY == comidaY && jugadorPosicionX == comidaX;
}

// Devuelve `true` si la apariencia del jugador representa un estado de congelado
bool JugadorEstaCongelado() 
{
    return aparienciaJugador.Equals(estados[2]);
}

// Devuelve `true` si la apariencia del jugador representa un estado de rapidez
bool JugadorEsRapido() 
{
    return aparienciaJugador.Equals(estados[1]);
}

// Cambiar la apariencia del jugador para que coincida con el alimento consumido
void CambiarAparienciaJugador() 
{
    aparienciaJugador = estados[indiceComidaActual];
    Console.SetCursorPosition(jugadorPosicionX, jugadorPosicionX);
    Console.Write(aparienciaJugador);
}

// Detiene temporalmente los movimientos del jugador
void CongelarJugador() 
{
    System.Threading.Thread.Sleep(1000);
    aparienciaJugador = estados[0];
}

// Leer entradas direccionales desde la consola y mover al jugador.
void Moverse(int velocidad = 1, bool salirConOtraTecla = false)
{
    int ultimaPosicionJugadorX = jugadorPosicionX;
    int ultimaPosicionJugadorY = jugadorPosicionY;

    switch(Console.ReadKey(true).Key)
    {
        case ConsoleKey.UpArrow:
            jugadorPosicionY--;
            break;
        case ConsoleKey.DownArrow:
            jugadorPosicionY++;
            break;
        case ConsoleKey.LeftArrow:
            jugadorPosicionX -= velocidad;
            break;
        case ConsoleKey.RightArrow:
            jugadorPosicionX += velocidad;
            break;
        case ConsoleKey.Escape:
            deberiaSalir = true;
            break;
        default:
            // Salir si se presiona cualquier otra tecla que no sea alguna de las anteriores.
            deberiaSalir = salirConOtraTecla;
            break;
    }

    // Limpiar caracteres en las posiciones previas
    Console.SetCursorPosition(ultimaPosicionJugadorX, ultimaPosicionJugadorY);
    for(int i = 0; i < aparienciaJugador.Length; i++)
    {
        Console.Write(" ");
    }

    // Matener las posiciones del jugador dentro de las fronteras de la ventana de la consola
    jugadorPosicionX = (jugadorPosicionX < 0) ? 0 : (jugadorPosicionX >= ancho ? ancho : jugadorPosicionX);
    jugadorPosicionY = (jugadorPosicionY < 0) ? 0 : (jugadorPosicionY >= altura ? altura : jugadorPosicionY);

    // Dibujar el jugador en la nueva posición
    Console.SetCursorPosition(jugadorPosicionX,  jugadorPosicionY);
    Console.Write(aparienciaJugador);
}

// Limpiamos la consola, mostrar la comida y el jugador
void IniciarJuego()
{
    Console.Clear();

    MostrarComida();
    Console.SetCursorPosition(0,0);
    Console.Write(aparienciaJugador);
}

