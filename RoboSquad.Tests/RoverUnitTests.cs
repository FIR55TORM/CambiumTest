using System.Linq;
using System.Numerics;
using RoboSquad.Core.Entities.Rovers;
using RoboSquad.Core.Shared.Entities.Rovers;
using Xunit;

namespace RoboSquad.Tests
{
    [Collection("Rover Unit Tests")]
    public class RoverUnitTests
    {
        [Theory]
        [InlineData(CardinalBearingEnum.North, CardinalBearingEnum.East, new[] { 'R' })]
        [InlineData(CardinalBearingEnum.North, CardinalBearingEnum.North, new[] { 'R', 'L' })]
        [InlineData(CardinalBearingEnum.East, CardinalBearingEnum.West, new[] { 'R', 'R' })]
        public void When_MakoRoverRotated_Expects_CorrectCardinalPoint(
            CardinalBearingEnum cardinalBearingStart, 
            CardinalBearingEnum cardinalBearingExpected, 
            char[] rotations)
        {
            //Arrange
            var sut = new Mako(new Vector2(0, 0), cardinalBearingStart, new[] { 'L', 'R', 'M' }.ToList());

            //Act
            foreach (char rotation in rotations)
            {
                sut.Rotate(rotation);
            }

            //Assert
            Assert.Equal(cardinalBearingExpected, sut.GetCardinalBearing());
        }

        [Theory]
        [InlineData(CardinalBearingEnum.North, 0, 0, new[] { 'M' }, 0, 1)]
        [InlineData(CardinalBearingEnum.East, 0, 0, new[] { 'M' }, 1, 0)]
        [InlineData(CardinalBearingEnum.South, 0, 0, new[] { 'M' }, 0, -1)]
        [InlineData(CardinalBearingEnum.West, 0, 0, new[] { 'M' }, -1, 0)]
        [InlineData(CardinalBearingEnum.East, 2, 3, new[] { 'M' }, 3, 3)]
        public void When_MakoRoverMoves_Expects_CorrectVector(
            CardinalBearingEnum cardinalBearingStart,
            float startingX,
            float startingY,
            char[] movements,
            float expectedX, 
            float expectedY)
        {
            //Arrange
            var sut = new Mako(new Vector2(startingX, startingY), cardinalBearingStart, movements.ToList());

            //Act
            sut.InstructOnce();

            //Assert
            Assert.Equal(new Vector2(expectedX, expectedY), sut.CurrentPosition);
        }

        [Theory]
        [InlineData(CardinalBearingEnum.North, 0, 0, new[] { 'M', 'R', 'M', 'M' }, 2, 1)]
        [InlineData(CardinalBearingEnum.North, 0, 0, new[] { 'M', 'R', 'M', 'R', 'M', 'R', 'M' }, 0, 0)]
        [InlineData(CardinalBearingEnum.North, 0, 0, new[] { 'M', 'L', 'M', 'L', 'M', 'L', 'M' }, 0, 0)]
        public void When_MakoRoverPerformsAllInstructions_Expect_CorrectVector(
            CardinalBearingEnum cardinalBearingStart,
            float startingX,
            float startingY,
            char[] movements,
            float expectedX,
            float expectedY
        )
        {
            //Arrange
            var sut = new Mako(new Vector2(startingX, startingY), cardinalBearingStart, movements.ToList());

            //Act
            sut.InstructAll();

            //Assert
            Assert.Equal(new Vector2(expectedX, expectedY), sut.CurrentPosition);
        }
    }
}
