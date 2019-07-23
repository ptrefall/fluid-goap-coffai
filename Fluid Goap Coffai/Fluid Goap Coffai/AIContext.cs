using System;
using System.Collections.Generic;
using System.Runtime.Remoting.Messaging;
using FluidHTN;
using FluidHTN.Contexts;
using FluidHTN.Debug;
using FluidHTN.Factory;

namespace Fluid
{
    public enum AIWorldState
    {
        Energy,
        HasCup,
        CupHasCoffee
    }

    public enum EnergyLevel
    {
        Low,
        Medium,
        High,
        SuperCharged,
    }

    public class AIContext : BaseContext
    {
        private readonly byte[] _worldState = new byte[Enum.GetValues(typeof(AIWorldState)).Length];

        public override IFactory Factory { get; set; } = new DefaultFactory();
        public override List<string> MTRDebug { get; set; }
        public override List<string> LastMTRDebug { get; set; }
        public override bool DebugMTR { get; } = false;
        public override Queue<IBaseDecompositionLogEntry> DecompositionLog { get; set; }
        public override bool LogDecomposition { get; } = true;
        public override byte[] WorldState => _worldState;

        public Player Player { get; }

        public AIContext(Player player)
        {
            Player = player;
        }

        public override void Init()
        {
            base.Init();

            // Custom init of state
        }

        public bool HasState(AIWorldState state, bool value)
        {
            return HasState((int)state, (byte)(value ? 1 : 0));
        }

        public bool HasState(AIWorldState state, byte value)
        {
            return HasState((int)state, value);
        }

        public bool HasState(AIWorldState state)
        {
            return HasState((int)state, 1);
        }

        public void SetState(AIWorldState state, byte value, EffectType type)
        {
            SetState((int)state, value, true, type);
        }

        public void SetState(AIWorldState state, bool value, EffectType type)
        {
            SetState(state, (byte)(value ? 1 : 0), type);
        }

        public void SetState(AIWorldState state, EnergyLevel value, EffectType type)
        {
            SetState(state, (byte)value, type);
        }

        public byte GetState(AIWorldState state)
        {
            return GetState((int)state);
        }

        public static KeyValuePair<byte, byte> CreateGoal(AIWorldState state, byte value)
        {
            return new KeyValuePair<byte, byte>((byte) state, value);
        }

        public static KeyValuePair<byte, byte> CreateEnergyGoal(EnergyLevel energyLevel)
        {
            return CreateGoal(AIWorldState.Energy, (byte) energyLevel);
        }
    }
}