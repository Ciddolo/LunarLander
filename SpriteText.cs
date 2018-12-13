using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Aiv.Draw;

namespace LunarLander
{
    class SpriteText
    {
        private string text;
        private SpriteObj[] sprites;

        public string Text
        {
            get { return text; }
            set { SetText(value); }
        }

        public Vector2 Position { get; set; }

        public SpriteText(Vector2 position, string text)
        {
            Position = position;
            sprites = new SpriteObj[32];
            Text = text;
        }

        private void SetText(string text)
        {
            this.text = text;
            int nextX = (int)Position.X;
            int nextY = (int)Position.Y;

            for (int i = 0; i < text.Length; i++)
            {
                char currentletter = text[i];
                if (currentletter == ' ')
                    sprites[i] = new SpriteObj("Assets/Font/s..png", nextX, nextY);
                else if (currentletter == ':')
                    sprites[i] = new SpriteObj("Assets/Font/2..png", nextX, nextY);
                else
                    sprites[i] = new SpriteObj("Assets/Font/" + currentletter + ".png", nextX, nextY);
                nextX += sprites[i].Width + 4;
            }
        }

        public void Draw()
        {
            for (int i = 0; i < sprites.Length; i++)
            {
                if (sprites[i] == null)
                {
                    return;
                }
                else
                {
                    sprites[i].Draw();
                }
            }
        }
    }
}