using UnityEngine;

namespace Code.Unit.Player
{
  public class PlayerMovement
  {
    private readonly UnitTransform _transform;
    private readonly PlayerInput _input;
    private readonly PlayerConfig _config;

    public Vector2 Velocity { get; private set; }

    public PlayerMovement(UnitTransform transform, PlayerInput input, PlayerConfig config)
    {
      _transform = transform;
      _input = input;
      _config = config;
    }
    
    public void Update(float deltaTime)
    {
      Velocity += _input.Vertical > 0
        ? _config.Acceleration * deltaTime * _transform.Forward
        : _config.Deceleration * deltaTime * -Velocity.normalized;
      
      Velocity = Vector3.ClampMagnitude(Velocity, _config.MaxSpeed);
      
      _transform.Position.Value += Velocity * deltaTime;
      _transform.Rotation.Value *= Quaternion.Euler(0f, 0f, -_config.RotationSpeed * _input.Horizontal * deltaTime * Mathf.Rad2Deg);
    }
  }
}