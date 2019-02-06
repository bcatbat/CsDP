using System;
using System.Collections.Generic;

namespace CsDP.Observer.Structural
{
    class MainApp
    {
        public static void TestCall()
        {
            ConcreteSubject s = new ConcreteSubject();

            s.Attach(new ConcreteObserver(s, "X"));
            s.Attach(new ConcreteObserver(s, "Y"));
            s.Attach(new ConcreteObserver(s, "Z"));

            s.SubjectState = "ABC";
            s.Notify();
            s.SubjectState = "XYZ";
            s.Notify();
        }
    }
    abstract class Subject
    {
        private List<Observer> _observers = new List<Observer>();
        public void Attach(Observer observer)
        {
            _observers.Add(observer);
        }
        public void Detach(Observer observer)
        {
            _observers.Remove(observer);
        }
        public void Notify()
        {
            foreach (Observer o in _observers)
            {
                o.Update();
            }
        }
    }
    class ConcreteSubject : Subject
    {
        private string _subjectState;
        public string SubjectState
        {
            get { return _subjectState; }
            set { _subjectState = value; }
        }
    }
    abstract class Observer
    {
        public abstract void Update();
    }
    class ConcreteObserver : Observer
    {
        private string _name;
        private string _observerState;
        private ConcreteSubject _subject;
        public ConcreteObserver(ConcreteSubject subject, string name)
        {
            this._subject = subject;
            this._name = name;
        }
        public override void Update()
        {
            _observerState = _subject.SubjectState;
            System.Console.WriteLine("Observer {0}'s new state is {1}", _name, _observerState);
        }
        public ConcreteSubject Subject
        {
            get { return _subject; }
            set { _subject = value; }
        }
    }

}

namespace CsDP.Observer.RealWorld
{
    class MainApp
    {
        public static void TestCall()
        {
            IBM ibm = new IBM("IBM", 120.00);
            ibm.Attach(new Investor("Sorros"));
            ibm.Attach(new Investor("Berkshire"));

            ibm.Price = 120.10;
            ibm.Price = 121.00;
            ibm.Price = 120.50;
            ibm.Price = 120.75;
        }
    }

    abstract class Stock
    {
        private string _symbol;
        private double _price;
        private List<IInvestor> _investors = new List<IInvestor>();
        public Stock(string symbol, double price)
        {
            this._symbol = symbol;
            this._price = price;
        }
        public void Attach(IInvestor investor)
        {
            _investors.Add(investor);
        }
        public void Detach(IInvestor investor)
        {
            _investors.Remove(investor);
        }
        public void Notify()
        {
            foreach (IInvestor investor in _investors)
            {
                investor.Update(this);
            }
            System.Console.WriteLine();
        }
        public double Price
        {
            get { return _price; }
            set
            {
                if (_price != value)
                {
                    _price = value;
                    Notify();
                }
            }
        }
        public string Symbol
        {
            get { return _symbol; }
        }
    }
    class IBM : Stock
    {
        public IBM(string symbol, double price) : base(symbol, price) { }
    }
    interface IInvestor
    {
        void Update(Stock stock);
    }
    class Investor : IInvestor
    {
        private string _name;
        private Stock _stock;
        public Investor(string name)
        {
            this._name = name;
        }
        public void Update(Stock stock)
        {
            System.Console.WriteLine("Notified {0} of {1}'s " +
            "change to {2:C}", _name, stock.Symbol, stock.Price);
        }

        public Stock Stock
        {
            get { return _stock; }
            set { _stock = value; }
        }
    }
}