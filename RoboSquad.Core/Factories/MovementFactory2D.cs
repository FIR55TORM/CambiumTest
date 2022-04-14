using RoboSquad.Core.Shared.Entities.Rovers;
using RoboSquad.Core.Shared.Exceptions;
using RoboSquad.Core.Shared.Factories;

namespace RoboSquad.Core.Factories
{
    public class MovementFactory2D : IMovementFactory
    {
        private IRover[,] _locations { get; } //Assumption: you can't have other rovers occupying the same space.
        private List<IRover> _rovers { get; }
        public int GridSizeX { get; }
        public int GridSizeY { get; }

        public MovementFactory2D(List<IRover> rovers, int gridSizeX, int gridSizeY)
        {
            _rovers = rovers;
            GridSizeX = gridSizeX;
            GridSizeY = gridSizeY;
            _locations = new IRover[gridSizeY, gridSizeX];
        }


        public IRover[,] InstructRovers()
        {
            foreach (var rover in _rovers)
            {
                rover.InstructAll();

                try
                {
                    _locations[(int)Math.Floor(rover.CurrentPosition.Y), (int)Math.Floor(rover.CurrentPosition.X)] = rover;
                }
                catch (IndexOutOfRangeException e)
                {
                    throw new BoundaryCollisionException(
                        $"Rover {nameof(rover)} with Id '{rover.Id}', collided with the edge of the plateau", e);
                }
            }

            return _locations;
        }
    }
}
