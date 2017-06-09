using System;
using System.Collections.Generic;
using System.Dynamic;
using Esevier;

namespace csharp
{
    class Program
    {
        static void Main(string[] args)
        {
            dynamic g = new ExpandoObject();
            dynamic i = new ExpandoObject();
            g.param = 0;
            i.param = 1;

            var exec = new TacoWorkflow();
            exec.AddCommand(new First());
            exec.AddCommand(new Second());
            exec.AddCommand(new Third());

            dynamic res = exec.Execute(i);
            Console.WriteLine("result is: {0}", res.param);
        }
    }
}

namespace Esevier
{
    public interface ITacoCommand
    {
        dynamic Exec(dynamic global, dynamic input);
    }

    public class First : ITacoCommand
    {
        public dynamic Exec(dynamic global, dynamic input)
        {
            dynamic res = new ExpandoObject();
            
            res.a="A";
            res.param = global.param + input.param;
            return res;
        }
    }

     public class Second : ITacoCommand
    {
        public dynamic Exec(dynamic global, dynamic input)
        {
            dynamic res = new ExpandoObject();
                        
            res.param = input.param + " A ";
            return res;
        }
    }

     public class Third : ITacoCommand
    {
        public dynamic Exec(dynamic global, dynamic input)
        {
            dynamic res = new ExpandoObject();
                        
            res.param = input.param + " ==123 ";
            return res;
        }
    }

    public class TacoWorkflow
    {
        private List<ITacoCommand> commands = new List<ITacoCommand>();
        private dynamic global = new ExpandoObject();
        public void AddCommand(ITacoCommand cmd)
        {
            commands.Add(cmd);
        }

        public dynamic Execute(dynamic startParameter)
        {
            global.param = 1;

            dynamic param = startParameter;
            foreach(var cmd in commands)
            {
                param = cmd.Exec(global, param);
            }

            param.success = true;
            return param;
        }
    }
}
