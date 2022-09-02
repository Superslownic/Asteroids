using Code.MonoEventProviders;
using UnityEngine;

namespace Code.Unit.Player
{
  public class PlayerController : IUpdateListener
  {
    private readonly PlayerModel _model;
    private readonly PlayerView _view;
    private readonly PlayerInput _input;
    private readonly PlayerMovement _movement;
    private readonly UnitPositionRepeater _positionRepeater;
    private readonly CollisionDetector _collisionDetector;

    public PlayerController(PlayerModel model, PlayerInput input, PlayerMovement movement,
      UnitPositionRepeater positionRepeater,CollisionDetector collisionDetector, PlayerView view)
    {
      _model = model;
      _input = input;
      _movement = movement;
      _positionRepeater = positionRepeater;
      _collisionDetector = collisionDetector;
      _view = view;
    }

    public void Update(float deltaTime)
    {
      Move(deltaTime);
      CheckCollisions();
    }

    private void Move(float deltaTime)
    {
      _input.Update();
      _movement.Update(deltaTime);
      _positionRepeater.Update();
      _view.SetPosition(_model.Transform.Position.Value);
      _view.SetRotation(_model.Transform.Rotation.Value);
    }

    private void CheckCollisions()
    {
      if (_collisionDetector.Update() <= 0)
        return;
      
      _model.IsAlive.Value = false;
      Object.Destroy(_view.gameObject);
    }
  }
}