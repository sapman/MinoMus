using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MinoMus.Menues
{
    public class TextHandler
    {
        static bool CapsLock;
        private Keys[] lastPressedKeys;
        public string Text { get; set; }

        public SpriteFont Font { get; private set; }
        public Color Color { get; set; }
        public Vector2 Position { get; set; }
        
        public bool SpaceAllowed { get; set; }
        public bool EnterAllowed { get; set; }
        public bool TabAllowed { get; set; }
        public bool CapsLockAllowed { get; set; }

        public TextHandler(SpriteFont Font)
        {
            lastPressedKeys = new Keys[0];
            this.Font = Font;
            Color = Color.White;
            Position = Vector2.Zero;
            Init();
        }

        public TextHandler(SpriteFont Font, Color colour, Vector2 position)
        {
            lastPressedKeys = new Keys[0];
            this.Font = Font;
            Color = colour;
            Position = position;
            Init();
        }

        private void Init()
        {
            Text = "";
            SpaceAllowed = true;
            EnterAllowed = true;
            TabAllowed = true;
            CapsLockAllowed = true;
        }

        public void Update()
        {
            KeyboardState kbState = Keyboard.GetState();
            Keys[] pressedKeys = kbState.GetPressedKeys();

            //check if any of the previous update's keys are no longer pressed
            foreach (Keys key in lastPressedKeys)
            {
                if (!pressedKeys.Contains(key))
                {
                    OnKeyUp(key);
                }
            }

            //check if the currently pressed keys were already pressed
            foreach (Keys key in pressedKeys)
            {
                if (!lastPressedKeys.Contains(key))
                    Text += OnKeyDown(key, (kbState.IsKeyDown(Keys.LeftShift) || kbState.IsKeyDown(Keys.RightShift)) && CapsLockAllowed);
            }
            //save the currently pressed keys so we can compare on the next update
            lastPressedKeys = pressedKeys;
        }

        /// <summary> 
        /// Convert a key to it's respective character or escape sequence. 
        /// </summary> 
        /// <param name="key">The key to convert.</param> 
        /// <param name="shift">Is the shift key pressed or caps lock down.</param> 
        /// <returns>The char for the key that was pressed or string.Empty if it doesn't have a char representation.</returns> 
        public string OnKeyDown(Keys key, bool shift)
        {
            if (CapsLock)
                shift = !shift;
            switch (key)
            {
                case Keys.Space: return SpaceAllowed ? " " : "";

                // Escape Sequences 
                case Keys.Enter: return EnterAllowed ? "\n" : "";                         // Create a new line 
                case Keys.Tab: return (TabAllowed && SpaceAllowed) ? "  " : "";                           // Tab to the right 



                // D-Numerics (strip above the alphabet) 
                case Keys.D0: return shift ? ")" : "0";
                case Keys.D1: return shift ? "!" : "1";
                case Keys.D2: return shift ? "@" : "2";
                case Keys.D3: return shift ? "#" : "3";
                case Keys.D4: return shift ? "$" : "4";
                case Keys.D5: return shift ? "%" : "5";
                case Keys.D6: return shift ? "^" : "6";
                case Keys.D7: return shift ? "&" : "7";
                case Keys.D8: return shift ? "*" : "8";
                case Keys.D9: return shift ? "(" : "9";

                // Numpad 
                case Keys.NumPad0: return "0";
                case Keys.NumPad1: return "1";
                case Keys.NumPad2: return "2";
                case Keys.NumPad3: return "3";
                case Keys.NumPad4: return "4";
                case Keys.NumPad5: return "5";
                case Keys.NumPad6: return "6";
                case Keys.NumPad7: return "7";
                case Keys.NumPad8: return "8";
                case Keys.NumPad9: return "9";
                case Keys.Add: return "+";
                case Keys.Subtract: return "-";
                case Keys.Multiply: return "*";
                case Keys.Divide: return "/";
                case Keys.Decimal: return ".";

                // Alphabet 
                case Keys.A: return shift ? "A" : "a";
                case Keys.B: return shift ? "B" : "b";
                case Keys.C: return shift ? "C" : "c";
                case Keys.D: return shift ? "D" : "d";
                case Keys.E: return shift ? "E" : "e";
                case Keys.F: return shift ? "F" : "f";
                case Keys.G: return shift ? "G" : "g";
                case Keys.H: return shift ? "H" : "h";
                case Keys.I: return shift ? "I" : "i";
                case Keys.J: return shift ? "J" : "j";
                case Keys.K: return shift ? "K" : "k";
                case Keys.L: return shift ? "L" : "l";
                case Keys.M: return shift ? "M" : "m";
                case Keys.N: return shift ? "N" : "n";
                case Keys.O: return shift ? "O" : "o";
                case Keys.P: return shift ? "P" : "p";
                case Keys.Q: return shift ? "Q" : "q";
                case Keys.R: return shift ? "R" : "r";
                case Keys.S: return shift ? "S" : "s";
                case Keys.T: return shift ? "T" : "t";
                case Keys.U: return shift ? "U" : "u";
                case Keys.V: return shift ? "V" : "v";
                case Keys.W: return shift ? "W" : "w";
                case Keys.X: return shift ? "X" : "x";
                case Keys.Y: return shift ? "Y" : "y";
                case Keys.Z: return shift ? "Z" : "z";

                // Oem 
                case Keys.OemOpenBrackets: return shift ? "{" : "[";
                case Keys.OemCloseBrackets: return shift ? "}" : "]";
                case Keys.OemComma: return shift ? "<" : ",";
                case Keys.OemPeriod: return shift ? ">" : ".";
                case Keys.OemMinus: return shift ? "_" : "-";
                case Keys.OemPlus: return shift ? "+" : "=";
                case Keys.OemQuestion: return shift ? "?" : "/";
                case Keys.OemSemicolon: return shift ? ":" : ";";
                case Keys.OemQuotes: return shift ? "\"" : "'";
                case Keys.OemPipe: return shift ? "|" : "\\";
                case Keys.OemTilde: return shift ? "~" : "`";
            }

            return string.Empty;
        }

        private void OnKeyUp(Keys key)
        {
            if (key == Keys.Back && Text.Length > 0)
                Text = Text.Remove(Text.Length - 1);
            if (key == Keys.CapsLock)
                CapsLock = !CapsLock;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
           spriteBatch.DrawString(Font, Text, Position, Color);
        }
    }
}
