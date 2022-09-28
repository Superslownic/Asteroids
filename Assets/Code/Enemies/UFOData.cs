using Code.Common;
using UnityEngine;

namespace Code.Enemies
{
  public class UFOData : ITransformable
  {
    public readonly float Speed;
    public readonly ITransformable Target;
    public readonly Observable<Vector2> Position;
    public readonly Observable<Quaternion> Rotation;

    public UFOData(float speed, ITransformable target)
    {
      Speed = speed;
      Target = target;
      Position = new Observable<Vector2>();
      Rotation = new Observable<Quaternion>(Quaternion.identity);
    }

    IReadOnlyObservable<Vector2> ITransformable.Position => Position;
    IReadOnlyObservable<Quaternion> ITransformable.Rotation => Rotation;
  }
}