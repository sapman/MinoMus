using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MinoMus.Menues
{
    public enum OrganizOrder
    {
        Ltr, 
        Rtl,
        Utd,
        Dtu,
        Dig,
    }
    public class ButtonCollection
    {
        public List<Button> Data { get; protected set; }

        public ButtonCollection()
        {
            Data = new List<Button>();
        }

        public void AddButton(Button btn)
        {
            Data.Add(btn);
        }

        public Button this[int index]
        {
            get { return Data[index]; }
        }

        public ButtonType Type {
            get
            {
                if (this[0] != null)
                    return this[0].Type;
                else
                    throw new IndexOutOfRangeException();
            }
            set
            {
                foreach (var Btn in Data)
                {
                    Btn.Type = value;
                }
            }
        }
        public ColorChangeAxis Axis
        {
            get
            {
                if (this[0] != null)
                    return this[0].Axis;
                else
                    throw new IndexOutOfRangeException();
            }
            set
            {
                foreach (var Btn in Data)
                {
                    Btn.Axis = value;
                }
            }
        }

        public virtual void Organize(OrganizOrder OrO, Vector2 FirstPos)
        {
            if (Data.Count == 0)
            {
                throw new EmptyCollectionException();
            }
            Data[0].SetPosition(FirstPos);
            int Interval = 0;
            foreach (var item in Data)
            {
                switch (OrO)
                {
                    case OrganizOrder.Ltr:
                    case OrganizOrder.Rtl:
                        Interval = Math.Max(Interval, item.rectangele.Width);
                        break;
                    case OrganizOrder.Utd:
                    case OrganizOrder.Dtu:
                        Interval = Math.Max(Interval, item.rectangele.Height);
                        break;
                    case OrganizOrder.Dig :
                        Interval = Math.Max(Interval, item.rectangele.Height);
                        break;
                }   
            }
            for (int i = 0; i < Data.Count; i++)
            {
                switch (OrO)
                {
                    case OrganizOrder.Ltr:
                        Data[i].SetPosition(new Vector2(FirstPos.X + (i * (Interval + 10)), Data[0].Position.Y));
                        break;
                    case OrganizOrder.Rtl:
                        Data[i].SetPosition(new Vector2(FirstPos.X - i * (Interval + 10), Data[0].Position.Y));
                        break;
                    case OrganizOrder.Utd:
                        Data[i].SetPosition(new Vector2(Data[0].Position.X, FirstPos.Y + i * (Interval + 10)));
                        break;
                    case OrganizOrder.Dtu:
                        Data[i].SetPosition(new Vector2(Data[0].Position.X, FirstPos.Y - i * (Interval + 10)));
                        break;
                    case OrganizOrder.Dig:
                        Data[i].SetPosition(new Vector2(FirstPos.X + i * (Interval + 10), FirstPos.Y + i * (Interval + 10 )));
                        break;
                }
            }
        }

        public virtual void Organize(OrganizOrder OrO, Vector2 FirstPos, int Width ,int Height)
        {
            if (Data.Count == 0)
            {
                throw new EmptyCollectionException();
            }
            
            Data[0].SetPosition(FirstPos);
            for (int i = 0; i < Data.Count; i++)
            {
                switch (OrO)
                {
                    case OrganizOrder.Ltr:
                        Data[i].SetPosition(new Vector2(FirstPos.X + i * (Width + 10), Data[0].Position.Y));
                        break;
                    case OrganizOrder.Rtl:
                        Data[i].SetPosition(new Vector2(FirstPos.X - i * (Width + 10), Data[0].Position.Y));
                        break;
                    case OrganizOrder.Utd:
                        Data[i].SetPosition(new Vector2(Data[0].Position.X, FirstPos.Y + i * (Height + 10)));
                        break;
                    case OrganizOrder.Dtu:
                        Data[i].SetPosition(new Vector2(Data[0].Position.X, FirstPos.Y - i * (Height + 10)));
                        break;
                }
            }
        }

        public virtual int Update()
        {
            for (int i = 0; i < Data.Count; i++)
                if (Data[i].Update())
                    return i;
            return -1;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (var item in Data)
            {
                item.Draw(spriteBatch);
            }
        }



    }

    [Serializable]
    public class EmptyCollectionException : Exception
    {
        public EmptyCollectionException() { }
        public EmptyCollectionException(string message) : base(message) { }
        public EmptyCollectionException(string message, Exception inner) : base(message, inner) { }
        protected EmptyCollectionException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context)
            : base(info, context) { }
    }
}
