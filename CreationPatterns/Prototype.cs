using System;
using System.Collections.Generic;

namespace CsDP.Prototype.Structural
{
    class MainApp
    {
        public static void TestCall()
        {
            ConcretePrototype1 p1 = new ConcretePrototype1("I");
            ConcretePrototype1 c1 = (ConcretePrototype1)p1.Clone();
            System.Console.WriteLine("cloned: {0}", c1.Id);

            ConcretePrototype2 p2 = new ConcretePrototype2("II");
            ConcretePrototype2 c2 = (ConcretePrototype2)p2.Clone();
            System.Console.WriteLine("cloned: {0}", c2.Id);
        }
    }

    abstract class Prototype
    {
        private string _id;
        public Prototype(string id)
        {
            this._id = id;
        }
        public string Id
        {
            get { return _id; }
        }
        public abstract Prototype Clone();
    }
    class ConcretePrototype1 : Prototype
    {
        public ConcretePrototype1(string id) : base(id) { }
        public override Prototype Clone()
        {
            return (Prototype)this.MemberwiseClone();
        }
    }
    class ConcretePrototype2 : Prototype
    {
        public ConcretePrototype2(string id) : base(id) { }

        public override Prototype Clone()
        {
            return (Prototype)this.MemberwiseClone();
        }
    }
}

namespace CsDP.Prototype.RealWorld
{
    class MainApp
    {
        public static void TestCall()
        {
            ColorManager colorManager = new ColorManager();

            // Initialize with standard colors
            colorManager["red"] = new Color(255, 0, 0);
            colorManager["green"] = new Color(0, 255, 0);
            colorManager["blue"] = new Color(0, 0, 255);

            // User adds personalized colors
            colorManager["angry"] = new Color(255, 54, 0);
            colorManager["peace"] = new Color(128, 211, 128);
            colorManager["flame"] = new Color(211, 34, 20);

            Color color1 = colorManager["red"].Clone() as Color;
            Color color2 = colorManager["peace"].Clone() as Color;
            Color color3 = colorManager["flame"].Clone() as Color;
        }
    }

    abstract class ColorProtytype
    {
        public abstract ColorProtytype Clone();
    }
    class Color : ColorProtytype
    {
        private int _red;
        private int _green;
        private int _blue;

        public Color(int red, int green, int blue)
        {
            this._red = red;
            this._green = green;
            this._blue = blue;
        }
        public override ColorProtytype Clone()
        {
            System.Console.WriteLine("Cloning color RGB: {0,3},{1,3},{2,3}", _red, _green, _blue);
            return this.MemberwiseClone() as ColorProtytype;
        }
    }
    class ColorManager
    {
        private Dictionary<string, ColorProtytype> _colors = new Dictionary<string, ColorProtytype>();
        public ColorProtytype this[string key]
        {
            get { return _colors[key]; }
            set { _colors.Add(key, value); }
        }
    }
}