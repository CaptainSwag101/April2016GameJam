using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeartQuest
{
    class Boss : Entity
    {
        private int FrameStart = 0;
        private float punchTimer = 0.0f;
        private float punchTime = 1.0f;
        private bool punching = false;
        private Player target;

        public Boss(Texture2D[] images, Vector2 startPos, Player target) : base(images, startPos, 0)
        {
            this.target = target;
            HasHeart = true;
        }

        public override void Update(GameTime gameTime)
        {
            punchTimer += (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (!punching && target.Bounds.Intersects(new Rectangle(Bounds.X - 16, Bounds.Y, Bounds.Width + 32, Bounds.Height)))
            {
                if (punchTimer > punchTime)
                {
                    punchTimer = 0.0f;
                    punching = true;
                    target.Health -= 5;
                }
            }
            else
            {
                if (punchTimer > punchTime)
                {
                    punchTimer = 0.0f;
                    punching = false;
                }
            }

            if (punching)
            {
                Velocity = new Vector2(0, Velocity.Y);

                if (HasHeart)
                {
                    // if already punching left or facing left
                    if (target.Position.X < Position.X)
                    {
                        // punch left
                        CurrentImage = 9;
                    }
                    else
                    {
                        // punch right
                        CurrentImage = 8;
                    }
                }
                else
                {
                    // if already punching left or facing left
                    if (target.Position.X < Position.X)
                    {
                        //punch left
                        CurrentImage = 11;
                    }
                    else
                    {
                        // punch right
                        CurrentImage = 10;
                    }
                }
            }
            else
            {
                IsMoving = target.Bounds.Right < Bounds.Left || target.Bounds.Left > Bounds.Right;
                int walkCount = 0;

                if (IsMoving)
                {
                    walkCount = (int)(gameTime.TotalGameTime.TotalSeconds * 10) % 2;
                }

                CurrentImage = walkCount + FrameStart + (HasHeart ? 0 : 4);

                if ((Vector2.Distance(target.Position, this.Position) > (1.5f * 32.0f)) && (Vector2.Distance(target.Position, this.Position) < (3.5f * 32.0f)) && (IsOnGround))
                {
                    Game1.jump.Play();
                    Velocity = new Vector2(Velocity.X, -110.0f);
                    IsOnGround = false;
                }

                if (target.Bounds.Right < Bounds.Left)
                {
                    //LastFrameStart = FrameStart;
                    //change to left pic
                    Velocity = new Vector2(-30.0f, Velocity.Y);
                    IsOnGround = false;
                    FrameStart = 2; //left, no walk
                }
                else if (target.Bounds.Left > Bounds.Right)
                {
                    //change to right pic
                    Velocity = new Vector2(30.0f, Velocity.Y);
                    IsOnGround = false;
                    FrameStart = 0;
                }
                else
                {
                    Velocity = new Vector2(0, Velocity.Y);
                }

            }

            base.Update(gameTime);
        }

    }
}
