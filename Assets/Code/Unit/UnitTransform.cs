using Code.Helpers;
using UnityEngine;

namespace Code.Unit
{
  public class UnitTransform
  {
    public Observable<Vector2> Position { get; } = new Observable<Vector2>();
    public Observable<Quaternion> Rotation { get; } = new Observable<Quaternion>();

    public Vector2 Forward => Rotation.Value * Vector2.up;

    public UnitTransform(Vector2 position, Quaternion rotation)
    {
      Position.Value = position;
      Rotation.Value = rotation;
    }
  }
}