using System;
using System.Collections.Generic;
using Fluid.Conditions;
using Fluid.Effects;
using Fluid.Operators;
using Fluid.Tasks;
using FluidHTN;
using FluidHTN.Compounds;
using FluidHTN.Factory;
using FluidHTN.PrimitiveTasks;

namespace Fluid
{
    public class AIDomainBuilder : BaseDomainBuilder<AIDomainBuilder, AIContext>
    {
        public AIDomainBuilder(string domainName) : base(domainName, new DefaultFactory())
        {
        }

        public AIDomainBuilder HasState(AIWorldState state)
        {
            var condition = new HasWorldStateCondition(state);
            condition.Depth = Pointer.Depth + 1;
            Pointer.AddCondition(condition);
            return this;
        }

        public AIDomainBuilder HasNotState(AIWorldState state)
        {
            var condition = new HasWorldStateCondition(state, 0);
            condition.Depth = Pointer.Depth + 1;
            Pointer.AddCondition(condition);
            return this;
        }

        public AIDomainBuilder HasState(AIWorldState state, byte value)
        {
            var condition = new HasWorldStateCondition(state, value);
            condition.Depth = Pointer.Depth + 1;
            Pointer.AddCondition(condition);
            return this;
        }

        public AIDomainBuilder HasEnergy(EnergyLevel value)
        {
            return HasState(AIWorldState.Energy, (byte) value);
        }

        public AIDomainBuilder SetState(AIWorldState state, EffectType type)
        {
            if (Pointer is IPrimitiveTask task)
            {
                var effect = new SetWorldStateEffect(state, type);
                effect.Depth = task.Depth + 1;
                task.AddEffect(effect);
            }
            return this;
        }

        public AIDomainBuilder SetState(AIWorldState state, bool value, EffectType type)
        {
            if (Pointer is IPrimitiveTask task)
            {
                var effect = new SetWorldStateEffect(state, value, type);
                effect.Depth = task.Depth + 1;
                task.AddEffect(effect);
            }
            return this;
        }

        public AIDomainBuilder SetState(AIWorldState state, byte value, EffectType type)
        {
            if (Pointer is IPrimitiveTask task)
            {
                var effect = new SetWorldStateEffect(state, value, type);
                effect.Depth = task.Depth + 1;
                task.AddEffect(effect);
            }
            return this;
        }

        public AIDomainBuilder SetEnergy(EnergyLevel energyLevel, EffectType type)
        {
            if (Pointer is IPrimitiveTask task)
            {
                var effect = new SetEnergyEffect(energyLevel, type);
                effect.Depth = task.Depth + 1;
                task.AddEffect(effect);
            }

            return this;
        }

        public AIDomainBuilder GOAPSequence(string name, params KeyValuePair<byte, byte>[] goal)
        {
            return this.GOAPSequence<AIDomainBuilder, AIContext>(name, goal);
        }

        /// <summary>
        /// Use this with dynamic cost calculations, where we calculate cost from the context data at runtime.
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public AIDomainBuilder GOAPAction<T>(string name)
            where T : GOAPDynamicCostTask<T>, new()
        {
            if (Pointer is GOAPSequence)
            {
                return this.GOAPAction<AIDomainBuilder, AIContext, T>(name);
            }

            throw new Exception("Pointer is not a GOAP Sequence task type. Did you forget an End() after a Primitive Task Action was defined?");
        }

        /// <summary>
        /// Use this with static cost goals, where we set a static cost at compile time.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="cost"></param>
        /// <returns></returns>
        public AIDomainBuilder GOAPAction(string name, float cost)
        {
            if (Pointer is GOAPSequence goapSequence)
            {
                var parent = new GOAPStaticCostTask { Name = name, StaticCost = cost };
                parent.Depth = goapSequence.Depth + 1;
                _domain.Add(goapSequence, parent);
                _pointers.Add(parent);
                return this;
            }

            throw new Exception("Pointer is not a GOAP Sequence task type. Did you forget an End() after a Primitive Task Action was defined?");
        }

        public AIDomainBuilder Do(string description)
        {
            if (Pointer is IPrimitiveTask task)
            {
                var op = new DoActionOperator(description);
                task.SetOperator(op);
            }

            return this;
        }

        public AIDomainBuilder FillCup(float cost)
        {
            GOAPAction("Fill cup", cost);
                HasEnergy(EnergyLevel.Low);
                HasNotState(AIWorldState.CupHasCoffee);
                HasNotState(AIWorldState.HasCup);
                Do("You fill the cup with coffee");
                SetState(AIWorldState.CupHasCoffee, EffectType.PlanAndExecute);
            End();
            return this;
        }

        public AIDomainBuilder GetCup(float cost)
        {
            GOAPAction("Get cup", cost);
                HasEnergy(EnergyLevel.Low);
                HasState(AIWorldState.CupHasCoffee);
                HasNotState(AIWorldState.HasCup);
                Do("You pick up the cup");
                SetState(AIWorldState.HasCup, EffectType.PlanAndExecute);
            End();
            return this;
        }

        public AIDomainBuilder DrinkCoffee(float cost)
        {
            GOAPAction("Drink coffee", cost);
                HasEnergy(EnergyLevel.Low);
                HasState(AIWorldState.HasCup);
                HasState(AIWorldState.CupHasCoffee);
                Do("You drink the coffee");
                SetEnergy(EnergyLevel.SuperCharged, EffectType.PlanAndExecute);
            End();
            return this;
        }
    }
}