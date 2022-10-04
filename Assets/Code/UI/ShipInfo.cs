using System.Globalization;
using Code.Player;
using UnityEngine;

namespace Code.UI
{
  public class ShipInfo
  {
    private readonly ShipModel _shipModel;
    private readonly ShipInfoView _view;
    private readonly string _positionLabelFormat;
    private readonly string _speedLabelFormat;
    private readonly string _angleLabelFormat;
    private readonly CultureInfo _cultureInfo;

    public ShipInfo(ShipModel shipModel, ShipInfoView view)
    {
      _shipModel = shipModel;
      _view = view;
      
      _positionLabelFormat = _view.PositionLabel.text;
      _speedLabelFormat = _view.SpeedLabel.text;
      _angleLabelFormat = _view.AngleLabel.text;
      _cultureInfo = new CultureInfo("en-US");
    }

    public void Enable()
    {
      _shipModel.Transformation.Position.OnChanged += HandlePositionChanged;
      _shipModel.Transformation.Rotation.OnChanged += HandleRotationChanged;
      _shipModel.Velocity.OnChanged += HandleVelocityChanged;
    }
    
    public void Disable()
    {
      _shipModel.Transformation.Position.OnChanged -= HandlePositionChanged;
      _shipModel.Transformation.Rotation.OnChanged -= HandleRotationChanged;
      _shipModel.Velocity.OnChanged -= HandleVelocityChanged;
    }

    private void HandlePositionChanged(Vector2 position)
    {
      _view.PositionLabel.text = string.Format(_positionLabelFormat, position.ToString("F1", _cultureInfo));
    }

    private void HandleVelocityChanged(Vector2 velocity)
    {
      _view.SpeedLabel.text = string.Format(_speedLabelFormat, velocity.magnitude.ToString("F1", _cultureInfo));
    }

    private void HandleRotationChanged(Quaternion rotation)
    {
      float angle = Quaternion.Angle(_shipModel.Transformation.Rotation.Value, Quaternion.LookRotation(Vector3.up, Vector3.forward));
      _view.AngleLabel.text = string.Format(_angleLabelFormat, angle.ToString("F0", _cultureInfo));
    }
  }
}