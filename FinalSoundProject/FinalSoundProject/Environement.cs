using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace FinalSoundProject
{
    public class Environement
    {
        //The Envirenement Is A Set Of RectanGles That  Represent The Blocks 
        //these rectangle are invisibles To The User Unless He Press The Space Button To Throw The Wave That View The map
        private List<Rectangle> buildings;
        private Point envrSize;
       
        public List<Rectangle> Buildings
        {
            get { return buildings; }
        }
        public Environement(int X,int Y )
        {
            envrSize=new Point(X,Y);
            SetEnvire();
        }
        public void SetEnvire()
        {
            //These Buildings Can be Generated Randomly 
            //Loaded From an Xml File 
            buildings=new List<Rectangle>();
            buildings.Add(new Rectangle(50,20,100,150));
            buildings.Add(new Rectangle(50, 20, 100, 150));
            buildings.Add(new Rectangle(50, 200, 100, 150));
            buildings.Add(new Rectangle(50, 2000, 100, 150));
            buildings.Add(new Rectangle(500, 20, 100, 150));
            buildings.Add(new Rectangle(1550, 20, 100, 150)); 
            buildings.Add(new Rectangle(50, 20, 100, 150));
            buildings.Add(new Rectangle(510, 20, 100, 130));
            buildings.Add(new Rectangle(350, 20, 160, 150));
            buildings.Add(new Rectangle(550, 20, 100, 150));
            buildings.Add(new Rectangle(850, 20, 100, 150)); 
            buildings.Add(new Rectangle(450, 20, 100, 150)); 
            buildings.Add(new Rectangle(250, 20, 100, 150));
            buildings.Add(new Rectangle(550, 20, 100, 150));
        }



    }
}
