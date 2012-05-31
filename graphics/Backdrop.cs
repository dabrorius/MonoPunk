using System;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Dabrorius.MonoPunk
{
	public class Backdrop : Graphic
	{

		public Backdrop(Texture2D texture, bool repeatX = true, bool repeatY = true) 
		{
			this.repeatX = repeatX;
			this.repeatY = repeatY;
			this.textWidth = (uint) texture.Width;
			this.textHeight = (uint) texture.Height;
			this.texture = texture;
		}
		
		/** @private Renders the Backdrop. */
		override public void Render(SpriteBatch target, Vector2 point, Vector2 camera)
		{
			if( texture == null) return;
			
			Vector2 origin = new Vector2();
			
			point += origin;
			
			target.Draw( texture, point, new Rectangle(0,0,(int)textWidth,(int)textHeight), Color.White, 0,
        				origin, 1.0f, SpriteEffects.None, 0f );
			
		}
		
		
		
		// Backdrop information.
		/** @private */ private Texture2D texture;
		/** @private */ private uint textWidth;
		/** @private */ private uint textHeight;
		/** @private */ private bool repeatX;
		/** @private */ private bool repeatY;
		/** @private */ private float x;
		/** @private */ private float y;
	


	}
}

