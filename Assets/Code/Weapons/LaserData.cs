using Code.Common;
using Code.Infrastructure.Timers;
using UnityEngine;

namespace Code.Weapons
{
  public class LaserData : ITransformable
  {
    public readonly Timer DestroyTimer;
    public readonly Observable<Vector2> Position;
    public readonly Observable<Quaternion> Rotation;

    public LaserData(Timer destroyTimer)
    {
      DestroyTimer = destroyTimer;
      Position = new Observable<Vector2>();
      Rotation = new Observable<Quaternion>(Quaternion.identity);
    }

    IReadOnlyObservable<Vector2> ITransformable.Position => Position;
    IReadOnlyObservable<Quaternion> ITransformable.Rotation => Rotation;
  }
}