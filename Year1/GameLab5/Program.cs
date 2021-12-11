using System;
using System.Collections.Generic;
using System.IO;
using static System.Console;
using System.Xml;
using NAudio.Wave;

namespace GameLab5
{
    class Program
    {

        static Controller Player = new Controller();
        static XmlDocument doc = new XmlDocument();
        static string[] LoadMenu = new string[] { "", "", "", "", "", "" };
        static int EndlessCounter = 50;
        static WaveOutEvent waveOut = new WaveOutEvent();
        static WaveFileReader song = new WaveFileReader("./Resources/Chiptune_Frontiers.wav");

        static void Main(string[] args)
        {
            CursorVisible = false;
            bool flag = true;
            Random RandomPos = new Random();
            bool MenuHasSpawned = false;
            int Selection = 0;
            waveOut.Init(song);
            waveOut.Play();


            while (flag)
            {
                
                if (!MenuHasSpawned)
                {
                    Selection = 0;
                    Clear();
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    Console.SetCursorPosition(10, 10);
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
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.SetCursorPosition(53, 17);
                    Console.Write("New Game");
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    Console.SetCursorPosition(53, 19);
                    Console.Write("Load Level");
                    Console.SetCursorPosition(53, 21);
                    Console.Write("Instructions");
                    Console.SetCursorPosition(53, 23);
                    Console.Write("Quit");

                    //Stars------------------------------------------------------------
                    Console.ForegroundColor = ConsoleColor.White;
                    for (int i = 0; i < 40; i++)
                    {
                        SetCursorPosition(RandomPos.Next(2, 118), RandomPos.Next(0, 9));
                        Console.Write("*");
                    }


                    for (int i = 0; i < 26; i++)
                    {
                        SetCursorPosition(RandomPos.Next(2, 50), RandomPos.Next(17, 29));
                        Console.Write("*");
                    }


                    for (int i = 0; i < 26; i++)
                    {
                        SetCursorPosition(RandomPos.Next(67, 118), RandomPos.Next(17, 29));
                        Console.Write("*");
                    }
                    MenuHasSpawned = true;

                }

                //Stars------------------------------------------------------------

                //MENU NAVIGATION
                string[] MainMenu = new string[] { "New Game", "Load Level", "Instructions", "Quit" };
                int[] MainMenuPos = new int[] { 17, 19, 21, 23 };
                bool enter = false;
                enter = MenuNavController(ref Selection, MainMenu, MainMenuPos);

                if (enter)
                {
                    switch (Selection)
                    {
                        case 0: // New Game
                            ClearSaved();
                            PlayGame(); //This is temp, this should reset xml
                            break;
                        case 1: //Load Level
                            ValidateLevels();
                            LoadLevelMenu();
                            break;
                        case 2: //Instructions
                            Instructions();
                            break;
                        case 3: //Quit
                            flag = false;
                            break;


                    }
                    ValidateLevels();
                    MenuHasSpawned = false;
                }
            }




        }

