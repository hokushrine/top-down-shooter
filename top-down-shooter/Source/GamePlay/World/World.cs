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
    public class World
    {
        public Vector2 offset;

        public Hero hero;
        public List<Projectile2d> projectiles = new List<Projectile2d>();
        public List<Mob> mobs = new List<Mob>();
        public List<SpawnPoint> spawnPoints = new List<SpawnPoint>();

        public World()
        {
            hero = new Hero("2d\\Hero", new Vector2(300, 300), new Vector2(48, 48));

            GameGlobals.PassProjectile = AddProjectile; // Whenever PassProjectile is called, AddProjectile is actually being called
            GameGlobals.PassMob = AddMob;
            offset = new Vector2(0, 0);

            // First vector location is center of screen
            // There are three different spawn times
            spawnPoints.Add(new SpawnPoint("2d\\Misc\\Circle", new Vector2(50, 50), new Vector2(35, 35)));
            spawnPoints.Add(new SpawnPoint("2d\\Misc\\Circle", new Vector2(Globals.screenWidth/2, 50), new Vector2(35, 35))); 
            spawnPoints[spawnPoints.Count - 1].spawnTimer.AddToTimer(500);

            spawnPoints.Add(new SpawnPoint("2d\\Misc\\Circle", new Vector2(Globals.screenWidth - 50, 50), new Vector2(35, 35)));
            spawnPoints[spawnPoints.Count - 1].spawnTimer.AddToTimer(1000);
        }

        public virtual void Update()
        {
            hero.Update(offset);

            // Update for spawn points
            for (int i = 0; i < spawnPoints.Count; i++)
            {
                spawnPoints[i].Update(offset);
            }

            // Update for projectiles
            for (int i = 0; i < projectiles.Count; i++)
            {
                projectiles[i].Update(offset, mobs.ToList<Unit>());
                if(projectiles[i].done)
                {
                    projectiles.RemoveAt(i);
                    i--;
                }
            }

            // Update for mobs
            for (int i = 0; i < mobs.Count; i++)
            {
                mobs[i].Update(offset, hero);
                if (mobs[i].dead)
                {
                    mobs.RemoveAt(i);
                    i--;
                }
            }
        }

        public virtual void AddMob(object INFO)
        {
            mobs.Add((Mob)INFO);
        }

        public virtual void AddProjectile(object INFO)
        {
            projectiles.Add((Projectile2d)INFO);

        }

        // Things that are drawn before, can be drawn on top of
        public virtual void Draw(Vector2 OFFSET)
        {
            hero.Draw(OFFSET);

            // Draw projectiles
            for (int i = 0; i < projectiles.Count; i++)
            {
                projectiles[i].Draw(offset);
            }

            // Draw spawn points
            for (int i = 0; i < spawnPoints.Count; i++)
            {
                spawnPoints[i].Draw(offset);
            }

            // Draw mobs
            for (int i = 0; i < mobs.Count; i++)
            {
                mobs[i].Draw(offset);
            }
        }
    }
}
