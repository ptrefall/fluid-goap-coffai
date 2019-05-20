using System.Collections.Generic;
using FluidHTN;

namespace Fluid
{
    public class AIDomain
    {
        public static Domain<AIContext> Create()
        {
            var goalState = new KeyValuePair<byte, byte>[]
            {
                AIContext.CreateEnergyGoal(EnergyLevel.SuperCharged)
            };

            return new AIDomainBuilder("Magic World")
                .GOAPSequence("Git SUPERCHARGED!!!", goalState)
                    .DrinkCoffee(1)
                    .FillCup(1)
                    .GetCup(1)
                .End()
                .Build();
        }
    }
}