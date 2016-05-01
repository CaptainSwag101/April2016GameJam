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
        public static KeyboardState CurrentState { get; private set; }
        public static KeyboardState LastState { get; private set; }

        public static void Initialize()
        {
            CurrentState = Keyboard.GetState();
            LastState = Keyboard.GetState();
        }

        public static void Update(GameTime gameTime)
        {
            LastState = CurrentState;
            CurrentState = Keyboard.GetState();
        }

        public static bool KeyPressed(Keys k)
        {
            return CurrentState.IsKeyDown(k) && LastState.IsKeyUp(k);
        }

        public static bool KeyReleased(Keys k)
        {
            return CurrentState.IsKeyUp(k) && LastState.IsKeyDown(k);
        }
    }
}
