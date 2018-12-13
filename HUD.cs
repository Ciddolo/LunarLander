using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LunarLander
{
    static class HUD
    {
        static Vector2 fuelPosition = new Vector2(GfxTools.Win.width - 150, 10);
        static string fuelString = "fuel: " + ((int)Player.Fuel).ToString("D3");
        static SpriteText fuelSprite = new SpriteText(fuelPosition, fuelString);

        static Vector2 horizontalMovementSpeedPosition = new Vector2(GfxTools.Win.width - 150, 30);
        static int horizontalMovementSpeedValue = (int)Player.MovementSpeed.X;
        static string horizontalMovementSpeedString = "h ms: " + horizontalMovementSpeedValue.ToString("D1");
        static SpriteText horizontalMovementSpeedSprite = new SpriteText(horizontalMovementSpeedPosition, horizontalMovementSpeedString);

        static Vector2 verticalMovementSpeedPosition = new Vector2(GfxTools.Win.width - 150, 50);
        static int verticalMovementSpeedValue = (int)Player.MovementSpeed.Y;
        static string verticalMovementSpeedString = "v ms: " + verticalMovementSpeedValue.ToString("D1");
        static SpriteText verticalMovementSpeedSprite = new SpriteText(verticalMovementSpeedPosition, verticalMovementSpeedString);

        // UPDATE

        private static void FuelUpdate()
        {
            fuelString = "fuel: " + ((int)Player.Fuel).ToString("D3");
            fuelSprite.Text = fuelString;
        }
        private static void MovementSpeedUpdate()
        {
            horizontalMovementSpeedValue = (int)Player.MovementSpeed.X;
            if (horizontalMovementSpeedValue < 0)
                horizontalMovementSpeedValue *= -1;
            horizontalMovementSpeedString = "h ms: " + horizontalMovementSpeedValue.ToString("D1");
            horizontalMovementSpeedSprite.Text = horizontalMovementSpeedString;

            verticalMovementSpeedValue = (int)Player.MovementSpeed.Y;
            if (verticalMovementSpeedValue < 0)
                verticalMovementSpeedValue *= -1;
            verticalMovementSpeedString = "v ms: " + verticalMovementSpeedValue.ToString("D1");
            verticalMovementSpeedSprite.Text = verticalMovementSpeedString;
        }

        public static void Update()
        {
            FuelUpdate();
            MovementSpeedUpdate();
        }

        // DRAW

        public static void Draw()
        {
            fuelSprite.Draw();
            horizontalMovementSpeedSprite.Draw();
            verticalMovementSpeedSprite.Draw();
        }
    }
}