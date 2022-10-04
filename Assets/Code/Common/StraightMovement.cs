using UnityEngine;

namespace Code.Common
{
  public class StraightMovement
  {
    private readonly Transformation _transformation;
    
    public StraightMovement(Transformation transformation)
    {
      _transformation = transformation;
    }

    public Vector2 Direction { get; set; }
    public float Speed { get; set; }

    public void Execute(float deltaTime)
    {
      _transformation.Position.Value += Speed * deltaTime * Direction;
    }
  }
}