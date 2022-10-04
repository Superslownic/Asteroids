using System;
using Code.Infrastructure.MonoEventProviders;
using Code.Weapons;
using UnityEngine;

namespace Code.Player
{
  public class Ship : IUpdateListener
  {
    private readonly ShipModel _model;
    private readonly ShipView _view;
    
    public Ship(ShipModel model, ShipView view)
    {
      _model = model;
      _view = view;
    }

    public event Action OnDestroy;

    public IWeapon SecondaryWeapon => _model.SecondaryWeapon;

    public void Enable()
    {
      _model.Updater.AddListener(this);
      _view.Activator.Enable();
    }
    
    public void Disable()
    {
      _model.Updater.RemoveListener(this);
      _view.Activator.Disable();
    }

    public void SetPosition(Vector2 value)
    {
      _model.Transformation.Position.Value = value;
    }

    public void SetVelocity(Vector2 value)
    {
      _model.Velocity.Value = value;
    }
    
    public void SetRotation(Quaternion value)
    {
      _model.Transformation.Rotation.Value = value;
    }

    public void SetPrimaryWeapon(IWeapon weapon)
    {
      _model.PrimaryWeapon = weapon;
    }
    
    public void SetSecondaryWeapon(IWeapon weapon)
    {
      _model.SecondaryWeapon = weapon;
    }

    public void Update(float deltaTime)
    {
      _model.Transformation.Rotation.Value *=
        Quaternion.Euler(0f, 0f, _model.RotationSpeed * -_model.Input.Rotation * deltaTime * Mathf.Rad2Deg);
      
      _model.Velocity.Value += _model.Input.Movement > 0
        ? _model.Acceleration * deltaTime * _model.Transformation.Forward
        : _model.Deceleration * deltaTime * -_model.Velocity.Value;

      _model.Velocity.Value = Vector3.ClampMagnitude(_model.Velocity.Value, _model.MaxSpeed);
      
      _model.Transformation.Position.Value += _model.Velocity.Value * deltaTime;
      _model.Wrapper.Execute();
      
      if(_model.ContactTrigger.HasContact())
      {
        OnDestroy?.Invoke();
        return;
      }

      if (_model.Input.PrimaryAttack)
        _model.PrimaryWeapon.TryShoot();

      if (_model.Input.SecondaryAttack)
        _model.SecondaryWeapon.TryShoot();
    }
  }
}