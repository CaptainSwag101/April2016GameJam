using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace HeartQuest
{
    class World
    {
        public Player Player { get; private set; }
        public Tile[,] Tiles { get; private set; }
        public int Width { get; private set; }
        public int Height { get; private set; }
        public Texture2D HealthBack { get; private set; }
        public Texture2D HealthFront { get; private set; }
        public bool IsPlayerDead
        {
            get
            {
                return Player.Bounds.Top > 500;
            }
        }

        public World(Texture2D[] tileImages, Texture2D[] playerImages, Texture2D healthBack, Texture2D healthFront)
        {
            Width = 25;
            Height = 15;
            Player = new Player(playerImages, new Vector2(100, 100));
            LoadTiles(tileImages);
            HealthBack = healthBack;
            HealthFront = healthFront;
           
        }

        private void LoadTiles(Texture2D[] tileImages)
        {
            Tiles = new Tile[Width, Height];

            for (int x = 0; x < Width; ++x)
            {
                for (int y = 0; y < Height; ++y)
                {
                    if (x == 0 || x == 24 || y == 0 || (y == 14 && !(x > 7 && x < 10)) || (x > 5 && x < 10 && y == 3))
                    {
                        Tiles[x, y] = new Tile(tileImages[0], new Vector2(x, y) * 32.0f, true);
                    }
                    else
                    {
                        Tiles[x, y] = null;
                    }
                }
            }
        }

        public void Update(GameTime gameTime)
        {
            //todo good/evil bar. Using GoodStatusBar.png and BadStatusBar.png

            Player.Update(gameTime);

            Vector2 potentialMove = Player.Velocity * (float)gameTime.ElapsedGameTime.TotalSeconds;

            Vector2 actualMove = CheckCollosions(potentialMove);

            Player.Stop(actualMove.X == 0, actualMove.Y == 0);

            Player.MoveBy(actualMove);
            
        }

        private Vector2 CheckCollosions(Vector2 potentialMove)
        {
            Vector2 actualMove = potentialMove;
            for (int x = 0; x < Width; ++x)
            {
                for (int y = 0; y < Height; ++y)
                {
                    if (Tiles[x,y] != null && Tiles[x, y].Bounds.Intersects(new Rectangle((int)(Player.Position.X + actualMove.X), (int)(Player.Position.Y + actualMove.Y), Player.Bounds.Width, Player.Bounds.Height)))
                    {
                        actualMove = Player.CollideWith(Tiles[x, y], actualMove);
                    }
                }
            }
            return actualMove;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            Player.Draw(spriteBatch);

            for (int x = 0; x < Width; ++x)
            {
                for (int y = 0; y < Height; ++y)
                {
                    if (Tiles[x, y] != null)
                    {
                        Tiles[x, y].Draw(spriteBatch);
                    }
                }
            }
            spriteBatch.Draw(HealthBack, new Rectangle(0, 0, HealthBack.Width, HealthBack.Height), Color.White);
            spriteBatch.Draw(HealthFront, new Rectangle(0, 0, Player.Health*2, HealthFront.Height), new Rectangle(0, 0, Player.Health * 2, HealthFront.Height), Color.White);
        }
    }
}
