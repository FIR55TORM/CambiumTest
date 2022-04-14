using RoboSquad.Core.Shared.Entities.Rovers;

namespace RoboSquad.Core.Shared.Factories;

//Ideally, the movement factory would consider multi-dimensional grids. Room for improvement here...
public interface IMovementFactory
{
    IRover[,] InstructRovers();
}