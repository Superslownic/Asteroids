using Code.Common;
using Code.Infrastructure.MonoEventProviders;
using Code.Player.Input;
using Code.Weapons;
using UnityEngine;

namespace Code.Player
{
  public class ShipModel
  {
    public readonly IInput Input;
    public readonly Transformation Transformation;
    public readonly Observable<Vector2> Velocity;
    public readonly PositionWrapper Wrapper;
    public readonly ContactTrigger ContactTrigger;
    public readonly Updater Updater;
    public readonly float Acceleration;
    public readonly float Deceleration;
    public readonly float MaxSpeed;
    public readonly float RotationSpeed;

    public IWeapon PrimaryWeapon { get; set; }
    public IWeapon SecondaryWeapon { get; set; }

    public ShipModel(IInput input, Transformation transformation, PositionWrapper wrapper,  ContactTrigger contactTrigger,
      Updater updater, float acceleration, float deceleration, float maxSpeed, float rotationSpeed)
    {
      Input = input;
      Transformation = transformation;
      Velocity = new Observable<Vector2>();
      Wrapper = wrapper;
      ContactTrigger = contactTrigger;
      Acceleration = acceleration;
      Deceleration = deceleration;
      MaxSpeed = maxSpeed;
      RotationSpeed = rotationSpeed;
      Updater = updater;
    }
  }
}
