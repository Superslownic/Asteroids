using Code.Common;
using UnityEngine;

namespace Code.Enemies
{
  public class UFOModel
  {
    public readonly Transformation Transformation;
    public readonly StraightMovement Movement;
    public readonly PositionWrapper Wrapper;
    public readonly Transformation Target;
    public readonly int Points;

    public UFOModel(Transformation transformation, StraightMovement movement, PositionWrapper wrapper,
      Transformation target, int points)
    {
      Transformation = transformation;
      Movement = movement;
      Target = target;
      Points = points;
      Wrapper = wrapper;
    }
  }
}