using System;
using System.Collections.Generic;
using System.Numerics;
using RoboSquad.Core.Entities.Rovers;
using RoboSquad.Core.Factories;
using RoboSquad.Core.MovementFactoryBuilder;
using RoboSquad.Core.Shared.Builders;
using RoboSquad.Core.Shared.Entities.Rovers;
using RoboSquad.Core.Shared.Exceptions;
using Xunit;

namespace RoboSquad.Tests
{
    [Collection("Movement factory tests")]
    public class MovementFactoryTests
    {
        private readonly IMovementFactoryBuilder _movementFactoryBuilder;
        private readonly List<IRover> _rovers;
        private readonly List<IRover> _doomedRovers;


        public MovementFactoryTests()
        {
            _movementFactoryBuilder = new MovementFactoryBuilder();
            _rovers = GetRovers();
            _doomedRovers = GetDoomedRovers();
        }
        
        /// <summary>
        /// Rovers that are expected to stay within the confines of the plateau
        /// </summary>
        /// <returns></returns>
        private List<IRover> GetRovers()
        {
            return new List<IRover>
            {
                new Mako(new Vector2(0, 0), CardinalBearingEnum.North, new List<char> { 'M', 'L' })
            };
        }

        /// <summary>
        /// Rovers that are expected to fail and doomed to fall off the edge and burn in a fiery display of incompetence.
        /// </summary>
        /// <returns></returns>
        private List<IRover> GetDoomedRovers()
        {
                return new List<IRover>
                {
                    new Mako(new Vector2(0, 0), CardinalBearingEnum.North, new List<char> { 'M', 'L', 'M', 'M' })
                };
            
        }

        [Theory]
        [InlineData(typeof(MovementFactory2D), 5, 5)]
        public void Should_Return_MovementFactory_OfType_With_SpecifiedGrid(Type expectedMovementFactoryType, int gridSizeX, int gridSizeY)
        {
            //Arrange & Act
            var sut = _movementFactoryBuilder.Build(gridSizeX, gridSizeY, _rovers);

            //Assert
            Assert.IsType(expectedMovementFactoryType, sut);
        }

        [Fact]
        public void Should_Throw_Boundary_Collisions_Exception_When_Instructing_rovers()
        {
            //Arrange
            var sut = _movementFactoryBuilder.Build(10, 10, _doomedRovers); //given a really small grid to collide on purpose

            //Act & Assert
            Assert.Throws<BoundaryCollisionException>(() => sut.InstructRovers());

        }


    }
}
