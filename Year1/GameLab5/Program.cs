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
            Console.CursorVisible = false;
           
            while (!gameover)
            {
                Player.EnableControls = true;
            }
            
        }
    }
}
