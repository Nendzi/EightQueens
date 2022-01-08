using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EightQueens
{
    internal class Matrix
    {
        private int n, m;
        public  int[,] item;

        public Matrix(int rowCount, int columnCount)
        {
            m = rowCount;
            n = columnCount;
            item = new int[rowCount, columnCount];
        }
    }
}
