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
            ConsoleKey userkey = ConsoleKey.NoName;            
            GetKey(ref userkey);
            Move(userkey);
        }

        private void GetKey(ref ConsoleKey userkey)
        {
            if (Console.KeyAvailable)
                userkey = Console.ReadKey().Key;
        }


        private void Move(ConsoleKey Key)
        {
            if (playerBullet.pBulletFiring)
            {
                playerBullet.moveBullet();
            }

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
                case ConsoleKey.Spacebar: 
                    if (!playerBullet.pBulletFiring)
                    {
                        playerBullet.SpawnBullet(x, y);                        
                    }
                    break;


            }


        }
    }
    class Bullets
    {
        private DateTime startTime = DateTime.Now;
        private const string bulletSprite = "-";
        private int bulletSpeed = 30;
        private int LastX, LastY;
        private int bulletX;
        public bool pBulletFiring;
        

        public void SpawnBullet(int PlayerX, int PlayerY)
        {
            pBulletFiring = true;
            bulletX = PlayerX + 1;
            Console.SetCursorPosition(bulletX, PlayerY);
            Console.Write(bulletSprite);
            LastX = bulletX;
            LastY = PlayerY;
            
            
        }


        
        public void moveBullet()
        {
            if (BulletTimer())
                if (bulletX != Console.WindowWidth)
                {                
                    bulletX++;
                    Console.SetCursorPosition(bulletX, LastY);
                    Console.Write(bulletSprite);
                    Console.SetCursorPosition(LastX, LastY);
                    Console.Write(" ");
                    LastX = bulletX;
                    if (bulletX == Console.WindowWidth - 1)
                    {
                        pBulletFiring = false;
                        Console.Write(" ");
                    }
                
                }
            
            
        }

        private bool BulletTimer()
        {
            
            DateTime timeElapsed = DateTime.Now;
            if (timeElapsed >= startTime.AddMilliseconds(bulletSpeed))
            {
                startTime = DateTime.Now;
                return true;
            }
            return false;
        }

    }

}
