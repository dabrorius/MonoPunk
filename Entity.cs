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
		
		public Entity (float x = 0, float y = 0)
		{
			Position = new Vector2(x,y);
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
		
		
		/**
		 * The rendering layer of this Entity. Higher layers are rendered first.
		 */
		public int Layer
		{
			get { return layer;}
			set 
			{
				if( layer == value) return;
				if( world == null )
				{
					layer = value;
					return;
				}
				world.removeRender(this);  
				layer = value;
				world.addRender(this);
			}
			
		}

		
		/**
		 * The collision type, used for collision checking.
		 */
		public String Type 
		{
			get { return type; }
			set
			{
				if( type == value ) return;
				if( world == null )
				{
					type = value;
					return;
				}
				if( type != null ) world.removeType(this);
				type = value;
				if( value != null ) world.addType(this);
			}
		}

		
		/**
		 * Graphical component to render to the screen.
		 */
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
		internal String type;
		internal Entity typePrev;
		internal Entity typeNext;
		
		internal Entity renderPrev;
		internal Entity renderNext;
		internal Entity updatePrev;
		internal Entity updateNext;
		internal int layer;
		
		internal Graphic graphic;
		
		//private ContentManager contentManager;

	}
}

