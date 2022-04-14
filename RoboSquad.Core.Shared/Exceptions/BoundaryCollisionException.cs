namespace RoboSquad.Core.Shared.Exceptions
{
    public class BoundaryCollisionException : Exception
    {
        public BoundaryCollisionException(string? message) : base(message)
        {
        }

        public BoundaryCollisionException(string? message, Exception? innerException) : base(message, innerException)
        {
        }
    }
}
