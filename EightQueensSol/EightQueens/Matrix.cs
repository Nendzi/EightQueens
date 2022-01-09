using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EightQueens
{
    internal class Matrix
    {
        private readonly int _n;
        private readonly int _m;
        public  int[,] item;

        public Matrix(int rowCount, int columnCount)
        {
            _m = rowCount;
            _n = columnCount;
            item = new int[_m, _n];
        }

        public Matrix(int dimension)
        {
            _n = dimension;
            item = new int[_n,_n];
        }
    }
}
