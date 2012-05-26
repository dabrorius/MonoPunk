using System;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Dabrorius.MonoPunk
{
	public class Spritemap : Graphic
	{
		
		public Spritemap (Texture2D source, int frameWidth = 0, int frameHeight = 0)
		{
			rect = new Rectangle(0,0,frameWidth, frameHeight);
			texture = source;
			
			if (frameWidth == 0) rect.Width = source.Width;
			if (frameHeight == 0) rect.Height = source.Height;
			
			width = source.Width;
			height = source.Height;
			columns = width / rect.Width;
			rows = height / rect.Height;
			frameCount = columns * rows;
		}
		
		private Texture2D texture;
		
		private Rectangle rect;
		private int width;
		private int height;
		private int columns;
		private int rows;
		private int frameCount;
	}
}

