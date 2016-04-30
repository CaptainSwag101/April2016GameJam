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
            menu = new Menu(test, Vector2.Zero, menuCorner, menuBar, menuCenter, 50, 30, font72);
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
                            menu = new Menu(options, menu.Position, menuCorner, menuBar, menuCenter, menu.Width, menu.Height, font72);
                        }
                        else if (menu.Selection == 1)
                        {
                            gameState = GameState.INFO_SCREEN;
                            string[] options = { "Return to title", "Break game" };
                            menu = new Menu(options, menu.Position, menuCorner, menuBar, menuCenter, menu.Width, menu.Height, font72);
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
                            menu = new Menu(options, menu.Position, menuCorner, menuBar, menuCenter, menu.Width, menu.Height, font72);
                        }

                        if (menu.Selection == 1)
                        {
                            gameState = GameState.GAMEOVER_SCREEN;
                            string[] options = { "Return to title", "Break game" };
                            menu = new Menu(options, menu.Position, menuCorner, menuBar, menuCenter, menu.Width, menu.Height, font72);
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
                            menu = new Menu(options, menu.Position, menuCorner, menuBar, menuCenter, menu.Width, menu.Height, font72);
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
                            menu = new Menu(options, menu.Position, menuCorner, menuBar, menuCenter, menu.Width, menu.Height, font72);
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
                            menu = new Menu(options, menu.Position, menuCorner, menuBar, menuCenter, menu.Width, menu.Height, font72);
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
                    menu.Draw(spriteBatch);
                    break;

                case GameState.PLAYING:
                    menu.Draw(spriteBatch);

                    break;

                case GameState.INFO_SCREEN:
                    menu.Draw(spriteBatch);

                    break;

                case GameState.VICTORY_SCREEN:
                    menu.Draw(spriteBatch);

                    break;

                case GameState.GAMEOVER_SCREEN:
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