using System;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework;
using System.Collections;
using System.Collections.Generic;

namespace Dabrorius.MonoPunk
{
	public static class MP
	{
		/**
	 	* The current screen buffer, drawn to in the render loop.
	 	*/
		public static SpriteBatch Buffer;
		public static double Elapsed;
		
		public static float Width;
		public static float Height;
		
		public static Vector2 Camera;
		
		public static World CurrentWorld
		{
			get {return currentWorld;}
			set 
			{
				if( currentWorld != value ) 
				{
					nextWorld = value;
				}
			}
			
		}
		
		public static float Degs2Rad(float degrees)
		{
			return ( degrees / 180 * ( (float) Math.PI ) );
		}
		
		internal static World currentWorld;
		internal static World nextWorld;
	}

	

}


