using RoboSquad.Core.Shared.Entities.Rovers;
using RoboSquad.Core.Shared.Factories;

namespace RoboSquad.Core.Shared.Builders;

public interface IMovementFactoryBuilder
{
    IMovementFactory Build(int gridSizeX, int gridSizeY, List<IRover> rovers);
} 