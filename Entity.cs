using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Dabrorius.MonoPunk;

namespace Dabrorius.MonoPunk
{
	public class Entity
	{
		public Boolean Visible;
		public Boolean Active;
		public Boolean Collidable;
		
		public Vector2 Position;
		
		public float X
		{
			get {return Position.X;}
			set {Position.X = value;}
		}
		
		public float Y
		{
			get {return Position.Y;}
			set {Position.Y = value;}			
		}
		
		public Vector2 Size;
		
		public float Width
		{
			get {return Size.X;}
			set {Size.X = value;}
		}
		
		public float Height
		{
			get {return Size.Y;}
			set {Size.Y = value;}
		}	
		
		/* TO-DO Hitbox Origin */
		
		public Entity ()
		{
			Position = new Vector2(0,0);
			Visible = true;
			Active = true;
		}
		
		public SpriteBatch renderTarget;
		
		public void Render()
		{
			if (graphic != null && graphic.Visible)
			{
				graphic.Render(renderTarget != null ? renderTarget : MP.Buffer, Position, MP.Camera);
			}
		}
		
		public void Added()
		{
			// to override
		}
		
		public Graphic Graphic
		{
			get {return graphic;}
			set 
			{
				if( graphic == value) return;
				graphic = value;			
			}
		}
	
		
		virtual public void Update(){}
		
		
		
		internal World world;
		internal Entity renderPrev;
		internal Entity renderNext;
		internal Entity updatePrev;
		internal Entity updateNext;
		internal int layer;
		
		internal Graphic graphic;
		
		//private ContentManager contentManager;

	}
}

