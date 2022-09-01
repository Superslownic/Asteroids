using Code.Helpers;
using Code.MonoEventProviders;
using UnityEngine;

namespace Code.Unit
{
  public class UnitPositionRepeater : IUpdateListener
  {
    private readonly UnitTransform _transform;
    private readonly ScreenLimits _screenLimits;

    public UnitPositionRepeater(UnitTransform transform, ScreenLimits screenLimits)
    {
      _transform = transform;
      _screenLimits = screenLimits;
    }

    public void Update(float deltaTime)
    {
      Vector3 position = _transform.Position.Value;
      
      if (position.x < _screenLimits.Horizontal.Min) position.x = _screenLimits.Horizontal.Max;
      if (position.x > _screenLimits.Horizontal.Max) position.x = _screenLimits.Horizontal.Min;
      
      if (position.y < _screenLimits.Vertical.Min) position.y = _screenLimits.Vertical.Max;
      if (position.y > _screenLimits.Vertical.Max) position.y = _screenLimits.Vertical.Min;
      
      _transform.Position.Value = position;
    }
  }
}