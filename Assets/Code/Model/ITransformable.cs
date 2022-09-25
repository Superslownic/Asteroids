using UnityEngine;

namespace Code.Model
{
  public interface ITransformable
  {
    IReadOnlyObservable<Vector2> Position { get; }
    IReadOnlyObservable<Quaternion> Rotation { get; }
  }
}