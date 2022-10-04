using UnityEngine;

namespace Code.Common
{
  public class Transformation
  {
    public Observable<Vector2> Position { get; }
    public Observable<Quaternion> Rotation { get; }

    public Transformation(Vector2 initialPosition, Quaternion initialRotation)
    {
      Position = new Observable<Vector2>(initialPosition);
      Rotation = new Observable<Quaternion>(initialRotation);
    }

    public Vector2 Forward => Rotation.Value * Vector2.up;
  }
}