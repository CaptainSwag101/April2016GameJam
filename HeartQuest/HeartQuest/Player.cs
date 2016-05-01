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
        public Player(Texture2D[] images, Vector2 startPos) : base(images, startPos, 0)
        {

        }

        public override void Update(GameTime gameTime)
        {
            if (InputManager.KeyPressed(Keys.W) && IsOnGround)
            {
                Velocity = new Vector2(Velocity.X, -100.0f);
                IsOnGround = false;
            }
            
            // todo is down crouch

            if (InputManager.CurrentState.IsKeyDown(Keys.A))
            {
                Velocity = new Vector2(-50.0f, Velocity.Y);
                IsOnGround = false;

            }
            else if (InputManager.CurrentState.IsKeyDown(Keys.D))
            {
                Velocity = new Vector2(50.0f, Velocity.Y);
                IsOnGround = false;

            }
            else
            {
                Velocity = new Vector2(0, Velocity.Y);
            }

            base.Update(gameTime);
        }
    }
}
