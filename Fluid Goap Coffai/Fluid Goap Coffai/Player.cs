using System;
using FluidHTN;

namespace Fluid
{
    public class Player
    {
        private Planner<AIContext> _planner;
        private AIContext _context;
        private Domain<AIContext> _domain;

        public AIContext Context => _context;

        public Player()
        {
            _planner = new Planner<AIContext>();
            _context = new AIContext(this);
            _context.Init();

            _domain = AIDomain.Create();
        }

        public void Tick()
        {
            while (!_context.HasState(AIWorldState.Energy, (byte) EnergyLevel.SuperCharged))
            {
                _planner.Tick(_domain, _context);

                if (_context.LogDecomposition)
                {
                    Console.WriteLine("---------------------- DECOMP LOG --------------------------");
                    while (_context.DecompositionLog?.Count > 0)
                    {
                        var entry = _context.DecompositionLog.Dequeue();
                        var depth = FluidHTN.Debug.Debug.DepthToString(entry.Depth);
                        Console.ForegroundColor = entry.Color;
                        Console.WriteLine($"{depth}{entry.Name}: {entry.Description}");
                    }
                    Console.ResetColor();
                    Console.WriteLine("-------------------------------------------------------------");
                }
            }
        }
    }
}