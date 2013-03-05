using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Dabrorius.MonoPunk;

namespace Dabrorius.MonoPunk
{
	public class Tilemap : Graphic
	{
		/**
		 * If x/y positions should be used instead of columns/rows.
		 */
		public Boolean usePositions = false;
		
		/**
		 * Constructor.
		 * @param	tileset			The source tileset image.
		 * @param	width			Width of the tilemap, in pixels.
		 * @param	height			Height of the tilemap, in pixels.
		 * @param	tileWidth		Tile width.
		 * @param	tileHeight		Tile height.
		 */
		public Tilemap (Texture2D tileset, uint width, uint height, int tileWidth, int tileHeight)
		{
			// set some tilemap information
			this.width = width;
			this.height = height;
			this.columns = (uint) Math.Ceiling( (Double) width / tileWidth);
			this.rows = (uint) Math.Ceiling ( (Double) height / tileHeight);
			
			tile = new Rectangle (0, 0, tileWidth, tileHeight);
			
			// create the canvas
			// note: not using canvas
			
			// load the tileset graphic
			this.tileset = tileset;
		
			setColumns = (uint) Math.Ceiling ( (Double) tileset.Width / tileWidth);
			setRows = (uint) Math.Ceiling ( (Double) tileset.Height / tileHeight);
			setCount = setColumns * setRows;
			
			this.map = new uint[columns,rows];
		}
		
		/**
		 * Sets the index of the tile at the position.
		 * @param	column		Tile column.
		 * @param	row			Tile row.
		 * @param	index		Tile index.
		 */
		public void SetTile(uint column,uint row,uint index = 0)
		{
			if (usePositions)
			{
				column /= (uint) tile.Width;
				row /= (uint) tile.Height;
			}
			
			this.map[column,row] = index;
		}
		
		/**
		 * Clears the tile at the position.
		 * @param	column		Tile column.
		 * @param	row			Tile row.
		 */
		public void ClearTile(uint column,uint row)
		{
			if (usePositions)
			{
				column /= (uint) tile.Width;
				row /= (uint) tile.Height;
			}
			
			this.map[column,row] = 0;
		}
		
		/** @public Renders the Tilemap. */
		override public void Render (SpriteBatch target, Vector2 point, Vector2 camera)
		{			
			Vector2 origin = new Vector2 ();
			
			for (int column = 0; column < columns; column++) {
				for (int row = 0; row < rows; row++) {
					uint tileIndex = map [column, row];
					Console.WriteLine (column + " " + row + "=>" + tileIndex);
					if (tileIndex > 0) {
						tileIndex -= 1;
						uint setRow = tileIndex / setColumns;
						uint setColumn = tileIndex % setColumns;
						int setX = (int)setRow * tile.Width;
						int setY = (int)setColumn * tile.Height;
						
						Vector2 tilePoint = point + new Vector2 (column * tile.Width, + row * tile.Height);
						Rectangle setClipRect = new Rectangle (setX, setY, tile.Width, tile.Height); 
						target.Draw (tileset, tilePoint, setClipRect, Color.White, 0,
	        				origin, 1.0f, SpriteEffects.None, 0f);
					}
				}
			}
			
			
			
		}
		
		private uint width;
		private uint height;
		private uint columns;
		private uint rows;
		private uint[,] map;
		private Texture2D tileset;
		private uint setColumns;
		private uint setRows;
		private uint setCount;
		private Rectangle tile;
			
	}
}

