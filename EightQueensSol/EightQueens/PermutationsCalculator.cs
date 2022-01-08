using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EightQueens
{
    internal static class PermutationsCalculator
    {
        public static int Factoriel(int n)
        {
            int result = 1;
            for (int i = 1; i <= n; i++)
            {
                result *= i;
            }
            return result;
        }
        public static Matrix Permutations(int n)
        {
            int ColumnCount = 0;
            Matrix output = new Matrix(Factoriel(n), n);
            int[] tempM = new int[n];
            int[] forLoopStartValues = new int[n];
            int[] forLoopEndValues = new int[n];
            int[] forNext = new int[n];
            for (int i = 0; i < n; i++)
            {
                tempM[i] = i;
                forLoopStartValues[i] = 1;
                forNext[i] = 1;
                forLoopEndValues[i] = i + 1;
            }
            int forLoopInProccessing = n - 1;
            bool needToWrite = true;
            bool goToStart = true;

            do
            {
                if (needToWrite)
                {
                    for (int i = 0; i < n; i++)
                    {
                        output.item[ColumnCount, i] = tempM[i];
                    }
                    ColumnCount++;
                }
                ShiftQueueLeft(tempM, forLoopInProccessing);
                forNext[forLoopInProccessing]++;
                if (forNext[forLoopInProccessing] > forLoopEndValues[forLoopInProccessing])
                {
                    forNext[forLoopInProccessing] = forLoopStartValues[forLoopInProccessing];
                    forLoopInProccessing--;
                    if (forLoopInProccessing == 0)
                    {
                        goToStart = false;
                    }
                    needToWrite = false;
                }
                else
                {
                    forLoopInProccessing = n - 1;
                    needToWrite = true;
                }
            } while (goToStart);
            return output;
        }
        private static int[] ShiftQueueLeft(int[] queue, int upperLimit)
        {
            int temp = queue[0];
            for (int i = 0; i < upperLimit; i++)
            {
                queue[i] = queue[i + 1];
            }
            queue[upperLimit] = temp;
            return queue;
        }
    }
}
