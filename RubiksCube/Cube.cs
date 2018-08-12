using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RubiksCube
{
    enum Colors { Red, Yellow, Green, Blue, Orange, White };

    class Side
    {
        public Colors[,] Data;
        public AdjacentVector[] AdjacentRows;

        public Side(Colors c)
        {
            this.Data = new Colors[,] { { c, c, c }, { c, c, c }, { c, c, c } };
        }

        public Colors[] GetColumn(int index)
        {
            return new Colors[] { this.Data[0, index], this.Data[1, index], this.Data[2, index] };
        }

        public Colors[] GetRow(int index)
        {
            return new Colors[] { this.Data[index, 0], this.Data[index, 1], this.Data[index, 2] };
        }

        public void SetColumn(Colors[] colors, int index)
        {
            this.Data[0, index] = colors[0];
            this.Data[1, index] = colors[1];
            this.Data[2, index] = colors[2];
        }

        public void SetRow(Colors[] colors, int index)
        {
            this.Data[index, 0] = colors[0];
            this.Data[index, 1] = colors[1];
            this.Data[index, 2] = colors[2];
        }
      
        public void Rotate()
        {
            RotateAdjacentVectors();
            RotateFace();
        }
        
        // Updates the adjacent rows in the Side objects "touching" the
        // Side object that was rotated.
        public void RotateAdjacentVectors()
        {
            int length = this.AdjacentRows.Length;
            Colors[] temp = new Colors[3];
            Array.Copy(this.AdjacentRows[length - 1].GetAdjacentVector(), temp, 3);

            for (int i = length - 1; i > 0; i--)
            {
                AdjacentVector current = this.AdjacentRows[i];
                AdjacentVector previous = this.AdjacentRows[i - 1];

                current.SetVector(previous.GetAdjacentVector());
            }

            this.AdjacentRows[0].SetVector(temp);
        }

        // Updates the position of each piece of data in a Side object that
        // has been rotated.
        public void RotateFace()
        {
            int xLength = this.Data.GetLength(0);
            int yLength = this.Data.GetLength(1);

            Colors[,] rotated = new Colors[xLength, yLength];

            for (int i = 0; i < xLength; i++)
            {
                for (int j = 0; j < yLength; j++)
                {
                    rotated[i, j] = this.Data[xLength - j - 1, i];
                }
            }

            this.Data = rotated;
        }

        public override string ToString()
        {
            string str = "";

            for (int x = 0; x < this.Data.GetLength(0); x += 1)
            {
                for (int y = 0; y < this.Data.GetLength(1); y += 1)
                {
                    str += this.Data[x, y] + " ";
                }

                str += "\n";
            }

            return str;
        }
    }

    class AdjacentVector
    {
        public Side Side;
        public int Index;
        public bool IsRow;

        // Generate a vector affected by the Side object that 
        // created it.
        public AdjacentVector(Side side, int index, bool isRow)
        {
            this.Side = side;
            this.Index = index;
            this.IsRow = isRow;
        }

        public Colors[] GetAdjacentVector()
        {
            if (this.IsRow)
            {
                return this.Side.GetRow(this.Index);
            }
            else
            {
                return this.Side.GetColumn(this.Index);
            }
        }

        public void SetVector(Colors[] colors)
        {
            if (IsRow)
            {
                this.Side.SetRow(colors, this.Index);
            }
            else
            {
                this.Side.SetColumn(colors, this.Index);
            }
        }

        public override string ToString()
        {
            string str = ""; 
            foreach (Colors color in this.GetAdjacentVector())
            {
                if (this.IsRow)
                {
                    str += color + " ";
                }
                else
                {
                    str += color + "\n";
                }
            }

            return str;
        }

    }

    partial class Program
    {
        /*
        public static void Main()
        {
            Cube c = new Cube();

            Cube.Action[] actions = new Cube.Action[]
            {
                Cube.Action.Front,
                Cube.Action.Right,
                Cube.Action.Left
            };

            foreach (Cube.Action action in actions) {
                cube.Execute(action);
                Console.WriteLine(cube);
            }

            cube.Execute(Cube.Action.Front);
            Console.WriteLine(cube);
            Console.Read();
            
        }
        */
    }
}
