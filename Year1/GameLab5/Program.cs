using System;
using System.Collections.Generic;
using System.IO;
using static System.Console;
namespace GameLab5
{
    class Program
    {
        static Controller Player = new Controller();
        static void Main(string[] args)
        {
            //Score Idea: Score goes down everytime you miss an alien

            bool gameover = false;        
            Console.CursorVisible = false;
            bool haveSpawned = false;
            List<Alien> aliens = new List<Alien>();


            int score = 0;
            int level = getLevel(); //If i dont put getLevel() in a variable than the GC has to work constantly, I doubt thats good but idk

            Console.SetCursorPosition(0, 1);
            for (int i = 0; i < Console.WindowWidth; i++)
            {
                Console.Write("_");
            }

            Player.StartPos(3, 15);
            

            while (!gameover)
            {
                if (!haveSpawned)
                    level = getLevel();
                updateHeader(level, score, Player.Lives);
                playLevel(level, ref haveSpawned, ref aliens);
                Player.Controls();
                Player.SendAliens(aliens);
                if (Player.Lives < 1)
                    gameover = true;
                                              
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

        static void playLevel(int currentLevel, ref bool haveSpawned, ref List<Alien> aliens)
        {           
            int LvlAlienCount;
            int deathCounter = 0;

            switch(currentLevel)
            {
                case 1:
                    LvlAlienCount = 3;
                    if (!haveSpawned)
                    {
                        aliens.Add(new Alien());
                        aliens.Add(new Alien());
                        aliens.Add(new Alien());

                        aliens[0].x = 60; //THESE NEED TO BE CHANGED
                        aliens[0].y = 7;

                        aliens[1].x = 50;
                        aliens[1].y = 15;

                        aliens[2].x = 60;
                        aliens[2].y = 23;

                        foreach (Alien a in aliens)
                        {
                            a.drawAlien();

                        }

                        haveSpawned = true; //***********end of level change this **********
                    }
                    else //Where level loops
                    {
                        foreach (Alien a in aliens)
                        {
                            if (a.isDead)
                            {
                                deathCounter++;
                            }
                            else 
                            {
                                a.drawAlien();
                            }

                            if (deathCounter == LvlAlienCount)
                            {
                                deathCounter = 0;
                                
                                //gotoLevel(2);
                                //haveSpawned = false;

                                foreach(Alien alien in aliens.ToArray())
                                {
                                    aliens.Remove(alien);
                                }
                                Console.SetCursorPosition(0, 0);
                                Console.Write("Level complete (placeholder text)"); //Replace this with maybe blinkining level two
                                break;
                            }                                                          
                        }

                        foreach (Alien a in aliens)
                        {                            
                            if (a.attack(Player.x, Player.y))
                            {
                                Player.Lives--;
                                Player.Redraw();
                            }                            
                                
                            
                        }
                    }
                    break;
                    
                case 2:
                    
                    break;

                case 3:

                    break;
            }
        }

        public static void updateHeader(int Level, int score, int lives)
        {
            Console.SetCursorPosition(10, 0);
            Console.Write("Level: " + Level);
            Console.SetCursorPosition(55, 0);
            Console.Write("Score: " + score);
            Console.SetCursorPosition(100, 0);
            Console.Write("Lives: " + lives);
        }
    }
}
