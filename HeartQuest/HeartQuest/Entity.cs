using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeartQuest
{
    abstract class Entity
    {
        public Texture2D[] Images { get; protected set; }
        public Vector2 Position { get; protected set; }
        public Vector2 Velocity { get; protected set; }
        public int CurrentImage { get; protected set; }
        public bool IsOnGround { get; protected set; }
        public bool IsMoving { get; protected set; }
        public Rectangle Bounds
        {
            get
            {
                return new Rectangle((int)Position.X, (int)Position.Y, Images[CurrentImage].Width, Images[CurrentImage].Height);
            }
        }
        private int health;
        public int Health
        {
            get
            {
                return health;
            }
            set
            {
                health = MathHelper.Clamp(value, 0, 100);
            }
        }
        public Entity(Texture2D[] images, Vector2 startPos, int startImage)
        {
            Images = images;
            Position = startPos;
            CurrentImage = startImage;
            Velocity = Vector2.Zero;
            IsOnGround = false;
            health = 100;
            IsMoving = false;
        }

        public void MoveBy(Vector2 amount)
        {
            Position += amount;
        }

        public virtual void Update(GameTime gameTime)
        {
            Velocity += (float)gameTime.ElapsedGameTime.TotalSeconds * Game1.Gravity;
        }

        public void Stop(bool x, bool y)
        {
            Velocity = new Vector2(x ? 0: Velocity.X, y ? 0 : Velocity.Y);
        }

        public Vector2 CollideWith(Tile t, Vector2 potentialMove)
        {
            Vector2 actualMove = potentialMove;

            if (t.IsSolid)
            {
                // hit bottom time
                if ((actualMove.Y > 0) && ((Bounds.Bottom + actualMove.Y) > t.Bounds.Top) && (t.Bounds.Right > (Bounds.Left + actualMove.X)) && (t.Bounds.Left < (Bounds.Right + actualMove.X)))
                {
                    actualMove = new Vector2(actualMove.X, 0);
                    IsOnGround = true;
                }
                else if ((actualMove.Y) < 0 && ((Bounds.Top + actualMove.Y) < t.Bounds.Bottom) && (t.Bounds.Right > (Bounds.Left + actualMove.X)) && (t.Bounds.Left < (Bounds.Right + actualMove.X)))
                {
                    actualMove = new Vector2(actualMove.X, 0);
                }

                // hit right tile
                if ((actualMove.X > 0) && ((Bounds.Right + actualMove.X) > t.Bounds.Left) && (t.Bounds.Bottom > (Bounds.Top + actualMove.Y)) && (t.Bounds.Top < (Bounds.Bottom + actualMove.Y)))
                {
                    actualMove = new Vector2(0, actualMove.Y);
                }
                else if ((actualMove.X < 0) && ((Bounds.Left + actualMove.X) < t.Bounds.Right) && (t.Bounds.Bottom > (Bounds.Top + actualMove.Y)) && (t.Bounds.Top < (Bounds.Bottom + actualMove.Y)))
                {
                    actualMove = new Vector2(0, actualMove.Y);
                }

                
            }

            return actualMove;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Images[CurrentImage], Bounds, Color.White);
        }
    }
}
