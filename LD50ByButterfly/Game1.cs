using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace LD50ByButterfly
{
    public class Game1 : Game
    {
        #region general assets and game functions
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        public Rectangle screenDimensions = new Rectangle(0, 0, 512, 320);

        enum GameState
        {
            StartMenu,
            OfficeView,
            EndScreen
        }
        private GameState _gameState;

        enum WindowSize
        {
            SizeByPixel,
            DoubleSize,
            Fullscreen
        }
        #endregion

        #region individual assets

        private Texture2D _texBackground;

        #endregion

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);SwitchToScreenSize(WindowSize.SizeByPixel);
            //Window.AllowUserResizing = true;

            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        void SwitchToScreenSize(WindowSize changeSize)
        {
            switch (changeSize)
            {
                case WindowSize.SizeByPixel:
                    _graphics.PreferredBackBufferWidth = screenDimensions.Width;
                    _graphics.PreferredBackBufferHeight = screenDimensions.Height;
                    _graphics.ApplyChanges();
                    break;
                case WindowSize.DoubleSize:
                    _graphics.PreferredBackBufferWidth = screenDimensions.Width*2;
                    _graphics.PreferredBackBufferHeight = screenDimensions.Height*2;
                    _graphics.ApplyChanges();
                    break;
                case WindowSize.Fullscreen:
                    _graphics.PreferredBackBufferWidth = screenDimensions.Width * 2;
                    _graphics.PreferredBackBufferHeight = screenDimensions.Height * 2;
                    _graphics.ApplyChanges();
                    break;
            }
        }

        protected override void Initialize()
        {
            _gameState = GameState.OfficeView;

            base.Initialize();
        }

        #region load content
        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            _texBackground = Content.Load<Texture2D>("Background");
        }
        #endregion

        #region update logic block
        protected override void Update(GameTime gameTime)
        {
            /*if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();*/

            if (Keyboard.GetState().IsKeyDown(Keys.S))
            {
                SwitchToScreenSize(WindowSize.SizeByPixel);
            }
            else if (Keyboard.GetState().IsKeyDown(Keys.D))
            {
                SwitchToScreenSize(WindowSize.DoubleSize);
            }
            else if (Keyboard.GetState().IsKeyDown(Keys.F))
            {
                SwitchToScreenSize(WindowSize.Fullscreen);
            }

            switch (_gameState)
            {
                case GameState.StartMenu:
                    UpdateStartMenu();
                    break;
                case GameState.OfficeView:
                    UpdateOfficeView();
                    break;
                case GameState.EndScreen:
                    UpdateEndScreen();
                    break;
            }

            base.Update(gameTime);
        }


        //update logic for Main Menu
        private void UpdateStartMenu()
        {

        }

        //update logic for Game/Office
        private void UpdateOfficeView()
        {

        }

        //update logic for End Screen
        private void UpdateEndScreen()
        {

        }
        #endregion

        #region draw block
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            _spriteBatch.Begin();

            switch (_gameState)
            {
                case GameState.StartMenu:
                    DrawStartMenu();
                    break;
                case GameState.OfficeView:
                    DrawOfficeView();
                    break;
                case GameState.EndScreen:
                    DrawEndScreen();
                    break;
            }

            _spriteBatch.End();

            base.Draw(gameTime);
        }

        //draw logic for Main Menu
        private void DrawStartMenu()
        {

        }

        //draw logic for Game/Office
        private void DrawOfficeView()
        {
            _spriteBatch.Draw(_texBackground, screenDimensions, Color.White);
        }

        //draw logic for End Screen
        private void DrawEndScreen()
        {

        }
        #endregion
    }
}
