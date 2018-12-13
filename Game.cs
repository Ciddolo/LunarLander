using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Aiv.Draw;

namespace LunarLander
{
    static class Game
    {
        static Window window;
        //static SpriteObj background = new SpriteObj("Assets/background.png");
        static bool isPause = false;
        static float pauseCooldown = 0;

        const float pauseTempo = 0.25f;

        public static float DeltaTime { get { return window.deltaTime; } }

        static Game()
        {
            window = new Window(1200, 700, "Lunar Lander", PixelFormat.RGB);
            //background.Position = new Vector2(-1, window.height - background.Height);
            GfxTools.Init(window);
        }

        public static void Play()
        {
            while (window.opened)
            {
                GfxTools.Clean();

                // INPUT

                if (window.GetKey(KeyCode.Esc) || window.GetKey(KeyCode.Return))
                    return;
                if (window.GetKey(KeyCode.P) && pauseCooldown <= 0)
                {
                    isPause = !isPause;
                    pauseCooldown = pauseTempo;
                }

                if (!isPause)
                {
                    Player.Input();
                }

                // UPDATE

                if (!isPause)
                {
                    Player.Update();
                    HUD.Update();
                }
                pauseCooldown -= DeltaTime;

                // DRAW

                //background.Draw();
                GfxTools.DrawHorizontalLine(0, window.height - 75, window.width, 255, 255, 255);
                Player.Draw();
                HUD.Draw();

                if (isPause)
                {
                    GfxTools.DrawRectangle(window.width / 2 - 25, window.height / 2 - 40, 20, 80, 255, 255, 255);
                    GfxTools.DrawRectangle(window.width / 2 + 5, window.height / 2 - 40, 20, 80, 255, 255, 255);
                }

                window.Blit();
            }
        }
    }
}