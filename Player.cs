using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Aiv.Draw;

namespace LunarLander
{
    static class Player
    {
        static SpriteObj ship = new SpriteObj("Assets/Player/player0.png", new Vector2(0, 0));
        static Vector2 centeredPosition = new Vector2(ship.Position.X + ship.Width / 2, ship.Position.Y + ship.Height / 2);
        static Vector2 movementSpeed = new Vector2(10, 0);
        static Vector2 accelleration = new Vector2(10, 0);
        static Vector2 gravity = new Vector2(0, 2);

        static float directionCooldown = 0f;
        static float fuel = 75f;
        static int fuelPerSecond = 1;
        static int positionNumber = 0;
        static int score = 0;
        static bool isAlive = true;
        static bool isLanded = false;

        const float directionTempo = 0f;
        const float standardAccelleration = 5f;
        const float horizontalMovementSpeedMax = 5f;
        const float verticalMovementSpeedMax = 5f;
        const int fuelPerSecondStandard = 1;
        const int fuelPerSecondWithAccelleration = 3;

        public static Vector2 Position { get { return ship.Position; } private set { ship.Position = value; } }
        public static Vector2 MovementSpeed { get { return movementSpeed; } }
        public static float Fuel { get { return fuel; } }
        public static int Width { get { return ship.Width; } }
        public static int Height { get { return ship.Height; } }

        // INPUT

        private static void AccellerationManager()
        {
            if (!isAlive || isLanded)
                return;

            if (fuel > 0 && (GfxTools.Win.GetKey(KeyCode.W) || GfxTools.Win.GetKey(KeyCode.Up) || GfxTools.Win.GetKey(KeyCode.Space)))
            {
                AccellerationModifier();
                movementSpeed.X += accelleration.X * Game.DeltaTime;
                movementSpeed.Y += accelleration.Y * Game.DeltaTime;
                fuelPerSecond = fuelPerSecondWithAccelleration;
            }
            else
            {
                movementSpeed.Y += gravity.Y * Game.DeltaTime;
                fuelPerSecond = fuelPerSecondStandard;
            }
        }
        private static void AccellerationModifier()
        {
            switch (positionNumber)
            {
                case 0:
                    {
                        accelleration.X = standardAccelleration;
                        accelleration.Y = gravity.Y;
                        break;
                    }
                case 1:
                    {
                        accelleration.X = standardAccelleration;
                        accelleration.Y = standardAccelleration / 2 + gravity.Y;
                        break;
                    }
                case 2:
                    {
                        accelleration.X = standardAccelleration;
                        accelleration.Y = standardAccelleration + gravity.Y;
                        break;
                    }
                case 3:
                    {
                        accelleration.X = standardAccelleration / 2;
                        accelleration.Y = standardAccelleration + gravity.Y;
                        break;
                    }
                case 4:
                    {
                        accelleration.X = 0;
                        accelleration.Y = standardAccelleration + gravity.Y;
                        break;
                    }
                case 5:
                    {
                        accelleration.X = -standardAccelleration / 2;
                        accelleration.Y = standardAccelleration + gravity.Y;
                        break;
                    }
                case 6:
                    {
                        accelleration.X = -standardAccelleration;
                        accelleration.Y = standardAccelleration + gravity.Y;
                        break;
                    }
                case 7:
                    {
                        accelleration.X = -standardAccelleration;
                        accelleration.Y = standardAccelleration / 2 + gravity.Y;
                        break;
                    }
                case 8:
                    {
                        accelleration.X = -standardAccelleration;
                        accelleration.Y = gravity.Y;
                        break;
                    }
                case 9:
                    {
                        accelleration.X = -standardAccelleration;
                        accelleration.Y = -standardAccelleration / 2 + gravity.Y;
                        break;
                    }
                case 10:
                    {
                        accelleration.X = -standardAccelleration;
                        accelleration.Y = -standardAccelleration + gravity.Y;
                        break;
                    }
                case 11:
                    {
                        accelleration.X = -standardAccelleration / 2;
                        accelleration.Y = -standardAccelleration + gravity.Y;
                        break;
                    }
                case 12:
                    {
                        accelleration.X = 0;
                        accelleration.Y = -standardAccelleration + gravity.Y;
                        break;
                    }
                case 13:
                    {
                        accelleration.X = standardAccelleration / 2;
                        accelleration.Y = -standardAccelleration + gravity.Y;
                        break;
                    }
                case 14:
                    {
                        accelleration.X = standardAccelleration;
                        accelleration.Y = -standardAccelleration + gravity.Y;
                        break;
                    }
                case 15:
                    {
                        accelleration.X = standardAccelleration;
                        accelleration.Y = -standardAccelleration / 2 + gravity.Y;
                        break;
                    }
            }
        }
        private static void DirectionManager()
        {
            if (!isAlive || isLanded || directionCooldown > 0)
                return;

            if (GfxTools.Win.GetKey(KeyCode.A) || GfxTools.Win.GetKey(KeyCode.Left))
            {
                positionNumber--;
                if (positionNumber < 0)
                    positionNumber = 15;
                directionCooldown = directionTempo;
                ship = new SpriteObj("Assets/Player/player" + positionNumber + ".png", ship.Position);
            }
            else if (GfxTools.Win.GetKey(KeyCode.D) || GfxTools.Win.GetKey(KeyCode.Right))
            {
                positionNumber++;
                if (positionNumber > 15)
                    positionNumber = 0;
                directionCooldown = directionTempo;
                ship = new SpriteObj("Assets/Player/player" + positionNumber + ".png", ship.Position);
            }
        }

