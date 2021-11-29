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
            //Score Idea: Score goes down everytime you miss an alien

            bool gameover = false;
            Controller Player = new Controller();          
            Console.CursorVisible = false;
            bool haveSpawned = false;
            List<Alien> aliens = new List<Alien>();

            int alienDeathCounter = 0;
            int score = 0;
            int level = getLevel(); //If i dont put getLevel() in a variable than the GC has to work constantly, I doubt thats good but idk

            Console.SetCursorPosition(0, 1);
            for (int i = 0; i < Console.WindowWidth; i++)
            {
                Console.Write("_");
            }

            Player.StartPos(3, 15);
            updateHeader(level, score, Player.Lives);

            while (!gameover)
            {
                if (!haveSpawned)
                    level = getLevel();
                playLevel(level, ref haveSpawned, ref aliens, ref alienDeathCounter);
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
