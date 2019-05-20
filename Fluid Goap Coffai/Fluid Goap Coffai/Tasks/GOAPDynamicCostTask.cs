using System;
using System.Collections.Generic;
using FluidHTN;
using FluidHTN.Compounds;
using FluidHTN.Conditions;
using FluidHTN.Operators;
using FluidHTN.PrimitiveTasks;

namespace Fluid.Tasks
{
    public abstract class GOAPDynamicCostTask<T> : PrimitiveTask, IGOAPTask
        where T : GOAPDynamicCostTask<T>
    {
        public float Cost(IContext ctx)
        {
            if (ctx is AIContext c)
            {
                return CalculateCost(c);
            }

            throw new Exception("Unexpected context type!");
        }

        protected abstract float CalculateCost(AIContext c);
    }
}