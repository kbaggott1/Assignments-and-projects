using System;
using System.Collections.Generic;
using System.IO;
using static System.Console;
using System.Xml;
namespace GameLab5
{
    class Program
    {
        static Controller Player = new Controller();
        static XmlDocument doc = new XmlDocument();
        static void Main(string[] args)
        {
            //Score Idea: Score goes down everytime you miss an alien
            doc.Load("../../../GameSave.xml");
            bool gameover = false;        
            Console.CursorVisible = false;
            bool haveSpawned = false;
            List<Alien> aliens = new List<Alien>();


            int score = 0;
            int oldScore = 0;
            int level = getLevel(); //If i dont put getLevel() in a variable than the GC has to work constantly, I doubt thats good but idk


            MainMenu();
            

            while (!gameover)
            {
                if (!haveSpawned)
                    level = getLevel();
                updateHeader(level, score, ref oldScore, Player.Lives);
                playLevel(level, ref haveSpawned, ref aliens);
                Player.Controls(ref score);
                Player.SendAliens(aliens);
                if (Player.Lives < 1)
                    gameover = true;
                                              
            }
            
        }

        static void gotoLevel(int currentLevel)
        {
            doc.Load("../../../GameSave.xml");
            //var Levels = doc.GetElementsByTagName("level"); use this for highscore check and save
            XmlNode selectedNode;
            selectedNode = doc.SelectSingleNode("save/currentLevel");
            selectedNode.InnerText = currentLevel.ToString();
            doc.Save("../../../GameSave.xml");
            
        }

        static int getLevel()
        {
            doc.Load("../../../GameSave.xml");
            int LeveltoReturn;
            XmlNode Level = doc.SelectSingleNode("save/currentLevel"); //FIX, garbage collecter is constantly working to delete object "insaved"
            LeveltoReturn = Convert.ToInt32(Level.InnerText);
            
            return LeveltoReturn;
        }

