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
        public Boss Boss { get; private set; }
        public Tile[,] Tiles { get; private set; }
        public int Width { get; private set; }
        public int Height { get; private set; }
        public Texture2D HealthBack { get; private set; }
        public Texture2D HealthFront { get; private set; }
        public string[][] CutsceneText { get; private set; }
        public int CutsceneNum { get; private set; }
        private int distBetweenScenes = 32;
        private int startX;
        private Menu continueMenu;
        private TextDisplayBox displayBox;


        private Menu menu;
        private int menuBelongsToX;
        private int menuBelongsToY;

        public bool IsPlayerDead
        {
            get
            {
                return Player.Bounds.Top > 500 || Player.Health <= 0;
            }
        }

        public bool IsBossDead
        {
            get
            {
                if (Boss == null)
                {
                    return false;
                }

                if (Boss.Health <= 0)
                {
                    Boss = null;
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
        public World(Texture2D[] tileImages, Texture2D[] playerImages, Texture2D healthBack, Texture2D healthFront)
        {
            Width = 25;
            Height = 15;
            Player = new Player(playerImages, new Vector2(450, 384));
            LoadTiles(tileImages);
            HealthBack = healthBack;
            HealthFront = healthFront;
            Boss = new Boss(Game1.bossImages, new Vector2(350, 384), Player);
            CutsceneText = new string[4][];
            CutsceneText[0] = new string[] { "Hey, friend. Its quite a nice night isn't it?", "Wait... What's that over there?" };
            CutsceneText[1] = new string[] { "*He punches you in the heart while you are distracted*" };
            CutsceneText[2] = new string[] { "Finally! A heart of my own!" };
            CutsceneText[3] = new string[] { "Dude, not cool!" };
            CutsceneNum = 0;
            displayBox = new TextDisplayBox(CutsceneText[CutsceneNum], new Vector2(40, 50), Game1.textCorner, Game1.textBar, Game1.textCenter, 45, 10, Game1.font16);
            continueMenu = new Menu(new string[] { "Continue" }, new Vector2(240, 240), Game1.menuCorner, Game1.menuBar, Game1.menuCenter, 20, 5, Game1.font16);
            startX = Player.Bounds.X + 32;
        }

        private void LoadTiles(Texture2D[] tileImages)
        {
            Tiles = new Tile[Width, Height];

            for (int x = 0; x < Width; ++x)
            {
                for (int y = 0; y < Height; ++y)
                {
                    if (x == 1 && y == 13 || x == 23 && y == 13 || x == 21 && y == 8 || x == 18 && y == 7 || x == 15 && y == 6
                        || x == 3 && y == 8 || x == 6 && y == 7 || x == 9 && y == 6)
                    {
                        Tiles[x, y] = new Tile(Game1.flowerImages, new Vector2(x, y) * 32.0f, false, false, true, 0, new string[] { "Ignore", "Plant Flower", "Break" });
                    }
                    else if (y == 14) // bottom wall
                    {
                        Tiles[x, y] = new Tile(tileImages[0], new Vector2(x, y) * 32.0f, true, false);
                    }
                    else if (x == 24) // right wall
                    {
                        Tiles[x, y] = new Tile(tileImages[1], new Vector2(x, y) * 32.0f, true, false);
                    }
                    else if (y == 0) // top wall
                    {
                        Tiles[x, y] = new Tile(tileImages[2], new Vector2(x, y) * 32.0f, true, false);
                    }
                    else if (x == 0) // left wall
                    {
                        Tiles[x, y] = new Tile(tileImages[1], new Vector2(x, y) * 32.0f, true, false);
                    }
                    else if (x == 21 && y > 8) // right tree
                    {
                        Tiles[x, y] = new Tile(tileImages[1], new Vector2(x, y) * 32.0f, y < 10, false);
                    }
                    else if (x == 18 && y > 7) // 2nd right tree
                    {
                        Tiles[x, y] = new Tile(tileImages[1], new Vector2(x, y) * 32.0f, y < 10, false);
                    }
                    else if (x == 15 && y > 6) // 3rd right tree
                    {
                        Tiles[x, y] = new Tile(tileImages[1], new Vector2(x, y) * 32.0f, y < 10, false);
                    }
                    else if (x == 3 && y > 8) // left tree
                    {
                        Tiles[x, y] = new Tile(tileImages[1], new Vector2(x, y) * 32.0f, y < 10, false);
                    }
                    else if (x == 6 && y > 7) // 2nd left tree
                    {
                        Tiles[x, y] = new Tile(tileImages[1], new Vector2(x, y) * 32.0f, y < 10, false);
                    }
                    else if (x == 9 && y > 6) // 3rd left tree
                    {
                        Tiles[x, y] = new Tile(tileImages[1], new Vector2(x, y) * 32.0f, y < 10, false);
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
            if (Player.Cutscene)
            {
                if (Math.Abs(Player.Bounds.X - startX) >= distBetweenScenes)
                {
                    Player.Velocity = Vector2.Zero;

                    if (continueMenu != null)
                    {
                        continueMenu.ProcessInput();

                        if (continueMenu.IsOver)
                        {
                            CutsceneNum++;
                            if (CutsceneNum >= CutsceneText.Length)
                            {
                                continueMenu = null;
                                displayBox = null;
                                Player.Cutscene = false;
                                Game1.roamLoop.Stop();
                                Game1.bossLoop.Play();
                                return;
                            }

                            if (CutsceneNum == 2)
                            {
                                Player.Health -= 5;
                            }

                            displayBox = new TextDisplayBox(CutsceneText[CutsceneNum], new Vector2(40, 50), Game1.textCorner, Game1.textBar, Game1.textCenter, 45, 10, Game1.font16);
                            continueMenu.ResetMenu();
                            startX = Player.Bounds.X;
                        }
                    }
                }
                else
                {
                    Player.Velocity = new Vector2(-50.0f, 0);
                }
            }

            Player.Update(gameTime);

            if (Player.Health >= Boss.Health)
            {
                Player.HasHeart = true;
                Boss.HasHeart = false;
            }
            else
            {
                Player.HasHeart = false;
                Boss.HasHeart = true;
            }

            // movement
            Vector2 potentialMove = Player.Velocity * (float)gameTime.ElapsedGameTime.TotalSeconds;
            Vector2 actualMove = CheckCollosionsFor(Player, potentialMove);
            Player.Stop(actualMove.X == 0, actualMove.Y == 0);
            Player.MoveBy(actualMove);

            if (Boss != null)
            {
                Boss.Update(gameTime);

                Vector2 bossMove = Boss.Velocity * (float)gameTime.ElapsedGameTime.TotalSeconds;
                Vector2 actualBossMove = CheckCollosionsFor(Boss, bossMove);
                Boss.Stop(actualBossMove.X == 0, actualBossMove.Y == 0);
                Boss.MoveBy(actualBossMove);

                // todo, if player hit different enemy
                if (Player.Punched)
                {
                    Player.Punched = false;

                    if (Boss.Bounds.Intersects(new Rectangle(Player.Bounds.X - 16, Player.Bounds.Y, Player.Bounds.Width + 32, Player.Bounds.Height)))
                    {
                        if (Player.CurrentImage == 11 || Player.CurrentImage == 9) 
                        {
                            if (Boss.Bounds.X < Player.Bounds.X)
                            {
                                Boss.Health -= 1;
                            }
                        }
                        else if (Player.CurrentImage == 10 || Player.CurrentImage == 8)
                        {
                            if (Boss.Bounds.X > Player.Bounds.X)
                            {
                                Boss.Health -= 1;
                            }
                        }
                      
                    }
                }
            }

            // interaction
            if (menu == null)
            {
                CheckInteractions();
            }
            else
            {
                menu.ProcessInput();
                if (menu.IsOver)
                {
                    Tiles[menuBelongsToX, menuBelongsToY].InteractedWith(Player, menu.Selection);
                    menu = null;
                }
            }
        }

        private void CheckInteractions()
        {
            for (int x = 0; x < Width; ++x)
            {
                for (int y = 0; y < Height; ++y)
                {
                    if (Tiles[x, y] != null && Player.Bounds.Intersects(Tiles[x, y].Bounds) && Tiles[x, y].IsInteractable && InputManager.KeyPressed(Microsoft.Xna.Framework.Input.Keys.Space))
                    {
                        menu = new Menu(Tiles[x, y].MenuOptions, new Vector2(240, 240), Game1.menuCorner, Game1.menuBar, Game1.menuCenter, 20, 10, Game1.font16);
                        menuBelongsToX = x;
                        menuBelongsToY = y;
                        return;
                    }
                }
            }
        }

        private Vector2 CheckCollosionsFor(Entity e, Vector2 potentialMove)
        {
            Vector2 actualMove = potentialMove;
            for (int x = 0; x < Width; ++x)
            {
                for (int y = 0; y < Height; ++y)
                {
                    if (Tiles[x, y] != null && Tiles[x, y].Bounds.Intersects(new Rectangle((int)(e.Position.X + actualMove.X), (int)(e.Position.Y + actualMove.Y), e.Bounds.Width, e.Bounds.Height)))
                    {
                        actualMove = e.CollideWith(Tiles[x, y], actualMove);
                    }
                }
            }
            return actualMove;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            // draw background tiles
            for (int x = 0; x < Width; ++x)
            {
                for (int y = 0; y < Height; ++y)
                {
                    if (Tiles[x, y] != null && Tiles[x, y].Background)
                    {
                        Tiles[x, y].Draw(spriteBatch);
                    }
                }
            }

            // draw player and other entities
            Player.Draw(spriteBatch);

            if (Boss != null)
            {
                Boss.Draw(spriteBatch);
            }

            // draw foreground tiles
            for (int x = 0; x < Width; ++x)
            {
                for (int y = 0; y < Height; ++y)
                {
                    if (Tiles[x, y] != null && !Tiles[x, y].Background)
                    {
                        Tiles[x, y].Draw(spriteBatch);
                    }
                }
            }

            if (!IsPlayerDead)
            {
                spriteBatch.Draw(HealthBack, new Rectangle(0, 0, HealthBack.Width, HealthBack.Height), Color.White);
                spriteBatch.Draw(HealthFront, new Rectangle(0, 0, Player.Health * 2, HealthFront.Height), new Rectangle(0, 0, Player.Health * 2, HealthFront.Height), Color.White);
            }

            if (Boss != null)
            {
                spriteBatch.Draw(HealthBack, new Rectangle(600, 0, HealthBack.Width, HealthBack.Height), Color.White);
                spriteBatch.Draw(HealthFront, new Rectangle(600, 0, Boss.Health * 2, HealthFront.Height), new Rectangle(0, 0, Boss.Health * 2, HealthFront.Height), Color.White);
            }

            spriteBatch.Draw(Game1.blackRect, Game1.screenBounds, Color.White * 0.5f);


            if (menu != null)
            {
                menu.Draw(spriteBatch);
            }

            if (continueMenu != null)
            {
                continueMenu.Draw(spriteBatch);
            }

            if (displayBox != null)
            {
                displayBox.Draw(spriteBatch);
            }
        }
    }
}
