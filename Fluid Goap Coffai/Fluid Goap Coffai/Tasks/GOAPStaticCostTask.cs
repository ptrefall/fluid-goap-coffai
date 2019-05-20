using System;
using System.Collections.Generic;
using FluidHTN;
using FluidHTN.Compounds;
using FluidHTN.Conditions;
using FluidHTN.Operators;
using FluidHTN.PrimitiveTasks;

namespace Fluid.Tasks
{
    public class GOAPStaticCostTask : PrimitiveTask, IGOAPTask
    {
        public float StaticCost { get; set; }

        public float Cost(IContext ctx)
        {
            if (ctx is AIContext c)
            {
                return StaticCost;
            }

            throw new Exception("Unexpected context type!");
        }
    }
}