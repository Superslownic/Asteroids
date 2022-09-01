using UnityEngine;

namespace Code.Core.Unit.Player
{
  public class PlayerController
  {
    private readonly PlayerModel _model;
    private readonly PlayerView _view;

    public PlayerController(PlayerModel model, PlayerView view)
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

    private void UpdatePosition(Vector3 value)
    {
      _view.SetPosition(value);
    }
    
    private void UpdateRotation(Quaternion value)
    {
      _view.SetRotation(value);
    }
  }
}