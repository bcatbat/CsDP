using System;
using System.Collections.Generic;

namespace CsDP.Mediator.Structural
{
    class MainApp
    {
        public static void TestCall()
        {
            ConreteMediator m = new ConreteMediator();

            ConcreteColleague1 c1 = new ConcreteColleague1(m);
            ConcreteColleague2 c2 = new ConcreteColleague2(m);

            m.Colleague1 = c1;
            m.Colleague2 = c2;

            c1.Send("How are you?");
            c2.Send("Fine, thanks");
        }
    }

    abstract class Mediator
    {
        public abstract void Send(string message, Colleague colleague);
    }
    class ConreteMediator : Mediator
    {
        private ConcreteColleague1 _colleague1;
        private ConcreteColleague2 _colleague2;
        public ConcreteColleague1 Colleague1
        {
            set { _colleague1 = value; }
        }
        public ConcreteColleague2 Colleague2
        {
            set { _colleague2 = value; }
        }
        public override void Send(string message, Colleague colleague)
        {
            if (colleague == _colleague1)
            {
                _colleague2.Notify(message);
            }
            else
            {
                _colleague1.Notify(message);
            }
        }
    }
    abstract class Colleague
    {
        protected Mediator mediator;
        public Colleague(Mediator mediator)
        {
            this.mediator = mediator;
        }
    }
    class ConcreteColleague1 : Colleague
    {
        public ConcreteColleague1(Mediator mediator) : base(mediator) { }
        public void Send(string message)
        {
            mediator.Send(message, this);
        }
        public void Notify(string message)
        {
            System.Console.WriteLine("Colleague1 gets message: " + message);
        }
    }
    class ConcreteColleague2 : Colleague
    {
        public ConcreteColleague2(Mediator mediator) : base(mediator) { }
        public void Send(string message)
        {
            mediator.Send(message, this);
        }
        public void Notify(string message)
        {
            System.Console.WriteLine("Colleague2 gets message: " + message);
        }
    }
}

namespace CsDP.Mediator.RealWorld
{
    class MainApp
    {
        public static void TestCall()
        {
            Chatroom chatroom = new Chatroom();

            Participant George = new Beatle("George");
            Participant Paul = new Beatle("Paul");
            Participant Ringo = new Beatle("Ringo");
            Participant John = new Beatle("John");
            Participant Yoko = new NonBeatle("Yoko");

            chatroom.Register(George);
            chatroom.Register(Paul);
            chatroom.Register(John);
            chatroom.Register(Yoko);
            chatroom.Register(Ringo);

            Yoko.Send("John", "Hi John!");
            Paul.Send("Ringo", "All you need is love");
            Ringo.Send("George", "My sweet Lord");
            Paul.Send("John", "Can't buy me love");
            John.Send("Yoko", "My sweet love");
        }
    }

    abstract class AbstactChatroom
    {
        public abstract void Register(Participant participant);
        public abstract void Send(string from, string to, string message);
    }
    class Chatroom : AbstactChatroom
    {
        private Dictionary<string, Participant> _participants = new Dictionary<string, Participant>();

        public override void Register(Participant participant)
        {
            if (!_participants.ContainsValue(participant))
            {
                _participants[participant.Name] = participant;
            }
            participant.Chatroom = this;
        }
        public override void Send(string from, string to, string message)
        {
            Participant participant = _participants[to];
            if (participant != null)
            {
                participant.Receive(from, message);
            }
        }
    }
    class Participant
    {
        private Chatroom _chatroom;
        private string _name;
        public Participant(string name)
        {
            this._name = name;
        }
        public string Name
        {
            get { return _name; }
        }
        public Chatroom Chatroom
        {
            set { _chatroom = value; }
            get { return _chatroom; }
        }
        public void Send(string to, string message)
        {
            _chatroom.Send(_name, to, message);
        }
        public virtual void Receive(string from, string message)
        {
            System.Console.WriteLine("{0} to {1}: '{2}'", from, Name, message);
        }
    }
    class Beatle : Participant
    {
        public Beatle(string name) : base(name) { }
        public override void Receive(string from, string message)
        {
            System.Console.Write("To a Beatle: ");
            base.Receive(from, message);
        }
    }
    class NonBeatle : Participant
    {
        public NonBeatle(string name) : base(name) { }
        public override void Receive(string from, string message)
        {
            System.Console.Write("To a non-Beatle: ");
            base.Receive(from, message);
        }
    }
}