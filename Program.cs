using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleEngine_1
{
    class Program
    {
        static void Main(string[] args)
        {
            //INITIALISATION DE LA MATRICE 
            int[,] GameMatrix = new int[20, 20];
            for (int y = 0; y < GameMatrix.GetLength(0); y++)
            {
                for (int x = 0; x < GameMatrix.GetLength(1); x++)
                {
                    GameMatrix[y, x] = 0;
                }
            }


            int[] CameraAnchor = new int[] { 0, 0 };
            int ViewDistance = 11;

            bool GameRunning = true;

            ConsoleKeyInfo Input;
            int[] Dir = new int[] { 0, 0 };
            int[] PlayerPos = new int[] { ViewDistance / 2, ViewDistance / 2 };
            int PlayerOn = GameMatrix[PlayerPos[0], PlayerPos[1]];



            //SPRITE
            GameMatrix[3, 4] = 5;
            GameMatrix[8, 0] = 5;
            GameMatrix[0, 0] = 5;

            while (GameRunning)
            {
                //COLLISION
                if (CheckIndex(PlayerPos[0] + Dir[0], PlayerPos[1] + Dir[1], GameMatrix))
                {
                    if (GameMatrix[PlayerPos[0] + Dir[0], PlayerPos[1] + Dir[1]] != 5)
                    {
                        //CAMERA
                        CameraAnchor[0] += Dir[0];
                        CameraAnchor[1] += Dir[1];

                        //PLAYER
                        if (CheckIndex(PlayerPos[0], PlayerPos[1], GameMatrix))
                        {
                            GameMatrix[PlayerPos[0], PlayerPos[1]] = PlayerOn;
                        }
                        PlayerPos[0] += Dir[0];
                        PlayerPos[1] += Dir[1];
                        if (CheckIndex(PlayerPos[0], PlayerPos[1], GameMatrix))
                        {
                            PlayerOn = GameMatrix[PlayerPos[0], PlayerPos[1]];
                            GameMatrix[PlayerPos[0], PlayerPos[1]] = 1;
                        }
                    }
                }


                


                //AFFICHAGE
                for (int y = 0; y < ViewDistance; y++)
                {
                    for (int x = 0; x < ViewDistance; x++)
                    {
                        //DETERMINATION DE LA COULEUR
                        int texel_y = CameraAnchor[0] + y;
                        int texel_x = CameraAnchor[1] + x;
                        if (texel_y < 0 || texel_y >= GameMatrix.GetLength(0) || texel_x < 0 || texel_x >= GameMatrix.GetLength(1))
                        {
                            Console.BackgroundColor = ConsoleColor.Magenta;
                        } else
                        {
                            switch (GameMatrix[CameraAnchor[0] + y, CameraAnchor[1] + x])
                            {
                                case 0:
                                    //FLOOR
                                    Console.BackgroundColor = ConsoleColor.Green;
                                    break;
                                case 5:
                                    //WALL
                                    Console.BackgroundColor = ConsoleColor.DarkGray;
                                    break;
                                case 1:
                                    //PLAYER
                                    Console.BackgroundColor = ConsoleColor.Blue;
                                    break;
                                default:
                                    Console.BackgroundColor = ConsoleColor.DarkMagenta;
                                    break;
                            }
                        }

                        Console.SetCursorPosition(x * 4, y * 2);
                        Console.WriteLine(" ");
                        Console.SetCursorPosition(x * 4, y * 2 + 1);
                        Console.WriteLine(" ");

                        Console.SetCursorPosition(x * 4 + 1, y * 2);
                        Console.WriteLine(" ");
                        Console.SetCursorPosition(x * 4 + 1, y * 2 + 1);
                        Console.WriteLine(" ");

                        Console.SetCursorPosition(x * 4 + 2, y * 2);
                        Console.WriteLine(" ");
                        Console.SetCursorPosition(x * 4 + 2, y * 2 + 1);
                        Console.WriteLine(" ");

                        Console.SetCursorPosition(x * 4 + 3, y * 2);
                        Console.WriteLine(" ");
                        Console.SetCursorPosition(x * 4 + 3, y * 2 + 1);
                        Console.WriteLine(" ");

                    }
                }

                //GET NEW DIR
                Input = Console.ReadKey(true);
                switch (Input.Key)
                {
                    case ConsoleKey.LeftArrow:
                        Dir[0] = 0;
                        Dir[1] = -1;
                        break;
                    case ConsoleKey.RightArrow:
                        Dir[0] = 0;
                        Dir[1] = 1;
                        break;
                    case ConsoleKey.UpArrow:
                        Dir[0] = -1;
                        Dir[1] = 0;
                        break;
                    case ConsoleKey.DownArrow:
                        Dir[0] = 1;
                        Dir[1] = 0;
                        break;
                    case ConsoleKey.R:
                        //RESET
                        Dir[0] = 0;
                        Dir[1] = 0;
                        CameraAnchor[0] = 0;
                        CameraAnchor[1] = 0;
                        GameMatrix[PlayerPos[0], PlayerPos[1]] = PlayerOn;
                        PlayerOn = GameMatrix[ViewDistance / 2, ViewDistance / 2];
                        PlayerPos[0] = ViewDistance / 2;
                        PlayerPos[1] = ViewDistance / 2;
                        break;
                    case ConsoleKey.E:
                        //END
                        GameRunning = false;
                        break;
                    default:
                        Dir[0] = 0;
                        Dir[1] = 0;
                        break;
                }

            }
            

            Console.BackgroundColor = ConsoleColor.Black;
            Console.WriteLine("\n");


        }

        static bool CheckIndex(int y, int x, int[,] GameMatrix)
        {
            if (y >= 0 && y < GameMatrix.GetLength(0) && x >= 0 && x < GameMatrix.GetLength(1))
            {
                return true;
            } else
            {
                return false;
            }
        }
    }
}
