using System.Numerics;
using RoboSquad.Core.Shared.Exceptions;

namespace RoboSquad.Core.Shared.Entities.Rovers
{
    public abstract class RoverBase : IRover
    {
        public Guid Id { get; set; }
        public Vector2 CurrentPosition { get; set; }
        public float CurrentBearing { get; set; }
        public CardinalBearingEnum CardinalBearing { get; set; }
        public List<char> MovementInstructions { get; set; }

        protected RoverBase(Vector2 currentPosition, CardinalBearingEnum cardinalBearing, List<char> movementInstructions)
        {
            CurrentPosition = currentPosition;
            CardinalBearing = cardinalBearing;
            CurrentBearing = InitializeCurrentBearing();
            MovementInstructions = movementInstructions;
            Id = Guid.NewGuid();
        }

        public CardinalBearingEnum GetCardinalBearing()
        {
            var numberOfCardinalValues = Enum.GetNames(typeof(CardinalBearingEnum)).Length;
            var bearingFactor = 360 / numberOfCardinalValues;

            return (CardinalBearingEnum)(CurrentBearing / bearingFactor);
        }

        /// <summary>
        /// Instructs rover to perform one action from <see cref="MovementInstructions"/> then proceeds to remove it once performed.
        /// </summary>
        /// <exception cref="InvalidInstructionException"></exception>
        public virtual void InstructOnce()
        {
            char instruction = char.ToUpperInvariant(MovementInstructions.First());

            switch (instruction)
            {
                case 'L' or 'R':
                    Rotate(instruction);
                    break;
                case 'M':
                    Move();
                    break;
                default:
                    throw new InvalidInstructionException($@"Invalid Instruction. Tried to execute instruction containing ""{instruction}"".");
            }

            //Remove instruction once executed
            MovementInstructions.RemoveAt(0);
        }

        /// <summary>
        /// Executes all rover instructions unless told to stop at a certain number of instructions.
        /// </summary>
        public virtual void InstructAll(int stopAt = Int32.MaxValue)
        {
            var instructionsToPerform = MovementInstructions.Count;

            //We check to make sure the instructions we are about to give don't exceed the array bounds primarily
            //when using the stopAt variable. This will be important later when dealing with "collision detection".
            if (stopAt != Int32.MaxValue)
            {
                if (MovementInstructions.Count < stopAt)
                    throw new InvalidInstructionException(
                        "RoverBase::InstructAll stopping at certain number of instructions is more than the total number of Movements originally given.");

                instructionsToPerform = stopAt;
            }

            for (var i = 0; i < instructionsToPerform; i++)
            {
                InstructOnce();
            }
        }

        /// <summary>
        /// Calculates new forward facing bearing (out of 360 degrees of a "circle")
        /// </summary>
        /// <param name="direction"></param>
        public virtual void Rotate(char direction)
        {
            switch (direction)
            {
                case 'L':
                    CurrentBearing = CurrentBearing == 0 ? 270 : CurrentBearing - 90;
                    break;
                case 'R':
                    CurrentBearing = CurrentBearing == 270 ? 0 : CurrentBearing + 90;
                    break;
            }
        }

        public virtual void Move()
        {
            switch (GetCardinalBearing())
            {
                case CardinalBearingEnum.North:
                    CurrentPosition = new Vector2(CurrentPosition.X, CurrentPosition.Y + 1);
                    break;
                case CardinalBearingEnum.East:
                    CurrentPosition = new Vector2(CurrentPosition.X + 1, CurrentPosition.Y);
                    break;
                case CardinalBearingEnum.South:
                    CurrentPosition = new Vector2(CurrentPosition.X, CurrentPosition.Y - 1);
                    break;
                case CardinalBearingEnum.West:
                    CurrentPosition = new Vector2(CurrentPosition.X - 1, CurrentPosition.Y);
                    break;

                default:
                    throw new InvalidInstructionException(
                        "RoverBase::Move intercepted invalid move command for this type of rover.");
            }
        }

        public float InitializeCurrentBearing()
        {
            var numberOfCardinalValues = Enum.GetNames(typeof(CardinalBearingEnum)).Length;
            return 360 / numberOfCardinalValues * (int)CardinalBearing;
        }
    }
}