        static void Instructions()
        {
            Clear();
            int minX = 18, startY = 2;
            Clear();
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.SetCursorPosition(minX, startY);
            startY++;
            Console.Write("██╗░░██╗░█████╗░░██╗░░░░░░░██╗  ████████╗░█████╗░  ██████╗░██╗░░░░░░█████╗░██╗░░░██╗");
            Console.SetCursorPosition(minX, startY);
            startY++;
            Console.Write("██║░░██║██╔══██╗░██║░░██╗░░██║  ╚══██╔══╝██╔══██╗  ██╔══██╗██║░░░░░██╔══██╗╚██╗░██╔╝");
            Console.SetCursorPosition(minX, startY);
            startY++;
            Console.Write("███████║██║░░██║░╚██╗████╗██╔╝  ░░░██║░░░██║░░██║  ██████╔╝██║░░░░░███████║░╚████╔╝░");
            Console.SetCursorPosition(minX, startY);
            startY++;
            Console.Write("██╔══██║██║░░██║░░████╔═████║░  ░░░██║░░░██║░░██║  ██╔═══╝░██║░░░░░██╔══██║░░╚██╔╝░░");
            Console.SetCursorPosition(minX, startY);
            startY++;
            Console.Write("██║░░██║╚█████╔╝░░╚██╔╝░╚██╔╝░  ░░░██║░░░╚█████╔╝  ██║░░░░░███████╗██║░░██║░░░██║░░░");
            Console.SetCursorPosition(minX, startY);
            startY++;
            Console.Write("╚═╝░░╚═╝░╚════╝░░░░╚═╝░░░╚═╝░░  ░░░╚═╝░░░░╚════╝░  ╚═╝░░░░░╚══════╝╚═╝░░╚═╝░░░╚═╝░░░");

            minX = 6;
            startY = 16;
            Console.ForegroundColor = ConsoleColor.White;
            Console.SetCursorPosition(50, 12);
            Console.Write("Your spaceship: " + Player.Sprite);
            Console.SetCursorPosition(50, 13);
            Console.Write("Alien spaceship: X");
            Console.SetCursorPosition(minX, startY);
            startY++;
            Console.Write("Move with Arrow Keys and shoot with spacebar. If you hit an Alien they will lose one life and shrink");
            Console.SetCursorPosition(minX, startY);
            startY++;
            Console.Write("they each have 2 lives. If you hit an alien you get 5 points but if you miss your score will go down by 1 point.");
            Console.SetCursorPosition(minX, startY);
            startY++;
            Console.Write("There are 5 levels and an endless mode, You need to complete a level to unlock the next one in the main menu");
            Console.SetCursorPosition(minX, startY);
            startY++;
            Console.Write("and you must complete all levels to unlock endless mode. All progress is saved to an xml file so you can");
            Console.SetCursorPosition(minX, startY);
            startY++;
            Console.Write("close the game and come back later to the level you left off! There are highscores for each level");
            Console.SetCursorPosition(minX, startY);
            startY += 3;
            Console.Write("and your total highscore is revealed at the end of the game. Have Fun!");
            Console.SetCursorPosition(minX, startY);
            Console.Write("Press enter to return to Main Menu.");
            Console.ReadLine();
            Clear();
        }
        static void LoadLevelMenu()
        {
            bool flag = true;
            int[] LoadMenuPos = new int[] { 17, 19, 21, 23, 25, 27 };
            doc.Load("./Resources/GameSave.xml");
            XmlNodeList levels = doc.GetElementsByTagName("level");

            for (int i = 0; i < LoadMenuPos.Length; i++)
            {
                if (i == 0)
                    Console.ForegroundColor = ConsoleColor.White;
                else
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.SetCursorPosition(53, LoadMenuPos[i]);
                if (LoadMenu[i] == "")
                    Console.Write("               ");
                else
                    if (i == 0)
                    Console.Write(LoadMenu[i] + " HighScore: " + levels[i].LastChild.InnerText);
                else
                    Console.Write(LoadMenu[i]);
            }

            int Selection = 0;
            bool enter = false;
            while (flag)
            {
                enter = MenuNavController(ref Selection, LoadMenu, LoadMenuPos);
                if (enter)
                {
                    flag = false;
                    switch (Selection)
                    {
                        case 0:
                            SetLevelTo(1, false);
                            break;
                        case 1:
                            SetLevelTo(2, false);
                            break;
                        case 2:
                            SetLevelTo(3, false);
                            break;
                        case 3:
                            SetLevelTo(4, false);
                            break;
                        case 4:
                            SetLevelTo(5, false);
                            break;
                        case 5:
                            Player.Lives = 3;
                            SetLevelTo(6, false);
                            break;
                    }
                    PlayGame();

                }

            }

        }

