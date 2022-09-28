using System;
using Code.Common;
using Code.Infrastructure.MonoEventProviders;
using UnityEngine;

namespace Code.Enemies
{
  public class UFO : IUpdateListener, IContactHandler
  {
    private readonly UFOData _data;
    private readonly UFOView _view;
    private readonly ScreenLimits _screenLimits;

    public UFO(UFOData data, UFOView view, ScreenLimits screenLimits)
    {
      _data = data;
      _view = view;
      _screenLimits = screenLimits;
    }

    public event Action<UFO> OnDestroy;

    public void Enable()
    {
      _view.Activator.Enable();
    }
    
    public void Disable()
    {
      _view.Activator.Disable();
    }
    
    public void SetPosition(Vector2 value)
    {
      _data.Position.Value = value;
    }

    public void Update(float deltaTime)
    {
      Vector2 direction = (_data.Target.Position.Value - _data.Position.Value).normalized;
      _data.Position.Value += _data.Speed * deltaTime * direction;
      _data.Position.Value = _screenLimits.Wrap(_data.Position.Value);
      _data.Rotation.Value = Quaternion.LookRotation(Vector3.forward, direction);
    }
    
    public void OnHit()
    {
      OnDestroy?.Invoke(this);
    }
  }
}