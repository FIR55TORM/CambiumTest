using RoboSquad.Core.Factories;
using RoboSquad.Core.Shared.Builders;
using RoboSquad.Core.Shared.Entities.Rovers;
using RoboSquad.Core.Shared.Factories;

namespace RoboSquad.Core.MovementFactoryBuilder
{
    public class MovementFactoryBuilder : IMovementFactoryBuilder
    {
        public IMovementFactory Build(int gridSizeX, int gridSizeY, List<IRover> rovers) //Ideally this would be more like a strategy builder but in the interest of time...
        {
            return new MovementFactory2D(rovers, gridSizeX, gridSizeY);
        }
    }
}
