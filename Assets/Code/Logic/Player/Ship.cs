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
    public readonly ShipData Data;
    
    private readonly PlayerInput _input;
    private readonly ScreenLimits _screenLimits;
    private readonly ContactTrigger _contactTrigger;
    private readonly ShipView _view;
    
    private IWeapon _primaryWeapon;
    private IWeapon _secondaryWeapon;

    public Ship(ShipData data, PlayerInput input, ScreenLimits screenLimits, ContactTrigger contactTrigger, ShipView view)
    {
      Data = data;
      _view = view;
      _input = input;
      _screenLimits = screenLimits;
      _contactTrigger = contactTrigger;
    }

    public event Action OnDestroy;

    public IWeapon SecondaryWeapon => _secondaryWeapon;

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
      Data.Rotation.Value *=
        Quaternion.Euler(0f, 0f, Data.RotationSpeed * -_input.Rotation * deltaTime * Mathf.Rad2Deg);
      
      Data.Velocity.Value += _input.Movement > 0
        ? Data.Acceleration * deltaTime * Data.Forward()
        : Data.Deceleration * deltaTime * -Data.Velocity.Value;

      Data.Velocity.Value = Vector3.ClampMagnitude(Data.Velocity.Value, Data.MaxSpeed);
      
      Data.Position.Value += Data.Velocity.Value * deltaTime;
      Data.Position.Value = _screenLimits.Wrap(Data.Position.Value);
      
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