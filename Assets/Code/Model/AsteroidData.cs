using UnityEngine;

namespace Code.Model
{
  public class AsteroidData : ITransformable
  {
    public readonly int Type;
    public readonly float Speed;
    public readonly int FragmentCount;
    public readonly Observable<Vector2> Position;
    public readonly Observable<Quaternion> Rotation;
    public readonly Observable<Vector2> Direction;

    public AsteroidData(int type, int fragmentCount, float speed)
    {
      Type = type;
      Speed = speed;
      FragmentCount = fragmentCount;
      Position = new Observable<Vector2>();
      Rotation = new Observable<Quaternion>();
      Direction = new Observable<Vector2>();
    }

    IReadOnlyObservable<Vector2> ITransformable.Position => Position;
    IReadOnlyObservable<Quaternion> ITransformable.Rotation => Rotation;
  }
}