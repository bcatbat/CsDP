using System;
using System.Collections.Generic;

namespace CsDP.Command.Structural
{
    class MainApp
    {
        public static void TestCall()
        {
            Receiver receiver = new Receiver();
            Command command = new ConcreteCommand(receiver);
            Invoker invoker = new Invoker();

            invoker.SetCommand(command);
            invoker.ExecuteCommand();
        }
    }

    abstract class Command
    {
        protected Receiver receiver;
        public Command(Receiver receiver)
        {
            this.receiver = receiver;
        }
        public abstract void Execute();
    }
    class ConcreteCommand : Command
    {
        public ConcreteCommand(Receiver receiver) : base(receiver) { }
        public override void Execute()
        {
            receiver.Action();
        }
    }
    class Receiver
    {
        public void Action()
        {
            System.Console.WriteLine("Called Receiver.Action()");
        }
    }
    class Invoker
    {
        private Command _command;
        public void SetCommand(Command command)
        {
            this._command = command;
        }
        public void ExecuteCommand()
        {
            _command.Execute();
        }
    }
}
namespace CsDP.Command.RealWorld
{
    class MainApp
    {
        public static void TestCall()
        {
            User user = new User();

            user.Compute('+', 100);
            user.Compute('-', 50);
            user.Compute('*', 10);
            user.Compute('/', 2);

            user.Undo(4);

            user.Redo(4);

            user.Redo(2);

        }
    }

    abstract class Command
    {
        public abstract void Execute();
        public abstract void UnExecute();
    }
    class CalculatorCommand : Command
    {
        private char _operator;
        private int _operand;
        private Calculator _calculator;
        public CalculatorCommand(Calculator calculator, char @operator, int operand)
        {
            this._calculator = calculator;
            this._operator = @operator;
            this._operand = operand;
        }
        public char Operator { set { _operator = value; } }
        public int Operand { set { _operand = value; } }
        public override void Execute()
        {
            _calculator.Operation(_operator, _operand);
        }
        public override void UnExecute()
        {
            _calculator.Operation(Undo(_operator), _operand);
        }
        private char Undo(char @operator)
        {
            switch (@operator)
            {
                case '+': return '-';
                case '-': return '+';
                case '*': return '/';
                case '/': return '*';
                default: throw new ArgumentException("@operator");
            }
        }
    }
    class Calculator
    {
        private int _curr = 0;

        // 如果变量名与标识符重复了，需要加上@前缀
        public void Operation(char @operator, int operand)
        {
            switch (@operator)
            {
                case '+': _curr += operand; break;
                case '-': _curr -= operand; break;
                case '*': _curr *= operand; break;
                case '/': _curr /= operand; break;
            }
            System.Console.WriteLine("Current value = {0,3} (following {1} {2})", _curr, @operator, operand);
        }
    }

    class User
    {
        private Calculator _calculator = new Calculator();
        private List<Command> _commands = new List<Command>();
        private int _current = 0;

        // 前进
        public void Redo(int levels)
        {
            System.Console.WriteLine("\n---- Redo {0} levels ", levels);
            for (int i = 0; i < levels; i++)
            {
                if (_current < _commands.Count)
                {
                    Command command = _commands[_current++];
                    command.Execute();
                }
            }
        }
        // 后退
        public void Undo(int levels)
        {
            System.Console.WriteLine("\n---- Undo {0} levels ", levels);
            for (int i = 0; i < levels; i++)
            {
                if (_current > 0)
                {
                    Command command = _commands[--_current] as Command;
                    command.UnExecute();
                }
            }
        }
        public void Compute(char @operator, int operand)
        {
            Command command = new CalculatorCommand(_calculator, @operator, operand);
            command.Execute();

            _commands.Add(command);
            _current++;
        }
    }
}