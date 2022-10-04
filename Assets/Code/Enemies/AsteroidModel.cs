using Code.Common;

namespace Code.Enemies
{
  public class AsteroidModel
  {
    public readonly Transformation Transformation;
    public readonly StraightMovement Movement;
    public readonly PositionWrapper Wrapper;
    public readonly int Type;
    public readonly float Speed;
    public readonly int FragmentCount;
    public readonly int Points;

    public AsteroidModel(Transformation transformation, StraightMovement movement, PositionWrapper wrapper,
      int type, int fragmentCount, float speed, int points)
    {
      Transformation = transformation;
      Movement = movement;
      Type = type;
      Speed = speed;
      FragmentCount = fragmentCount;
      Points = points;
      Wrapper = wrapper;
    }
  }
}