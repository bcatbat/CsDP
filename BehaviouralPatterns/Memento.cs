using System;

namespace CsDP.Memento.Structural
{
    class MainApp
    {
        public static void TestCall()
        {
            Originator o = new Originator();
            o.State = "On";

            Caretaker c = new Caretaker();
            c.Memento = o.CreateMemento();

            o.State = "Off";
            o.SetMemento(c.Memento);

        }
    }

    class Originator
    {
        private string _state;
        public string State
        {
            get { return _state; }
            set
            {
                _state = value;
                System.Console.WriteLine("State = " + _state);
            }
        }
        public Memento CreateMemento()
        {
            return (new Memento(_state));
        }
        public void SetMemento(Memento memento)
        {
            System.Console.WriteLine("Restoring state...");
            State = memento.State;
        }
    }
    class Memento
    {
        private string _state;
        public Memento(string state)
        {
            this._state = state;
        }
        public string State
        {
            get { return _state; }
        }
    }
    class Caretaker
    {
        private Memento _memento;
        public Memento Memento
        {
            get { return _memento; }
            set { _memento = value; }
        }
    }
}

namespace CsDP.Memento.RealWorld
{
    class MainApp
    {
        public static void TestCall()
        {
            SalesProspect s = new SalesProspect()
            {
                Name = "Noel van Halen",
                Phone = "(412) 256-0990",
                Budget = 25000.0
            };

            ProspectMemory m = new ProspectMemory();
            m.Memento = s.SaveMemento();

            s.Name = "Leo Welch";
            s.Phone = "(310) 209-7111";
            s.Budget = 1000000.0;

            s.RestoreMemento(m.Memento);
        }
    }

    class SalesProspect
    {
        private string _name;
        private string _phone;
        private double _budget;

        // Gets or sets name
        public string Name
        {
            get { return _name; }
            set
            {
                _name = value;
                Console.WriteLine("Name:  " + _name);
            }
        }

        // Gets or sets phone
        public string Phone
        {
            get { return _phone; }
            set
            {
                _phone = value;
                Console.WriteLine("Phone: " + _phone);
            }
        }

        // Gets or sets budget
        public double Budget
        {
            get { return _budget; }
            set
            {
                _budget = value;
                Console.WriteLine("Budget: " + _budget);
            }
        }

        // Stores memento
        public Memento SaveMemento()
        {
            Console.WriteLine("\nSaving state --\n");
            return new Memento(_name, _phone, _budget);
        }

        // Restores memento
        public void RestoreMemento(Memento memento)
        {
            Console.WriteLine("\nRestoring state --\n");
            this.Name = memento.Name;
            this.Phone = memento.Phone;
            this.Budget = memento.Budget;
        }
    }
    class Memento
    {
        private string _name;
        private string _phone;
        private double _budget;

        // Constructor
        public Memento(string name, string phone, double budget)
        {
            this._name = name;
            this._phone = phone;
            this._budget = budget;
        }

        // Gets or sets name
        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        // Gets or set phone
        public string Phone
        {
            get { return _phone; }
            set { _phone = value; }
        }

        // Gets or sets budget
        public double Budget
        {
            get { return _budget; }
            set { _budget = value; }
        }
    }
    class ProspectMemory
    {
        private Memento _memento;

        // Property
        public Memento Memento
        {
            set { _memento = value; }
            get { return _memento; }
        }
    }
}