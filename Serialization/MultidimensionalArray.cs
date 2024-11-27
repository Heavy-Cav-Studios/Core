using System;
using UnityEngine;

namespace HeavyCavStudios.Core.Serialization
{
    /// <summary>
    /// Represents a serializable multidimensional array.
    /// </summary>
    /// <typeparam name="T">The type of elements stored in the array.</typeparam>
    [Serializable]
    public abstract class MultidimensionalArray<T>
    {
        [SerializeField]
        int m_Rows;
        [SerializeField]
        int m_Columns;
        [SerializeField]
        T[] m_Array;

        /// <summary>
        /// Initializes a new instance of the <see cref="MultidimensionalArray{T}"/> class with specified rows and columns.
        /// </summary>
        /// <param name="rows">The number of rows.</param>
        /// <param name="columns">The number of columns.</param>
        protected MultidimensionalArray(int rows, int columns)
        {
            m_Rows = rows;
            m_Columns = columns;
            m_Array = new T[rows * columns];
        }

        /// <summary>
        /// Gets or sets the value at the specified row and column.
        /// </summary>
        /// <param name="i">The row index.</param>
        /// <param name="j">The column index.</param>
        /// <returns>The value at the specified row and column.</returns>
        /// <exception cref="IndexOutOfRangeException">Thrown when the provided indices are out of bounds.</exception>
        public T this[int i, int j]
        {
            get
            {
                ValidateIndex(i, j);
                return m_Array[i * m_Columns + j];
            }
            set
            {
                ValidateIndex(i, j);
                m_Array[i * m_Columns + j] = value;
            }
        }

        /// <summary>
        /// Validates that the given indices are within the bounds of the array.
        /// </summary>
        /// <param name="i">The row index.</param>
        /// <param name="j">The column index.</param>
        /// <exception cref="IndexOutOfRangeException">Thrown when the indices are out of range.</exception>
        void ValidateIndex(int i, int j)
        {
            if (i < 0 || i >= m_Rows || j < 0 || j >= m_Columns)
            {
                throw new IndexOutOfRangeException($"Index [{i}, {j}] is out of range. Array dimensions are {m_Rows}x{m_Columns}.");
            }
        }
    }
}
