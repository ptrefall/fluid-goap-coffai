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
            }
        }
    }
}