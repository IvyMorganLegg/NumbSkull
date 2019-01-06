#region Using Statements

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using System;

#endregion

namespace NumbSkull
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

        //textures
        Texture2D far_background, mid_background;
        Texture2D tiles_images;

        //Rectangles
        Rectangle screenRect, desktopRect;

        //Positions
        static public Vector2 background_pos;

        //Render
        RenderTarget2D MainTarget;

        //MapData
        const int MAX_SHEET_PARTS = 300;
        Sheet[] sheet;
        SheetManager sheet_mgr;

        //Input
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
            //Setup
            spriteBatch = new SpriteBatch(GraphicsDevice);
            MainTarget = new RenderTarget2D(GraphicsDevice, SCREENWIDTH, SCREENHEIGHT);
            pp = GraphicsDevice.PresentationParameters;
            SurfaceFormat format = pp.BackBufferFormat;
            screenW = MainTarget.Width;
            screenH = MainTarget.Height;
            desktopRect = new Rectangle(0, 0, pp.BackBufferWidth, pp.BackBufferHeight);
            screenRect = new Rectangle(0, 0, screenW, screenH);
            screen_center = new Vector2(screenW / 2.0f, screenH / 2.0f) - new Vector2(32f, 32f);

            inp = new Input();
            base.Initialize();

            //Map
            sheet = new Sheet[MAX_SHEET_PARTS];
            sheet_mgr = new SheetManager();
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

            if (inp.Keydown(Keys.Left)) background_pos.X++;
            if (inp.Keydown(Keys.Right)) background_pos.X--;
            if (inp.Keydown(Keys.Up)) background_pos.Y++;
            if (inp.Keydown(Keys.Down)) background_pos.Y--;

            base.Update(gameTime);
        }

        // D R A W //
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.SetRenderTarget(MainTarget);

            //Far BG
            spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.Opaque, SamplerState.LinearWrap);
            spriteBatch.Draw(far_background, screenRect, new Rectangle((int)(-background_pos.X * 0.5f), 0, far_background.Width, far_background.Height), Color.White);
            spriteBatch.End();

            //Mid BG
            //spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.Opaque, SamplerState.LinearWrap);
            //spriteBatch.Draw(mid_background, screenRect, new Rectangle((int)(-background_pos.X), (int)(-background_pos.Y), far_background.Width, far_background.Height), Color.White);
            //spriteBatch.End();

            //DrawStuff
            GraphicsDevice.SetRenderTarget(null);
            spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.Opaque, SamplerState.LinearWrap, DepthStencilState.None, RasterizerState.CullNone);
            spriteBatch.Draw(MainTarget, desktopRect, Color.White);
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
