using System;
using FluidHTN;

namespace Fluid.Effects
{
    public class SetEnergyEffect : IEffect
    {
        public string Name { get; }
        public EffectType Type { get; }
        public EnergyLevel EnergyLevel { get; }

        public SetEnergyEffect(EnergyLevel energyLevel, EffectType type)
        {
            Name = $"SetEnergy({energyLevel})";
            Type = type;
            EnergyLevel = energyLevel;
        }

        public void Apply(IContext ctx)
        {
            if (ctx is AIContext c)
            {
                c.SetState(AIWorldState.Energy, EnergyLevel, Type);
                if(c.ContextState == ContextState.Executing) Console.WriteLine($"Your energy is {ToVerbose(EnergyLevel)}");
                return;
            }

            throw new Exception("Unexpected context type!");
        }

        private string ToVerbose(EnergyLevel energyLevel)
        {
            switch (energyLevel)
            {
                case EnergyLevel.Low:
                default:
                    return "low";
                case EnergyLevel.Medium:
                    return "medium";
                case EnergyLevel.High:
                    return "high";
                case EnergyLevel.SuperCharged:
                    return "SUPER CHAAAAARGED!!!";
            }
        }
    }
}