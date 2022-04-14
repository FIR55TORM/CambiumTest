using System.Numerics;
using RoboSquad.Core.Shared.Entities.Rovers;

namespace RoboSquad.Core.Entities.Rovers
{
    public class Mako : RoverBase
    {
        public Mako(Vector2 currentPosition, CardinalBearingEnum cardinalBearing, List<char> movementInstructions) : base(currentPosition, cardinalBearing, movementInstructions)
        {
        }
    }
}
