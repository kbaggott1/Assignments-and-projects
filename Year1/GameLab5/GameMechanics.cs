using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Collections;

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


        private void Move(ConsoleKey Key) //Remove magic numbers
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
        private const string bulletSprite = "-";
        private int bulletSpeed = 30;
        private int LastX, LastY;
        private int bulletX;
        public bool pBulletFiring;
        stopwatch BulletTimer = new stopwatch();
        Program Time = new Program();


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
            if (BulletTimer.isTimerDone(bulletSpeed))
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



    }
    class Alien
    {
        private int x, y;
        public int HP { get; set; }
        public string sprite { get; set; }
        public bool isDead { get; set; }

        public int StartX
        {
            set { x = value; }
        }
        public int StartY
        {
            set { y = value; }
        }

        public Alien()
        {
            HP = 2;
            sprite = "X";
            isDead = false;
           
        }

        public void drawAlien()
        {
            Console.SetCursorPosition(x, y);
            Console.Write(sprite);
        }

    }

    class stopwatch
    {
        DateTime initialTime = DateTime.Now;
        public bool isTimerDone(int TimeInMili)
        {
            DateTime timeElapsed = DateTime.Now;
            if (timeElapsed >= initialTime.AddMilliseconds(TimeInMili))
            {
                initialTime = DateTime.Now;
                return true;
            }
            return false;
        }
    }
}
