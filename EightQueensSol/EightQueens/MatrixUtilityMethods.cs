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
        readonly Matrix _chessBoard ;
        private const int _cellLength = 3;

        public MatrixUtilityMethods(int _numberOfQueens)
        {
            this._numberOfQueens = _numberOfQueens;
            this._chessBoard = new Matrix(_numberOfQueens);
            
        }

        public void StrikeThroughFields(int row, int column)
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
        public void AttackField(int row, int column)
        {
            if (row < 0 || row >= _numberOfQueens || column < 0 || column >= _numberOfQueens)
            {
                return;
            }
            _chessBoard.item[row, column]++;
        }
        public bool FieldIsEmpty(int row, int column)
        {
            if (_chessBoard.item[row, column] != 0)
            {
                return false;
            }
            return true;
        }
        public void ClearTable()
        {
            for (int i = 0; i < _numberOfQueens; i++)
            {
                for (int j = 0; j < _numberOfQueens; j++)
                {
                    _chessBoard.item[i, j] = 0;
                }
            }
        }
        public void EncodeSolution(string solution)
        {
            string output = "";

            for (int i = 0; i < solution.Length; i++)
            {
                output += Convert.ToChar(65 + i).ToString() + (Convert.ToInt32(solution[i].ToString()) + 1).ToString() + " ";
            }

            Console.WriteLine(output);
        }
        public string CellWidth(int count)
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
        public string DrawBorder(int[] borderElements)
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
        public string PutQueen(int location, int position, string solution)
        {
            int extract = Convert.ToInt32(solution[location].ToString());
            if (position == extract)
            {
                return Convert.ToChar(0x263A).ToString();
            }
            return " ";
        }
        public void FormRowOnChessBoard(int position, string solution)
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
    }
}
