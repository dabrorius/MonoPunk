using System;
using Microsoft.Xna.Framework;
using System.Collections;
using System.Collections.Generic;

namespace Dabrorius.MonoPunk
{
	public class World
	{
		public Vector2 Camera = new Vector2(0,0);
		public Boolean Active;
		public World ()
		{
			Active = true;
		}
		
		
		public virtual void Begin()
		{
			// to override
		}
		
		public virtual void End()
		{
			// to override
		}
		
		/**
		 * Performed by the game loop, renders all contained Entities.
		 * If you override this to give your World render code, remember
		 * to call super.render() or your Entities will not be rendered.
		 */
		public void Render() 
		{
			// render the entities in order of depth
			Entity e;
			int i = layerList.Count;
			while (i > 0)
			{
				i--;
				e = renderLast[layerList[i]] as Entity;
				while (e != null)
				{
					if (e.Visible) e.Render();
					e = e.renderPrev;
				}
			}
		}
		
		/**
		 * Performed by the game loop, updates all contained Entities.
		 * If you override this to give your World update code, remember
		 * to call super.update() or your Entities will not be updated.
		 */
		public void Update() 
		{
			// update the entities
			Entity e = updateFirst;
			while (e != null)
			{
				if (e.Active)
				{
					//if (e._tween) e.updateTweens();
					e.Update();
				}
				//if (e._graphic && e._graphic.active) e._graphic.update();
				e = e.updateNext;
			}
		}
		
		/**
		 * Adds the Entity to the World at the end of the frame.
		 * @param	e		Entity object you want to add.
		 * @return	The added Entity object.
		 */
		public Entity Add(Entity e)
		{
			toAdd.Add(e);
			return e;
		}
		
		
		/**
		 * Updates the add/remove lists at the end of the frame.
		 */
		public void UpdateLists()
		{			
			// remove entities
			/*
			if (_remove.length)
			{
				for each (e in _remove)
				{
					if (!e._world)
					{
						if(_add.indexOf(e) >= 0)
							_add.splice(_add.indexOf(e), 1);
						
						continue;
					}
					if (e._world !== this)
						continue;
					
					e.removed();
					e._world = null;
					
					removeUpdate(e);
					removeRender(e);
					if (e._type) removeType(e);
					if (e._name) unregisterName(e);
					if (e.autoClear && e._tween) e.clearTweens();
				}
				_remove.length = 0;
			}
			*/
			// add entities
			if (toAdd.Count > 0)
			{
				foreach (Entity e in toAdd)
				{
					if (e.world != null) continue;
					
					addUpdate(e);
					addRender(e);
					
					if (e.type != null) addType(e);
					//if (e._name) registerName(e);
					
					e.world = this;
					e.Added();
				}
				toAdd.Clear();
			}
			
			// recycle entities
			/*
			if (_recycle.length)
			{
				for each (e in _recycle)
				{
					if (e._world || e._recycleNext)
						continue;
					
					e._recycleNext = _recycled[e._class];
					_recycled[e._class] = e;
				}
				_recycle.length = 0;
			}
			*/
			
			// sort the depth list
			if (layerSort)
			{
				if (layerList.Count > 1) layerList.Sort();
				layerSort = false;
			}
		}
		
		/** @private Adds Entity to the render list. */
		internal void addRender(Entity e)
		{
			if (renderFirst.ContainsKey(e.layer))
			{
				Entity f = renderFirst[e.layer] as Entity;

				// Append entity to existing layer.
				e.renderNext = f;
				f.renderPrev = e;
				layerCount[e.layer]++;
			}
			else
			{
				// Create new layer with entity.
				renderLast[e.layer] = e;
				layerList.Add(e.layer);
				layerSort = true;
				e.renderNext = null;
				layerCount[e.layer] = 1;
			}
			renderFirst[e.layer] = e;
			e.renderPrev = null;
		}
		
		/** @private Removes Entity from the render list. */
		internal void removeRender(Entity e)
		{
			if (e.renderNext != null) e.renderNext.renderPrev = e.renderPrev;
			else renderLast[e.layer] = e.renderPrev;
			if (e.renderPrev != null) e.renderPrev.renderNext = e.renderNext;
			else
			{
				// Remove this entity from the layer.
				renderFirst[e.layer] = e.renderNext;
				if (e.renderNext == null)
				{
					// Remove the layer from the layer list if this was the last entity.
					if (layerList.Count > 1)
					{
						layerList.Remove(e.Layer);
						layerSort = true;
					}
				}
			}
			layerCount[e.layer] --;
			e.renderNext = e.renderPrev = null;
		}
		
		
		/** @private Adds Entity to the update list. */
		private void addUpdate(Entity e)
		{
			// add to update list
			if (updateFirst != null)
			{
				updateFirst.updatePrev = e;
				e.updateNext = updateFirst;
			}
			else e.updateNext = null;
			e.updatePrev = null;
			updateFirst = e;
			count ++;
			/* TO-DO class count*/
		}
		
		/** @private Removes Entity from the update list. */
		private void removeUpdate(Entity e)
		{
			// remove from the update list
			if (updateFirst == e) updateFirst = e.updateNext;
			if (e.updateNext != null) e.updateNext.updatePrev = e.updatePrev;
			if (e.updatePrev != null) e.updatePrev.updateNext = e.updateNext;
			e.updateNext = e.updatePrev = null;
			
			count --;
			//classCount[e.class] --;
		}
		
		/** @private Adds Entity to the type list. */
		internal void addType(Entity e)
		{
			// add to type list
			if( typeFirst.ContainsKey(e.type) )
			{
				typeFirst[e.type].typePrev = e;
				e.typeNext = typeFirst[e.type];
				typeCount[e.type]++;
			}
			else
			{
				e.typeNext = null;
				typeCount[e.type] = 1;
			}
			e.typePrev = null;
			typeFirst[e.type] = e;
		}
		
		/** @private Removes Entity from the type list. */
		internal void removeType(Entity e)
		{
			// remove from the type list
			if (typeFirst[e.type] == e) typeFirst[e.type] = e.typeNext;
			if (e.typeNext != null) e.typeNext.typePrev = e.typePrev;
			if (e.typePrev != null) e.typePrev.typeNext = e.typeNext;
			e.typeNext = e.typePrev = null;
			typeCount[e.type] --;
		}
		
		
		// Adding and removal.
		private List<Entity> toAdd = new List<Entity>();
		private List<Entity> toRemove = new List<Entity>();
		private List<Entity> toRecycle = new List<Entity>();
		
		// Update information.
		private Entity updateFirst;
		private int count = 0;	
		
		// Render information
		private Dictionary<int,Entity> renderFirst = new Dictionary<int,Entity>();
		private Dictionary<int,Entity> renderLast = new Dictionary<int,Entity>();
		private Dictionary<int,int> layerCount = new Dictionary<int,int>();
		private List<int> layerList = new List<int>();
		private Dictionary<String,Entity> typeFirst = new Dictionary<String, Entity>();
		private Dictionary<String,int> typeCount = new Dictionary<String, int>();

		private Boolean layerSort;
	}
}

