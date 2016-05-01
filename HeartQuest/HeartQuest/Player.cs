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
        private int FrameStart = 0;
        private int LastFrameStart = 0;
        public Player(Texture2D[] images, Vector2 startPos) : base(images, startPos, 0)
        {

        }

        public override void Update(GameTime gameTime)
        {

            IsMoving = InputManager.CurrentState.IsKeyDown(Keys.A) || InputManager.CurrentState.IsKeyDown(Keys.D);
            
            if (IsMoving)
            {
                int walkCount = (int)(gameTime.TotalGameTime.TotalSeconds * 10) % 2;
                CurrentImage = walkCount + FrameStart;
            }

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

            base.Update(gameTime);
        }
    }
}
