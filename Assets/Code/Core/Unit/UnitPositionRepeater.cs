using Code.Core.MonoEventProviders;
using UnityEngine;

namespace Code.Core.Unit
{
  public class UnitPositionRepeater : IUpdateListener
  {
    private readonly UnitTransform _transform;
    private readonly FloatRange _horizontal;
    private readonly FloatRange _vertical;

    public UnitPositionRepeater(UnitTransform transform, FloatRange horizontal, FloatRange vertical)
    {
      _horizontal = horizontal;
      _vertical = vertical;
      _transform = transform;
    }

    public void Update(float deltaTime)
    {
      Vector3 position = _transform.Position.Value;
      
      if (position.x < _horizontal.Min) position.x = _horizontal.Max;
      if (position.x > _horizontal.Max) position.x = _horizontal.Min;
      
      if (position.y < _vertical.Min) position.y = _vertical.Max;
      if (position.y > _vertical.Max) position.y = _vertical.Min;
      
      _transform.Position.Value = position;
    }
  }
}