using System;
using System.Collections.Generic;
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
            List<Alien> aliens = new List<Alien>();


            Player.DrawStart(3, 13);
            // draw "O" before loop and reset default x and y for player controller
            while (!gameover)
            {
                playLevel(getLevel(), ref haveSpawned, ref aliens);
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

        static void playLevel(int currentLevel, ref bool haveSpawned, ref List<Alien> aliens)
        {
            int i = 0;

            switch(currentLevel)
            {
                case 1:
                    if (!haveSpawned)
                    {
                        Alien alien0 = new Alien();
                        Alien alien1 = new Alien();
                        Alien alien2 = new Alien();
                        aliens.Add(alien0);
                        aliens.Add(alien1);
                        aliens.Add(alien2);

                        aliens[0].StartX = 10;
                        aliens[0].StartY = 10;
                        aliens[1].StartX = 12;
                        aliens[1].StartY = 12;
                        aliens[2].StartX = 14;
                        aliens[2].StartY = 14;
                        foreach (Alien a in aliens)
                        {
                            a.drawAlien();
                        }

                        haveSpawned = true; //***********end of level change this **********
                    }
                    else
                    {
                        foreach (Alien a in aliens)
                        {
                            if (a.isDead)
                                i++;
                            if (i == aliens.Count)
                                saveLevel(2);
                            
                        }
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
