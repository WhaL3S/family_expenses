 class Matrix
    {
        const int CMaxRows = 100;
        const int CMaxColumns = 7;
        private Person[,] A;
        public int n { get; set; }
        public int m { get; set; }

        public Matrix()
        {
            n = 0;
            m = 0;
            A = new Person[CMaxRows, CMaxColumns];
        }
        
        public void Set(int i, int j, Person v)
        {
            A[i, j] = v;
        }

        public Person Get(int i, int j)
        {
            return A[i, j];
        }
    }
