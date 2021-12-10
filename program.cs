 class Program
    {
        static void Read(string fd, ref Matrix familyExpenses)
        {
            int nn, mm;
            double expenses;
            string line, name;
            Person person;
            using(StreamReader reader = new StreamReader(fd))
            {
                line = reader.ReadLine();
                string[] parts;
                nn = int.Parse(line);
                line = reader.ReadLine();
                mm = int.Parse(line);
                familyExpenses.n = nn;
                familyExpenses.m = mm;
                for(int i = 0; i < nn; i++)
                {
                    line = reader.ReadLine();
                    parts = line.Split(';');
                    for(int j = 0; j < mm; j++)
                    {
                        name = parts[2 * j];
                        expenses = double.Parse(parts[2 * j + 1]);
                        person = new Person(name, expenses);
                        familyExpenses.Set(i, j, person);
                    }
                }
            }
        }

        static void Print(string fv, Matrix familyExpenses, string header)
        {
            Person person; 
            using (StreamWriter writer = new StreamWriter(fv, true))
            {
                writer.WriteLine(header);
                writer.WriteLine();
                writer.WriteLine("Number of weeks {0}", familyExpenses.n);
                writer.WriteLine("Number of days {0}", familyExpenses.m);
                writer.WriteLine();
                for (int j = 0; j < familyExpenses.m; j++)
                    writer.Write("{0, 10}-day  ", j + 1);
                writer.WriteLine();
                for(int i = 0; i < familyExpenses.n; i++)
                {
                    for(int j = 0; j < familyExpenses.m; j++)
                    {
                        person = familyExpenses.Get(i, j);
                        writer.Write("{0,8} {1,6:f2} ", person.GetFamilyMember(), person.GetExpenses());
                    }
                    writer.WriteLine();
                }
            }
        }

        static decimal TotalExpenditures(Matrix familyExpenses)
        {
            Person person;
            double sum = 0;
            for (int i = 0; i < familyExpenses.n; i++)
                for (int j = 0; j < familyExpenses.m; j++)
                {
                    person = familyExpenses.Get(i, j);
                    sum = sum + person.GetExpenses();
                }
            return (decimal)sum;
        }

        static decimal PersonalExpenses(Matrix familyExpenses, string familyMember)
        {
            Person person;
            double sum = 0;
            for (int i = 0; i < familyExpenses.n; i++)
                for (int j = 0; j < familyExpenses.m; j++)
                {
                    person = familyExpenses.Get(i, j);
                    if (person.GetFamilyMember() == familyMember)
                    {
                        sum = sum + person.GetExpenses();
                    }
                }
            return (decimal)sum;
        }

        static void ExpensesInWeek(string fv, ref Matrix familyExpenses)
        {
            using (StreamWriter writer = new StreamWriter(fv, true))
            {
                for (int i = 0; i < familyExpenses.n; i++)
                {
                    double sum = 0;
                    for (int j = 0; j < familyExpenses.m; j++)
                    {
                        Person x = familyExpenses.Get(i, j);
                        sum = sum + x.GetExpenses();
                    }
                    writer.WriteLine("Week no. {0} expenses {1,5:c2}.", i + 1,
                    (decimal)sum);
                }
                writer.WriteLine();
            }
        }

        static void ExpensesInDay(string fv, Matrix familyExpenses)
        {
            using (StreamWriter writer = new StreamWriter(fv, true))
            {
                
                for (int j = 0; j < familyExpenses.m; j++)
                {
                    double sum = 0;
                    for (int i = 0; i < familyExpenses.n; i++)
                    {
                        Person x = familyExpenses.Get(i, j);
                        sum = sum + x.GetExpenses();
                    }
                    switch (j+1)
                    {
                        case 1:
                            writer.WriteLine("Monday expenses {0,5:c2}.", (decimal)sum);
                            break;
                        case 2:
                            writer.WriteLine("Tuesday expenses {0,5:c2}.", (decimal)sum);
                            break;
                        case 3:
                            writer.WriteLine("Wednesday expenses {0,5:c2}.", (decimal)sum);
                            break;
                        case 4:
                            writer.WriteLine("Thursday expenses {0,5:c2}.", (decimal)sum);
                            break;
                        case 5:
                            writer.WriteLine("Friday expenses {0,5:c2}.", (decimal)sum);
                            break;
                        case 6:
                            writer.WriteLine("Saturday expenses {0,5:c2}.", (decimal)sum);
                            break;
                        case 7:
                            writer.WriteLine("Sunday expenses {0,5:c2}.", (decimal)sum);
                            break;
                        default:
                            writer.WriteLine("No such day exists");
                            break;
                    }
                }
            }
        }


        const string Cfd = "FamilyExpensesData.txt";
        const string Cfr = "Results.txt";

        static void Main(string[] args)
        {
            if (File.Exists(Cfr)) File.Delete(Cfr);
            Matrix familyExpenses = new Matrix();
            Read(Cfd, ref familyExpenses);
            Print(Cfr, familyExpenses, "Initial data");
            using (StreamWriter writer = new StreamWriter(Cfr, true))
            {
                writer.WriteLine();
                writer.WriteLine("Total expenditure: {0,5:c2}.",
                TotalExpenditures(familyExpenses));
                writer.WriteLine("Total expenditure of a husband: {0,5:c2}.",
                        PersonalExpenses(familyExpenses, "husband"));
                writer.WriteLine("Total expenditure of a wife: {0,5:c2}.",
                        PersonalExpenses(familyExpenses, "wife"));
                writer.WriteLine();
            }
            ExpensesInWeek(Cfr, ref familyExpenses);
            ExpensesInDay(Cfr, familyExpenses);
        }
    }
