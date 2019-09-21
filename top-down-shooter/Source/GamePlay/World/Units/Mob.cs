#region Includes
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
#endregion

namespace top_down_shooter
{
    public class Mob : Unit
    {


        public Mob(string PATH, Vector2 POS, Vector2 DIMS) : base(PATH, POS, DIMS)
        {
            speed = 2.0f;
        }

        public virtual void Update(Vector2 OFFSET, Hero HERO)
        {
            AI(HERO);
        }

        public virtual void AI(Hero HERO)
        {
            pos += Globals.RadialMovement(HERO.pos, pos, speed);
            rotation = Globals.RotateTowards(pos, HERO.pos); // first arg is position of the mob
        }

        public override void Draw(Vector2 OFFSET)
        {
            base.Draw(OFFSET);
        }
    }
}