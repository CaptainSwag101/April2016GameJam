using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace HeartQuest
{
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        Texture2D menuCorner;
        Texture2D menuBar;
        Texture2D menuCenter;
        SpriteBatch spriteBatch;
        GameState gameState = GameState.TITLE_SCREEN;
        SpriteFont font144;
        SpriteFont font72;
        SpriteFont font16;
        SpriteFont font12;
        SpriteFont font10;
        Menu menu; // todo move to ui manager

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        protected override void Initialize()
        {
            this.IsMouseVisible = true;

            InputManager.Initialize();

            base.Initialize();
        }

        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            font144 = Content.Load<SpriteFont>("source-sans-144");
            font72 = Content.Load<SpriteFont>("source-sans-72");
            font16 = Content.Load<SpriteFont>("source-sans-16");
            font12 = Content.Load<SpriteFont>("source-sans-12");
            font10 = Content.Load<SpriteFont>("source-sans-10");

            string[] test = {"Start game", "Info Screen"};
            menuCorner = Content.Load<Texture2D>("MenuCorner");
            menuBar = Content.Load<Texture2D>("MenuSide");
            menuCenter = Content.Load<Texture2D>("Center");
            menu = new Menu(test, new Vector2(240, 240), menuCorner, menuBar, menuCenter, 20, 10, font16);
        }
        
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            switch (gameState)
            {
                case GameState.TITLE_SCREEN:
                    InputManager.Update(gameTime);
                    menu.ProcessInput();

                    if (menu.IsOver)
                    {
                        if (menu.Selection == 0)
                        {
                            gameState = GameState.PLAYING;
                            string[] options = { "Win", "Lose" };
                            menu = new Menu(options, menu.Position, menuCorner, menuBar, menuCenter, menu.Width, menu.Height, font16);
                        }
                        else if (menu.Selection == 1)
                        {
                            gameState = GameState.INFO_SCREEN;
                            string[] options = { "Return to title", "Break game" };
                            menu = new Menu(options, menu.Position, menuCorner, menuBar, menuCenter, menu.Width, menu.Height, font16);
                        }
                        // TODO handle other options
                    }
                    break;

                case GameState.PLAYING:
                    InputManager.Update(gameTime);
                    menu.ProcessInput();

                    if (menu.IsOver)
                    {
                        if (menu.Selection == 0)
                        {
                            gameState = GameState.VICTORY_SCREEN;
                            string[] options = { "Return to Title", "Break game" };
                            menu = new Menu(options, menu.Position, menuCorner, menuBar, menuCenter, menu.Width, menu.Height, font16);
                        }

                        if (menu.Selection == 1)
                        {
                            gameState = GameState.GAMEOVER_SCREEN;
                            string[] options = { "Return to title", "Break game" };
                            menu = new Menu(options, menu.Position, menuCorner, menuBar, menuCenter, menu.Width, menu.Height, font16);
                        }
                    }
                    break;

                case GameState.INFO_SCREEN:
                    InputManager.Update(gameTime);
                    menu.ProcessInput();

                    if (menu.IsOver)
                    {
                        if (menu.Selection == 0)
                        {
                            gameState = GameState.TITLE_SCREEN;
                            string[] options = { "Start game", "Info Screen" };
                            menu = new Menu(options, menu.Position, menuCorner, menuBar, menuCenter, menu.Width, menu.Height, font16);
                        }
                        
                    }
                    break;

                case GameState.VICTORY_SCREEN:
                    InputManager.Update(gameTime);
                    menu.ProcessInput();

                    if (menu.IsOver)
                    {
                        if (menu.Selection == 0)
                        {
                            gameState = GameState.TITLE_SCREEN;
                            string[] options = { "Start game", "Info Screen" };
                            menu = new Menu(options, menu.Position, menuCorner, menuBar, menuCenter, menu.Width, menu.Height, font16);
                        }
                        
                    }
                    break;

                case GameState.GAMEOVER_SCREEN:
                    InputManager.Update(gameTime);
                    menu.ProcessInput();

                    if (menu.IsOver)
                    {
                        if (menu.Selection == 0)
                        {
                            gameState = GameState.TITLE_SCREEN;
                            string[] options = { "Start game", "Info Screen" };
                            menu = new Menu(options, menu.Position, menuCorner, menuBar, menuCenter, menu.Width, menu.Height, font16);
                        }
                        
                    }
                    break;

            }

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin();

            switch (gameState)
            {
                case GameState.TITLE_SCREEN:
                    string titleScreenText = "Heart Quest";
                    Vector2 textDimmensions = font72.MeasureString(titleScreenText);
                    spriteBatch.DrawString(font72, titleScreenText, new Vector2((GraphicsDevice.Viewport.Width - textDimmensions.X)/2.0f, 50), Color.Black);
                    string creditsText = "Made by Devin Duren and Justin Furtado";
                    Vector2 otherText = font16.MeasureString(creditsText);
                    spriteBatch.DrawString(font16, creditsText, new Vector2((GraphicsDevice.Viewport.Width - otherText.X) / 2.0f, GraphicsDevice.Viewport.Height - 25), Color.Black);
                    menu.Draw(spriteBatch);
                    break;

                case GameState.PLAYING:
                    string todo = "INSERT GAME HERE";
                    Vector2 temp = font72.MeasureString(todo);
                    spriteBatch.DrawString(font72, todo, new Vector2((GraphicsDevice.Viewport.Width - temp.X) / 2.0f, 50), Color.Black);
                    menu.Draw(spriteBatch);

                    break;

                case GameState.INFO_SCREEN:
                    string infoText = "Made the weekened of April 29 2016 at Neumont University for a Game Jam";
                    string infoText2 = "Created by Justin Furtado and Devin Duren";
                    string infoText3 = "Special thanks to Blake Dennis (ideas) and Doug Gatto (motivation/rectangles)";
                    Vector2 d1 = font16.MeasureString(infoText);
                    Vector2 d2 = font16.MeasureString(infoText2);
                    Vector2 d3 = font16.MeasureString(infoText3);
                    spriteBatch.DrawString(font16, infoText, new Vector2(50, 50), Color.Black);
                    spriteBatch.DrawString(font16, infoText2, new Vector2(50, 75), Color.Black);
                    spriteBatch.DrawString(font16, infoText3, new Vector2(50, 100), Color.Black);

                    menu.Draw(spriteBatch);

                    break;

                case GameState.VICTORY_SCREEN:
                    string victoryText = "Conratulations!";
                    string thanks = "Hope you enjoyed playing HeartQuest.";
                    Vector2 t1 = font72.MeasureString(victoryText);
                    Vector2 t2 = font16.MeasureString(thanks);
                    spriteBatch.DrawString(font72, victoryText, new Vector2((GraphicsDevice.Viewport.Width - t1.X) / 2.0f, 50), Color.Black);
                    spriteBatch.DrawString(font16, thanks, new Vector2((GraphicsDevice.Viewport.Width - t2.X) / 2.0f, 150), Color.Black);
                    creditsText = "Made by Devin Duren and Justin Furtado";
                    otherText = font16.MeasureString(creditsText);
                    spriteBatch.DrawString(font16, creditsText, new Vector2((GraphicsDevice.Viewport.Width - otherText.X) / 2.0f, GraphicsDevice.Viewport.Height - 25), Color.Black);
                    menu.Draw(spriteBatch);

                    break;

                case GameState.GAMEOVER_SCREEN:
                    string gameOverText = "Game Over!";
                    thanks = "Hope you enjoyed playing HeartQuest.";
                    t1 = font72.MeasureString(gameOverText);
                    t2 = font16.MeasureString(thanks);
                    spriteBatch.DrawString(font72, gameOverText, new Vector2((GraphicsDevice.Viewport.Width - t1.X) / 2.0f, 50), Color.Black);
                    spriteBatch.DrawString(font16, thanks, new Vector2((GraphicsDevice.Viewport.Width - t2.X) / 2.0f, 150), Color.Black);
                    creditsText = "Made by Devin Duren and Justin Furtado";
                    otherText = font16.MeasureString(creditsText);
                    spriteBatch.DrawString(font16, creditsText, new Vector2((GraphicsDevice.Viewport.Width - otherText.X) / 2.0f, GraphicsDevice.Viewport.Height - 25), Color.Black);
                    menu.Draw(spriteBatch);

                    break;

            }

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}

enum GameState
{
    TITLE_SCREEN,
    PLAYING,
    GAMEOVER_SCREEN,
    VICTORY_SCREEN,
    INFO_SCREEN
}