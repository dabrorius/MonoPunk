using System;
namespace Dabrorius.MonoPunk
{
	public class Anim
	{
		public Anim(string name, int[] frames, double frameRate = 0, bool loop = true)
		{
			this.name = name;
			this.frames = frames;
			this.frameRate = frameRate;
			this.loop = loop;
			this.frameCount = (uint) frames.Length;
		}
		
		
		/**
		 * Plays the animation.
		 * @param	reset		If the animation should force-restart if it is already playing.
		 */
		public void Play(bool reset = false)
		{
			parent.Play(name, reset);
		}
		
		/**
		 * Name of the animation.
		 */
		public string Name { get {return name;} }
		
		/**
		 * Array of frame indices to animate.
		 */
		public int[] Frames { get {return frames;} }
		
		/**
		 * Animation speed.
		 */
		public double FrameRate { get {return frameRate;} }
		
		/**
		 * Amount of frames in the animation.
		 */
		public uint FrameCount { get {return frameCount;} }
		
		/**
		 * If the animation loops.
		 */
		public bool Loop { get {return loop;} }
		
		internal Spritemap parent;
		internal string name;
		internal int[] frames;
		internal double frameRate;
		internal uint frameCount;
		internal bool loop;
	}
}

