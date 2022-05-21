using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;




namespace zmeyka
{
    class Program
    {
        static int foodX;
        static int foodY;

        static void SpawnFood()
        {
            Random rnd = new Random();
            foodX = rnd.Next(0, 118);
            if (foodX % 2 != 0) foodX += 1;

            foodY = rnd.Next(3, 40);
        }      
               
       
        static void Main(string[] args)
        {
            Console.SetBufferSize(120, 40);
            Console.SetWindowSize(120, 40);
            Console.CursorVisible = false;

            int[] PlayerX = new int[200];
            int[] PlayerY = new int[200];
            PlayerX[0]= 30;
            PlayerY[0] = 30;
            int PlayerDir = 0;
            int speed = 250;
            int snakesize = 1;

            for (int i = 1; i < snakesize; i++)
            {
                PlayerX[i] = PlayerX[i - 1];
                PlayerY[i] = PlayerY[i - 1] + 1;
            }

            foodX = 60;
            foodY = 20;

            bool play = true;
            bool error = false;

            Console.SetCursorPosition(0, 2);
            Console.Write("________________________________________________________________________________________________________________________");

            while (true)
            {
                if (play == true)
                {
                    Console.SetCursorPosition(PlayerX[snakesize - 1], PlayerY[snakesize - 1]);
                    Console.Write("  ");

                    Console.SetCursorPosition(foodX, foodY);
                    Console.Write("  ");

                    if (Console.KeyAvailable == true)
                    {
                        ConsoleKeyInfo key;
                        Console.SetCursorPosition(0, 0);
                        key = Console.ReadKey();
                        Console.SetCursorPosition(0, 0);
                        Console.Write("  ");

                        if (key.Key == ConsoleKey.W)
                        {
                            if (PlayerDir != 2) PlayerDir = 0;
                            else PlayerDir = 2;
                        }

                        if (key.Key == ConsoleKey.A)
                        {
                            if (PlayerDir != 3) PlayerDir = 1;
                            else PlayerDir = 3;
                        }

                        if (key.Key == ConsoleKey.S)
                        {
                            if (PlayerDir != 0) PlayerDir = 2;
                            else PlayerDir = 0;
                        }

                        if (key.Key == ConsoleKey.D)
                        {
                            if (PlayerDir != 1) PlayerDir = 3;
                            else PlayerDir = 1;
                        }

                    }

                    if (snakesize > 1)
                    {
                        for (int i = snakesize - 1; i > 0; i--)
                        {
                            PlayerX[i] = PlayerX[i - 1];
                            PlayerY[i] = PlayerY[i - 1];
                        }

                    }


                    if (PlayerDir == 0) PlayerY[0] -= 1;
                    if (PlayerDir == 1) PlayerX[0] -= 2;
                    if (PlayerDir == 2) PlayerY[0] += 1;
                    if (PlayerDir == 3) PlayerX[0] += 2;

                    if (PlayerX[0] == 0 && PlayerDir == 1) PlayerX[0] = 118;
                    if (PlayerX[0] == 120 && PlayerDir == 3) PlayerX[0] = 0;
                    if (PlayerY[0] == 2 && PlayerDir == 0) PlayerY[0] = 39;
                    if (PlayerY[0] == 40 && PlayerDir == 2) PlayerY[0] = 3;

                    if (PlayerX[0] == foodX && PlayerY[0] == foodY)
                    {
                        while (true) 
                        {
                            SpawnFood();
                            error = false;

                            for (int i = 0; i < snakesize-1; i++)
                            {
                                if (PlayerX[i] == foodX && PlayerY[i] == foodY) error = true;
                            }

                            if (error == false) break;
                        }
                        
                        snakesize += 1;
                        
                        if (speed > 30) speed -= 15;


                    }
                }

                for (int i = 1; i < snakesize-1; i++)
                {
                    if (PlayerX[0] == PlayerX[i] && PlayerY[0] == PlayerY[i]) play = false;                   

                }

                if (play == false)
                {
                    speed = 10;
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.SetCursorPosition(40, 0);
                    Console.Write("                                       ");
                    Console.SetCursorPosition(40, 0);
                    Console.Write("  Столкновение! Игра окончена.");
                    Console.SetCursorPosition(40, 1);
                    Console.Write("                                       ");
                    Console.SetCursorPosition(40, 1);
                    Console.Write("ДЛЯ ПЕРЕЗАПУСКА ИГРЫ НАЖМИТЕ \"R\"");


                    if (Console.KeyAvailable == true)
                    {
                        ConsoleKeyInfo key;
                        Console.SetCursorPosition(0, 0);
                        key = Console.ReadKey();
                        Console.SetCursorPosition(0, 0);
                        Console.Write("  ");

                        if (key.Key == ConsoleKey.R)
                        {
                            Console.Clear();
                            Console.ForegroundColor = ConsoleColor.White;
                            for (int i = 1; i < snakesize; i++) 
                            {
                                PlayerX[i] = 0;
                                PlayerY[i] = 0;
                            }
                            play = true;
                            snakesize =1;
                            PlayerX[0]= 30;
                            PlayerY[0] = 30;
                            PlayerDir = 0;
                            speed = 250;
                            SpawnFood();
                            Console.SetCursorPosition(0, 2);
                            Console.Write("________________________________________________________________________________________________________________________");
                        }
                    }
                }

                for (int i = 0; i < snakesize; i++)
                {
                    if (PlayerY[i] != 0) 
                    {
                        Console.SetCursorPosition(PlayerX[i], PlayerY[i]);
                        Console.Write("██");
                    }
                    
                }                               

                Console.SetCursorPosition(foodX, foodY);
                Console.Write("██");

                Console.SetCursorPosition(92, 1);
                Console.Write("                      ");
                Console.SetCursorPosition(92, 1);
                Console.Write("Размер вашей змейки: " + snakesize);

                System.Threading.Thread.Sleep(speed);
            }

            


        }
    }
}
