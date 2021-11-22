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


            // draw "O" before loop and reset default x and y for player controller
            while (!gameover)
            {
                playLevel(getLevel());
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

        static void playLevel(int currentLevel)
        {
            switch(currentLevel)
            {
                case 1:
                    
                    break;

                case 2:

                    break;

                case 3:

                    break;
            }
        }

    }
}
