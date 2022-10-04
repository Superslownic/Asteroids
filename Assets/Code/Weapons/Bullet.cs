using System;
using Code.Common;
using Code.Infrastructure.MonoEventProviders;
using UnityEngine;

namespace Code.Weapons
{
  public class Bullet : IUpdateListener
  {
    private readonly BulletModel _model;
    private readonly BulletView _view;

    public Bullet(BulletModel model, BulletView view)
    {
      _model = model;
      _view = view;
    }

    public event Action<Bullet> OnDestroy;

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
    
    public void SetRotation(Quaternion value)
    {
      _model.Transformation.Rotation.Value = value;
    }
    
    public void SetMovementDirection(Vector2 value)
    {
      _model.Movement.Direction = value;
    }
    
    public void StartDestroyTimer(float cooldown)
    {
      _model.DisableTimer.Run(cooldown, Destroy);
    }

    public void Update(float deltaTime)
    {
      _model.Movement.Execute(deltaTime);
      
      if (_model.ContactTrigger.HasContact())
        Destroy();
    }

    private void Destroy()
    {
      OnDestroy?.Invoke(this);
    }
  }
}