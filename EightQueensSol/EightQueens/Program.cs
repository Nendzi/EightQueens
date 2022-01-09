

namespace EightQueens
{
    class Program
    {
        static void Main()
        {
            Console.WriteLine("Welcome. Application wiil find all solutions for you.");

            ProgramLogic programLogic = new ProgramLogic(8);

            Matrix perm = programLogic.FindAllPermutations();
            List<string> solutions = programLogic.SearchForSolutions(perm);

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
                        Console.Write(perm.item[i, x]);
                        oneRow += perm.item[i, x].ToString();
                    }
                    Console.Write("\t");
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

        void ClearTable()
        {
            for (int i = 0; i < _numberOfQueens; i++)
            {
                for (int j = 0; j < _numberOfQueens; j++)
                {
                    _chessBoard.item[i, j] = 0;
                }
            }
        }
    }
}