        public static void Input()
        {
            DirectionManager();
            AccellerationManager();
        }

        // UPDATE

        private static void MovementManager()
        {
            if (!isAlive || isLanded)
                return;

            if (movementSpeed.X > 0 && movementSpeed.X > horizontalMovementSpeedMax)
                movementSpeed.X = horizontalMovementSpeedMax;
            else if (movementSpeed.X < 0 && movementSpeed.X < -horizontalMovementSpeedMax)
                movementSpeed.X = -horizontalMovementSpeedMax;

            if (movementSpeed.Y > 0 && movementSpeed.Y > verticalMovementSpeedMax)
                movementSpeed.Y = verticalMovementSpeedMax;
            else if (movementSpeed.Y < 0 && movementSpeed.Y < -verticalMovementSpeedMax)
                movementSpeed.Y = -verticalMovementSpeedMax;

            ship.Translate(movementSpeed);

            centeredPosition = new Vector2(ship.Position.X + ship.Width / 2, ship.Position.Y + ship.Height / 2);
        }
        private static void Landing()
        {
            if (!isAlive || isLanded)
                return;

            if (ship.Position.Y + ship.Height >= GfxTools.Win.height - 75)
            {
                if (movementSpeed.X > -1 && movementSpeed.X < 1 && movementSpeed.Y < 1 && positionNumber == 12)
                {
                    isLanded = true;
                    score += 1000;
                }
                else
                {
                    isAlive = false;
                }

                movementSpeed = new Vector2(0, 0);
            }
        }
        private static void Timers()
        {
            if (!isAlive || isLanded)
                return;

            directionCooldown -= Game.DeltaTime;

            if (fuel > 0)
                fuel -= fuelPerSecond * Game.DeltaTime;
            else
                fuel = 0;
        }
        private static void Explosion()
        {
            if (!isAlive)
            {
                positionNumber++;
                if (positionNumber >= 16)
                    positionNumber = 0;
                ship = new SpriteObj("Assets/Player/player" + positionNumber + ".png", ship.Position);
            }
        }

        public static void Update()
        {
            MovementManager();
            Landing();
            Explosion();
            Timers();
        }

        // DRAW

        public static void Draw()
        {
            ship.Draw();
        }
    }
}