using UnityEngine;

namespace Code.Core.Unit
{
  public class UnitTransform
  {
    public Observable<Vector3> Position { get; } = new Observable<Vector3>();
    public Observable<Quaternion> Rotation { get; } = new Observable<Quaternion>();

    public Vector3 Forward => Rotation.Value * Vector3.up;

    public UnitTransform(Vector3 position, Quaternion rotation)
    {
      Position.Value = position;
      Rotation.Value = rotation;
    }
  }
}