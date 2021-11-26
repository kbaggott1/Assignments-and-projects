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
            int deathCounter = 0;



            Player.StartPos(3, 13);

            while (!gameover)
            {
                playLevel(getLevel(), ref haveSpawned, ref aliens, ref deathCounter);
                Player.Controls();
                Player.SendAliens(aliens);
            }
            
        }

        static void gotoLevel(int currentLevel)
        {
            TextWriter save = new StreamWriter("save.txt"); //FIX, garbage collecter is constantly working to delete object "save"
            save.Flush();
            save.WriteLine(currentLevel);
            save.Close();
        }

        static int getLevel()
        {
            int LeveltoReturn;
            TextReader insaved = new StreamReader("save.txt"); //FIX, garbage collecter is constantly working to delete object "insaved"
            LeveltoReturn = Convert.ToInt32(insaved.ReadLine());
            insaved.Close();
            return LeveltoReturn;
        }

        static void playLevel(int currentLevel, ref bool haveSpawned, ref List<Alien> aliens, ref int deathCounter)
        {           
            int LvlAlienCount;

            switch(currentLevel)
            {
                case 1:
                    LvlAlienCount = 3;
                    if (!haveSpawned)
                    {
                        aliens.Add(new Alien());
                        aliens.Add(new Alien());
                        aliens.Add(new Alien());

                        aliens[0].x = 10; //THESE NEED TO BE CHANGED
                        aliens[0].y = 10;

                        aliens[1].x = 12;
                        aliens[1].y = 12;

                        aliens[2].x = 14;
                        aliens[2].y = 14;

                        foreach (Alien a in aliens)
                        {
                            a.drawAlien();

                        }

                        haveSpawned = true; //***********end of level change this **********
                    }
                    else //Where level loops
                    {
                        foreach (Alien a in aliens.ToArray())
                        {
                            if (a.isDead)
                            {
                                deathCounter++;
                                aliens.Remove(a);
                            }
                            else //This else is essential to erase dead
                            {
                                a.drawAlien();
                            }

                            if (deathCounter == LvlAlienCount)
                            {
                                deathCounter = 0;
                                //gotoLevel(2);
                                //haveSpawned = false;
                                Console.SetCursorPosition(0, 0);
                                Console.Write("Level complete (placeholder text)");
                                break;
                            }                                                          
                        }

                        foreach (Alien a in aliens)
                        {
                            
                            a.attack();
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
