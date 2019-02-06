using System;
using System.Collections;

namespace CsDP.Iterator.Structual
{
    class MainApp
    {
        public static void TestCall()
        {
            ConcreteAggregate a = new ConcreteAggregate();
            a[0] = "item a";
            a[1] = "item b";
            a[2] = "item c";
            a[3] = "item d";

            Iterator i = a.CreateIterator();

            System.Console.WriteLine("iterating over collection:");
            object item = i.First();
            while (item != null)
            {
                System.Console.WriteLine(item);
                item = i.Next();
            }
        }
    }

    abstract class Aggregate
    {
        public abstract Iterator CreateIterator();
    }
    class ConcreteAggregate : Aggregate
    {
        private ArrayList _items = new ArrayList();
        public override Iterator CreateIterator()
        {
            return new ConcreteIterator(this);
        }
        public int Count
        {
            get { return _items.Count; }
        }
        public object this[int index]
        {
            get { return _items[index]; }
            set { _items.Insert(index, value); }
        }
    }

    abstract class Iterator
    {
        public abstract object First();
        public abstract object Next();
        public abstract bool IsDone();
        public abstract object CurrentItem();
    }
    class ConcreteIterator : Iterator
    {
        private ConcreteAggregate _aggregate;
        private int _current = 0;

        public ConcreteIterator(ConcreteAggregate aggregate)
        {
            this._aggregate = aggregate;
        }
        public override object First()
        {
            return _aggregate[0];
        }
        public override object Next()
        {
            object ret = null;
            if (_current < _aggregate.Count - 1)
            {
                ret = _aggregate[++_current];
            }
            return ret;
        }
        public override object CurrentItem()
        {
            return _aggregate[_current];
        }
        public override bool IsDone()
        {
            return _current >= _aggregate.Count;
        }
    }
}
namespace CsDP.Iterator.RealWorld
{
    class MainApp
    {
        public static void TestCall()
        {
            Collection collection = new Collection();
            collection[0] = new Item("item 0");
            collection[1] = new Item("item 1");
            collection[2] = new Item("item 2");
            collection[3] = new Item("item 3");
            collection[4] = new Item("item 4");
            collection[5] = new Item("item 5");
            collection[6] = new Item("item 6");
            collection[7] = new Item("item 7");
            collection[8] = new Item("item 8");

            Iterator iterator = collection.CreateIterator();
            iterator.Step = 2;
            System.Console.WriteLine("Iterating over collection:");
            for (var item = iterator.First();
            !iterator.IsDone; item = iterator.Next())
            {
                System.Console.WriteLine(item.Name);
            }
        }
    }
    class Item
    {
        private string _name;
        public Item(string name)
        {
            this._name = name;
        }
        public string Name
        {
            get { return _name; }
        }
    }
    interface IAbstractCollection
    {
        Iterator CreateIterator();
    }
    class Collection : IAbstractCollection
    {
        private ArrayList _items = new ArrayList();
        public Iterator CreateIterator()
        {
            return new Iterator(this);
        }
        public int Count
        {
            get { return _items.Count; }
        }
        public object this[int index]
        {
            get { return _items[index]; }
            set { _items.Add(value); }
        }
    }

    interface IAbstractIterator
    {
        Item First();
        Item Next();
        Item CurrentItem { get; }
        bool IsDone { get; }
    }
    class Iterator : IAbstractIterator
    {
        private Collection _collection;
        private int _current = 0;
        private int _step = 1;
        public Iterator(Collection collection)
        {
            this._collection = collection;
        }
        public Item First()
        {
            _current = 0;
            return _collection[_current] as Item;
        }
        public Item Next()
        {
            _current += _step;
            if (!IsDone)
                return _collection[_current] as Item;
            else
                return null;
        }
        public int Step
        {
            get { return _step; }
            set { _step = value; }
        }
        public Item CurrentItem
        {
            get { return _collection[_current] as Item; }
        }
        public bool IsDone
        {
            get { return _current >= _collection.Count; }
        }
    }
}