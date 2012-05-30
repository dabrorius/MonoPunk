using System;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace Dabrorius.MonoPunk
{
	public class Spritemap : Image
	{
		
		public Spritemap (Texture2D source, int frameWidth = 0, int frameHeight = 0) : base(source)
		{
			rect = new Rectangle(0,0,frameWidth, frameHeight);
			texture = source;
			
			if (frameWidth == 0) rect.Width = source.Width;
			if (frameHeight == 0) rect.Height = source.Height;
			
			clipRect.Width = rect.Width;
			clipRect.Height = rect.Height;
			
			width = source.Width;
			height = source.Height;
			columns = width / rect.Width;
			rows = height / rect.Height;
			frameCount = columns * rows;
			
			anims = new Dictionary<string, Anim>();
		}
		
		public void updateClipRect()
		{
			clipRect.X = (int) (rect.Width * (frame % columns));
			clipRect.Y = (int) (rect.Height * ((uint) (frame / columns)));
			//if (flipped) rect.X = (width - rect.Width) - rect.X;
		}
		
		public Anim Add(string name, int[] frames, double frameRate = 0, bool loop = true)
		{
			if (anims.ContainsKey(name) ) throw new Exception("Cannot have multiple animations with the same name");
			(anims[name] = new Anim(name, frames, frameRate, loop)).parent = this;
			return anims[name];
		}
		
		public Anim Play(string name = "", bool reset = false, int frame = 0)
		{
			if (!reset && anim != null && anim.Name == name) return anim;
			anim = anims[name];
			if (anim == null)
			{
				frame = 0; //(uint) index; // = (uint) 0;
				index = 0;
				Complete = true;
				updateClipRect();
				return null;
			}
			index = 0;
			timer = 0;
			int setFrame =  frame % (int) anim.frameCount;
			this.frame = (uint) anim.frames[setFrame];
			Complete = false;
			updateClipRect();
			return anim;
		}
		
		public bool Complete;
		
		private Texture2D texture;
		
		private Rectangle rect;
		private int width;
		private int height;
		private int columns;
		private int rows;
		private int frameCount;
		
		private uint index;
		private uint frame;
		private Anim anim;
		private Dictionary<string,Anim> anims;
		private double timer = 0;
	}
}

