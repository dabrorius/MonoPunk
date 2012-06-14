namespace Dabrorius.MonoPunk
{
    /**
     * Updateable Tween container.
     */
    class Tweener
    {
        /**
         * Persistent Tween type, will stop when it finishes.
         */
        public const uint PERSIST = 0;
        /**
         * Looping Tween type, will restart immediately when it finishes.
         */
        public const uint LOPPING = 1;
        /**
         * Oneshot Tween type, will stop and remove itself from its core container when it finishes.
         */
        public const uint ONESHOT = 2;

        /**
         * If the Tweener should update.
         */
        public bool active = true;

        /**
         * If the Tweener should clear on removal. For Entities, this is when they are
         * removed from a World, and for World this is when the active World is switched.
         */
        public bool autoClear = false;

        /**
         * Constructor.
         */
        public  Tweener()
        {
     
        }

        /**
         * Updates the Tween container.
         */
        public void update()
        { 
        
        }

        /**
         * Adds a new Tween.
         * @param	t			The Tween to add.
         * @param	start		If the Tween should call start() immediately.
         * @return	The added Tween.
         */
        public Tween addTween(Tween t, bool start = false)
        {
            if (t._parent != null) throw new System.ArgumentException("Cannot add a Tween object more than once.");
            t._parent = this;
            t._next = _tween;
            if (_tween != null) _tween._prev = t;
            _tween = t;
            if (start) _tween.start();
            return t;
        }
        
        /**
         * Removes a Tween.
         * @param	t		The Tween to remove.
         * @return	The removed Tween.
         */
        public Tween removeTween(Tween t)
        {
            if (t._parent != this) throw new System.ArgumentException("Core object does not contain Tween.");
            if (t._next != null) t._next._prev = t._prev;
            if (t._prev != null) t._prev._next = t._next;
            else _tween = t._next;
            t._next = t._prev = null;
            t._parent = null;
            t.active = false;
            return t;        
        }

        /**
         * Removes all Tweens.
         */
        public void clearTweens()
        {
            Tween t = _tween;
            Tween n;
            while (t != null)
            {
                n = t._next;
                removeTween(t);
                t = n;
            }
        }

        /** 
		 * Updates all contained tweens.
		 */
        public void updateTweens()
        {
            Tween t = _tween;
            Tween n;
            while (t != null)
            {
                n = t._next;
                if (t.active)
                {
                    t.update();
                    if (t._finish) t.finish();
                }
                t = n;
            }
        }

        // List information.
        /** @private */ internal Tween _tween;
    }
}
