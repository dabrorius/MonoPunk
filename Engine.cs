using System;
using Microsoft.Xna;
using Dabrorius.MonoPunk;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace Dabrorius.MonoPunk
{
	public class Engine : Microsoft.Xna.Framework.Game
	{
		GraphicsDeviceManager graphics;
		
		public static Engine currentEngine;
		
		
		
		public Engine (int width, int height, string assetsDirectory = "./") : base()
		{
			MP.Width = width;
			MP.Height = height;
			MP.currentWorld = new World ();
			
			Window.AllowUserResizing = true;

			graphics = new GraphicsDeviceManager (this);
			graphics.SynchronizeWithVerticalRetrace = false;
			graphics.PreferredBackBufferWidth = MP.Width;
			graphics.PreferredBackBufferHeight = MP.Height;
			graphics.ApplyChanges ();
			
			this.IsFixedTimeStep = false;
			//graphics.IsFullScreen = true;
			
			//graphics.ApplyChanges ();
			Content.RootDirectory = assetsDirectory;
			Engine.currentEngine = this;
			
		}
		
	
		
		public static Texture2D EmbeddFile(String filename)
		{
			return currentEngine.Content.Load<Texture2D>(filename);
		}
		
		protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures
            MP.Buffer = new SpriteBatch(GraphicsDevice);
            // TODO: use this.Content to load your game content here
            //eater.LoadGraphic(Content,"smiley.png");
			//MP.CurrentWorld.Add (eater);
			//myWorld.addRender(eater);
			//myWorld.addUpdate(eater);
        }
		
		
		
		protected override void Draw(GameTime gameTime)
        {
            graphics.GraphicsDevice.Clear(Color.CornflowerBlue);

            MP.Buffer.Begin();
            Render();
            MP.Buffer.End();

            base.Draw(gameTime);
        }
	
		protected override void Update (GameTime gameTime)
		{
			Input.UpdateKeyboardInput ();
			MP.Elapsed = gameTime.ElapsedGameTime.TotalMilliseconds / 1000;
			
			// Write frame rate to console
			frameRateSum += 1 / MP.Elapsed;
			frameRateCount += 1;
			if (frameRateCount == 100) {
				Console.WriteLine (frameRateSum / frameRateCount);
				frameRateSum = 0;
				frameRateCount = 0;
			}
			
			//if (FP.tweener.active && FP.tweener._tween) FP.tweener.updateTweens();
			if (MP.CurrentWorld.Active)
			{
				//if (FP._world._tween) FP._world.updateTweens();
				MP.CurrentWorld.Update();
			}
			MP.CurrentWorld.UpdateLists();
			if (MP.nextWorld != null) checkWorld();
			Input.SaveOldKeyboardInput();
			base.Update (gameTime);
		}
		
		/**
		 * Renders the game, rendering the World and Entities.
		 */
		public void Render()
		{
			MP.CurrentWorld.Render();
		}
		
		
		/** @private Switch Worlds if they've changed. */
		private void checkWorld()
		{
			if (MP.nextWorld == null) return;
			MP.currentWorld.End();
			MP.currentWorld.UpdateLists();
			//if (FP._world && FP._world.autoClear && FP._world._tween) FP._world.clearTweens();
			MP.currentWorld = MP.nextWorld;
			MP.nextWorld = null;
			MP.Camera = MP.CurrentWorld.Camera;
			MP.currentWorld.UpdateLists();
			MP.currentWorld.Begin();
			MP.currentWorld.UpdateLists();
		}
		
		private double frameRateSum = 0;
		private int frameRateCount = 0;
	}
}

