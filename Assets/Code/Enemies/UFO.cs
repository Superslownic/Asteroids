using System;
using Code.Common;
using Code.Infrastructure.MonoEventProviders;
using UnityEngine;

namespace Code.Enemies
{
  public class UFO : IUpdateListener, IContactHandler
  {
    private readonly UFOModel _model;
    private readonly UFOView _view;

    public UFO(UFOModel model, UFOView view)
    {
      _model = model;
      _view = view;
    }

    public event Action<UFO> OnDestroy;
    public int Points => _model.Points;

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
      _model.Transformation.Position.Value = value;
    }

    public void Update(float deltaTime)
    {
      _model.Movement.Direction = (_model.Target.Position.Value - _model.Transformation.Position.Value).normalized;
      _model.Movement.Execute(deltaTime);
      _model.Wrapper.Execute();
      _model.Transformation.Rotation.Value = Quaternion.LookRotation(Vector3.forward, _model.Movement.Direction);
    }
    
    public void OnHit()
    {
      OnDestroy?.Invoke(this);
    }
  }
}