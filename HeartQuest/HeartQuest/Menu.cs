using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace HeartQuest
{
    class Menu
    {
        public string[] Options { get; private set; }
        public bool IsOver { get; private set; }
        public int Selection { get; private set; }
        public Texture2D CornerPiece { get; private set; }
        public Texture2D StraightPiece { get; private set; }
        public Texture2D CenterPiece { get; private set; }
        public int Width { get; private set; } // in num images
        public int Height { get; private set; } // in num images
        public Vector2 Position { get; private set; }
        public SpriteFont Font { get; private set; }


        public Menu(string[] options, Vector2 pos, Texture2D corner, Texture2D straight, Texture2D center, int width, int height, SpriteFont font)
        {
            Options = options;
            IsOver = false;
            Selection = 0;
            Position = pos;
            Width = width;
            Height = height;
            CornerPiece = corner;
            StraightPiece = straight;
            CenterPiece = center;
            Font = font;
        }

        public void ResetMenu()
        {
            IsOver = false;
            Selection = 0;
        }

        public void ProcessInput()
        {
            if (!IsOver)
            {
                if (InputManager.KeyPressed(Keys.Down))
                {
                    Game1.menuSelection.Play();
                    Selection = (Selection + 1) % Options.Length;
                }

                if (InputManager.KeyPressed(Keys.Up)) 
                {

                    Game1.menuSelection.Play();
                    Selection--;
                    if (Selection < 0) Selection = Options.Length - 1;
                }

                if (InputManager.KeyPressed(Keys.Enter))
                {
                    Game1.menuSelection.Play();
                    IsOver = true;
                }
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            DrawBG(spriteBatch);
            DrawText(spriteBatch);
           
        }

        private void DrawBG(SpriteBatch spriteBatch)
        {
            // draw top-left
            spriteBatch.Draw(CornerPiece, Position + new Vector2(CornerPiece.Width / 2.0f, CornerPiece.Height / 2.0f), null, Color.White, MathHelper.ToRadians(0.0f), new Vector2(CornerPiece.Width / 2.0f, CornerPiece.Height / 2.0f), Vector2.One, SpriteEffects.None, 0.0f);

            // draw top right
            spriteBatch.Draw(CornerPiece, Position + new Vector2(CornerPiece.Width / 2.0f, CornerPiece.Height / 2.0f) + (Width - 1) * (new Vector2(CornerPiece.Width, 0.0f)), null, Color.White, MathHelper.ToRadians(90.0f), new Vector2(CornerPiece.Width / 2.0f, CornerPiece.Height / 2.0f), Vector2.One, SpriteEffects.None, 0.0f);

            // draw bottom right
            spriteBatch.Draw(CornerPiece, Position + new Vector2(CornerPiece.Width / 2.0f, CornerPiece.Height / 2.0f) + (new Vector2((Width - 1) * CornerPiece.Width, (Height - 1) * CornerPiece.Height)), null, Color.White, MathHelper.ToRadians(180.0f), new Vector2(CornerPiece.Width / 2.0f, CornerPiece.Height / 2.0f), Vector2.One, SpriteEffects.None, 0.0f);

            // draw bottom left
            spriteBatch.Draw(CornerPiece, Position + new Vector2(CornerPiece.Width / 2.0f, CornerPiece.Height / 2.0f) + (Height - 1) * (new Vector2(0.0f, CornerPiece.Height)), null, Color.White, MathHelper.ToRadians(270.0f), new Vector2(CornerPiece.Width / 2.0f, CornerPiece.Height / 2.0f), Vector2.One, SpriteEffects.None, 0.0f);

            // draw left wall
            for (int y = 1; y < Height - 1; y++)
            {
                spriteBatch.Draw(StraightPiece, Position + new Vector2(StraightPiece.Width / 2.0f, StraightPiece.Height / 2.0f) + y * (new Vector2(0.0f, StraightPiece.Height)), null, Color.White, MathHelper.ToRadians(0.0f), new Vector2(StraightPiece.Width / 2.0f, StraightPiece.Height / 2.0f), Vector2.One, SpriteEffects.None, 0.0f);
            }

            // draw right wall
            for (int y = 1; y < Height - 1; y++)
            {
                spriteBatch.Draw(StraightPiece, Position + new Vector2(StraightPiece.Width / 2.0f, StraightPiece.Height / 2.0f) + (new Vector2((Width - 1) * CornerPiece.Width, y * StraightPiece.Height)), null, Color.White, MathHelper.ToRadians(180.0f), new Vector2(StraightPiece.Width / 2.0f, StraightPiece.Height / 2.0f), Vector2.One, SpriteEffects.None, 0.0f);
            }

            // draw top wall
            for (int x = 1; x < Width - 1; x++)
            {
                spriteBatch.Draw(StraightPiece, Position + new Vector2(StraightPiece.Width / 2.0f, StraightPiece.Height / 2.0f) + x * (new Vector2(StraightPiece.Width, 0.0f)), null, Color.White, MathHelper.ToRadians(90.0f), new Vector2(StraightPiece.Width / 2.0f, StraightPiece.Height / 2.0f), Vector2.One, SpriteEffects.None, 0.0f);
            }

            // draw bottom wall
            for (int x = 1; x < Width - 1; x++)
            {
                spriteBatch.Draw(StraightPiece, Position + new Vector2(StraightPiece.Width / 2.0f, StraightPiece.Height / 2.0f) + (new Vector2(x * StraightPiece.Width, (Height - 1) * StraightPiece.Height)), null, Color.White, MathHelper.ToRadians(270.0f), new Vector2(StraightPiece.Width / 2.0f, StraightPiece.Height / 2.0f), Vector2.One, SpriteEffects.None, 0.0f);
            }

            for (int x = 1; x < Width - 1; x++)
            {
                for (int y = 1; y < Height - 1; y++)
                {
                    spriteBatch.Draw(CenterPiece, new Rectangle((int)Position.X + (x * CenterPiece.Width), (int)Position.Y + (y * CenterPiece.Height), CenterPiece.Width, CenterPiece.Height), Color.White);
                }
            }
        }

        private void DrawText(SpriteBatch spriteBatch)
        {
            for (int j = 0; j < Options.Length; j++)
            {
                Color color = (j == Selection) ? Color.DarkGoldenrod: Color.White;
                Vector2 textSize = Font.MeasureString(Options[j]);
                spriteBatch.DrawString(Font, Options[j], Position + (new Vector2(StraightPiece.Width + 0.5f * (((Width-2) * CenterPiece.Width) - textSize.X), StraightPiece.Height + (j*textSize.Y))), color);
            }
        }

    }
}
