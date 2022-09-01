using Code.MonoEventProviders;
using UnityEngine;

namespace Code.Unit.Enemies.Asteroids
{
  public class AsteroidMovement : IUpdateListener
  {
    private readonly UnitTransform _transform;
    private readonly AsteroidConfig _config;
    private readonly Vector2 _direction;

    public AsteroidMovement(UnitTransform transform, AsteroidConfig config, Vector2 direction)
    {
      _transform = transform;
      _config = config;
      _direction = direction;
    }

    public void Update(float deltaTime)
    {
      _transform.Position.Value += _config.MovementSpeed * deltaTime * _direction;
    }
  }
}