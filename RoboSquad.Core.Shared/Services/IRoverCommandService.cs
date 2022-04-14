using Microsoft.AspNetCore.Http;
using RoboSquad.Core.Shared.Dtos;
using RoboSquad.Core.Shared.Entities.Rovers;

namespace RoboSquad.Core.Shared.Services
{
    public interface IRoverCommandService
    {
        Task<IRover[,]> ReadCommands(IFormFile file, GridRequestDetailsDto requestDto);
    }
}
