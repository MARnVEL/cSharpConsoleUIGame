using System;

Random random = new();
Console.CursorVisible = false;
int altura = Console.WindowHeight - 1;
int ancho = Console.WindowWidth - 1;
bool deberiaSalir = false;

// Posición en la consola del jugador:
int jugadorX = 0;
int jugadorY = 0;

// Posición en la consola de la comida:
int comidaX = 0;
int comidaY = 0;

// Strings disponibles para el jugador y la comida:
string[] estados = ["('-')", "(^-^)", "(X_X)"];
string[] comidas = ["@@@@@", "$$$$$", "#####"];

// String actual del juagador mostrada en la consola
string aparienciaJugador = estados[0];

// Indice de la apariencia actual de la comida:
int indiceComidaActual = 0;

IniciarJuego();

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
        Moverse();
    }
}

// Mostrar una comida aleatoria en una posición aleatoria
void MostrarComida()
{
     // Actualizar la comida a un índice aleatoria.
    indiceComidaActual = random.Next(0, comidas.Length);

    // Update food position to a random location
    comidaX = random.Next(0, ancho - aparienciaJugador.Length);
    comidaY = random.Next(0, altura - 1);

    // Display the food at the location
    Console.SetCursorPosition(comidaX, comidaY);
    Console.Write(comidas[indiceComidaActual]);
}

// Devuelve true si la terminal cambia de tamaño. 
bool TerminalCambiada() 
{
    return altura != Console.WindowHeight - 1 || ancho != Console.WindowWidth - 5;
}

// Limpiamos la consola, mostrar la comida y el jugador
void IniciarJuego()
{
    Console.Clear();

    MostrarComida();
    Console.SetCursorPosition(0,0);
    Console.Write(aparienciaJugador);
}

