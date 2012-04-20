using System;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Dabrorius.MonoPunk
{
	public class Image : Graphic
	{
		private Texture2D texture;
		
		public Image (Texture2D source)
		{
			texture = source;
		}
		
		override public void Render(SpriteBatch target, Vector2 point, Vector2 camera)
		{
			if( texture == null) return;
			target.Draw(texture, point, Color.White);
		}

	}
}

