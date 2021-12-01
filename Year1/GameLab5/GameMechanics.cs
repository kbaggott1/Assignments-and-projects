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
        public int x = 0, y = 0;
        private int LastX = 0, LastY = 0;
        private int lives = 3; 
        public int Lives { get { return lives; } set { lives = value; } }
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
                    if (x < maxX && checkAlienPosRight(aliens, x, y))
                        x++;
                    Console.SetCursorPosition(LastX, LastY);
                    Console.Write(" ");
                    Console.SetCursorPosition(x, y);
                    Console.Write(Sprite);
                    LastX = x;
                    break;

                case ConsoleKey.LeftArrow:
                    if (x > 0  && checkAlienPosLeft(aliens, x, y))
                        x--;
                    Console.SetCursorPosition(LastX, LastY);
                    Console.Write(" ");
                    Console.SetCursorPosition(x, y);
                    Console.Write(Sprite);
                    LastX = x;
                    break;

                case ConsoleKey.UpArrow:
                    if (y > 2 && checkAlienPosUp(aliens, x, y))
                        y--;
                    Console.SetCursorPosition(LastX, LastY);
                    Console.Write(" ");
                    Console.SetCursorPosition(x, y);
                    Console.Write(Sprite);
                    LastY = y;
                    break;

                case ConsoleKey.DownArrow:
                    if (y < maxY && checkAlienPosDown(aliens, x, y))                    
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
        private bool checkAlienPosDown(List<Alien> aliens, int PlayerX, int PlayerY) 
        {
            int alientemppos;

            foreach (Alien alien in aliens)
            {
                alientemppos = alien.y;
                if (PlayerX == alien.x && PlayerY == alientemppos - 1)
                {
                    
                    return false;
                }
            }
            return true;
        }

        private bool checkAlienPosUp(List<Alien> aliens, int PlayerX, int PlayerY) 
        {
            int alientemppos;

            foreach (Alien alien in aliens)
            {
                alientemppos = alien.y;
                if (PlayerX == alien.x && PlayerY == alientemppos + 1)
                {

                    return false;
                }
            }
            return true;
        }

        private bool checkAlienPosRight(List<Alien> aliens, int PlayerX, int PlayerY) 
        {
            int alientemppos;

            foreach (Alien alien in aliens)
            {
                alientemppos = alien.x;
                if (PlayerY == alien.y && PlayerX == alientemppos - 1)
                {

                    return false;
                }
            }
            return true;
        }

        private bool checkAlienPosLeft(List<Alien> aliens, int PlayerX, int PlayerY) 
        {
            int alientemppos;

            foreach (Alien alien in aliens)
            {
                alientemppos = alien.x;
                if (PlayerY == alien.y && PlayerX == alientemppos + 1)
                {

                    return false;
                }
            }
            return true;
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
        private AlienBullets Bullet = new AlienBullets(); //zthis needs to be outisde the object or each will have their own list 
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
        private bool CanFire = true;

        private bool IsDead = false;
        public bool isDead
        {
            get { return IsDead; }
            set
            {
                IsDead = value;
                if (value)
                    CanFire = false;
            }
        }



        
       


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

        public bool attack(int PlayerX, int PlayerY)
        {

            if (Bullet.isFiring)
            {
                Bullet.MoveBullet(PlayerX, PlayerY);
                if (Bullet.PlayerHit)
                    return true;
            }
            else
            {
                if (CanFire)
                    Bullet.spawnBullet(x, y);
            }
            return false;
          

        }



    }


    class AlienBullets
    {
        private string BulletSprite = "~";
        private int BulletX;
        private int BulletY;
        private int LastX;
        private int LastY;
        private int AlienBulletCooldown = 50;

        public bool isFiring = false;
        //private bool hasSpawned = false;
        private stopwatch bTimer = new stopwatch();

        public bool PlayerHit;
                
        public void spawnBullet(int AlienX, int AlienY)
        {
            BulletX = AlienX - 1;
            BulletY = AlienY;
            LastX = BulletX;
            LastY = BulletY;
            Console.SetCursorPosition(BulletX, BulletY);
            Console.Write(BulletSprite);
            isFiring = true;
            PlayerHit = false;
        }
        public void MoveBullet(int PlayerX, int PlayerY)
        {
            if (BulletX > 1)
            {
                if (bTimer.isTimerDone(AlienBulletCooldown))
                {
                    if (BulletX == (PlayerX + 1) && BulletY == PlayerY)
                    {
                        isFiring = false;
                        PlayerHit = true;
                        Console.SetCursorPosition(LastX, LastY);
                        Console.Write(" ");
                        return;
                    }
                    BulletX--;
                    Console.SetCursorPosition(BulletX, BulletY);
                    Console.Write(BulletSprite);
                    Console.SetCursorPosition(LastX, LastY);
                    Console.Write(" ");
                    LastX = BulletX;
                    LastY = BulletY;


                }
            }
            else
            {
                isFiring = false;
                Console.SetCursorPosition(LastX, LastY);
                Console.Write(" ");
            }
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
