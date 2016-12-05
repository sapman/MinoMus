using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MinoMus.Menues;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using MinoMus.Other;

namespace MinoMus.Menues
{
    public class MenuManager
    {
        MainMenu Main;
        LoadingScreen2 Load;
        SimpleScreen Controls;
        SimpleScreen Credits;
        GameState gameState;

        public MenuManager(Button[] Buttons,Texture2D MainBackGround,Texture2D LoadingBar,Texture2D LoadingBackGround,Texture2D ControlsBackGround,Texture2D CreditsBackGround,GraphicsDevice graphics)
        {
            Main = new MainMenu(Buttons, graphics.Viewport);
            Main.SetBackground(MainBackGround,graphics.Viewport);
            Load = new LoadingScreen2(LoadingBar, LoadingBackGround, graphics.Viewport);
            Controls = new SimpleScreen(ControlsBackGround, Buttons[4], graphics.Viewport);
            Credits = new SimpleScreen(CreditsBackGround, Buttons[4],graphics.Viewport);
            gameState = MinoMusSystem.GameState;
        }

        public bool Update(GameTime gameTime,Game This)// If Need To Quit
        {
            gameState = MinoMusSystem.GameState;
            if (gameState == GameState.Quiting)
                This.Exit();
            else if (gameState == GameState.Loading)
                Load.Update(gameTime);
            else if (gameState == GameState.MainMenu)
                Main.Update(gameTime);
            else if (gameState == GameState.Options)
                Controls.Update();
            else if (gameState == GameState.Credits)
                Credits.Update();
            return false;
        }

        public void SetButtonsType(ButtonType Type)
        {
            Main.SetButtonsType(Type);
            Controls.ButtonType = Type;
            Credits.ButtonType = Type;
        }

        public GameState State
        {
            get { return gameState; }
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            GameState Gamestate = MinoMusSystem.GameState;
            if (Gamestate == GameState.Loading)
                Load.Draw(spriteBatch);
            if (Gamestate == GameState.MainMenu)
                Main.Draw(spriteBatch);
            if (Gamestate == GameState.Options)
                Controls.Draw(spriteBatch);
            if (Gamestate == GameState.Credits)
                Credits.Draw(spriteBatch);
        }
    }
}
