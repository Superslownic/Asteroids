using Code.MonoEventProviders;

namespace Code.Unit.Enemies.Asteroids
{
  public class AsteroidController : IUpdateListener
  {
    private readonly AsteroidModel _model;
    private readonly AsteroidMovement _movement;
    private readonly UnitPositionRepeater _positionRepeater;
    private readonly AsteroidView _view;

    public AsteroidController(AsteroidModel model, AsteroidMovement movement, UnitPositionRepeater positionRepeater, AsteroidView view)
    {
      _model = model;
      _movement = movement;
      _positionRepeater = positionRepeater;
      _view = view;
    }
    
    public void Update(float deltaTime)
    {
      _movement.Update(deltaTime);
      _positionRepeater.Update();
      _view.SetPosition(_model.Transform.Position.Value);
      _view.SetRotation(_model.Transform.Rotation.Value);
    }
  }
}