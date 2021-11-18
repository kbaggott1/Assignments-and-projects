using System;
using static System.Console;
namespace GameLab5
{
    class Program
    {

        static void Main(string[] args)
        {
            bool gameover = false;
            Controller Player = new Controller();
            Stages stages = new Stages();
            Console.CursorVisible = false;


            // draw "O" before loop and reset default x and y for player controller
            while (!gameover)
            {
                Player.Controls();
                stages.Start();
            }
            

        }

      
    }
}
