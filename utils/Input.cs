using System;
using Microsoft.Xna.Framework.Input;

namespace Dabrorius.MonoPunk
{
	public static class Input
	{
		
		public static bool Check(Keys key)
		{
			return keyboardState.IsKeyDown(key);
		}
		
		public static bool Pressed(Keys key)
		{
			bool isPressedNow = keyboardState.IsKeyDown(key);
			bool wasPressedLastFrame = oldKeyboardState.IsKeyDown(key);
			return ( isPressedNow && (! wasPressedLastFrame) );
		}
		
		public static bool Released(Keys key)
		{
			bool isPressedNow = keyboardState.IsKeyDown(key);
			bool wasPressedLastFrame = oldKeyboardState.IsKeyDown(key);
			return ( (! isPressedNow) && wasPressedLastFrame);
		}
		
		internal static void UpdateKeyboardInput()
		{
			keyboardState = Keyboard.GetState();
		}
		
		internal static void SaveOldKeyboardInput()
		{
			oldKeyboardState = Keyboard.GetState();
		}
		
		private static KeyboardState keyboardState = Keyboard.GetState();
		private static KeyboardState oldKeyboardState = Keyboard.GetState();

	}
}

