using System;
using System.Collections.Generic;
using System.Text;

namespace GameLab5
{
    class Controller
    {
        public Controller ()
        {
             int x = 0, y = 0;
        }

        private const string sprite = "x";
        private bool enablecontrols = false;


        public bool EnableControls
        {
            get { return enablecontrols; }
            set { enablecontrols = value; Controls(); }
        }

        public string Sprite
        {
            get { return sprite; }
        }


        private void Controls()
        {
            while (enablecontrols == true) 
            {
                Console.WriteLine("Hello");
            }

        }
        
    }
}
