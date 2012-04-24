using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Dabrorius.MonoPunk;

namespace Dabrorius.MonoPunk
{
	public class Entity
	{
		public Boolean Visible = true;
		public Boolean Active = true; // this should be movet to tweener
		public Boolean Collidable = true;
		
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
		
		/**
		 * X origin of the Entity's hitbox.
		 */
		public int OriginX;
		
		/**
		 * Y origin of the Entity's hitbox.
		 */
		public int OriginY;
		
		public Entity (float x = 0, float y = 0)
		{
			Position = new Vector2(x,y);

		}
		
		public SpriteBatch renderTarget;
		
		public void Render()
		{
			if (graphic != null && graphic.Visible)
			{
				graphic.Render(renderTarget != null ? renderTarget : MP.Buffer, Position, MP.Camera);
			}
		}
		
		virtual public void Added()
		{
			// to override
		}
		
		virtual public void Removed()
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
		 * Sets the Entity's hitbox properties.
		 * @param	width		Width of the hitbox.
		 * @param	height		Height of the hitbox.
		 * @param	originX		X origin of the hitbox.
		 * @param	originY		Y origin of the hitbox.
		 */
		public void setHitbox(int width = 0, int height = 0, int originX = 0, int originY = 0)
		{
			this.Width = width;
			this.Height = height;
			this.OriginX = originX;
			this.OriginY = originY;
		}
		
			/**
		 * Checks for a collision against an Entity type.
		 * @param	type		The Entity type to check for.
		 * @param	x			Virtual x position to place this Entity.
		 * @param	y			Virtual y position to place this Entity.
		 * @return	The first Entity collided with, or null if none were collided.
		 */
		public Entity Collide(String type, float x, float y)
		{
			
			if (world == null) return null;
			
			Entity e = world.typeFirst[type];
			if (e == null) return null;
			
			_x = this.X; 
			_y = this.Y;
			
			this.X = x; 
			this.Y = y;
			
			if (mask == null)
			{
				while (e != null)
				{
					if (e.Collidable && e != this
					&& X - OriginX + Width > e.X - e.OriginX
					&& Y - OriginY + Height > e.Y - e.OriginY
					&& X - OriginX < e.X - e.OriginX + e.Width
					&& Y - OriginY < e.Y - e.OriginY + e.Height)
					{
						if (e.mask == null || e.mask.Collide(HITBOX))
						{
							this.X = _x; this.Y = _y;
							return e;
						}
					}
					e = e.typeNext;
				}
				this.X = _x; this.Y = _y;
				return null;
			}
			
			while (e != null)
			{
				if (e.Collidable && e != this
				&& X - OriginX + Width > e.X - e.OriginX
				&& Y - OriginY + Height > e.Y - e.OriginY
				&& X - OriginX < e.X - e.OriginX + e.Width
				&& Y - OriginY < e.Y - e.OriginY + e.Height)
				{
					if (mask.Collide(e.mask != null ? e.mask : e.HITBOX))
					{
						this.X = _x; this.Y = _y;
						return e;
					}
				}
				e = e.typeNext;
			}
			this.X = _x; this.Y = _y;
			return null;
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
		
		private Mask HITBOX = new Mask();
		private Mask mask;
		
		private float _x;
		private float _y;
		
		//private ContentManager contentManager;

	}
}

