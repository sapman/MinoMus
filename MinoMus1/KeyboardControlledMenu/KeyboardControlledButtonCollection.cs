using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Input;
using MinoMus.Menues;

namespace MinoMus.KeyboardControlledMenu
{
    public class KeyboardControlledButtonCollection : ButtonCollection
    {

        Keys IndexUp;
        Keys IndexDown;

        /// <summary>
        /// Represents the index of the focused button
        /// </summary>
        public int Index { get; private set; }

        public bool IsLooping { get; set; }

        public KeyboardControlledButtonCollection()
        {
            Data = new List<Button>();
            Index = 0;
        }

        KeyboardState lastState;
        public override int Update()
        {
            KeyboardState keyboardState = Keyboard.GetState();

            if (keyboardState.IsKeyDown(IndexUp) && lastState.IsKeyUp(IndexUp))
            {
                Index++;
                if (Index >= Data.Count)
                {
                    if (IsLooping)
                        Index = 0;
                    else
                        Index = Data.Count - 1;
                }
            }
            if (keyboardState.IsKeyDown(IndexDown) && lastState.IsKeyUp(IndexDown))
            {
                Index--;
                if (Index < 0)
                {
                    if (IsLooping)
                        Index = Data.Count - 1;
                    else
                        Index = 0;
                }
            }

            for (int i = 0; i < Data.Count; i++)
            {
                if (i == Index) continue;

                Data[i].ReturnToBasicState();
            }

            Data[Index].UpdateEffects();

            if (keyboardState.IsKeyDown(Keys.Enter) && lastState.IsKeyUp(Keys.Enter))
                return Index;

            lastState = keyboardState;

            return -1;
        }

        public override void Organize(OrganizOrder OrO, Microsoft.Xna.Framework.Vector2 FirstPos)
        {
            base.Organize(OrO, FirstPos);

            switch (OrO)
            {
                case OrganizOrder.Ltr:
                    IndexUp = Keys.Right;
                    IndexDown = Keys.Left;
                    break;
                case OrganizOrder.Rtl:
                    IndexUp = Keys.Left;
                    IndexDown = Keys.Right;
                    break;
                case OrganizOrder.Utd:
                    IndexUp = Keys.Down;
                    IndexDown = Keys.Up;
                    break;
                case OrganizOrder.Dtu:
                    IndexUp = Keys.Up;
                    IndexDown = Keys.Down;
                    break;
                case OrganizOrder.Dig:
                    IndexUp = Keys.Down;
                    IndexDown = Keys.Up;
                    break;
                default:
                    break;
            }
        }
        public override void Organize(OrganizOrder OrO, Microsoft.Xna.Framework.Vector2 FirstPos, int Width, int Height)
        {
            base.Organize(OrO, FirstPos, Width, Height);

            base.Organize(OrO, FirstPos);

            switch (OrO)
            {
                case OrganizOrder.Ltr:
                    IndexUp = Keys.Right;
                    IndexDown = Keys.Left;
                    break;
                case OrganizOrder.Rtl:
                    IndexUp = Keys.Left;
                    IndexDown = Keys.Right;
                    break;
                case OrganizOrder.Utd:
                    IndexUp = Keys.Down;
                    IndexDown = Keys.Up;
                    break;
                case OrganizOrder.Dtu:
                    IndexUp = Keys.Up;
                    IndexDown = Keys.Down;
                    break;
                case OrganizOrder.Dig:
                    IndexUp = Keys.Down;
                    IndexDown = Keys.Up;
                    break;
                default:
                    break;
            }
        }
    }
}
