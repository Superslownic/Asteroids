using System;
using Code.Infrastructure.MonoEventProviders;
using Code.Logic.Common;
using Code.Model;
using Code.View;
using UnityEngine;

namespace Code.Logic.Weapons
{
  public class Bullet : IUpdateListener
  {
    private readonly BulletData _data;
    private readonly BulletView _view;
    private readonly ContactTrigger _contactTrigger;

    public Bullet(BulletData data, BulletView view, ContactTrigger contactTrigger)
    {
      _data = data;
      _view = view;
      _contactTrigger = contactTrigger;
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
      _data.Position.Value = value;
    }
    
    public void SetRotation(Quaternion value)
    {
      _data.Rotation.Value = value;
    }
    
    public void SetMovementDirection(Vector2 value)
    {
      _data.Direction.Value = value;
    }
    
    public void StartDestroyTimer(float cooldown)
    {
      _data.DisableTimer.Run(cooldown, Destroy);
    }

    public void Update(float deltaTime)
    {
      _data.Position.Value += _data.Speed * deltaTime * _data.Direction.Value;
      
      if (_contactTrigger.HasContact())
        Destroy();
    }

    private void Destroy()
    {
      OnDestroy?.Invoke(this);
    }
  }
}