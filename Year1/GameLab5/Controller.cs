using System;
using System.Collections.Generic;
using System.Text;

namespace GameLab5
{
    class Controller
    {
        
        private const string sprite = "O";
        private int x = 0, y = 0;
        private int LastX = 0, LastY = 0;
        

        public string Sprite
        {
            get { return sprite; }
        }


        public void Controls()
        {
            ConsoleKey Move = Console.ReadKey().Key;
            Right(Move);
            Left(Move);
            Up(Move);
            Down(Move);

        }

        private void Right(ConsoleKey Move)
        {
            if (Move == ConsoleKey.RightArrow)
            {
                if (x < 119)
                    x++;
                Console.SetCursorPosition(LastX, LastY);
                Console.Write(" ");
                Console.SetCursorPosition(x, y);
                Console.Write(Sprite);
                LastX = x;
            }
        }

        private void Left(ConsoleKey Move)
        {
            if (Move == ConsoleKey.LeftArrow)
            {

                if (x > 0)
                    x--;
                Console.SetCursorPosition(LastX, LastY);
                Console.Write(" ");
                Console.SetCursorPosition(x, y);
                Console.Write(Sprite);
                LastX = x;
                

            }
        }

        private void Up(ConsoleKey Move)
        {
            if (Move == ConsoleKey.UpArrow)
            {
                if (y > 0)
                    y--;
                Console.SetCursorPosition(LastX, LastY);
                Console.Write(" ");
                Console.SetCursorPosition(x, y);
                Console.Write(Sprite);
                LastY = y;
            }
        }

        private void Down(ConsoleKey Move)
        {
            if (Move == ConsoleKey.DownArrow)
            {
               // if (y < 120)
                y++;
                Console.SetCursorPosition(LastX, LastY);
                Console.Write(" ");
                Console.SetCursorPosition(x, y);
                Console.Write(Sprite);
                LastY = y;
            }
        }
    }
}
