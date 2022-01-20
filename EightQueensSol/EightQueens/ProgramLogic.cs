using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EightQueens
{
    internal class ProgramLogic
    {
        // this class will handle logic for generating solution

        private readonly int _numberOfQueens;
        /*readonly Matrix _chessBoard;*/
        int _numberOfPermutation;
        MatrixUtilityMethods _utilityMethods;

        public ProgramLogic(int numberOfQueens, MatrixUtilityMethods utilityMethods)
        {
            _numberOfQueens = numberOfQueens;
            /*_chessBoard = new Matrix(_numberOfQueens);*/
            this._utilityMethods = utilityMethods;
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
                _utilityMethods.ClearTable();
                for (int column = 0; column < _numberOfQueens; column++)
                {
                    row = perm.item[i, column];
                    if (!_utilityMethods.FieldIsEmpty(row, column))
                    {
                        isSolution = false; break;
                    }
                    _utilityMethods.StrikeThroughFields(row, column);
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

        public List<string> FindAndClearDependentSolutions(List<string> solutions)
        {
            int removed = 0;
            for (int i = 0; i < solutions.Count; i++)
            {
                string horizontalyMirrored = MatrixUtilityMethods.GetHorizontalyMirrored(solutions[i]);
                string verticalyMirrored = MatrixUtilityMethods.GetVerticalyMirrored(solutions[i]);
                string rotate90degCCW = _utilityMethods.GetRotated90degCCW(solutions[i]);
                string rotate180degCCW = _utilityMethods.GetRotated180degCCW(solutions[i]);
                string rotate270degCCW = _utilityMethods.GetRotated270degCCW(solutions[i]);
                removed += solutions.RemoveAll(x => x == horizontalyMirrored);
                removed += solutions.RemoveAll(x => x == verticalyMirrored);
                removed += solutions.RemoveAll(x => x == rotate90degCCW);
                removed += solutions.RemoveAll(x => x == rotate180degCCW);
                removed += solutions.RemoveAll(x => x == rotate270degCCW);
            }
            return solutions;
        }
        #region Private methods

        #endregion
    }
}
