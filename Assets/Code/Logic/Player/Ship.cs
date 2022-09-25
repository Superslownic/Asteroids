using System;
using Code.Infrastructure.Extensions;
using Code.Infrastructure.MonoEventProviders;
using Code.Logic.Common;
using Code.Logic.Weapons;
using Code.Model;
using Code.View;
using UnityEngine;

namespace Code.Logic.Player
{
  public class Ship : IUpdateListener
  {
    private readonly ShipData _data;
    private readonly PlayerInput _input;
    private readonly ScreenLimits _screenLimits;
    private readonly ContactTrigger _contactTrigger;
    private readonly ShipView _view;
    
    private IWeapon _primaryWeapon;
    private IWeapon _secondaryWeapon;

    public Ship(ShipData data, PlayerInput input, ScreenLimits screenLimits, ContactTrigger contactTrigger, ShipView view)
    {
      _data = data;
      _view = view;
      _input = input;
      _screenLimits = screenLimits;
      _contactTrigger = contactTrigger;
    }

    public event Action OnDestroy;

    public void Enable()
    {
      _view.Activator.Enable();
    }
    
    public void Disable()
    {
      _view.Activator.Disable();
    }

    public void SetPrimaryWeapon(IWeapon weapon)
    {
      _primaryWeapon = weapon;
    }
    
    public void SetSecondaryWeapon(IWeapon weapon)
    {
      _secondaryWeapon = weapon;
    }

    public void Update(float deltaTime)
    {
      _data.Rotation.Value *=
        Quaternion.Euler(0f, 0f, _data.RotationSpeed * -_input.Rotation * deltaTime * Mathf.Rad2Deg);
      
      _data.Velocity.Value += _input.Movement > 0
        ? _data.Acceleration * deltaTime * _data.Forward()
        : _data.Deceleration * deltaTime * -_data.Velocity.Value;

      _data.Velocity.Value = Vector3.ClampMagnitude(_data.Velocity.Value, _data.MaxSpeed);
      
      _data.Position.Value += _data.Velocity.Value * deltaTime;
      _data.Position.Value = _screenLimits.Wrap(_data.Position.Value);
      
      if(_contactTrigger.HasContact())
      {
        OnDestroy?.Invoke();
        return;
      }

      if (_input.PrimaryAttack)
        _primaryWeapon.TryShoot();

      if (_input.SecondaryAttack)
        _secondaryWeapon.TryShoot();
    }
  }
}