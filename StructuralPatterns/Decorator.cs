using System;
using System.Collections.Generic;

namespace CsDP.Decorator.Structural
{
    class MainApp
    {
        public static void TestCall()
        {
            ConcreteComponent c = new ConcreteComponent();
            ConcreteDecoratorA d1 = new ConcreteDecoratorA();
            ConcreteDecoratorB d2 = new ConcreteDecoratorB();

            d1.SetComponent(c);
            d2.SetComponent(d1);

            d2.Operation();
        }
    }

    abstract class Component
    {
        public abstract void Operation();
    }
    class ConcreteComponent : Component
    {
        public override void Operation()
        {
            System.Console.WriteLine("ConcreteComponent.Opeartion()");
        }
    }
    abstract class Decorator : Component
    {
        protected Component component;
        public void SetComponent(Component component)
        {
            this.component = component;
        }
        public override void Operation()
        {
            if (component != null)
            {
                component.Operation();
            }
        }
    }

    class ConcreteDecoratorA : Decorator
    {
        public override void Operation()
        {
            base.Operation();
            System.Console.WriteLine("ConcreteDecoratorA.Operation()");
        }
    }
    class ConcreteDecoratorB : Decorator
    {
        public override void Operation()
        {
            base.Operation();
            AddedBehaivour();
            System.Console.WriteLine("ConcreteDecoratorB.Operation()");
        }
        void AddedBehaivour()
        {
            System.Console.WriteLine("--This is added behaviour--");
        }
    }
}

namespace CsDP.Decorator.RealWorld
{
    class MainApp
    {
        public static void TestCall()
        {
            Book book = new Book("Worley", "Inside ASP.NET", 10);
            book.Display();

            Video video = new Video("Spielberg", "Jaws", 23, 92);
            video.Display();

            System.Console.WriteLine("\nMaking video borrowable:");

            Borrowable bv = new Borrowable(video);
            bv.BorrowItem("Customer #1");
            bv.BorrowItem("Customer #2");

            bv.Display();
        }
    }

    abstract class LibraryItem
    {
        private int _numCopies;
        public int NumCopies
        {
            get { return _numCopies; }
            set { _numCopies = value; }
        }
        public abstract void Display();
    }
    class Book : LibraryItem
    {
        private string _author;
        private string _title;
        public Book(string author, string title, int numCopies)
        {
            this._author = author;
            this._title = title;
            this.NumCopies = numCopies;
        }
        public override void Display()
        {
            System.Console.WriteLine("\nBook ------ ");
            System.Console.WriteLine(" Author: {0}", _author);
            System.Console.WriteLine(" Title: {0}", _title);
            System.Console.WriteLine(" # Copies: {0}", NumCopies);
        }
    }

    class Video : LibraryItem
    {
        private string _director;
        private string _title;
        private int _playTime;
        public Video(string director, string title, int numCopies, int playTime)
        {
            this._director = director;
            this._title = title;
            this.NumCopies = numCopies;
            this._playTime = playTime;

        }

        public override void Display()
        {
            Console.WriteLine("\nVideo ----- ");
            Console.WriteLine(" Director: {0}", _director);
            Console.WriteLine(" Title: {0}", _title);
            Console.WriteLine(" # Copies: {0}", NumCopies);
            Console.WriteLine(" Playtime: {0}\n", _playTime);
        }
    }

    abstract class Decorator : LibraryItem
    {
        protected LibraryItem libraryItem;
        public Decorator(LibraryItem item)
        {
            this.libraryItem = item;
        }
        public override void Display()
        {
            libraryItem.Display();
        }
    }

    class Borrowable : Decorator
    {
        protected List<string> borrowers = new List<string>();

        public Borrowable(LibraryItem item) : base(item) { }

        public void BorrowItem(string name)
        {
            borrowers.Add(name);
            libraryItem.NumCopies--;
        }
        public void ReturnItem(string name)
        {
            borrowers.Remove(name);
            libraryItem.NumCopies++;
        }

        public override void Display()
        {
            base.Display();

            foreach (var borrower in borrowers)
            {
                System.Console.WriteLine(" borrower: " + borrower);
            }
        }
    }
}