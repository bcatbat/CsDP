using System;
using System.Collections.Generic;

namespace CsDP.Composite.Structural
{
    class MainApp
    {
        public static void TestCall()
        {
            Composite root = new Composite("root");
            root.Add(new Leaf("leaf A"));
            root.Add(new Leaf("leaf B"));

            Composite comp = new Composite("composite X");
            comp.Add(new Leaf("leaf XA"));
            comp.Add(new Leaf("leaf XB"));

            root.Add(comp);
            root.Add(new Leaf("leaf C"));

            Leaf leaf = new Leaf("leaf D");
            root.Add(leaf);
            root.Remove(leaf);

            root.Display(1);
        }
    }

    abstract class Component
    {
        protected string name;
        public Component(string name)
        {
            this.name = name;
        }
        public abstract void Add(Component c);
        public abstract void Remove(Component c);
        public abstract void Display(int depth);
    }

    class Composite : Component
    {
        private List<Component> _children = new List<Component>();
        public Composite(string name) : base(name) { }
        public override void Add(Component c)
        {
            _children.Add(c);
        }
        public override void Remove(Component c)
        {
            _children.Remove(c);
        }
        public override void Display(int depth)
        {
            System.Console.WriteLine(new string('-', depth) + name);
            foreach (var c in _children)
            {
                c.Display(depth + 2);
            }
        }
    }
    class Leaf : Component
    {
        public Leaf(string name) : base(name) { }
        public override void Add(Component c)
        {
            System.Console.WriteLine("cannot add to a leaf");
        }

        public override void Display(int depth)
        {
            System.Console.WriteLine(new string('-', depth) + name);
        }

        public override void Remove(Component c)
        {
            System.Console.WriteLine("cannot remove from a leaf");
        }
    }
}

namespace CsDP.Composite.RealWorld
{
    class MainApp
    {
        public static void TestCall()
        {
            CompositeElement root = new CompositeElement("Pciture");
            root.Add(new PrimitiveElement("Red Line"));
            root.Add(new PrimitiveElement("Blue Circle"));
            root.Add(new PrimitiveElement("Green Box"));

            CompositeElement comp = new CompositeElement("Two Circles");
            comp.Add(new PrimitiveElement("Black Circle"));
            comp.Add(new PrimitiveElement("White Circle"));
            root.Add(comp);

            PrimitiveElement pe = new PrimitiveElement("Yellow Line");
            root.Add(pe);
            root.Remove(pe);

            root.Display(1);
        }
    }

    abstract class DrawingElement
    {
        protected string _name;
        public DrawingElement(string name)
        {
            this._name = name;
        }
        public abstract void Add(DrawingElement d);
        public abstract void Remove(DrawingElement d);
        public abstract void Display(int indent);
    }
    class PrimitiveElement : DrawingElement
    {
        public PrimitiveElement(string name) : base(name) { }
        public override void Add(DrawingElement d)
        {
            System.Console.WriteLine("cannot add to a Primitive");
        }

        public override void Display(int indent)
        {
            System.Console.WriteLine(new string('-', indent) + " " + _name);
        }

        public override void Remove(DrawingElement d)
        {
            System.Console.WriteLine("cannot remove from a PrimitiveElement");
        }
    }
    class CompositeElement : DrawingElement
    {
        private List<DrawingElement> elements = new List<DrawingElement>();
        public CompositeElement(string name) : base(name) { }

        public override void Add(DrawingElement d)
        {
            elements.Add(d);
        }

        public override void Display(int indent)
        {
            System.Console.WriteLine(new string('-', indent) + "+ " + _name);
            foreach (DrawingElement d in elements)
            {
                d.Display(indent + 2);
            }
        }

        public override void Remove(DrawingElement d)
        {
            elements.Remove(d);
        }
    }
}