        static bool MenuNavController(ref int Selection, string[] MenuOptions, int[] Placement)
        {
            doc.Load("./Resources/GameSave.xml");
            XmlNodeList levels = doc.GetElementsByTagName("level");

            int max;
            int counter = 0;
            foreach (string option in MenuOptions)
            {
                if (option != "")
                    counter++;
            }
            max = counter - 1;

            int PreviousSelection = Selection;
            ConsoleKey KeyPress;
            if (Console.KeyAvailable)
            {
                KeyPress = Console.ReadKey().Key;
                if (KeyPress == ConsoleKey.DownArrow)
                {
                    if (Selection == max)
                        Selection = 0;
                    else
                        Selection++;
                }

                if (KeyPress == ConsoleKey.UpArrow)
                {
                    if (Selection == 0)
                        Selection = max;
                    else
                        Selection--;
                }

                if (KeyPress == ConsoleKey.Enter)
                    return true;


                for (int i = 0; i < MenuOptions.Length; i++)
                {
                    Console.SetCursorPosition(53, Placement[i]);
                    if (i == Selection)
                    {
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.Write(MenuOptions[i]);
                        if (LoadMenu[i] == MenuOptions[i])
                            Console.Write(" HighScore: " + levels[i].LastChild.InnerText);
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.DarkRed;
                        Console.Write(MenuOptions[i] + "                ");
                    }
                }
            }
            return false;
        }

        static void PlayGame()
        {
            Console.ForegroundColor = ConsoleColor.White;
            Clear();
            //Score Idea: Score goes down everytime you miss an alien
            doc.Load("./Resources/GameSave.xml");
            bool gameover = false;
            Console.CursorVisible = false;
            bool haveSpawned = false;
            List<Alien> aliens = new List<Alien>();


            int score = 0;
            int oldScore = 0;
            int level = getLevel(); //If i dont put getLevel() in a variable than the GC has to work constantly, I doubt thats good but idk




            while (!gameover)
            {
                PlaySong();
                if (!haveSpawned)
                    level = getLevel();
                updateHeader(level, score, ref oldScore, Player.Lives);
                playLevel(level, ref haveSpawned, ref aliens, ref score);
                Player.Controls(ref score);
                Player.SendAliens(aliens);
                if (Player.Lives < 1)
                    gameover = true;

            }
        }

        static void PlaySong()
        {


            if (waveOut.PlaybackState.ToString() == "Stopped")
            {

                waveOut.Volume = 0.1F;
                waveOut.Play();
            }

        }
        static void ClearSaved()
        {
            doc.Load("./Resources/GameSave.xml");
            XmlNodeList Levels = doc.GetElementsByTagName("level");
            XmlNode CurrentLevel = doc.SelectSingleNode("save/currentLevel");

            CurrentLevel.InnerText = "1";

            for (int i = 1; i < Levels.Count; i++)
            {
                Levels[i].FirstChild.InnerText = "";
                Levels[i].LastChild.InnerText = "0";
                LoadMenu[i] = "";
            }
            Levels[0].LastChild.InnerText = "0";


            doc.Save("./Resources/GameSave.xml");

        }

        static void SetLevelTo(int currentLevel, bool save = true)
        {
            doc.Load("./Resources/GameSave.xml");
            XmlNodeList Levels = doc.GetElementsByTagName("level");
            XmlNode selectedNode;

            selectedNode = doc.SelectSingleNode("save/currentLevel");
            if (save)
            {
                if (currentLevel < 5)
                    Levels[Convert.ToInt32(selectedNode.InnerText)].FirstChild.InnerText = "Load Level " + currentLevel;
                else
                if (currentLevel == 5)
                    Levels[Convert.ToInt32(selectedNode.InnerText)].FirstChild.InnerText = "Boss Battle";
                else
                    Levels[Convert.ToInt32(selectedNode.InnerText)].FirstChild.InnerText = "Endless Mode";
            }


            //Change current Level
            selectedNode.InnerText = currentLevel.ToString();
            doc.Save("./Resources/GameSave.xml");


        }

