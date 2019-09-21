#region Includes
using System;
using System.Collections.Generic;
using System.Linq;
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
    public class Projectile2d : Basic2d
    {
        public bool done;
        public float speed;
        public Vector2 direction;
        public Unit owner;
        public McTimer timer;
        public Projectile2d(string PATH, Vector2 POS, Vector2 DIMS, Unit OWNER, Vector2 TARGET) : base(PATH, POS, DIMS)
        {
            done = false;
            speed = 5.0f;
            owner = OWNER;

            direction = TARGET - owner.pos; // When subtracting a vector from a vector, a directional vector is returned
             // Normally the pythagoreum theorum returns a number that is not one
             // Calling Normalize will make the length of the vector 1, which is then multiplied by the speed of the projectile
             // If Normalize was not called, the further the target is, the faster the projectile would be.
            direction.Normalize();

            rotation = Globals.RotateTowards(pos, new Vector2(TARGET.X, TARGET.Y));


            // 60fps at a rate of 5 units is roughly 300
            timer = new McTimer(1200);
        }

        public virtual void Update(Vector2 OFFSET, List<Unit> UNITS)
        {
            pos += direction * speed;
            if(timer.Test()) { done = true; } // makes sure that projectiles do not go on forever
            if(HitSomething(UNITS))
            {
                // This is where we would tell the mob/unit that it is hit and trigger health reduction etc.
                done = true;
            }
        }

        public virtual bool HitSomething(List<Unit> UNITS)
        {
            for(int i = 0; i < UNITS.Count; i++)
            {
                // 
                if(Globals.GetDistance(pos, UNITS[i].pos) < UNITS[i].hitDist)
                {
                    UNITS[i].GetHit();
                    return true;
                }
            }
            return false;
        }

        public override void Draw(Vector2 OFFSET)
        {
            base.Draw(OFFSET);
        }
    }
}