using System.Numerics;

namespace RoboSquad.Core.Shared.Entities.Rovers;

public interface IRover
{
    public Guid Id { get; set; }
    public Vector2 CurrentPosition { get; set; } // x/y coordinate position
    public float CurrentBearing { get; set; } // which way rover is facing forward out of 360 degrees
    public CardinalBearingEnum CardinalBearing { get; set; } // representing a cardinal compass point respective to Bearing
    public List<char> MovementInstructions { get; set; }

    CardinalBearingEnum GetCardinalBearing();
    void InstructOnce();
    void InstructAll(int stopAt = Int32.MaxValue);
    void Rotate(char direction);
    void Move();
    float InitializeCurrentBearing();
}