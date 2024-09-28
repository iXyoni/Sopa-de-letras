using System;
using System.Collections;

namespace Funciones
{
    public class sopaFunciones
    {
        private char[,] sopa;
        private int tamano;
        public int palabrarest;
        private Hashtable palabrasPosiciones;
        private int filaActual = 0;
        private Random random = new Random();
        private List<(int, string)> pistasImpresas = new List<(int, string)>();
        private List<string> respuetasEncontradas = new List<string>();
        private Hashtable diccionario;

        public sopaFunciones(int tma, int palabrarest)
        {
            this.palabrarest = palabrarest;
            diccionario = new Hashtable();
            palabrasPosiciones = new Hashtable();
            sopa = new char[tma, tma];
            tamano = tma;

            // Inicializar la sopa con caracteres '-'
            for (int i = 0; i < tamano; i++)
            {
                for (int j = 0; j < tamano; j++)
                {
                    sopa[i, j] = '-';
                }
            }
        }

        public void cargarPalabras(string archivo, int cantPalabras)
        {
            string[] lineas = File.ReadAllLines(archivo);

            for (int i = 0; i < cantPalabras; i++)
            {
                int random2 = random.Next(0, lineas.Length);
                string lineaSeleccionada = lineas[random2];
                string[] palabras = lineaSeleccionada.Trim('|').Split('|');

                if (palabras.Length == 2)
                {
                    string llave = palabras[0].Trim().ToLower();
                    string pista = palabras[1].Trim().ToLower();

                    diccionario[llave] = pista;
                }
            }
        }

        public void llenarSopa()
        {
            // Colocar las palabras en la sopa
            ColocarPalabras();

            // Llenar los espacios vacíos con caracteres aleatorios
            for (int i = 0; i < tamano; i++)
            {
                for (int j = 0; j < tamano; j++)
                {
                    if (sopa[i, j] == '-')
                    {
                        sopa[i, j] = (char)('a' + random.Next(26));
                    }
                }
            }
        }

        public void ColocarPalabras()
        {
            int maxIntentos = 100; // número máximo de intentos por palabra
            int cantpal = palabrarest;

            foreach (DictionaryEntry palabra in diccionario) // recorremos el diccionario
            {
                if (palabra.Key == null || palabra.Value == null) continue;

                string palabraActual = palabra.Key.ToString();
                string pista = palabra.Value.ToString();
                bool colocada = false;
                int intentos = 0;

                while (!colocada && cantpal > 0 && intentos < maxIntentos)
                {
                    int direccion = random.Next(0, 2); // 0: horizontal, 1: vertical

                    if (direccion == 0)
                    {
                        colocada = ColocarHorizontal(palabraActual);
                    }
                    else
                    {
                        colocada = ColocarVertical(palabraActual);
                    }

                    if (colocada)
                    {
                        cantpal--;
                    }

                    intentos++;
                }

                if (!colocada)
                {
                    Console.WriteLine($"No se pudo colocar la palabra: {palabraActual} después de {maxIntentos} intentos.");
                }
            }
        }

      // Método de depuración que imprime solo las palabras y sus pistas



public void ImprimirSopa()
{

    for (int i = 0; i < tamano; i++)
    {
        for (int j = 0; j < tamano; j++)
        {
            if (Encontrar(i, j))
                Console.ForegroundColor = ConsoleColor.Blue;
            else
                Console.ForegroundColor = ConsoleColor.Gray;

            Console.Write(sopa[i, j] + " ");
        }
        Console.WriteLine();
    }

    // Llamar a la función de depuración para verificar las palabras y posiciones después del Clear
    
}

        private bool ColocarHorizontal(string palabra)
        {
            int fila = random.Next(0, tamano);
            int colInicio = random.Next(0, tamano - palabra.Length + 1);

            // Verificar que no haya colisiones con otras palabras
            for (int i = 0; i < palabra.Length; i++)
            {
                if (sopa[fila, colInicio + i] != '-' && sopa[fila, colInicio + i] != palabra[i])
                {
                    return false;
                }
            }

            List<(int, int)> posiciones = new List<(int, int)>();
            // Colocar la palabra en la matriz
            for (int j = 0; j < palabra.Length; j++)
            {
                sopa[fila, colInicio + j] = palabra[j];
                posiciones.Add((fila, colInicio + j));
            }

            palabrasPosiciones[palabra] = posiciones; // CORRECCIÓN: almacenar las posiciones
            return true;
        }

        private bool ColocarVertical(string palabra)
        {
            int col = random.Next(0, tamano);
            int filaInicio = random.Next(0, tamano - palabra.Length + 1);

            // Verificar que no haya colisiones con otras palabras
            for (int i = 0; i < palabra.Length; i++)
            {
                if (sopa[filaInicio + i, col] != '-' && sopa[filaInicio + i, col] != palabra[i])
                {
                    return false;
                }
            }

            List<(int, int)> posiciones = new List<(int, int)>(); // CORRECCIÓN: almacenar las posiciones
            // Colocar la palabra en la matriz
            for (int i = 0; i < palabra.Length; i++)
            {
                sopa[filaInicio + i, col] = palabra[i];
                posiciones.Add((filaInicio + i, col)); // CORRECCIÓN: añadir posiciones
            }

            palabrasPosiciones[palabra] = posiciones; // CORRECCIÓN: almacenar posiciones
            return true;
        }

        public void pista()
        {
            foreach (DictionaryEntry entry in diccionario) // Recorremos el diccionario
            {
                string palabra = entry.Key.ToString();
                string pista = entry.Value.ToString();

                Console.WriteLine($"{palabra}: {pista}");

                // Guarda la posición de la pista y el texto impreso
                pistasImpresas.Add((filaActual, $"{palabra}: {pista}"));
                filaActual++;
            }
        }

        public bool Encontrar(int fila, int columna)
        {
            foreach (var palabra in respuetasEncontradas)
            {
                List<(int, int)> posiciones = (List<(int, int)>)palabrasPosiciones[palabra];
                if (posiciones.Contains((fila, columna)))
                {
                    
                    return true;
                }
            }
            return false;
        }

        public bool PintarPalabra(string palabra)
{
    // Verificar si la palabra ya fue encontrada previamente
    if (respuetasEncontradas.Contains(palabra))
    {
        // Ya se encontró esta palabra, no hacer nada
        return false;
    }

    // Verificar si la palabra existe en la lista de palabras colocadas
    if (palabrasPosiciones.ContainsKey(palabra))
    {
        // Marcar la palabra como encontrada
        respuetasEncontradas.Add(palabra);

        // Restar del contador solo si es una nueva palabra encontrada
        palabrarest--;
        return true;
    }

    return false;
}

}// SopaFunciones
}// Funciones