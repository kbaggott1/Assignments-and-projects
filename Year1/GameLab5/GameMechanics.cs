using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
namespace GameLab5
{
    class Controller
    {
        
        private const string sprite = "O";
        private int x = 0, y = 0;
        private int LastX = 0, LastY = 0;
        private Bullets playerBullet = new Bullets();

        public string Sprite
        {
            get { return sprite; }
        }


        public void Controls()  // looped in main
        {
            /// function to get the key
            ConsoleKey userkey = Console.ReadKey().Key;
            Move(userkey);           
            if (bulletfiring)
            {
                movebullet
            }
        }


        //  in this function to get the key"
            // is it spacebar? Yes? bulletfiring = true

        private void Move(ConsoleKey Key)
        {

            switch (Key)
            {
                case ConsoleKey.RightArrow:
                    if (x < 119)
                        x++;
                    Console.SetCursorPosition(LastX, LastY);
                    Console.Write(" ");
                    Console.SetCursorPosition(x, y);
                    Console.Write(Sprite);
                    LastX = x;
                    break;

                case ConsoleKey.LeftArrow:
                    if (x > 0)
                        x--;
                    Console.SetCursorPosition(LastX, LastY);
                    Console.Write(" ");
                    Console.SetCursorPosition(x, y);
                    Console.Write(Sprite);
                    LastX = x;
                    break;

                case ConsoleKey.UpArrow:
                    if (y > 0)
                        y--;
                    Console.SetCursorPosition(LastX, LastY);
                    Console.Write(" ");
                    Console.SetCursorPosition(x, y);
                    Console.Write(Sprite);
                    LastY = y;
                    break;

                case ConsoleKey.DownArrow:
                    if (y < 120)
                        y++;
                    Console.SetCursorPosition(LastX, LastY);
                    Console.Write(" ");
                    Console.SetCursorPosition(x, y);
                    Console.Write(Sprite);
                    LastY = y;
                    break;
                case ConsoleKey.Spacebar: //Take this and put into new function, then recall that function in main game loop
                    playerBullet.fire(x, y);
                    break;


            }


        }
    }
    class Bullets
    {
        private const string bulletSprite = "-";
        private int bulletSpeed = 100;
        public void fire(int PlayerX, int PlayerY)
        {
            int LastX;
            int bulletX = PlayerX + 1;
            Console.SetCursorPosition(bulletX , PlayerY);
            while (true)
            {
                Console.Write(bulletSprite);
                Thread.Sleep(bulletSpeed);
                LastX = bulletX;
                bulletX++;
                Console.SetCursorPosition(LastX, PlayerY);
                Console.Write(" ");
                if (bulletX == Console.WindowWidth)
                {
                    break;
                }
                Console.SetCursorPosition(bulletX, PlayerY);
                
            }
        }

    }

}
