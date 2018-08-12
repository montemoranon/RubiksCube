using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RubiksCube
{
    public class Cube
    {
        public enum Action
        {
            Front,
            FrontPrime,
            Upper,
            UpperPrime,
            Right,
            RightPrime,
            Back,
            BackPrime,
            Left,
            LeftPrime,
            Down,
            DownPrime
        }

        Side Upper;
        Side Back;
        Side Right;
        Side Left;
        Side Front;
        Side Down;

        public Cube()
        {
            this.Upper = new Side(Colors.White);
            this.Back = new Side(Colors.Yellow);
            this.Right = new Side(Colors.Red);
            this.Left = new Side(Colors.Orange);
            this.Front = new Side(Colors.Green);
            this.Down = new Side(Colors.Blue);

            // Wires up the proper Side object vectors touching the face of 
            // the 'back' Side object.
            AdjacentVector upperBackConnection = new AdjacentVector(this.Back, 0, true);
            AdjacentVector upperFrontConnection = new AdjacentVector(this.Front, 0, true);
            AdjacentVector upperLeftConnection = new AdjacentVector(this.Left, 0, true);
            AdjacentVector upperRightConnection = new AdjacentVector(this.Right, 0, true);

            // The initial order of this array is critical, as it represents
            // the actual physical cube and must be consistent across Side objects.
            this.Upper.AdjacentRows = new AdjacentVector[] { upperBackConnection, upperRightConnection, upperFrontConnection, upperLeftConnection };

            AdjacentVector backUpperConnection = new AdjacentVector(this.Upper, 0, true);
            AdjacentVector backLeftConnection = new AdjacentVector(this.Left, 0, false);
            AdjacentVector backDownConnection = new AdjacentVector(this.Down, 2, true);
            AdjacentVector backRightConnection = new AdjacentVector(this.Right, 2, false);

            this.Back.AdjacentRows = new AdjacentVector[] { backUpperConnection, backLeftConnection, backDownConnection, backRightConnection };

            AdjacentVector rightTopConnection = new AdjacentVector(this.Upper, 2, false);
            AdjacentVector rightBackConnection = new AdjacentVector(this.Back, 0, false);
            AdjacentVector rightDownConnection = new AdjacentVector(this.Down, 2, false);
            AdjacentVector rightFrontConnection = new AdjacentVector(this.Front, 2, false);

            this.Right.AdjacentRows = new AdjacentVector[] { rightTopConnection, rightBackConnection, rightDownConnection, rightFrontConnection };

            AdjacentVector downFrontConnection = new AdjacentVector(this.Front, 2, true);
            AdjacentVector downRightConnection = new AdjacentVector(this.Right, 2, true);
            AdjacentVector downBackConnection = new AdjacentVector(this.Back, 2, true);
            AdjacentVector downLeftConnection = new AdjacentVector(this.Left, 2, true);

            this.Down.AdjacentRows = new AdjacentVector[] { downFrontConnection, downRightConnection, downBackConnection, downLeftConnection };

            AdjacentVector frontUpperConnection = new AdjacentVector(this.Upper, 2, true);
            AdjacentVector frontRightConnection = new AdjacentVector(this.Right, 0, false);
            AdjacentVector frontDownConnection = new AdjacentVector(this.Down, 0, true);
            AdjacentVector frontLeftConnection = new AdjacentVector(this.Left, 2, false);

            Front.AdjacentRows = new AdjacentVector[] { frontUpperConnection, frontRightConnection, frontDownConnection, frontLeftConnection };

            AdjacentVector leftUpperConnection = new AdjacentVector(this.Upper, 0, false);
            AdjacentVector leftFrontConnection = new AdjacentVector(this.Front, 0, false);
            AdjacentVector leftDownConnection = new AdjacentVector(this.Down, 0, false);
            AdjacentVector leftBackConnection = new AdjacentVector(this.Back, 2, false);

            this.Left.AdjacentRows = new AdjacentVector[] { leftUpperConnection, leftFrontConnection, leftDownConnection, leftBackConnection };
        }

        public void Execute(Action action)
        {
            Dictionary<Action, Side> sides = new Dictionary<Action, Side>
            {
                { Action.Front, this.Front }, { Action.FrontPrime, this.Front },
                { Action.Upper, this.Upper }, { Action.UpperPrime, this.Upper },
                { Action.Right, this.Right }, { Action.RightPrime, this.Right },
                { Action.Back, this.Back }, { Action.BackPrime, this.Back },
                { Action.Left, this.Left }, { Action.LeftPrime, this.Left },
                { Action.Down, this.Right }, { Action.DownPrime, this.Down },
            };
            Side side = sides[action];
            
            switch (action)
            {
                case Action.Front:
                case Action.Upper:
                case Action.Right:
                case Action.Back:
                case Action.Left:
                case Action.Down:
                    this.RotateFaceClockwise(side);
                    break;
                case Action.FrontPrime:
                case Action.UpperPrime:
                case Action.RightPrime:
                case Action.BackPrime:
                case Action.LeftPrime:
                case Action.DownPrime:
                    this.RotateFaceClockwise(side);
                    break;
            }
        }

        private void RotateFaceClockwise(Side face)
        {
            face.Rotate();
        }

        private void RotateFaceCounterClockwise(Side face)
        {
            face.Rotate();
            face.Rotate();
            face.Rotate();
        }

        public override string ToString()
        {
            Dictionary<string, Side> sides = new Dictionary<string, Side>
            {
                { "FRONT", this.Front },
                { "UPPER", this.Upper },
                { "BACK", this.Back },
                { "LEFT", this.Left },
                { "RIGHT", this.Right },
                { "DOWN", this.Down },
            };

            StringBuilder str = new StringBuilder();
            foreach (KeyValuePair<string, Side> entry in sides)
            {
                str.AppendLine(entry.Key);
                str.AppendLine(entry.Value.ToString());
                str.AppendLine();
            }


            return str.ToString();
        }

        public static void Main()
        {
            Cube c = new Cube();

            c.RotateFaceClockwise(c.Left);
            Console.WriteLine(c.Upper);
            c.RotateFaceClockwise(c.Left);
            Console.WriteLine(c.Upper);
            c.RotateFaceClockwise(c.Left);
            Console.WriteLine(c.Upper);
            c.RotateFaceClockwise(c.Left);
            Console.WriteLine(c.Upper);

            Console.Read();
        }
    }
}
