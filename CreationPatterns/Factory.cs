using System;
using System.Collections.Generic;

namespace CsDP.Factory.Structural
{
    class MainApp
    {
        public static void TestCall()
        {
            Creator[] creators = new Creator[2];
            creators[0] = new ConcreteCreatorA();
            creators[1] = new ConcreteCreatorB();

            foreach (var creator in creators)
            {
                Product product = creator.FactoryMethod();
                System.Console.WriteLine("Created {0}", product.GetType().Name);
            }
        }
    }

    abstract class Product { }
    class ConcreteProductA : Product { }
    class ConcreteProductB : Product { }

    abstract class Creator
    {
        public abstract Product FactoryMethod();
    }
    class ConcreteCreatorA : Creator
    {
        public override Product FactoryMethod()
        {
            return new ConcreteProductA();
        }
    }
    class ConcreteCreatorB : Creator
    {
        public override Product FactoryMethod()
        {
            return new ConcreteProductB();
        }
    }
}

namespace CsDP.Factory.RealWorld
{
    class MainApp
    {
        public static void TestCall()
        {
            Document[] documents = new Document[2];
            documents[0] = new Resume();
            documents[1] = new Report();

            foreach (var doc in documents)
            {
                System.Console.WriteLine("\n" + doc.GetType().Name + "--");
                foreach (var page in doc.Pages)
                {
                    System.Console.WriteLine(" " + page.GetType().Name);
                }
            }
        }
    }

    abstract class Page { }
    class SkillsPage : Page { }
    class EducationPage : Page { }
    class ExperiencePage : Page { }
    class IntroductionPage : Page { }
    class ResultsPage : Page { }
    class ConclusionPage : Page { }
    class SummaryPage : Page { }
    class BibliographyPage : Page { }
    abstract class Document
    {
        private List<Page> _pages = new List<Page>();
        public Document()
        {
            this.CreatePages();
        }
        public List<Page> Pages { get { return _pages; } }
        public abstract void CreatePages();
    }
    class Resume : Document
    {
        public override void CreatePages()
        {
            Pages.Add(new SkillsPage());
            Pages.Add(new EducationPage());
            Pages.Add(new ExperiencePage());
        }
    }
    class Report : Document
    {
        public override void CreatePages()
        {
            Pages.Add(new IntroductionPage());
            Pages.Add(new ResultsPage());
            Pages.Add(new ConclusionPage());
            Pages.Add(new SummaryPage());
            Pages.Add(new BibliographyPage());
        }
    }
}