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
    public class Basic2d
    {
        public float rotation;
        public Vector2 pos, dims;
        public Texture2D myModel;

        public Basic2d(string PATH, Vector2 POS, Vector2 DIMS)
        {
            pos = POS;
            dims = DIMS;

            myModel = Globals.content.Load<Texture2D>(PATH);
        }

        public virtual void Update()
        {

        }

        public virtual void Draw(Vector2 OFFSET)
        {
            if(myModel != null)
            {
                /* Draw takes in a Texture2D (myModel), a rectangle displayed at the positions listed, with dimensions determining height and width
                 * null allows us to use only a piece of a picture if we want to
                 * Putting any other color than white will tint the picture
                 * 0.0f refers to rotation - will revisit later
                 * Bounds is the literal pixel size/dimensions
                 *  - We are currently drawing from the middle due to the divide by two
                 *  SpriteEffects is actually pretty inefficient because it is constantly being called, it's better to do it in the constructor
                 *  Final arg refers to layer depth
                 *  
                 *  pos.X gives the relative position, offset.x gives the shifted position
                */
                Globals.spriteBatch.Draw(myModel, new Rectangle((int)(pos.X + OFFSET.X), (int)(pos.Y + OFFSET.Y), (int)dims.X, (int)dims.Y), null, Color.White, rotation, new Vector2(myModel.Bounds.Width / 2, myModel.Bounds.Height / 2), new SpriteEffects(), 0);
            }
        }

        public virtual void Draw(Vector2 OFFSET, Vector2 ORIGIN)

        {
            if (myModel != null)
            {
                Globals.spriteBatch.Draw(myModel, new Rectangle((int)(pos.X + OFFSET.X), (int)(pos.Y + OFFSET.Y), (int)dims.X, (int)dims.Y), null, Color.White, rotation, new Vector2(ORIGIN.X, ORIGIN.Y), new SpriteEffects(), 0);
            }
        }
    }
}
