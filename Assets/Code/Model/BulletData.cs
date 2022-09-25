using Code.Infrastructure.Timers;
using UnityEngine;

namespace Code.Model
{
  public class BulletData : ITransformable
  {
    public readonly float Speed;
    public readonly Timer DisableTimer;
    public readonly Observable<Vector2> Position;
    public readonly Observable<Quaternion> Rotation;
    public readonly Observable<Vector2> Direction;

    public BulletData(float speed, Timer disableTimer)
    {
      Speed = speed;
      DisableTimer = disableTimer;
      Position = new Observable<Vector2>();
      Rotation = new Observable<Quaternion>();
      Direction = new Observable<Vector2>();
    }

    IReadOnlyObservable<Vector2> ITransformable.Position => Position;
    IReadOnlyObservable<Quaternion> ITransformable.Rotation => Rotation;
  }
}