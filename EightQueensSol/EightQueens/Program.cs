

namespace EightQueens
{
    class Program
    {
        static void Main()
        {
            Console.WriteLine("Welcome. Application will find all solutions for you.");

            ProgramLogic programLogic = new ProgramLogic(8);

            Matrix perm = programLogic.FindAllPermutations();
            List<string> solutions = programLogic.SearchForSolutions(perm);

            int count = 1;
            foreach (var solution in solutions)
            {
                if (count < 10)
                {
                    Console.Write(count + ".  ");
                }
                else
                {
                    Console.Write(count + ". ");
                }
                programLogic.EncodeSolution(solution);
                programLogic.VisiulizeSolution(solution);
                Console.WriteLine();
                Console.WriteLine("____________________________________");
                count++;
            }
            Console.ReadLine();
        }
    }
    class ProgramLogic
    {
        private readonly int _numberOfQueens;
        Matrix _chessBoard;
        int _numberOfPermutation;

        public ProgramLogic(int numberOfQueens)
        {
            _numberOfQueens = numberOfQueens;
            _chessBoard = new Matrix(_numberOfQueens);
        }
        public Matrix FindAllPermutations()
        {
            _numberOfPermutation = PermutationsCalculator.Factoriel(_numberOfQueens);
            Matrix output = new Matrix(_numberOfPermutation, _numberOfQueens);
            output = PermutationsCalculator.Permutations(_numberOfQueens);
            return output;
        }
        public List<string> SearchForSolutions(Matrix perm)
        {
            List<string> output = new List<string>();
            string oneRow = "";
            int row;
            bool isSolution;
            for (int i = 0; i < _numberOfPermutation; i++)
            {
                isSolution = true;
                ClearTable();
                for (int column = 0; column < _numberOfQueens; column++)
                {
                    row = perm.item[i, column];
                    if (!FieldIsEmpty(row, column))
                    {
                        isSolution = false; break;
                    }
                    StrikeThroughFields(row, column);
                }
                if (isSolution)
                {
                    for (int x = 0; x < _numberOfQueens; x++)
                    {
                        oneRow += perm.item[i, x].ToString();
                    }
                    output.Add(oneRow);
                    oneRow = "";
                }
            }
            return output;
        }
        private void StrikeThroughFields(int row, int column)
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
        private void AttackField(int row, int column)
        {
            if (row < 0 || row >= _numberOfQueens || column < 0 || column >= _numberOfQueens)
            {
                return;
            }
            _chessBoard.item[row, column]++;
        }
        private bool FieldIsEmpty(int row, int column)
        {
            if (_chessBoard.item[row, column] != 0)
            {
                return false;
            }
            return true;
        }
        private void ClearTable()
        {
            for (int i = 0; i < _numberOfQueens; i++)
            {
                for (int j = 0; j < _numberOfQueens; j++)
                {
                    _chessBoard.item[i, j] = 0;
                }
            }
        }
        internal void EncodeSolution(string solution)
        {
            string output = "";

            for (int i = 0; i < solution.Length; i++)
            {
                output += Convert.ToChar(65 + i).ToString() + (Convert.ToInt32(solution[i].ToString()) + 1).ToString() + " ";
            }

            Console.WriteLine(output);
        }
        internal void VisiulizeSolution(string solution)
        {
            //top border
            string row1 = "";
            row1 += Convert.ToChar(0x250C);
            row1 += Convert.ToChar(0x2500);
            row1 += Convert.ToChar(0x252C);
            row1 += Convert.ToChar(0x2500);
            row1 += Convert.ToChar(0x252C);
            row1 += Convert.ToChar(0x2500);
            row1 += Convert.ToChar(0x252C);
            row1 += Convert.ToChar(0x2500);
            row1 += Convert.ToChar(0x252C);
            row1 += Convert.ToChar(0x2500);
            row1 += Convert.ToChar(0x252C);
            row1 += Convert.ToChar(0x2500);
            row1 += Convert.ToChar(0x252C);
            row1 += Convert.ToChar(0x2500);
            row1 += Convert.ToChar(0x252C);
            row1 += Convert.ToChar(0x2500);
            row1 += Convert.ToChar(0x2510);

            // middle border
            string row3 = "";
            row3 += Convert.ToChar(0x251C);
            row3 += Convert.ToChar(0x2500);
            row3 += Convert.ToChar(0x253C);
            row3 += Convert.ToChar(0x2500);
            row3 += Convert.ToChar(0x253C);
            row3 += Convert.ToChar(0x2500);
            row3 += Convert.ToChar(0x253C);
            row3 += Convert.ToChar(0x2500);
            row3 += Convert.ToChar(0x253C);
            row3 += Convert.ToChar(0x2500);
            row3 += Convert.ToChar(0x253C);
            row3 += Convert.ToChar(0x2500);
            row3 += Convert.ToChar(0x253C);
            row3 += Convert.ToChar(0x2500);
            row3 += Convert.ToChar(0x253C);
            row3 += Convert.ToChar(0x2500);
            row3 += Convert.ToChar(0x2524);

            // bottom border
            string row4 = "";
            row4 += Convert.ToChar(0x2514);
            row4 += Convert.ToChar(0x2500);
            row4 += Convert.ToChar(0x2534);
            row4 += Convert.ToChar(0x2500);
            row4 += Convert.ToChar(0x2534);
            row4 += Convert.ToChar(0x2500);
            row4 += Convert.ToChar(0x2534);
            row4 += Convert.ToChar(0x2500);
            row4 += Convert.ToChar(0x2534);
            row4 += Convert.ToChar(0x2500);
            row4 += Convert.ToChar(0x2534);
            row4 += Convert.ToChar(0x2500);
            row4 += Convert.ToChar(0x2534);
            row4 += Convert.ToChar(0x2500);
            row4 += Convert.ToChar(0x2534);
            row4 += Convert.ToChar(0x2500);
            row4 += Convert.ToChar(0x2518);

            Console.WriteLine(row1);
            for (int i = 7; i >= 0; i--)
            {
                FormRowOnChessBoard(i, solution);
                if (i == 0)
                {
                    Console.WriteLine(row4);
                }
                else
                {
                    Console.WriteLine(row3);
                }
            }
        }
        private string PutQueen(int location, int position, string solution)
        {
            int extract = Convert.ToInt32(solution[location].ToString());
            if (position == extract)
            {
                return Convert.ToChar(0x263A).ToString();
            }
            return " ";
        }
        private void FormRowOnChessBoard(int position, string solution)
        {
            string output = "";
            output += Convert.ToChar(0x2502);
            for (int i = 0; i < _numberOfQueens; i++)
            {
                output += PutQueen(i, position, solution);
                output += Convert.ToChar(0x2502);
            }
            Console.WriteLine(output);
        }
    }
}
