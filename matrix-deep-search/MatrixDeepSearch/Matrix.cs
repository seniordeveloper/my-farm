namespace MatrixDeepSearch
{
    /// <summary>
    /// Represents a high level abstraction over matrix.
    /// </summary>
    public class Matrix
    {
        private Matrix() { }

        public static Matrix FromFile(string fileName) 
        {
            var rows = GetRows(fileName);

            var matrix = new Matrix();

            if (rows.Length > 0) 
            {
                matrix.EnsureInitialized(rows);

                for (var i = 0; i < rows.Length; i++)
                {
                    var row = rows[i];
                    var columns = row.Split(",", StringSplitOptions.RemoveEmptyEntries);

                    for (var j = 0; j < columns.Length; j++)
                    {
                        matrix._innerArray[i, j] = columns[j] == "1";
                    }
                }
            }

            return matrix;
        }

        public bool IsValid => _innerArray != null;

        public int NumberOfValidAreas 
        {
            get 
            {
                if (IsValid && _numberOfValidAreas == -1)  
                {
                    _numberOfValidAreas = GetNumberOfValidAreas();
                }

                return _numberOfValidAreas;
            }
        }

        private int GetNumberOfValidAreas() 
        {
            var areasCount = 0;

            for (int i = 0; i < _rowsCount; i++)
            {
                for (int j = 0; j < _columnsCount; j++)
                {
                    if (_innerArray[i, j]) 
                    {
                        areasCount++;
                        _innerArray[i, j] = false;
                        DoDeepSearch(i, j);
                    }
                }
            }

            return areasCount;
        }

        private void DoDeepSearch(int rowIndex, int columnIndex) 
        {
            if (IsInRange(rowIndex - 1, columnIndex) && _innerArray[rowIndex - 1, columnIndex]) 
            {
                _innerArray[rowIndex - 1, columnIndex] = false;
                DoDeepSearch(rowIndex - 1, columnIndex);
            }

            if (IsInRange(rowIndex, columnIndex - 1) && _innerArray[rowIndex, columnIndex - 1]) 
            {
                _innerArray[rowIndex, columnIndex - 1] = false;
                DoDeepSearch(rowIndex, columnIndex - 1);
            }

            if (IsInRange(rowIndex + 1, columnIndex) && _innerArray[rowIndex + 1, columnIndex]) 
            {
                _innerArray[rowIndex + 1, columnIndex] = false;
                DoDeepSearch(rowIndex + 1, columnIndex);
            }

            if (IsInRange(rowIndex, columnIndex + 1) && _innerArray[rowIndex, columnIndex + 1])
            {
                _innerArray[rowIndex, columnIndex + 1] = false;
                DoDeepSearch(rowIndex, columnIndex + 1);
            }
        }

        private bool IsInRange(int rowIndex, int columnIndex) => 
            _rowsCount > 0 && 
            _columnsCount > 0 && 
            rowIndex >= 0 && 
            rowIndex < _rowsCount &&
            columnIndex >=0 &&
            columnIndex < _columnsCount;
        
        public int RowsCount => _rowsCount;
        public int ColumnsCount => _columnsCount;

        public override string ToString()
        {
            return $"{NumberOfValidAreas}";
        }

        private void EnsureInitialized(string[] rows) 
        {
            if (_innerArray == null) 
            {
                _rowsCount = rows.Length;

                var columns = rows[0].Split(",", StringSplitOptions.RemoveEmptyEntries);
                _columnsCount = columns.Length;

                _innerArray = new bool[_rowsCount, _columnsCount];
            }
        }

        private static string[] GetRows(string fileName) 
        {
            var fileContent = File.ReadAllText(fileName);

            return fileContent.Split(";", StringSplitOptions.RemoveEmptyEntries);
        }

        private bool[,] _innerArray;
        private int _rowsCount = 0;
        private int _columnsCount = 0;
        private int _numberOfValidAreas = -1;
    }
}
