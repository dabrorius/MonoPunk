using System;
namespace Dabrorius.MonoPunk
{
    /**
     * Base class for all Tween objects, can be added to any Core-extended classes.
     */
    class Tween
    {
        /**
		 * Persistent Tween type, will stop when it finishes.
		 */
        public const uint PERSIST = 0;
        /**
		 * Looping Tween type, will restart immediately when it finishes.
		 */
        public const uint LOOPING = 1;
        /**
		 * Oneshot Tween type, will stop and remove itself from its core container when it finishes.
		 */
        public const uint ONESHOT = 2;
        /**
		 * If the tween should update.
		 */
        public bool active;
        /**
		 * Tween completion callback.
		 */
        public Action complete;

        /**
	   	 * Constructor. Specify basic information about the Tween.
		 * @param	duration		Duration of the tween (in seconds or frames).
		 * @param	type			Tween type, one of Tween.PERSIST (default), Tween.LOOPING, or Tween.ONESHOT.
		 * @param	complete		Optional callback for when the Tween completes.
		 * @param	ease			Optional easer function to apply to the Tweened value.
		 */
        public Tween(double duration, uint type = 0, Action complete = null, Func<double, double> ease = null)
        {
            _target = duration;
            _type = type;
            this.complete = complete;
            _ease = ease;

        }

        /**
	 	 * Updates the Tween, called by World.
		 */
        public virtual void update()
        {
            _time += MP.timeInFrames ? 1 : MP.elapsed;
            _t = _time / _target;
            if (_time >= _target)
            {
                _t = 1;
                _finish = true;
            }
            if (_ease != null) _t = _ease(_t);
        }

        /**
	 	 * Starts the Tween, or restarts it if it's currently running.
		 */
        public virtual void start()
        {
            _time = 0;
            if (_target == 0)
            {
                active = false;
                return;
            }
            active = true;
        }

        /**
         * Immediately stops the Tween and removes it from its Tweener without calling the complete callback.
         */
        public void cancel()
        {
            active = false;
            if (_parent != null) _parent.removeTween(this);
        }

        /** @private Called when the Tween completes. */
        internal void finish()
        {
            switch (_type)
            {
                case PERSIST:
                    _time = _target;
                    active = false;
                    break;
                case LOOPING:
                    _time %= _target;
                    _t = _time / _target;
                    if (_ease != null) _t = _ease(_t);
                    start();
                    break;
                case ONESHOT:
                    _time = _target;
                    active = false;
                    _parent.removeTween(this);
                    break;
            }
            _finish = false;
            if (complete != null) complete();
        }

        /**
		 * The completion percentage of the Tween.
		 */
        public double percent
        {
            get { return _time / _target; }
            set { _time = _target * value; }
        }

        /**
		 * The current time scale of the Tween (after easer has been applied).
		 */
        public double scale { get { return _t; } }

        // Tween information.
        /** @private */ private uint _type;
        /** @private */ protected Func<double, double> _ease;
        /** @private */ protected double _t = 0;

        // Timing information.
        /** @private */ protected double _time;
        /** @private */ protected double _target;

        // List information.
        /** @private */ internal bool _finish;
        /** @private */ internal Tweener _parent;
        /** @private */ internal Tween _prev;
        /** @private */ internal Tween _next;
    }
}