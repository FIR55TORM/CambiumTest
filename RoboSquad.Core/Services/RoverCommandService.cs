using System.Numerics;
using Microsoft.AspNetCore.Http;
using RoboSquad.Core.Entities.Rovers;
using RoboSquad.Core.Shared.Builders;
using RoboSquad.Core.Shared.Dtos;
using RoboSquad.Core.Shared.Entities.Rovers;
using RoboSquad.Core.Shared.Extensions;
using RoboSquad.Core.Shared.Services;

namespace RoboSquad.Core.Services
{
    enum CoordinateArrayPositions
    {
        X = 0,
        Y = 1,
        Bearing = 2
    }

    public class RoverCommandService : IRoverCommandService
    {
        private readonly IMovementFactoryBuilder _movementFactoryBuilder;

        public RoverCommandService(IMovementFactoryBuilder movementFactoryBuilder)
        {
            _movementFactoryBuilder = movementFactoryBuilder;
        }

        public async Task<IRover[,]> ReadCommands(IFormFile file, GridRequestDetailsDto requestDto)
        {
            List<string> commands = new List<string>();

            using var reader = new StreamReader(file.OpenReadStream());
            while (!reader.EndOfStream)
            {
                var response = await reader.ReadLineAsync();

                if (!string.IsNullOrWhiteSpace(response) && response.Contains('|'))
                    commands.Add(response.RemoveAllWhitespace()); // remove all white space for consistency when executing commands
            }

            var makoRovers = new List<IRover>();

            foreach (var command in commands)
            {
                var coordsAndMovements = command.Split('|');

                var coords = coordsAndMovements[0].ToCharArray();//first part is position
                var movementCommands = coordsAndMovements[1].ToCharArray();

                CardinalBearingEnum cardinalBearing = GetCardinalBearingEnum(coords);
                
                int x = int.Parse(coords[(int)CoordinateArrayPositions.X].ToString());
                int y = int.Parse(coords[((int)CoordinateArrayPositions.Y)].ToString());

                makoRovers.Add(new Mako(new Vector2(x, y),
                    cardinalBearing, movementCommands.ToList()));
            }

            var factory = _movementFactoryBuilder.Build(requestDto.X, requestDto.Y, makoRovers);

            return factory.InstructRovers();
        }

        private static CardinalBearingEnum GetCardinalBearingEnum(char[] coords)
        {
            switch (coords[(int)CoordinateArrayPositions.Bearing].ToString().ToUpperInvariant())
            {
                case "E":
                    return CardinalBearingEnum.East;
                case "S":
                    return CardinalBearingEnum.South;
                case "W":
                    return CardinalBearingEnum.West;

                default: //"N"
                    return CardinalBearingEnum.North;

            }
        }
    }
}
