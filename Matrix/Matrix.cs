using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Matrix
{
    public class IntMatrix : IMatrix
    {
        public static IntMatrix Generate(int n, int m)
        {
            if (n <= 0) throw new ArgumentOutOfRangeException(nameof(n));
            if (m <= 0) throw new ArgumentOutOfRangeException(nameof(m));

            var matrix = new int[n, m];

            for(int i = 0; i < n; i++)
            {
                for (int j = 0; j < m; j++)
                {
                    matrix[i, j] = Random.Shared.Next(0, 10);
                }
            }

            return new IntMatrix(matrix);
        }

        private readonly int[,] _matrix;

        public IntMatrix(int[,] matrix)
        {
            _matrix = matrix;
        }

        public bool IsSymmetric()
        {
            if (_matrix.GetLength(0) != _matrix.GetLength(1))
                return false;

            var length = _matrix.GetLength(0);

            for (int i = 0; i < length; i++)
            {
                for (int j = 0; j < length; j++)
                {
                    if (i == j)
                        continue;

                    if (_matrix[i, j] != _matrix[j, i])
                        return false;
                }
            }
            return true;
        }

        public bool IsMagic()
        {
            if (_matrix.GetLength(0) != _matrix.GetLength(1))
                return false;

            var length = _matrix.GetLength(0);

            var dim1Sums = new int[length];
            var dim2Sums = new int[length];
            var diag1Sum = 0;
            var diag2Sum = 0;

            for (int i = 0; i < length; i++)
            {
                for (int j = 0; j < length; j++)
                {
                    if (i == j)
                    {
                        diag1Sum += _matrix[i, j];
                    }

                    if (i == length - j - 1)
                    {
                        diag2Sum += _matrix[i, j];
                    }

                    dim1Sums[i] += _matrix[i, j];
                    dim2Sums[j] += _matrix[i, j];
                }
            }

            if (diag1Sum != diag2Sum)
                return false;

            for (int i = 0; i < length; i++)
            {
                if (dim1Sums[i] != diag1Sum || dim2Sums[i] != diag1Sum)
                    return false;
            }

            return true;
        }

        public override string ToString()
        {
            var columnCount = _matrix.GetLength(0);
            var rowCount = _matrix.GetLength(1);

            var stringMatrix = new string[columnCount, rowCount];

            for (int i = 0; i < columnCount; i++)
            {
                for (int j = 0; j < rowCount; j++)
                {
                    stringMatrix[i, j] = _matrix[i, j].ToString();
                }
            }

            for (int i = 0; i < columnCount; i++)
            {
                var maxLength = 0;

                for (int j = 0; j < rowCount; j++)
                {
                    maxLength = maxLength < stringMatrix[i, j].Length ? stringMatrix[i, j].Length : maxLength;
                }

                for (int j = 0; j < rowCount; j++)
                {
                    stringMatrix[i, j] = stringMatrix[i, j] + new string(' ', 2 + maxLength - stringMatrix[i, j].Length);
                }
            }

            var stringBuilder = new StringBuilder();

            for (int j = 0; j < rowCount; j++)
            {
                stringBuilder.Append("| ");

                for (int i = 0; i < columnCount; i++)
                {
                    stringBuilder.Append(stringMatrix[i, j]);
                }

                stringBuilder.AppendLine("|");
            }

            return stringBuilder.ToString();
        }
    }
}
