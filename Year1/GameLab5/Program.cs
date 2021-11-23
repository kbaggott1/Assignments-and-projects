using System;
using System.IO;
using static System.Console;
namespace GameLab5
{
    class Program
    {

        static void Main(string[] args)
        {
            bool gameover = false;
            Controller Player = new Controller();          
            Console.CursorVisible = false;
            bool haveSpawned = false;
            Alien[] aliens = new Alien[4];
            // draw "O" before loop and reset default x and y for player controller
            while (!gameover)
            {
                playLevel(getLevel(), haveSpawned, ref aliens);
                Player.Controls();
            }
            


        }

        static void saveLevel(int currentLevel)
        {
            TextWriter save = new StreamWriter("save.txt");
            save.Flush();
            save.WriteLine(currentLevel);
            save.Close();
        }

        static int getLevel()
        {
            int LeveltoReturn;
            TextReader insaved = new StreamReader("save.txt");
            LeveltoReturn = Convert.ToInt32(insaved.ReadLine());
            insaved.Close();
            return LeveltoReturn;
        }

        static void playLevel(int currentLevel, bool haveSpawned, ref Alien[] aliens)
        {

            switch(currentLevel)
            {
                case 1:
                    if (!haveSpawned)
                    {
                        aliens[0].StartX = 10;
                        aliens[0].StartY = 10;
                        aliens[1].StartX = 12;
                        aliens[1].StartY = 14;
                        aliens[2].StartX = 16;
                        aliens[2].StartY = 14;

                        foreach (Alien a in aliens)
                        {
                            a.drawAlien();
                        }

                        haveSpawned = true; //***********end of level change this **********
                    }
                    break;
                    
                case 2:

                    break;

                case 3:

                    break;
            }
        }

    }
}
