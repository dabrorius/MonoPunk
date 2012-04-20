using System;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Dabrorius.MonoPunk
{
	public class Graphic
	{
		public Boolean Active = true;
		public Boolean Visible = true;
		
		public Graphic ()
		{
		}
		
		virtual public void Render(SpriteBatch target, Vector2 point, Vector2 camera)
		{
			// to override
		}
	}
}

