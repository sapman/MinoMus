//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using Microsoft.Xna.Framework.Input;

//namespace MinoMus.KeyboardControlledMenu
//{
//    public class KeyboardControlledButtonCollection : MinoMus.ButtonCollection
//    {

//        /// <summary>
//        /// Represents the index of the focused button
//        /// </summary>
//        int Index;

//        public bool IsLooping { get; set; }

//        public KeyboardControlledButtonCollection()
//        {
//            Data = new List<Button>();
//            Index = 0;
//        }

//        KeyboardState lastState;
//        public override int Update()
//        {
//            KeyboardState keyboardState = Keyboard.GetState();

//            if (keyboardState.IsKeyDown(Keys.Up) && lastState.IsKeyUp(Keys.Up))
//            {
//                Index++;
//                if (Index >= Data.Count)
//                {
//                    if (IsLooping)
//                        Index = 0;
//                    else
//                        Index = Data.Count - 1;
//                }
//            }
//            if (keyboardState.IsKeyDown(Keys.Down) && lastState.IsKeyUp(Keys.Down))
//            {
//                Index--;
//                if (Index < 0)
//                {
//                    if (IsLooping)
//                        Index = Data.Count - 1;
//                    else
//                        Index = 0;
//                }
//            }

//            for (int i = 0; i < Data.Count; i++)
//            {
//                if (i == Index) continue;

//                Data[i].ReturnToBasicState();
//            }

//            Data[Index].Update();

//            if (keyboardState.IsKeyDown(Keys.Enter) && lastState.IsKeyUp(Keys.Enter))
//                return Index;

//            return -1;
//        }

//    }
//}
