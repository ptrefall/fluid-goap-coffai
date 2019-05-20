using System;
using FluidHTN;
using FluidHTN.Operators;

namespace Fluid.Operators
{
    public class DoActionOperator : IOperator
    {
        public string Description { get; }

        public DoActionOperator(string description)
        {
            Description = description;
        }

        public TaskStatus Update(IContext ctx)
        {
            Console.WriteLine(Description);
            return TaskStatus.Success;
        }

        public void Stop(IContext ctx)
        {
        }
    }
}