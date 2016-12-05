using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MinoMus.Other;

namespace MinoMus.Menues
{
    public enum ActiveButton {None, Playing,Credits,Controls,Quit };
    public class MainMenu
    {
       public Button Play;
       public Button Credits;
       public Button Controls;
       public Button Quit;

        int Position;

        bool Finished;

        const int Speed = 10;
        int Target;
        int Width;
        int Height;

        Viewport viewport;
        Background Background;

        /// <param name="Width,Height">The Width And Height of the buttons. They'r equal between All The Buttons</param>
        /// <param name="Target">The Final X value of the entrance animation. At Start Set To Quit.X</param>
        /// <param name="Speed">The Speed Movment Of The Buttons</param>
        /// <param name="Play">Play Button</param>
        /// <param name="Credits">Credits Button</param>
        /// <param name="Controls">Controls Button</param>
        /// <param name="Quit">Quit Button</param>

        public MainMenu(Button Play, Button Credits, Button Controls, Button Quit,Viewport Viewport)
        {
            this.Play = Play;
            this.Credits = Credits;
            this.Controls = Controls;
            this.Quit = Quit;
            viewport = Viewport;
            Initialize();
        }
        public MainMenu(Button[] Buttons, Viewport Viewport)
        {
            Play = Buttons[0];
            Credits = Buttons[1];
            Controls = Buttons[2];
            Quit = Buttons[3];
            this.viewport = Viewport;
            Initialize();
        }//0-Play,1-Credits,2-Controls,3-Quit

        private void Initialize()
        {
            Width = Play.GetRectangle().Width;
            Height = Play.GetRectangle().Height;
            Finished = false;

            Target = viewport.Width / 2 - Width / 2;
            Position = -Width;
            SetPositons();
        }

        public void SetBackground(Texture2D Background, Viewport view)
        {
            this.Background = new Background(Background, view);
        }
        private void SetPositons()
        {
            int interval = (viewport.Height - (4 * Height)) / 5;
            Play.SetRectangle(new Rectangle(Position * 1, interval + Height, Width, Height));
            Credits.SetRectangle(new Rectangle(Position * 3, Play.rectangele.Y + Height + interval, Width, Height));
            Controls.SetRectangle(new Rectangle(Position * 5, Credits.rectangele.Y + Height + interval, Width, Height));
            Quit.SetRectangle(new Rectangle(Position * 7, Controls.rectangele.Y + Height + interval, Width, Height));
        }

        private void Move()
        {
            if (Play.Position.X < Target) Play.SetRectangle(new Rectangle(Position + Speed, (int)Play.Position.Y,
                   Width, Height));

            if (Credits.Position.X < Target) Credits.SetRectangle(new Rectangle((int)Credits.Position.X + Speed,
                (int)Credits.Position.Y, Width, Height));

            if (Controls.Position.X < Target) Controls.SetRectangle(new Rectangle((int)Controls.Position.X + Speed,
                (int)Controls.Position.Y, Width, Height));

            if (Quit.Position.X < Target) Quit.SetRectangle(new Rectangle((int)Quit.Position.X + Speed,
                (int)Quit.Position.Y, Width, Height));

            Position += Speed;
        }
        public ActiveButton Update()
        {
            if (Quit.Position.X <= Target &&!Finished)
            {
                Move();
                return ActiveButton.None;
            }
            else
            {
                Finished = true;
                if (Play.Update()) return ActiveButton.Playing;
                else if (Credits.Update()) return ActiveButton.Credits;
                else if (Controls.Update()) return ActiveButton.Controls;
                else if (Quit.Update()) return ActiveButton.Quit;
                else return ActiveButton.None;
            }
        }
        public void Update(GameTime gameTime)
        {
            if (Quit.Position.X >= Target)
            {
                Finished = true;
            }
            if (Quit.Position.X <= Target && !Finished)
            {
                Move();
            }
            else
            {
                Finished = true;
                if (Play.Update()) MinoMusSystem.GameState = GameState.Playing;
                else if (Credits.Update()) MinoMusSystem.GameState = GameState.Credits;
                else if (Controls.Update()) MinoMusSystem.GameState = GameState.Options;
                else if (Quit.Update()) MinoMusSystem.GameState = GameState.Quiting;
            }
        }

        public void SetButtonsType(ButtonType Type)
        {
            Play.Type = Type;
            Credits.Type = Type;
            Controls.Type = Type;
            Quit.Type = Type;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (Background != null)
                Background.Draw(spriteBatch);
            Play.Draw(spriteBatch);
            Credits.Draw(spriteBatch);
            Controls.Draw(spriteBatch);
            Quit.Draw(spriteBatch);
        }
    }
}
