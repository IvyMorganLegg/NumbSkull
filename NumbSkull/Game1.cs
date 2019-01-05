#region Using Statements

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

#endregion

namespace MonoGame
{
    public class Game1 : Game
    {

        // D I S P L A Y //
        const int SCREENWIDTH = 1024, SCREENHEIGHT = 768; //TARGET FORMAT
        const bool FULLSCREEN = false;

        GraphicsDeviceManager graphics;
        PresentationParameters pp;
        SpriteBatch spriteBatch;

        static public int screenW, screenH;
        static public Vector2 screen_center;

        // T E X T U R E S //
        Texture2D far_background, mid_background;
        Texture2D tiles_images;

        // R E C T A N G L E S //
        Rectangle screenRect, desktopRect;

        // P O S I T I O N S //
        static public Vector2 background_pos;

        // R E N D E R  T A R G E T S //
        RenderTarget2D MainTarget;

        // I N P U T //
        Input inp;

        // C O N S T R U C T //
        public Game1()
        {
            int initial_screen_width = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width - 10;
            int initial_screen_height = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height - 10;
            graphics = new GraphicsDeviceManager(this)
            {
                PreferredBackBufferWidth = initial_screen_height,
                PreferredBackBufferHeight = initial_screen_width,
                IsFullScreen = FULLSCREEN,
                PreferredDepthStencilFormat = DepthFormat.Depth16
            };
            Window.IsBorderless = true;
            Content.RootDirectory = "Content";
            graphics.IsFullScreen = true;
        }

        // I N I T //
        protected override void Initialize()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            MainTarget = new RenderTarget2D(GraphicsDevice, SCREENWIDTH, SCREENHEIGHT);
            pp = GraphicsDevice.PresentationParameters;
            SurfaceFormat format = pp.BackBufferFormat;
            screenW = MainTarget.Width;
            screenH = MainTarget.Height;
            desktopRect = new Rectangle(0, 0, pp.BackBufferWidth, pp.BackBufferHeight);
            screenRect = new Rectangle(0, 0, screenW, screenH);
            //screenRect.Center
            screen_center = new Vector2(screenW / 2.0f, screenH / 2.0f) - new Vector2(32f, 32f);

            inp = new Input();
            base.Initialize();
        }

        // L O A D //
        protected override void LoadContent()
        {
            //Graphics

            far_background = Content.Load<Texture2D>("back_cave");
            mid_background = Content.Load<Texture2D>("mid-ground");
            tiles_images = Content.Load<Texture2D>("tiles1");
        }

        // U P D A T E //
        protected override void Update(GameTime gameTime)
        {
            inp.Update();
            if (inp.Keypress(Keys.Escape)) Exit();

            base.Update(gameTime);
        }

        // D R A W //
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.SetRenderTarget(MainTarget);

            //DrawStuff
            //graphics.SetRenderTarget(null);
            spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.Opaque, SamplerState.LinearWrap, DepthStencilState.None, RasterizerState.CullNone);
            spriteBatch.Draw(MainTarget, desktopRect, Color.White);

            base.Draw(gameTime);
        }
    }
}
