using UnityEngine;

namespace Code.Common.Extensions
{
  public static class TransformExtensions
  {
    public static Vector2 Forward(this ITransformable transformable)
    {
      return transformable.Rotation.Value * Vector2.up;
    }
  }
}