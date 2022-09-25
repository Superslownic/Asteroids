using Code.Model;
using UnityEngine;

namespace Code.Infrastructure.Extensions
{
  public static class TransformExtensions
  {
    public static Vector2 Forward(this ITransformable transformable)
    {
      return transformable.Rotation.Value * Vector2.up;
    }
  }
}