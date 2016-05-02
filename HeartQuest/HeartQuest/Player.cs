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
    class Player : Entity
    {
        public bool Cutscene { get; set; }
        private int FrameStart = 0;
        private float punchTimer = 0.0f;
        private float punchTime = 1.0f;
        private bool punching = false;

        public bool Punched { get; set; }
        
        public Player(Texture2D[] images, Vector2 startPos) : base(images, startPos, 0)
        {
            HasHeart = true;
            Cutscene = true;
        }

        public override void Update(GameTime gameTime)
        {
            if (!Cutscene)
            {
                punchTimer += (float)gameTime.ElapsedGameTime.TotalSeconds;

                if (InputManager.KeyPressed(Keys.E))
                {
                    if (punchTimer > punchTime)
                    {
                        punchTimer = 0.0f;
                        punching = true;
                        Punched = true;
                    }
                }
                else
                {
                    if (punchTimer > punchTime)
                    {
                        punching = false;
                    }
                }

                // set frame to punching frame
                if (punching)
                {
                    Velocity = new Vector2(0, Velocity.Y);

                    if (HasHeart)
                    {
                        // if already punching left or facing left
                        if (CurrentImage == 9 || CurrentImage == 2 || CurrentImage == 3 || CurrentImage == 6 || CurrentImage == 7)
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
                        if (CurrentImage == 11 || CurrentImage == 6 || CurrentImage == 7 || CurrentImage == 2 || CurrentImage == 3)
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
                else // only move if not punching because punching is hard!
                {
                    IsMoving = InputManager.CurrentState.IsKeyDown(Keys.A) || InputManager.CurrentState.IsKeyDown(Keys.D);

                    int walkCount = 0;

                    if (IsMoving)
                    {
                        walkCount = (int)(gameTime.TotalGameTime.TotalSeconds * 10) % 2;
                    }

                    CurrentImage = walkCount + FrameStart + (HasHeart ? 0 : 4);

                    if (InputManager.KeyPressed(Keys.W) && IsOnGround)
                    {
                        Game1.jump.Play();
                        Velocity = new Vector2(Velocity.X, -100.0f);
                        IsOnGround = false;
                    }

                    // todo is down crouch

                    if (InputManager.CurrentState.IsKeyDown(Keys.A))
                    {
                        //LastFrameStart = FrameStart;
                        //change to left pic
                        Velocity = new Vector2(-50.0f, Velocity.Y);
                        IsOnGround = false;
                        FrameStart = 2; //left, no walk
                    }
                    else if (InputManager.CurrentState.IsKeyDown(Keys.D))
                    {
                        //change to right pic
                        Velocity = new Vector2(50.0f, Velocity.Y);
                        IsOnGround = false;
                        FrameStart = 0;
                    }
                    else
                    {
                        Velocity = new Vector2(0, Velocity.Y);
                    }
                }
            }
            else
            {
                // cutscene
                int walkCount = 0;

                if (IsMoving)
                {
                    walkCount = (int)(gameTime.TotalGameTime.TotalSeconds * 10) % 2;
                }

                CurrentImage = walkCount + FrameStart + (HasHeart ? 0 : 4);

                if (Velocity.X < 0)
                {
                    FrameStart = 2; //left, no walk
                }
                else if (Velocity.X > 0)
                {
                    FrameStart = 0;
                }
            }

            base.Update(gameTime);
        }
    }
}
