using System;

namespace Dabrorius.MonoPunk
{
	public class Mask
	{
		public Mask ()
		{
		}
		
		/**
		 * Checks for collision with another Mask.
		 * @param	mask	The other Mask to check against.
		 * @return	If the Masks overlap.
		 */
		public Boolean Collide(Mask mask)
		{
			/*
			if (_check[mask._class] != null) return _check[mask._class](mask);
			if (mask._check[_class] != null) return mask._check[_class](this);
			*/
			return false;
		}
		
		/** @private Collide against an Entity. */
		/*
		private function collideMask(other:Mask):Boolean
		{
			return parent.x - parent.originX + parent.width > other.parent.x - other.parent.originX
				&& parent.y - parent.originY + parent.height > other.parent.y - other.parent.originY
				&& parent.x - parent.originX < other.parent.x - other.parent.originX + other.parent.width
				&& parent.y - parent.originY < other.parent.y - other.parent.originY + other.parent.height;
		}
		*/
	}
}

