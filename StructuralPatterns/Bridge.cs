using System;
using System.Collections.Generic;

namespace CsDP.Bridge.Structural
{
    class MainApp
    {
        public static void TestCall()
        {
            Abstraction ab = new Abstraction();

            ab.Implementor = new ConcreteImplementA();
            ab.Operation();

            ab.Implementor = new ConcreteImplementB();
            ab.Operation();
        }
    }

    class Abstraction
    {
        protected Implementor implementor;
        public Implementor Implementor
        {
            set { implementor = value; }
        }
        public virtual void Operation()
        {
            implementor.Operation();
        }
    }

    abstract class Implementor
    {
        public abstract void Operation();
    }
    class RefinedAbstraction : Abstraction
    {
        public override void Operation()
        {
            implementor.Operation();
        }
    }
    class ConcreteImplementA : Implementor
    {
        public override void Operation()
        {
            System.Console.WriteLine("ConcreteImplementA Operation");
        }
    }
    class ConcreteImplementB : Implementor
    {
        public override void Operation()
        {
            System.Console.WriteLine("ConcreteImplementB Operation");
        }
    }
}

namespace CsDP.Bridge.RealWorld
{
    class MainApp
    {
        public static void TestCall()
        {
            Customers customers = new Customers("Chicago");

            customers.Data = new CustomersData();

            customers.Show();
            customers.Next();
            customers.Show();
            customers.Next();
            customers.Show();
            customers.Add("Henry Velasquez");
            customers.ShowAll();
        }

        class CustomerBase
        {
            private DataObject _dataObject;
            protected string group;

            public CustomerBase(string group)
            {
                this.group = group;
            }
            public DataObject Data
            {
                set { _dataObject = value; }
                get { return _dataObject; }
            }
            public virtual void Next()
            {
                _dataObject.NextRecord();
            }
            public virtual void Prior()
            {
                _dataObject.PriorRecord();
            }
            public virtual void Add(string customer)
            {
                _dataObject.AddRecord(customer);
            }
            public virtual void Delete(string customer)
            {
                _dataObject.DeleteRecord(customer);
            }
            public virtual void Show()
            {
                _dataObject.ShowRecord();
            }
            public virtual void ShowAll()
            {
                _dataObject.ShowAllRecords();
            }
        }
        class Customers : CustomerBase
        {
            public Customers(string group) : base(group) { }
        }
        abstract class DataObject
        {
            public abstract void NextRecord();
            public abstract void PriorRecord();
            public abstract void AddRecord(string name);
            public abstract void DeleteRecord(string name);
            public abstract void ShowRecord();
            public abstract void ShowAllRecords();
        }
        class CustomersData : DataObject
        {
            private List<string> _customers = new List<string>();
            private int _current = 0;

            public CustomersData()
            {
                _customers.Add("Jim Jones");
                _customers.Add("Samual Jackson");
                _customers.Add("Allen Good");
                _customers.Add("Ann Stills");
                _customers.Add("Lisa Giolani");
            }
            public override void NextRecord()
            {
                if (_current <= _customers.Count - 1)
                    _current++;
            }
            public override void PriorRecord()
            {
                if (_current > 0)
                    _current--;
            }
            public override void AddRecord(string customer)
            {
                _customers.Add(customer);
            }
            public override void DeleteRecord(string customer)
            {
                _customers.Remove(customer);
            }
            public override void ShowRecord()
            {
                System.Console.WriteLine(_customers[_current]);
            }
            public override void ShowAllRecords()
            {
                foreach (var customer in _customers)
                    System.Console.WriteLine(" " + customer);
            }
        }
    }
}