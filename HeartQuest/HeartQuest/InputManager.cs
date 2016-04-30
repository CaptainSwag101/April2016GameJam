using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;

namespace HeartQuest
{
    static class InputManager
    {
        public static KeyboardState currentState { get; private set; }
        public static KeyboardState lastState { get; private set; }

        public static void Initialize()
        {
            currentState = Keyboard.GetState();
            lastState = Keyboard.GetState();
        }

        public static void Update(GameTime gameTime)
        {
            lastState = currentState;
            currentState = Keyboard.GetState();
        }

        public static bool KeyPressed(Keys k)
        {
            return currentState.IsKeyDown(k) && lastState.IsKeyUp(k);
        }

        public static bool KeyReleased(Keys k)
        {
            return currentState.IsKeyUp(k) && lastState.IsKeyDown(k);
        }
    }
}