        static void SaveScore(int currentLevel, int highscore)
        {
            doc.Load("./Resources/GameSave.xml");
            XmlNodeList Levels = doc.GetElementsByTagName("level");

            if (Convert.ToInt32(Levels[currentLevel - 1].LastChild.InnerText) < highscore)
                Levels[currentLevel - 1].LastChild.InnerText = highscore.ToString();
            doc.Save("./Resources/GameSave.xml");
        }

        static int getLevel()
        {
            doc.Load("./Resources/GameSave.xml");
            int LeveltoReturn;
            XmlNode Level = doc.SelectSingleNode("save/currentLevel"); //FIX, garbage collecter is constantly working to delete object "insaved"
            LeveltoReturn = Convert.ToInt32(Level.InnerText);

            return LeveltoReturn;
        }

        static void playLevel(int currentLevel, ref bool haveSpawned, ref List<Alien> aliens, ref int score)
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
                        Player.Lives = 3;
                        for (int i = 0; i < LvlAlienCount; i++)
                        {
                            aliens.Add(new Alien());
                        }

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

                                SetLevelTo(2);
                                haveSpawned = false;

                                foreach (Alien alien in aliens.ToArray())
                                {
                                    aliens.Remove(alien);
                                }
                                Clear();
                                SaveScore(currentLevel, score);
                                RoundWinScreen(currentLevel, score);
                                score = 0;

