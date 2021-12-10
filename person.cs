class Person
    {
        private string familyMember;
        private double expenses;
        
        public Person(string familyMember, double expenses)
        {
            this.familyMember = familyMember;
            this.expenses = expenses;
        }

        public string GetFamilyMember() { return familyMember; }
        public double GetExpenses() { return expenses; }
    }
