using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeartQuest
{
    class Tile
    {
        public Texture2D[] Images { get; private set; }
        public Vector2 Position { get; private set; }
        public bool IsSolid { get; private set; }
        public bool IsInteractable { get; private set; }
        public int CurrentImage { get; private set; }
        public string[] MenuOptions { get; private set; }

        public Rectangle Bounds
        {
            get
            {
                return new Rectangle((int)Position.X, (int)Position.Y, Images[CurrentImage].Width, Images[CurrentImage].Height);
            }
        }

        public Tile(Texture2D image, Vector2 position, bool solid)
        {
            Images = new Texture2D[1];
            Images[0] = image;
            Position = position;
            IsSolid = solid;
            IsInteractable = false;
            CurrentImage = 0;
            MenuOptions = new string[0];

        }

        public Tile(Texture2D[] images, Vector2 position, bool solid, bool interactable, int startImage, string[] menuOptions)
        {
            Images = images;
            Position = position;
            IsSolid = solid;
            IsInteractable = interactable;
            CurrentImage = startImage;
            MenuOptions = menuOptions;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Images[CurrentImage], Bounds, Color.White);
        }

        public void InteractedWith(Player p, int selection)
        {
            if (IsInteractable)
            {
                CurrentImage = selection;

                if (selection == 1)
                {
                    IsInteractable = false;
                    p.Health += 10;
                }
                else if (selection == 2)
                {
                    //TODO if this is a flower, play sound.
                    Game1.potBreak.Play();
                    IsInteractable = false;
                    p.Health -= 10;
                }

            }
        }

    }
}
