using System;

namespace CsDP.Abstract.Structural
{
    class MainApp
    {
        public static void TestCall()
        {
            AbstractFactory factory1 = new ConcreteFactory1();
            Client client1 = new Client(factory1);
            client1.Run();

            AbstractFactory factory2 = new ConcreteFactory2();
            Client client2 = new Client(factory2);
            client2.Run();

        }
    }

    abstract class AbstractFactory
    {
        public abstract AbstractProductA CreateProductA();
        public abstract AbstractProductB CreateProductB();
    }
    class ConcreteFactory1 : AbstractFactory
    {
        public override AbstractProductA CreateProductA()
        {
            return new ProductA1();
        }

        public override AbstractProductB CreateProductB()
        {
            return new ProductB1();
        }
    }
    class ConcreteFactory2 : AbstractFactory
    {
        public override AbstractProductA CreateProductA()
        {
            return new ProductA2();
        }
        public override AbstractProductB CreateProductB()
        {
            return new ProductB2();
        }
    }

    abstract class AbstractProductA { }
    abstract class AbstractProductB
    {
        public abstract void Interact(AbstractProductA a);
    }
    class ProductA1 : AbstractProductA { }
    class ProductA2 : AbstractProductA { }
    class ProductB1 : AbstractProductB
    {
        public override void Interact(AbstractProductA a)
        {
            System.Console.WriteLine(this.GetType().Name + " interacts with " + a.GetType().Name);
        }
    }
    class ProductB2 : AbstractProductB
    {
        public override void Interact(AbstractProductA a)
        {
            System.Console.WriteLine(this.GetType().Name + " interacts with " + a.GetType().Name);
        }
    }
    // 产品的交互环境
    class Client
    {
        private AbstractProductA _abstractProductA;
        private AbstractProductB _abstractProductB;
        public Client(AbstractFactory factory)
        {
            _abstractProductB = factory.CreateProductB();
            _abstractProductA = factory.CreateProductA();
        }
        public void Run()
        {
            _abstractProductB.Interact(_abstractProductA);
        }
    }
}

namespace CsDP.Abstract.RealWorld
{
    class MainApp
    {
        public static void TestCall() {
            ContinentFactory africa = new AfricaFactory();
            AnimalWorld world = new AnimalWorld(africa);
            world.RunFoodChain();

            ContinentFactory america = new AmericaFactory();
            world = new AnimalWorld(america);
            world.RunFoodChain();
         }
    }

    abstract class ContinentFactory
    {
        public abstract Herbivore CreateHerbivore();
        public abstract Carnivore CreateCarnivore();
    }
    class AfricaFactory : ContinentFactory
    {
        public override Carnivore CreateCarnivore()
        {
            return new Lion();
        }

        public override Herbivore CreateHerbivore()
        {
            return new Wildebeest();
        }
    }
    class AmericaFactory : ContinentFactory
    {
        public override Carnivore CreateCarnivore()
        {
            return new Wolf();
        }

        public override Herbivore CreateHerbivore()
        {
            return new Bison();
        }
    }

    abstract class Herbivore{ }
    abstract class Carnivore
    {
        public abstract void Eat(Herbivore h);
    }
    class Wildebeest : Herbivore{ }
    class Lion : Carnivore
    {
        public override void Eat(Herbivore h)
        {
            System.Console.WriteLine(this.GetType().Name + " eats " + h.GetType().Name);
        }
    }
    class Bison : Herbivore{ }
    class Wolf : Carnivore
    {
        public override void Eat(Herbivore h)
        {
            System.Console.WriteLine(this.GetType().Name + " eats " + h.GetType().Name);
        }
    }
    class AnimalWorld
    {
        private Herbivore _herbivore;
        private Carnivore _carnivore;
        public AnimalWorld(ContinentFactory factory)
        {
            _herbivore = factory.CreateHerbivore();
            _carnivore = factory.CreateCarnivore();
        }
        public void RunFoodChain()
        {
            _carnivore.Eat(_herbivore);
        }
    }
}