        static void playLevel(int currentLevel, ref bool haveSpawned, ref List<Alien> aliens)
        {
            int LvlAlienCount;
            int deathCounter = 0;
            int MinXSpawn = 30;
            int MinYSpawn = 5;
            Random pos = new Random();
            stopwatch shoot = new stopwatch(); 

            switch (currentLevel)
            {
                case 1:
                    LvlAlienCount = 3;
                    if (!haveSpawned)
                    {
                        aliens.Add(new Alien());
                        aliens.Add(new Alien());
                        aliens.Add(new Alien());

                        aliens[0].x = pos.Next(MinXSpawn, WindowWidth - 1); //THESE NEED TO BE CHANGED
                        aliens[0].y = pos.Next(MinYSpawn, 10);

                        aliens[1].x = pos.Next(MinXSpawn, WindowWidth - 1);
                        aliens[1].y = pos.Next(11, 20);

                        aliens[2].x = pos.Next(MinXSpawn, WindowWidth - 1);
                        aliens[2].y = pos.Next(21, 29);

                        foreach (Alien a in aliens)
                        {
                            a.drawAlien();

                        }
                        Player.StartPos(10, 15);
                        haveSpawned = true; //***********end of level change this **********
                    }
                    else //Where level loops
                    {
                        foreach (Alien a in aliens)
                        {
                            if (a.isDead)
                            {
                                deathCounter++; //Cant remove aliens yet because the last bullet they shot would stop moving
                            }
                            else 
                            {
                                a.drawAlien();
                            }

                            if (deathCounter == LvlAlienCount)
                            {
                                deathCounter = 0;

                                gotoLevel(2);
                                haveSpawned = false;

                                foreach (Alien alien in aliens.ToArray())
                                {
                                    aliens.Remove(alien);
                                }
                                Clear();
                                
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
                    LvlAlienCount = 4;
                    if (!haveSpawned)
                    {
                        aliens.Add(new Alien());
                        aliens.Add(new Alien());
                        aliens.Add(new Alien());
                        aliens.Add(new Alien());

                        aliens[0].x = pos.Next(MinXSpawn, WindowWidth - 1); //THESE NEED TO BE CHANGED
                        aliens[0].y = pos.Next(MinYSpawn, 7);

                        aliens[1].x = pos.Next(MinXSpawn, WindowWidth - 1);
                        aliens[1].y = pos.Next(8, 15);

                        aliens[2].x = pos.Next(MinXSpawn, WindowWidth - 1);
                        aliens[2].y = pos.Next(16, 22);

                        aliens[3].x = pos.Next(MinXSpawn, WindowWidth - 1);
                        aliens[3].y = pos.Next(23, 29);

                        foreach (Alien a in aliens)
                        {
                            a.drawAlien();

                        }
                        Player.StartPos(10, 15);
                        haveSpawned = true; //***********end of level change this **********
                    }
                    else //Where level loops
                    {
                        foreach (Alien a in aliens)
                        {
                            if (a.isDead)
                            {
                                deathCounter++; //Cant remove aliens yet because the last bullet they shot would stop moving
                            }
                            else
                            {
                                a.drawAlien();
                            }

                            if (deathCounter == LvlAlienCount)
                            {
                                deathCounter = 0;

                                gotoLevel(3);
                                haveSpawned = false;

                                foreach (Alien alien in aliens.ToArray())
                                {
                                    aliens.Remove(alien);
                                }
                                Clear();

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

                case 3:

                    break;
            }
        }

        public static void updateHeader(int Level, int score, ref int oldScore, int lives)
        {
            Console.SetCursorPosition(0, 1);
            for (int i = 0; i < Console.WindowWidth; i++)
            {
                Console.Write("_");
            }

            Console.SetCursorPosition(10, 0);
            Console.Write("Level: " + Level);

            if (score != oldScore)
            {
                for (int i = 62; i < 65; i++)
                {
                    Console.SetCursorPosition(i, 0);
                    Console.Write(" ");
                }
                oldScore = score;
            }

            Console.SetCursorPosition(55, 0);
            Console.Write("Score: " + score);

            Console.SetCursorPosition(100, 0);
            Console.Write("Lives: " + lives);
        }

        public static void MainMenu()
        {
            bool flag = true;
            //Stars:
            Random RandomPos = new Random();
            Console.ForegroundColor = ConsoleColor.White;
            for (int i = 0; i < 40; i++)
            {
                SetCursorPosition(RandomPos.Next(2, 118), RandomPos.Next(0, 9));
                Console.Write("*");
            }


            //
            



            while (flag)
            {
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.SetCursorPosition(10,10);
                Console.Write("░█████╗░██╗░░░░░██╗███████╗███╗░░██╗██╗░██████╗  ░█████╗░████████╗████████╗░█████╗░░█████╗░██╗░░██╗");
                Console.SetCursorPosition(10, 11);
                Console.Write("██╔══██╗██║░░░░░██║██╔════╝████╗░██║╚█║██╔════╝  ██╔══██╗╚══██╔══╝╚══██╔══╝██╔══██╗██╔══██╗██║░██╔╝");
                Console.SetCursorPosition(10, 12);
                Console.Write("███████║██║░░░░░██║█████╗░░██╔██╗██║░╚╝╚█████╗░  ███████║░░░██║░░░░░░██║░░░███████║██║░░╚═╝█████═╝░");
                Console.SetCursorPosition(10, 13);
                Console.Write("██╔══██║██║░░░░░██║██╔══╝░░██║╚████║░░░░╚═══██╗  ██╔══██║░░░██║░░░░░░██║░░░██╔══██║██║░░██╗██╔═██╗░");
                Console.SetCursorPosition(10, 14);
                Console.Write("██║░░██║███████╗██║███████╗██║░╚███║░░░██████╔╝  ██║░░██║░░░██║░░░░░░██║░░░██║░░██║╚█████╔╝██║░╚██╗");
                Console.SetCursorPosition(10, 15);
                Console.Write("╚═╝░░╚═╝╚══════╝╚═╝╚══════╝╚═╝░░╚══╝░░░╚═════╝░  ╚═╝░░╚═╝░░░╚═╝░░░░░░╚═╝░░░╚═╝░░╚═╝░╚════╝░╚═╝░░╚═╝");
                Console.SetCursorPosition(18, 17);
                Console.Write("1. Play");
                Console.SetCursorPosition(50, 17);
                Console.Write("2. Instructions");
                Console.SetCursorPosition(90, 17);
                Console.Write("3. Quit");
                Console.ForegroundColor = ConsoleColor.White;
                for (int i = 0; i < 55; i++)
                {
                    SetCursorPosition(RandomPos.Next(2, 118), RandomPos.Next(20, 29));
                    Console.Write("*");
                }
                Console.ReadLine();
            }
        }
    }
}
