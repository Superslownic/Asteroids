using UnityEngine;

namespace Code.Common
{
  public interface ITransformable
  {
    IReadOnlyObservable<Vector2> Position { get; }
    IReadOnlyObservable<Quaternion> Rotation { get; }
  }
}