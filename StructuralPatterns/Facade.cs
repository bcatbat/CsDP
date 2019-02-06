using System;

namespace CsDP.Facade.Structural
{
    class MainApp
    {
        public static void TestCall()
        {
            Facade facade = new Facade();
            facade.MethodA();
            facade.MethodB();
        }
    }

    class SubSystemOne
    {
        public void MethodOne()
        {
            System.Console.WriteLine(" SubSystemOne Method");
        }
    }
    class SubSystemTwo
    {
        public void MethodTwo()
        {
            System.Console.WriteLine(" SubSystemTwo Method");
        }
    }
    class SubSystemThree
    {
        public void MethodThree()
        {
            System.Console.WriteLine(" SubSysemThree Method");
        }
    }
    class SubSystemFour
    {
        public void MethodFour()
        {
            System.Console.WriteLine(" SubSystemFour Method");
        }
    }
    class Facade
    {
        private SubSystemOne _one;
        private SubSystemTwo _two;
        private SubSystemThree _three;
        private SubSystemFour _four;
        public Facade()
        {
            _one = new SubSystemOne();
            _two = new SubSystemTwo();
            _three = new SubSystemThree();
            _four = new SubSystemFour();
        }

        public void MethodA()
        {
            System.Console.WriteLine("\nMethodA() ---- ");
            _one.MethodOne();
            _two.MethodTwo();
            _four.MethodFour();
        }
        public void MethodB()
        {
            System.Console.WriteLine("\nMethodB() ---- ");
            _two.MethodTwo();
            _three.MethodThree();
        }
    }
}

namespace CsDP.Facade.RealWorld
{
    class MainApp
    {
        public static void TestCall()
        {
            Mortgage mortgage = new Mortgage();

            Customer customer = new Customer("Ann McKinsey");
            bool eligible = mortgage.IsEligible(customer, 125000);

            System.Console.WriteLine("\n" + customer.Name + " has been " +
            (eligible ? "Approved" : "Rejected"));
        }
    }

    class Bank
    {
        public bool HasSufficientSavings(Customer c, int amount)
        {
            System.Console.WriteLine("check bank for " + c.Name);
            return true;
        }
    }
    class Credit
    {
        public bool HasGoodCredit(Customer c)
        {
            System.Console.WriteLine("check credit for " + c.Name);
            return true;
        }
    }
    class Loan
    {
        public bool HasNoBadLoans(Customer c)
        {
            System.Console.WriteLine("check loans for " + c.Name);
            return true;
        }
    }
    class Customer
    {
        private string _name;
        public Customer(string name)
        {
            this._name = name;
        }
        public string Name
        {
            get { return _name; }
        }
    }
    class Mortgage
    {
        private Bank _bank = new Bank();
        private Credit _credit = new Credit();
        private Loan _loan = new Loan();
        public bool IsEligible(Customer cust, int amount)
        {
            System.Console.WriteLine("{0} applies for {1:C} loan\n", cust.Name, amount);

            bool eligible = true;

            if (!_bank.HasSufficientSavings(cust, amount))
            {
                eligible = false;
            }
            else if (!_loan.HasNoBadLoans(cust))
            {
                eligible = false;
            }
            else if (!_credit.HasGoodCredit(cust))
            {
                eligible = false;
            }
            return eligible;
        }
    }
}