using UnityEngine;

namespace Code.Core.Unit.Enemies.Asteroids
{
  public class AsteroidController
  {
    private readonly AsteroidModel _model;
    private readonly AsteroidView _view;

    public AsteroidController(AsteroidModel model, AsteroidView view)
    {
      _model = model;
      _view = view;
    }

    public void Enable()
    {
      _model.Transform.Position.OnChanged += UpdatePosition;
      _model.Transform.Rotation.OnChanged += UpdateRotation;
    }
    
    public void Disable()
    {
      _model.Transform.Position.OnChanged -= UpdatePosition;
      _model.Transform.Rotation.OnChanged -= UpdateRotation;
    }

    private void UpdatePosition(Vector2 value)
    {
      _view.SetPosition(value);
    }
    
    private void UpdateRotation(Quaternion value)
    {
      _view.SetRotation(value);
    }
  }
}