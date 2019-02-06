using System;
using System.Data;
using System.Data.OleDb;

namespace CsDP.Template.Structural
{
    class MainApp
    {
        public static void TestCall()
        {
            AbstractClass aA = new ConcreteClassA();
            aA.TemplateMethod();

            AbstractClass aB = new ConcreteClassB();
            aB.TemplateMethod();
        }
    }

    abstract class AbstractClass
    {
        public abstract void PrimitiveOperation1();
        public abstract void PrimitiveOperation2();

        // The "Template method"
        public void TemplateMethod()
        {
            PrimitiveOperation1();
            PrimitiveOperation2();
            Console.WriteLine("");
        }
    }

    class ConcreteClassA : AbstractClass
    {
        public override void PrimitiveOperation1()
        {
            Console.WriteLine("ConcreteClassA.PrimitiveOperation1()");
        }
        public override void PrimitiveOperation2()
        {
            Console.WriteLine("ConcreteClassA.PrimitiveOperation2()");
        }
    }
    class ConcreteClassB : AbstractClass
    {
        public override void PrimitiveOperation1()
        {
            Console.WriteLine("ConcreteClassB.PrimitiveOperation1()");
        }
        public override void PrimitiveOperation2()
        {
            Console.WriteLine("ConcreteClassB.PrimitiveOperation2()");
        }
    }
}

namespace CsDP.Template.RealWorld
{
    class MainApp
    {
        public static void TestCall()
        {
            DataAccessObject daoCa = new Categories();
            daoCa.Run();
            DataAccessObject doPr = new Products();
            doPr.Run();
        }
    }

    abstract class DataAccessObject
    {
        protected string connectionString;
        protected DataSet dataSet;

        public virtual void Connect()
        {
            // Make sure mdb is available to app
            connectionString =
              "provider=Microsoft.JET.OLEDB.4.0; " +
              "data source=..\\..\\..\\db1.mdb";
            System.Console.WriteLine("连接数据库...");
        }

        public abstract void Select();
        public abstract void Process();

        public virtual void Disconnect()
        {
            connectionString = "";
        }

        // The 'Template Method' 
        public void Run()
        {
            Connect();
            Select();
            Process();
            Disconnect();
        }
    }

    class Categories : DataAccessObject
    {
        public override void Select()
        {
            // string sql = "select CategoryName from Categories";
            // OleDbDataAdapter dataAdapter = new OleDbDataAdapter(
            //   sql, connectionString);

            // dataSet = new DataSet();
            // dataAdapter.Fill(dataSet, "Categories");
            System.Console.WriteLine("此处略去策略名的查询过程");
        }

        public override void Process()
        {
            Console.WriteLine("Categories ---- ");

            // DataTable dataTable = dataSet.Tables["Categories"];
            // foreach (DataRow row in dataTable.Rows)
            // {
            //     Console.WriteLine(row["CategoryName"]);
            // }

            System.Console.WriteLine("此处略去处理策略名的过程");
            Console.WriteLine();
        }
    }

    class Products : DataAccessObject
    {
        public override void Select()
        {
            // string sql = "select ProductName from Products";
            // OleDbDataAdapter dataAdapter = new OleDbDataAdapter(
            //   sql, connectionString);

            // dataSet = new DataSet();
            // dataAdapter.Fill(dataSet, "Products");
            System.Console.WriteLine("此处略去产品名的查询过程");
        }

        public override void Process()
        {
            // Console.WriteLine("Products ---- ");
            // DataTable dataTable = dataSet.Tables["Products"];
            // foreach (DataRow row in dataTable.Rows)
            // {
            //     Console.WriteLine(row["ProductName"]);
            // }
            System.Console.WriteLine("此处略去产品名的处理过程");
            Console.WriteLine();
        }
    }
}