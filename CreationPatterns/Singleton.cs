using System;
using System.Collections.Generic;

namespace CsDP.Singleton.Structural
{
    class MainApp
    {
        // 入口点程序
        public static void TestCall()
        {
            Singleton s1 = Singleton.Instance();
            Singleton s2 = Singleton.Instance();
            // 
            if (s1 == s2)
                System.Console.WriteLine("Objects are the same instance");
        }
    }

    class Singleton
    {
        private static Singleton _instance;
        protected Singleton() { }
        public static Singleton Instance()
        {
            // 使用惰性初始化
            // 注意：非线程安全
            if (_instance == null)
                _instance = new Singleton();
            return _instance;
        }
    }
}

namespace CsDP.Singleton.RealWorld
{
    class MainApp
    {
        public static void TestCall()
        {
            LoadBalancer b1 = LoadBalancer.GetLoadBalancer();
            LoadBalancer b2 = LoadBalancer.GetLoadBalancer();
            LoadBalancer b3 = LoadBalancer.GetLoadBalancer();
            LoadBalancer b4 = LoadBalancer.GetLoadBalancer();

            // same?
            if (b1 == b2 && b2 == b3 && b4 == b3)
            {
                System.Console.WriteLine("same instance");
            }

            LoadBalancer balancer = LoadBalancer.GetLoadBalancer();
            for (int i = 0; i < 15; i++)
            {
                string server = balancer.Server;
                System.Console.WriteLine("dispatch request to: " + server);
            }
        }
    }

    class LoadBalancer
    {
        private static LoadBalancer _instance;
        private List<string> _servers = new List<string>();
        private Random _random = new Random();
        private static object syncLock = new object();
        // 注：protected
        protected LoadBalancer()
        {
            _servers.Add("Server1");
            _servers.Add("Server2");
            _servers.Add("Server3");
            _servers.Add("Server4");
            _servers.Add("Server5");
        }
        public static LoadBalancer GetLoadBalancer()
        {
            // 支持多线程应用，通过【双重检测锁】模式，
            // 避免每一方法调用时的锁定
            if (_instance == null)
            {
                lock (syncLock)
                {
                    if (_instance == null)
                    {
                        _instance = new LoadBalancer();
                    }
                }
            }
            return _instance;
        }
        // 简单随机调用balancer
        public string Server
        {
            get
            {
                int r = _random.Next(_servers.Count);
                return _servers[r].ToString();
            }
        }
    }
}

namespace CsDP.Singleton.NETOptimized
{
    class MainApp
    {
        public static void TestCall()
        {
            LoadBalancer b1 = LoadBalancer.GetLoadBalancer();
            LoadBalancer b2 = LoadBalancer.GetLoadBalancer();
            LoadBalancer b3 = LoadBalancer.GetLoadBalancer();
            LoadBalancer b4 = LoadBalancer.GetLoadBalancer();

            if (b1 == b2 && b2 == b3 && b3 == b4)
            {
                System.Console.WriteLine("same instance\n");
            }

            LoadBalancer balancer = LoadBalancer.GetLoadBalancer();

            for (int i = 0; i < 15; i++)
            {
                string serverName = balancer.NextServer.Name;
                System.Console.WriteLine("dispatch request to: " + serverName);
            }
        }
    }

    sealed class LoadBalancer
    {
        // 静态成员“迫切”要求初始化，
        // 类第一次载入的时候会直接进行
        // .NET会保证静态初始化的线程安全
        private static readonly LoadBalancer _instance = new LoadBalancer();
        // 类型安全的泛型list
        private List<Server> _servers;
        private Random _random = new Random();
        //  注意：private
        private LoadBalancer()
        {
            _servers = new List<Server>
            {
                new Server{ Name = "ServerI", IP = "120.14.220.18" },
                new Server{ Name = "ServerII", IP = "120.14.220.19" },
                new Server{ Name = "ServerIII", IP = "120.14.220.20" },
                new Server{ Name = "ServerIV", IP = "120.14.220.21" },
                new Server{ Name = "ServerV", IP = "120.14.220.22" },
            };
        }
        public static LoadBalancer GetLoadBalancer()
        {
            return _instance;
        }
        public Server NextServer
        {
            get
            {
                int r = _random.Next(_servers.Count);
                return _servers[r];
            }
        }
    }
    class Server
    {
        public string Name { get; set; }
        public string IP { get; set; }
    }
}