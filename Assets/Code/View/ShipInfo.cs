using System.Globalization;
using Code.Model;
using UnityEngine;

namespace Code.View
{
  public class ShipInfo
  {
    private readonly ShipData _shipData;
    private readonly ShipInfoView _view;
    private readonly string _positionLabelFormat;
    private readonly string _speedLabelFormat;
    private readonly string _angleLabelFormat;
    private readonly CultureInfo _cultureInfo;

    public ShipInfo(ShipData shipData, ShipInfoView view)
    {
      _shipData = shipData;
      _view = view;
      
      _positionLabelFormat = _view.PositionLabel.text;
      _speedLabelFormat = _view.SpeedLabel.text;
      _angleLabelFormat = _view.AngleLabel.text;
      _cultureInfo = new CultureInfo("en-US");
    }

    public void Enable()
    {
      _shipData.Position.OnChanged += HandlePositionChanged;
      _shipData.Velocity.OnChanged += HandleVelocityChanged;
      _shipData.Rotation.OnChanged += HandleRotationChanged;
    }
    
    public void Disable()
    {
      _shipData.Position.OnChanged -= HandlePositionChanged;
      _shipData.Velocity.OnChanged -= HandleVelocityChanged;
      _shipData.Rotation.OnChanged -= HandleRotationChanged;
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
      float angle = Quaternion.Angle(_shipData.Rotation.Value, Quaternion.LookRotation(Vector3.up, Vector3.forward));
      _view.AngleLabel.text = string.Format(_angleLabelFormat, angle.ToString("F0", _cultureInfo));
    }
  }
}