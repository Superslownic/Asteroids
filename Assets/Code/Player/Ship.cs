using System;
using Code.Common;
using Code.Common.Extensions;
using Code.Infrastructure.MonoEventProviders;
using Code.Player.Input;
using Code.Weapons;
using UnityEngine;

namespace Code.Player
{
  public class Ship : IUpdateListener
  {
    public readonly ShipData Data;
    
    private readonly IInput _input;
    private readonly ScreenLimits _screenLimits;
    private readonly ContactTrigger _contactTrigger;
    private readonly ShipView _view;
    private readonly Updater _updater;

    private IWeapon _primaryWeapon;
    private IWeapon _secondaryWeapon;

    public Ship(ShipData data, IInput input, ScreenLimits screenLimits, ContactTrigger contactTrigger,
      ShipView view, Updater updater)
    {
      Data = data;
      _view = view;
      _updater = updater;
      _input = input;
      _screenLimits = screenLimits;
      _contactTrigger = contactTrigger;
    }

    public event Action OnDestroy;

    public IWeapon SecondaryWeapon => _secondaryWeapon;

    public void Enable()
    {
      _updater.AddListener(this);
      _view.Activator.Enable();
    }
    
    public void Disable()
    {
      _updater.RemoveListener(this);
      _view.Activator.Disable();
    }

    public void SetPosition(Vector2 value)
    {
      Data.Position.Value = value;
    }

    public void SetVelocity(Vector2 value)
    {
      Data.Velocity.Value = value;
    }
    
    public void SetRotation(Quaternion value)
    {
      Data.Rotation.Value = value;
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