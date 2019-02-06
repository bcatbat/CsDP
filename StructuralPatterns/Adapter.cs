using System;

namespace CsDP.Adapter.Structural
{
    class MainApp
    {
        public static void TestCall()
        {
            Target target = new Adapter();
            target.Request();
        }
    }

    class Target
    {
        public virtual void Request()
        {
            System.Console.WriteLine("called target request()");
        }
    }

    class Adapter : Target
    {
        private Adaptee _adaptee = new Adaptee();
        public override void Request()
        {
            // 可做一些其他的工作，然后调用指定请求。
            System.Console.WriteLine("Pass by Adapter");
            _adaptee.SpecificRequest();
        }
    }

    class Adaptee
    {
        public void SpecificRequest()
        {
            System.Console.WriteLine("called specificRequest()");
        }
    }
}

namespace CsDP.Adapter.RealWorld{
    class MainApp{
        public static void TestCall(){
            Compound unknown = new Compound("Unknown");
            unknown.Display();

            Compound water = new RichCompound("Water");
            water.Display();
            Compound benzene = new RichCompound("Benzene");
            benzene.Display();
            Compound ethanol = new RichCompound("Ethanol");
            ethanol.Display();
        }
    }

// 化合物
    class Compound{
        protected string _chemical;
        protected float _boilingPoint;
        protected float _meltingPoint;
        protected double _molecularWeight;
        protected string _molecularFormula;
        public Compound(string chemical){
            this._chemical = chemical;
        }
        public virtual void Display(){
            System.Console.WriteLine("\nCompound: {0} ------ ",_chemical);
        }
    }
    // 高能化合物
    class RichCompound:Compound{
        private ChemicalDataBank _bank;
        public RichCompound(string name):base(name){}
        public override void Display(){
            _bank = new ChemicalDataBank();

            _boilingPoint = _bank.GetCriticalPoint(_chemical,"B");
            _meltingPoint = _bank.GetCriticalPoint(_chemical,"M");
            _molecularWeight = _bank.GetMelecularWeight(_chemical);
            _molecularFormula = _bank.GetMolecularStructure(_chemical);

            base.Display();
            System.Console.WriteLine(" Formula: {0}",_molecularFormula);
            System.Console.WriteLine(" Weight : {0}",_molecularWeight);
            System.Console.WriteLine(" Melting Pt: {0}",_meltingPoint);
            System.Console.WriteLine(" Boiling Pt: {0}",_boilingPoint);
        }
    }
    class ChemicalDataBank{
        public float GetCriticalPoint(string compound,string point){
            if(point=="M"){
                switch(compound.ToLower()){
                    case "water":return 0.0f;
                    case "benzene":return 5.5f;
                    case "ethanol":return -114.1f;
                    default:return 0f;
                }
            }else{
                switch(compound.ToLower()){
                    case "water":return 100.0f;
                    case "benzene":return 80.1f;
                    case "ethanol":return 78.3f;
                    default:return 0f;
                }
            }
        }
        public string GetMolecularStructure(string compound){
            switch(compound.ToLower()){
                case "water":return "H20";
                case "benzene":return "C6H6";
                case "ethanol":return "C2H5OH";
                default: return "";
            }
        }
        public double GetMelecularWeight(string compound){
            switch(compound.ToLower()){
                case "water":return 18.015;
                case "benzene":return 78.1134;
                case "ethanol":return 46.0688;
                default:return 0d;
            }
        }
    }
}