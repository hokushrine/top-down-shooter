#region Includes
using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;

using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
#endregion

namespace top_down_shooter
{

    public delegate void PassObject(object i);
    public delegate object PassObjectAndReturn(object i);



    public class Globals
    {
        public static int screenHeight, screenWidth;

        public static ContentManager content;
        public static SpriteBatch spriteBatch;


        public static McKeyboard keyboard;
        public static McMouseControl mouse;

        public static GameTime gameTime;

        // Distance formula - there is actually a built in distance function, but this is what it would be if there wasn't one
        public static float GetDistance(Vector2 pos, Vector2 target)
        {
            return (float)Math.Sqrt(Math.Pow(pos.X - target.X, 2) + Math.Pow(pos.Y - target.Y, 2));
        }

        // Gets the position of where you are and where you are trying to go to.
        // It then asks if distance is less than speed
            // If true, then return the distance - if the distance is shorter than you can move, then just go that shorter distance
        // else
            // take the directional vector that is returned and multiply it by speed over distance
            // If distance is 10 and speed is 2, it will go 20% of the directional vector

        public static Vector2 RadialMovement(Vector2 focus, Vector2 pos, float speed)
        {
            float dist = Globals.GetDistance(pos, focus);

            if(dist <= speed)
            {
                return focus - pos;
            }
            else
            {
                return (focus - pos) * speed/dist;
            }
        }


        public static float RotateTowards(Vector2 Pos, Vector2 focus)
        {
            // Geometry for calculating the first quadrant
            float h, sineTheta, angle;
            if(Pos.Y-focus.Y != 0)
            {
                h = (float)Math.Sqrt(Math.Pow(Pos.X-focus.X, 2) + Math.Pow(Pos.Y-focus.Y, 2));
                sineTheta = (float)(Math.Abs(Pos.Y-focus.Y)/h); //* ((item.Pos.Y-focus.Y)/(Math.Abs(item.Pos.Y-focus.Y))));
            }
            else
            {
                h = Pos.X-focus.X;
                sineTheta = 0;
            }

            angle = (float)Math.Asin(sineTheta);

            // Drawing diagonial lines here.
            //Quadrant 2
            if(Pos.X-focus.X > 0 && Pos.Y-focus.Y > 0)
            {
                angle = (float)(Math.PI*3/2 + angle);
            }
            //Quadrant 3
            else if(Pos.X-focus.X > 0 && Pos.Y-focus.Y < 0)
            {
                angle = (float)(Math.PI*3/2 - angle);
            }
            //Quadrant 1
            else if(Pos.X-focus.X < 0 && Pos.Y-focus.Y > 0)
            {
                angle = (float)(Math.PI/2 - angle);
            }
            else if(Pos.X-focus.X < 0 && Pos.Y-focus.Y < 0)
            {
                angle = (float)(Math.PI/2 + angle);
            }
            else if(Pos.X-focus.X > 0 && Pos.Y-focus.Y == 0)
            {
                angle = (float)Math.PI*3/2;
            }
            else if(Pos.X-focus.X < 0 && Pos.Y-focus.Y == 0)
            {
                angle = (float)Math.PI/2;
            }
            else if(Pos.X-focus.X == 0 && Pos.Y-focus.Y > 0)
            {
                angle = (float)0;
            }
            else if(Pos.X-focus.X == 0 && Pos.Y-focus.Y < 0)
            {
                angle = (float)Math.PI;
            }

            return angle;
        }
    }
}
