using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EightQueens
{
    internal class MatrixUtilityMethods
    {
        //this class will provide functionalities to work with the matrix
        // such as state of matrix, visualization etc

        private readonly int _numberOfQueens;
        private readonly Matrix _chessBoard;
        private const int _cellLength = 3;

        internal delegate Position RotateExistingSolution(int position, int valueOnPosition);

        internal RotateExistingSolution _setOn90deg = new(Rotate90deg);
        internal RotateExistingSolution _setOn180deg = new(Rotate180deg);
        internal RotateExistingSolution _setOn270deg = new(Rotate270deg);
        static Position Rotate90deg(int position, int valueOnPosition)
        {
            Position queenPosition = new Position();
            queenPosition.position = 7 - valueOnPosition;
            queenPosition.valueOnPosition=Convert.ToChar((position).ToString());
            return queenPosition;
        }
        static Position Rotate180deg(int position, int valueOnPosition)
        {
            Position queenPosition = new Position();
            queenPosition.position = 7 - position;
            queenPosition.valueOnPosition = Convert.ToChar((7 - valueOnPosition).ToString());
            return queenPosition;
        }
        static Position Rotate270deg(int position, int valueOnPosition)
        {
            Position queenPosition = new Position();
            queenPosition.position = valueOnPosition;
            queenPosition.valueOnPosition = Convert.ToChar((7 - position).ToString());
            return queenPosition;
        }
        internal MatrixUtilityMethods(int numberOfQueens)
        {
            _numberOfQueens = numberOfQueens;
            _chessBoard = new Matrix(numberOfQueens);
        }
        internal void StrikeThroughFields(int row, int column)
        {
            int zeroBasedNumberOfElements = _numberOfQueens - 1;
            int maxRow = ((zeroBasedNumberOfElements - row) < row) ? row : zeroBasedNumberOfElements - row;
            int maxColumn = ((zeroBasedNumberOfElements - column) < column) ? column : zeroBasedNumberOfElements - column;
            int counter = (maxRow <= maxColumn) ? maxColumn : maxRow;
            int rowUp;
            int rowDown;
            int columnLeft;
            int columnRight;
            for (int i = 1; i <= counter; i++)
            {
                rowUp = row - i;
                rowDown = row + i;
                columnLeft = column - i;
                columnRight = column + i;

                AttackField(rowUp, column);
                AttackField(rowDown, column);
                AttackField(row, columnLeft);
                AttackField(row, columnRight);
                AttackField(rowUp, columnLeft);
                AttackField(rowUp, columnRight);
                AttackField(rowDown, columnLeft);
                AttackField(rowDown, columnRight);
            }
        }
        internal bool FieldIsEmpty(int row, int column)
        {
            if (_chessBoard.item[row, column] != 0)
            {
                return false;
            }
            return true;
        }
        internal void ClearTable()
        {
            for (int i = 0; i < _numberOfQueens; i++)
            {
                for (int j = 0; j < _numberOfQueens; j++)
                {
                    _chessBoard.item[i, j] = 0;
                }
            }
        }
        internal void AttackField(int row, int column)
        {
            if (row < 0 || row >= _numberOfQueens || column < 0 || column >= _numberOfQueens)
            {
                return;
            }
            _chessBoard.item[row, column]++;
        }
        internal string GetRotated90degCCW(string inputData)
        {
            char[] output = FindDependentSolution(inputData, _setOn90deg);
            return JoinChar(output);
        }
        internal string GetRotated180degCCW(string inputData)
        {
            char[] output = FindDependentSolution(inputData, _setOn180deg);
            return JoinChar(output);
        }
        internal string GetRotated270degCCW(string inputData)
        {
            char[] output = FindDependentSolution(inputData, _setOn270deg);
            return JoinChar(output);
        }
        internal static char[] FindDependentSolution(string inputData, RotateExistingSolution setCharOnPosition)
        {
            char[] output = new char[inputData.Length];
            Position position1 = new Position(); 
            for (int i = 0; i < inputData.Length; i++)
            {
                int position = i;
                int valueOnPosition = Convert.ToInt32(inputData[i].ToString());
                position1 = setCharOnPosition.Invoke(position,valueOnPosition);
                output[position1.position] = position1.valueOnPosition;
            }
            return output;
        }
        internal static string JoinChar(char[] input)
        {
            string output = "";
            for (int i = 0; i < input.Length; i++)
            {
                output += input[i].ToString();
            }
            return output;
        }
        internal static string GetVerticalyMirrored(string inputData)
        {
            string output = "";
            foreach (char item in inputData)
            {
                output += (7 - Convert.ToInt32(item.ToString())).ToString();
            }
            return output;
        }
        internal static string GetHorizontalyMirrored(string inputData)
        {
            string output = "";
            foreach (var myChar in inputData.Reverse())
            {
                output += myChar.ToString();
            }
            return output;
        }
        internal static void EncodeSolution(string solution)
        {
            string output = "";

            for (int i = 0; i < solution.Length; i++)
            {
                output += Convert.ToChar(65 + i).ToString() + (Convert.ToInt32(solution[i].ToString()) + 1).ToString() + " ";
            }

            Console.WriteLine(output);
        }
        internal static string CellWidth(int count)
        {
            string output = "";
            for (int i = 0; i < count; i++)
            {
                output += Convert.ToChar(0x2500);
            }
            return output;
        }
        internal void VisualizeSolution(string solution)
        {
            Console.WriteLine(DrawBorder(new int[] { 0x250C, 0x2510, 0x252C }));
            for (int i = 7; i >= 0; i--)
            {
                FormRowOnChessBoard(i, solution);
                if (i == 0)
                {
                    Console.WriteLine(DrawBorder(new int[] { 0x2514, 0x2518, 0x2534 }));
                }
                else
                {
                    Console.WriteLine(DrawBorder(new int[] { 0x251C, 0x2524, 0x253C }));
                }
            }
        }
        internal string DrawBorder(int[] borderElements)
        {
            string output = "";
            output += Convert.ToChar(borderElements[0]);
            for (int i = 0; i < _numberOfQueens; i++)
            {
                output += CellWidth(_cellLength);
                if (i == _numberOfQueens - 1)
                {
                    output += Convert.ToChar(borderElements[1]);
                }
                else
                {
                    output += Convert.ToChar(borderElements[2]);
                }
            }
            return output;
        }
        internal static string PutQueen(int location, int position, string solution)
        {
            int extract = Convert.ToInt32(solution[location].ToString());
            if (position == extract)
            {
                return Convert.ToChar(0x263A).ToString();
            }
            return " ";
        }
        internal void FormRowOnChessBoard(int position, string solution)
        {
            string output = "";
            output += Convert.ToChar(0x2502);
            for (int i = 0; i < _numberOfQueens; i++)
            {
                output += " ";
                output += PutQueen(i, position, solution);
                output += " ";
                output += Convert.ToChar(0x2502);
            }
            Console.WriteLine(output);
        }
        internal class Position
        {
            internal int position;
            internal char valueOnPosition;
        }
    }
}