                                break;
                            }
                        }


                        foreach (Alien a in aliens)
                        {

                            if (a.attack(Player.x, Player.y))
                            {
                                Player.Lives--;
                                if (Player.Lives < 1)
                                    SaveScore(currentLevel, score);
                                Player.Redraw();
                            }


                        }
                    }
                    break;

                case 2:
                    LvlAlienCount = 4;
                    if (!haveSpawned)
                    {
                        Player.Lives = 3;
                        for (int i = 0; i < LvlAlienCount; i++)
                        {
                            aliens.Add(new Alien());
                        }

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

                                SetLevelTo(3);
                                haveSpawned = false;

                                foreach (Alien alien in aliens.ToArray())
                                {
                                    aliens.Remove(alien);
                                }
                                Clear();
                                SaveScore(currentLevel, score);
                                RoundWinScreen(currentLevel, score);
                                score = 0;

                                break;
                            }
                        }


                        foreach (Alien a in aliens)
                        {

                            if (a.attack(Player.x, Player.y))
                            {
                                Player.Lives--;
                                if (Player.Lives < 1)
                                    SaveScore(currentLevel, score);
                                Player.Redraw();
                            }


                        }
                    }
                    break;

                case 3:
                    LvlAlienCount = 3;
                    if (!haveSpawned)
                    {
                        Player.Lives = 3;
                        for (int i = 0; i < LvlAlienCount; i++)
                        {
                            aliens.Add(new Alien());
                        }

                        aliens[0].x = pos.Next(MinXSpawn, WindowWidth - 1); //THESE NEED TO BE CHANGED
                        aliens[0].y = pos.Next(MinYSpawn, 10);

                        aliens[1].x = pos.Next(MinXSpawn, WindowWidth - 1);
                        aliens[1].y = pos.Next(11, 20);

                        aliens[2].x = pos.Next(MinXSpawn, WindowWidth - 1);
                        aliens[2].y = pos.Next(21, 29);

                        foreach (Alien a in aliens)
                        {
                            a.drawAlien();
                            a.BulletSpeed = 25;

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

                                SetLevelTo(4);
                                haveSpawned = false;

                                foreach (Alien alien in aliens.ToArray())
                                {
                                    aliens.Remove(alien);
                                }
                                Clear();
                                SaveScore(currentLevel, score);
                                RoundWinScreen(currentLevel, score);
                                score = 0;

                                break;
                            }
                        }


                        foreach (Alien a in aliens)
                        {

                            if (a.attack(Player.x, Player.y))
                            {
                                Player.Lives--;
                                if (Player.Lives < 1)
                                    SaveScore(currentLevel, score);
                                Player.Redraw();
                            }


                        }
                    }
                    break;

                case 4:
                    LvlAlienCount = 4;
                    if (!haveSpawned)
                    {
                        Player.Lives = 3;
                        for (int i = 0; i < LvlAlienCount; i++)
                        {
                            aliens.Add(new Alien());
                        }

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
                            a.BulletSpeed = 25;
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

                                SetLevelTo(5);
                                haveSpawned = false;

                                foreach (Alien alien in aliens.ToArray())
                                {
                                    aliens.Remove(alien);
                                }
                                Clear();
                                SaveScore(currentLevel, score);
                                RoundWinScreen(currentLevel, score);
                                score = 0;

                                break;
                            }
                        }


                        foreach (Alien a in aliens)
                        {

                            if (a.attack(Player.x, Player.y))
                            {
                                Player.Lives--;
                                if (Player.Lives < 1)
                                    SaveScore(currentLevel, score);
                                Player.Redraw();
                            }


                        }
                    }
                    break;
                case 5: //BOSS FIGHT
                    LvlAlienCount = 28;
                    int startY = 2, startX = 80;
                    if (!haveSpawned)
                    {
                        Player.Lives = 3;
                        for (int i = 0; i < LvlAlienCount; i++)
                        {
                            aliens.Add(new Alien());
                        }

                        foreach (Alien a in aliens)
                        {
                            a.x = startX;
                            a.y = startY;
                            startY++;
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

                                SetLevelTo(6);
                                haveSpawned = false;

                                foreach (Alien alien in aliens.ToArray())
                                {
                                    aliens.Remove(alien);
                                }
                                Clear();
                                SaveScore(currentLevel, score);
                                WinScreen(score);
                                score = 0;

                                break;
                            }
                        }


                        foreach (Alien a in aliens)
                        {

                            if (a.attack(Player.x, Player.y))
                            {
                                Player.Lives--;
                                if (Player.Lives < 1)
                                    SaveScore(currentLevel, score);
                                Player.Redraw();
                            }


                        }
                    }
                    break;
                case 6:
                    LvlAlienCount = 3;
                    if (!haveSpawned)
                    {
                        if (EndlessCounter == 50)
                            Player.Lives = 3;
                        for (int i = 0; i < LvlAlienCount; i++)
                        {
                            aliens.Add(new Alien());
                        }

                        aliens[0].x = pos.Next(MinXSpawn, WindowWidth - 1); //THESE NEED TO BE CHANGED
                        aliens[0].y = pos.Next(MinYSpawn, 10);

                        aliens[1].x = pos.Next(MinXSpawn, WindowWidth - 1);
                        aliens[1].y = pos.Next(11, 20);

                        aliens[2].x = pos.Next(MinXSpawn, WindowWidth - 1);
                        aliens[2].y = pos.Next(21, 29);

                        foreach (Alien a in aliens)
                        {
                            a.drawAlien();
                            a.BulletSpeed = EndlessCounter;
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

                                SetLevelTo(6, false);
                                haveSpawned = false;

                                foreach (Alien alien in aliens.ToArray())
                                {
                                    aliens.Remove(alien);
                                }
                                Clear();

                                //Increase difficulty
                                if (EndlessCounter > 26)
                                    EndlessCounter = EndlessCounter - 2;
                                else
                                    if (EndlessCounter > 15)
                                    EndlessCounter--;
                                break;
                            }
                        }


                        foreach (Alien a in aliens)
                        {

                            if (a.attack(Player.x, Player.y))
                            {
                                Player.Lives--;
                                if (Player.Lives < 1)
                                {
                                    EndlessCounter = 50;
                                    SaveScore(currentLevel, score);
                                }

                                Player.Redraw();
                            }


                        }
                    }
                    break;

            }
            if (Player.Lives < 1)
                LoseScreen(score, currentLevel);
        }

        public static void RoundWinScreen(int level, int score)
        {
            int minX = 5, startY = 10;
            Clear();
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.SetCursorPosition(minX, startY);
            startY++;
            Console.Write("██╗░░░░░███████╗██╗░░░██╗███████╗██╗░░░░░  ░█████╗░░█████╗░███╗░░░███╗██████╗░██╗░░░░░███████╗████████╗███████╗");
            Console.SetCursorPosition(minX, startY);
            startY++;
            Console.Write("██║░░░░░██╔════╝██║░░░██║██╔════╝██║░░░░░  ██╔══██╗██╔══██╗████╗░████║██╔══██╗██║░░░░░██╔════╝╚══██╔══╝██╔════╝");
            Console.SetCursorPosition(minX, startY);
            startY++;
            Console.Write("██║░░░░░█████╗░░╚██╗░██╔╝█████╗░░██║░░░░░  ██║░░╚═╝██║░░██║██╔████╔██║██████╔╝██║░░░░░█████╗░░░░░██║░░░█████╗░░");
            Console.SetCursorPosition(minX, startY);
            startY++;
            Console.Write("██║░░░░░██╔══╝░░░╚████╔╝░██╔══╝░░██║░░░░░  ██║░░██╗██║░░██║██║╚██╔╝██║██╔═══╝░██║░░░░░██╔══╝░░░░░██║░░░██╔══╝░░");
            Console.SetCursorPosition(minX, startY);
            startY++;
            Console.Write("███████╗███████╗░░╚██╔╝░░███████╗███████╗  ╚█████╔╝╚█████╔╝██║░╚═╝░██║██║░░░░░███████╗███████╗░░░██║░░░███████╗");
            Console.SetCursorPosition(minX, startY);
            startY++;
            Console.Write("╚══════╝╚══════╝░░░╚═╝░░░╚══════╝╚══════╝  ░╚════╝░░╚════╝░╚═╝░░░░░╚═╝╚═╝░░░░░╚══════╝╚══════╝░░░╚═╝░░░╚══════╝");

            Console.ForegroundColor = ConsoleColor.White;
            Console.SetCursorPosition(43, 18);
            Console.Write("You won level " + level + " with a score of " + score);
            Console.SetCursorPosition(49, 19);
            Console.Write("Press Enter to Continue");
            Console.SetCursorPosition(42, 22);
            Console.Write("Your Highscore for this level is " + getHighscore(level));

            Console.ReadLine();
            Clear();
        }
        public static void WinScreen(int score)
        {
            int minX = 30, startY = 10;
            Clear();
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.SetCursorPosition(minX, startY);
            startY++;
            Console.Write("██╗░░░██╗░█████╗░██╗░░░██╗  ░██╗░░░░░░░██╗██╗███╗░░██╗██╗");
            Console.SetCursorPosition(minX, startY);
            startY++;
            Console.Write("╚██╗░██╔╝██╔══██╗██║░░░██║  ░██║░░██╗░░██║██║████╗░██║██║");
            Console.SetCursorPosition(minX, startY);
            startY++;
            Console.Write("░╚████╔╝░██║░░██║██║░░░██║  ░╚██╗████╗██╔╝██║██╔██╗██║██║");
            Console.SetCursorPosition(minX, startY);
            startY++;
            Console.Write("░░╚██╔╝░░██║░░██║██║░░░██║  ░░████╔═████║░██║██║╚████║╚═╝");
            Console.SetCursorPosition(minX, startY);
            startY++;
            Console.Write("░░░██║░░░╚█████╔╝╚██████╔╝  ░░╚██╔╝░╚██╔╝░██║██║░╚███║██╗");
            Console.SetCursorPosition(minX, startY);
            startY++;
            Console.Write("░░░╚═╝░░░░╚════╝░░╚═════╝░  ░░░╚═╝░░░╚═╝░░╚═╝╚═╝░░╚══╝╚═╝");

            Console.ForegroundColor = ConsoleColor.White;
            Console.SetCursorPosition(40, 18);
            Console.Write("You won the last level with a score of " + score);
            Console.SetCursorPosition(42, 19);
            Console.Write("Your Highscore for this level is " + getHighscore(5));
            Console.SetCursorPosition(38, 22);
            Console.Write("You won the game with a total highscore of " + getTotalHighscore());
            Console.SetCursorPosition(35, 24);
            Console.Write("You have unlocked endless mode! Press enter to Play!");


            Console.ReadLine();
            Clear();
        }

        public static int getHighscore(int level)
        {
            doc.Load("./Resources/GameSave.xml");
            XmlNodeList Levels = doc.GetElementsByTagName("level");

            int Highscore = Convert.ToInt32(Levels[level - 1].LastChild.InnerText);
            return Highscore;
        }

        public static int getTotalHighscore()
        {
            doc.Load("./Resources/GameSave.xml");
            XmlNodeList Levels = doc.GetElementsByTagName("level");

            int total = 0;
            for (int i = 0; i < 5; i++)
            {
                total = total + Convert.ToInt32(Levels[i].LastChild.InnerText);
            }
            return total;
        }
        public static void LoseScreen(int score, int level)
        {
            int minX = 30, startY = 10;
            Clear();
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.SetCursorPosition(minX, startY);
            startY++;
            Console.Write("██╗░░░██╗░█████╗░██╗░░░██╗  ██╗░░░░░░█████╗░░██████╗███████╗");
            Console.SetCursorPosition(minX, startY);
            startY++;
            Console.Write("╚██╗░██╔╝██╔══██╗██║░░░██║  ██║░░░░░██╔══██╗██╔════╝██╔════╝");
            Console.SetCursorPosition(minX, startY);
            startY++;
            Console.Write("░╚████╔╝░██║░░██║██║░░░██║  ██║░░░░░██║░░██║╚█████╗░█████╗░░");
            Console.SetCursorPosition(minX, startY);
            startY++;
            Console.Write("░░╚██╔╝░░██║░░██║██║░░░██║  ██║░░░░░██║░░██║░╚═══██╗██╔══╝░░");
            Console.SetCursorPosition(minX, startY);
            startY++;
            Console.Write("░░░██║░░░╚█████╔╝╚██████╔╝  ███████╗╚█████╔╝██████╔╝███████╗");
            Console.SetCursorPosition(minX, startY);
            startY++;
            Console.Write("░░░╚═╝░░░░╚════╝░░╚═════╝░  ╚══════╝░╚════╝░╚═════╝░╚══════╝");

            Console.ForegroundColor = ConsoleColor.White;
            Console.SetCursorPosition(45, 18);
            Console.Write("You died with a score of " + score);
            Console.SetCursorPosition(41, 19);
            Console.Write("Press enter to return to main menu");


            Console.ReadLine();

        }

        public static void updateHeader(int Level, int score, ref int oldScore, int lives)
        {
            Console.SetCursorPosition(0, 1);
            for (int i = 0; i < Console.WindowWidth; i++)
            {
                Console.Write("_");
            }

            Console.SetCursorPosition(10, 0);
            if (Level == 6)
                Console.Write("Level: Endless Mode");
            else
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
            Console.Write("                    ");
            Console.SetCursorPosition(100, 0);
            Console.Write("Lives: " + lives);
        }

        static void ValidateLevels()
        {
            doc.Load("./Resources/GameSave.xml");
            XmlNodeList Levels = doc.GetElementsByTagName("level");
            for (int i = 0; i < Levels.Count; i++)
            {
                if (Levels[i].FirstChild.InnerText != "")
                {
                    LoadMenu[i] = (Levels[i].FirstChild.InnerText);
                }
            }

        }

    }
}
