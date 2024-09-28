using System;
using System.Security.Cryptography.X509Certificates;
using System.Collections;
using Funciones;

class Sopa
{
    public static void Main(string[] args)
    {
        Menu:
        Console.WriteLine("SELECCIONA LA DIFICULTAD" + 
                        "\n1. EASY"+
                        "\n2. MEDIUM"+
                        "\n3. HARD"+
                        "\n0. SALIR");
        int nivel = int.Parse(Console.ReadLine());

        switch (nivel)
        {
            case 0:
            break;

            case 1:
                sopaFunciones easy = new sopaFunciones(20,15);
                easy.cargarPalabras("C:\\sopa\\easy.txt", 15);
                easy.llenarSopa();
                while(true){
                if (easy.palabrarest == 0){
                    Console.WriteLine("Excelente, has ganado mi amor");
                    goto Menu;
                } 
                easy.ImprimirSopa();

                Console.Write($"Palabras restantes:{easy.palabrarest} ");
                string respuesta = Console.ReadLine().ToLower();
                easy.PintarPalabra(respuesta);
                Console.Clear();
                }
                break;

            case 2:
                sopaFunciones medium = new sopaFunciones(30,20);
                medium.cargarPalabras("C:\\sopa\\medium.txt", 20);
                medium.llenarSopa();
                while(true){
                if (medium.palabrarest == 0){
                    Console.WriteLine("Excelente, has ganado bebe");
                    goto Menu;
                } 
                medium.ImprimirSopa();

                Console.Write($"Palabras restantes:{medium.palabrarest} ");
                string respuesta = Console.ReadLine().ToLower();
                medium.PintarPalabra(respuesta);
                Console.Clear();
                }
                break;

            case 3:
            sopaFunciones hard = new sopaFunciones(50,30);
                hard.cargarPalabras("C:\\sopa\\hard.txt", 30);
                hard.llenarSopa();
                while(true){
                if (hard.palabrarest == 0){
                    Console.WriteLine("Excelente, has ganado bebe");
                    goto Menu;
                } 
                hard.ImprimirSopa();

                Console.Write($"Palabras restantes:{hard.palabrarest} ");
                string respuesta = Console.ReadLine().ToLower();
                hard.PintarPalabra(respuesta);
                Console.Clear();
                }
                break;

            default:
                Console.WriteLine("Nivel no reconocido.");
                return;
        }//Switch
    }//main
}//Class sopa