using UnityEngine;

namespace Code.Model
{
  public class ShipData : ITransformable
  {
    public readonly float Acceleration;
    public readonly float Deceleration;
    public readonly float MaxSpeed;
    public readonly float RotationSpeed;
    public readonly Observable<Vector2> Position;
    public readonly Observable<Quaternion> Rotation;
    public readonly Observable<Vector2> Velocity;

    public ShipData(float acceleration, float deceleration, float maxSpeed, float rotationSpeed)
    {
      Acceleration = acceleration;
      Deceleration = deceleration;
      MaxSpeed = maxSpeed;
      RotationSpeed = rotationSpeed;
      Position = new Observable<Vector2>();
      Rotation = new Observable<Quaternion>(Quaternion.identity);
      Velocity = new Observable<Vector2>();
    }

    IReadOnlyObservable<Vector2> ITransformable.Position => Position;
    IReadOnlyObservable<Quaternion> ITransformable.Rotation => Rotation;
  }
}
