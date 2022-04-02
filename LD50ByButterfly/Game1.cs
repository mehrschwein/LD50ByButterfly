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
        public float windowScale = 1.0f;

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

        private SpriteFont _font;
        #endregion

        #region individual assets
        private WallClock _wallClock = new WallClock();
        double runStartTime = 0;
        double lastClockTick = 0;

        int sleepTimer = 100;
        double sleepSpeedTick = 0;
        double sleepyness = 500;

        int money = 0;
        int allTimeMoney = 0;
        double lastMoneyTick = 0;

        private Texture2D _texBackground;
        private Texture2D _texHourArrow;
        private Texture2D _texMinuteArrow;
        private Texture2D _texForeGround;
        private Texture2D _texMug;
        private Texture2D _texFlamingo;

        private Texture2D _texMugBar;
        private Rectangle _rectMugBar;
        private Rectangle _rectMugDefault;
        private Rectangle _rectMugCoffeemachine;
        private Rectangle _rectCoffeemachine;

        #endregion

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);SwitchToScreenSize(WindowSize.SizeByPixel);

            Window.Title = "desk flamingo";
            Window.AllowUserResizing = false;

            //SwitchToScreenSize(WindowSize.DoubleSize);
            windowScale = 2.0f;
            ReloadScreensize();

            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        void SwitchToScreenSize(WindowSize changeSize)
        {
            switch (changeSize)
            {
                case WindowSize.SizeByPixel:
                    _graphics.PreferredBackBufferWidth = 512;
                    _graphics.PreferredBackBufferHeight = 320;
                    _graphics.ApplyChanges();
                    break;
                case WindowSize.DoubleSize:
                    _graphics.PreferredBackBufferWidth = 512 * 2;
                    _graphics.PreferredBackBufferHeight = 320 * 2;
                    _graphics.ApplyChanges();
                    break;
                case WindowSize.Fullscreen:
                    _graphics.PreferredBackBufferWidth = 512 * 2;
                    _graphics.PreferredBackBufferHeight = 320 * 2;
                    _graphics.ApplyChanges();
                    break;
            }
            screenDimensions.Width = _graphics.PreferredBackBufferWidth;
            screenDimensions.Height = _graphics.PreferredBackBufferHeight;
        }

        void ReloadScreensize()
        {
            _graphics.PreferredBackBufferWidth = (int)(512 * windowScale);
            _graphics.PreferredBackBufferHeight = (int)(320 * windowScale);
            _graphics.ApplyChanges();

            screenDimensions.Width = _graphics.PreferredBackBufferWidth;
            screenDimensions.Height = _graphics.PreferredBackBufferHeight;
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

            _font = Content.Load<SpriteFont>("Pixeled");

            _texBackground = Content.Load<Texture2D>("Background");
            _texHourArrow = Content.Load<Texture2D>("HourArrow");
            _texMinuteArrow = Content.Load<Texture2D>("MinuteArrow");
            _texFlamingo = Content.Load<Texture2D>("FlamingoWorker");
            _texForeGround = Content.Load<Texture2D>("foreground");
            _texMug = Content.Load<Texture2D>("Mug");
        }
        #endregion

        #region update logic block
        protected override void Update(GameTime gameTime)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
            {
                Exit();
            }

            if (Keyboard.GetState().IsKeyDown(Keys.P))
            {
                windowScale += 0.02f;
                ReloadScreensize();
            }
            else if (Keyboard.GetState().IsKeyDown(Keys.OemMinus))
            {
                windowScale -= 0.02f;
                ReloadScreensize();
            }

            switch (_gameState)
            {
                case GameState.StartMenu:
                    UpdateStartMenu(gameTime);
                    break;
                case GameState.OfficeView:
                    UpdateOfficeView(gameTime);
                    break;
                case GameState.EndScreen:
                    UpdateEndScreen(gameTime);
                    break;
            }

            base.Update(gameTime);
        }


        //update logic for Main Menu
        private void UpdateStartMenu(GameTime gameTime)
        {

            if (Keyboard.GetState().IsKeyDown(Keys.Enter))
            {
                _gameState = GameState.OfficeView;
            }
        }

        //update logic for Game/Office
        private void UpdateOfficeView(GameTime gameTime)
        {
            //_wallClock.AddTime();
            if ((gameTime.TotalGameTime.TotalMilliseconds - runStartTime) > (lastClockTick + 300))
            {
                _wallClock.AddTime();
                sleepTimer--;
                lastClockTick = gameTime.TotalGameTime.TotalMilliseconds;
            }

            if ((gameTime.TotalGameTime.TotalMilliseconds - runStartTime) > (sleepSpeedTick + sleepyness))
            {
                sleepTimer--;
                sleepyness -= 3;
                sleepSpeedTick = gameTime.TotalGameTime.TotalMilliseconds;
            }

            if ((gameTime.TotalGameTime.TotalMilliseconds - runStartTime) > (lastMoneyTick + 1000))
            {
                money++;
                allTimeMoney++;
                lastMoneyTick = gameTime.TotalGameTime.TotalMilliseconds;
            }
        }

        //update logic for End Screen
        private void UpdateEndScreen(GameTime gameTime)
        {

            if (Keyboard.GetState().IsKeyDown(Keys.Enter))
            {
                _gameState = GameState.OfficeView;
            }
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
            //_spriteBatch.Draw(_texHourArrow, new Rectangle((int)(392*windowScale), (int)(84 * windowScale), (int)(50 * windowScale), (int)(50 * windowScale)), Color.White);
            //_spriteBatch.Draw(_texMinuteArrow, new Rectangle((int)(392 * windowScale), (int)(84 * windowScale), (int)(50 * windowScale), (int)(50 * windowScale)), Color.White);
            Vector2 clockPos = new Vector2((int)((392 + 25) * windowScale), (int)((84+25) * windowScale));
            Vector2 clockOrigin = new Vector2((int)(25), (int)(25));
            _spriteBatch.Draw(_texHourArrow, clockPos, null, Color.White, _wallClock.GetHourRotation(), clockOrigin, windowScale, SpriteEffects.None, 1);
            _spriteBatch.Draw(_texMinuteArrow, clockPos, null, Color.White, _wallClock.GetMinuteRotation(), clockOrigin, windowScale, SpriteEffects.None, 1);
            _spriteBatch.Draw(_texFlamingo, screenDimensions, Color.White);
            _spriteBatch.Draw(_texForeGround, screenDimensions, Color.White);
            _spriteBatch.Draw(_texMug, new Rectangle((int)(300 * windowScale), (int)(206 * windowScale), (int)(18 * windowScale), (int)(18 * windowScale)), Color.White);

            _spriteBatch.DrawString(_font, "$ " + money, new Vector2(20, 20), Color.Black);
            //_spriteBatch.DrawString(_font, "Sleep Timer:  " + sleepTimer, new Vector2(20, 50), Color.Black);
            //_spriteBatch.DrawString(_font, "Sleepyness:  " + sleepyness, new Vector2(20, 80), Color.Black);
        }

        //draw logic for End Screen
        private void DrawEndScreen()
        {

        }
        #endregion
    }
}
