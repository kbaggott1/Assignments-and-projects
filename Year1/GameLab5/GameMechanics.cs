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
        //Player Position-------------------
        private const string sprite = "O";
        private int x = 0, y = 0;
        private int LastX = 0, LastY = 0;
        //----------------------------------


        //Player Bullets------------------------------------------
        private int BulletCDinMili = 250;
        private List<Bullets> playerBullet = new List<Bullets>();
        stopwatch BulletCoolDown = new stopwatch();
        //--------------------------------------------------------


        //alien storage for hit detection
        private List<Alien> aliens;
        //-------------------------------


        public string Sprite
        {
            get { return sprite; }
        }

        public void SendAliens(List<Alien> alienList)
        {
            aliens = alienList;
        }
        

        public void StartPos(int StartX, int StartY)
        {
            x = StartX;
            y = StartY;
            Console.SetCursorPosition(x, y);
            Console.Write(sprite);
            LastX = x;
            LastY = y;
        }

        public void Controls()
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
            int maxX = 119;
            int maxY = 29;

            foreach (Bullets bullet in playerBullet.ToArray()) //need .ToArray otherwise values will change during enumeration I.E: error (:
            {
                if (bullet.pBulletFiring)
                {
                    bullet.moveBullet(aliens);
                }
                else
                {
                    playerBullet.Remove(bullet);
                }
            }
            

            switch (Key)
            {
                case ConsoleKey.RightArrow:
                    if (x < maxX)
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
                    if (y < maxY)
                        y++;
                    Console.SetCursorPosition(LastX, LastY);
                    Console.Write(" ");
                    Console.SetCursorPosition(x, y);
                    Console.Write(Sprite);
                    LastY = y;
                    break;
                case ConsoleKey.Spacebar: 
                    if (BulletCoolDown.isTimerDone(BulletCDinMili))
                    {
                        playerBullet.Add(new Bullets());
                        playerBullet[playerBullet.Count - 1].SpawnBullet(x, y);
                    }
                    break;
                    

            }


        }
    }
    class Bullets
    {
        private const string bulletSprite = "-";
        private int bulletSpeed = 15;      
        private int LastX, LastY;
        private int bulletX;
        private int bulletY;
        public bool pBulletFiring;
        stopwatch BulletTimer = new stopwatch();
        


        public void SpawnBullet(int PlayerX, int PlayerY)
        {
            pBulletFiring = true;
            bulletX = PlayerX + 1;
            bulletY = PlayerY;
            Console.SetCursorPosition(bulletX, PlayerY);
            Console.Write(bulletSprite);
            LastX = bulletX;
            LastY = PlayerY;
            
            
        }


        
        public void moveBullet(List<Alien> Aliens)
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
                    foreach (Alien Alien in Aliens.ToArray())
                    {
                        if (bulletX == Alien.x && bulletY == Alien.y)
                        {
                            Alien.HP--;
                            pBulletFiring = false;
                            Console.Write(" ");
                        }
                    }
                    
                }
        }

    }
    class Alien
    {
        public int x { get; set; }
        public int y { get; set; }
        private int hp;
        public int HP
        {
            get { return hp; }
            set
            {
                hp = value;
                if (hp == 1)
                    sprite = "x";
                else
                    if (hp < 1)
                        isDead = true;
            }
        }
        public string sprite { get; set; }
        public bool isDead { get; set; }

        //public int StartX
        //{
        //    set { x = value; }
        //}
        //public int StartY
        //{
        //    set { y = value; }
        //}

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

        public void attack()
        {

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
