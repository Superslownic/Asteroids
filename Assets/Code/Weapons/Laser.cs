using System;
using UnityEngine;

namespace Code.Weapons
{
  public class Laser
  {
    private readonly LaserModel _model;
    private readonly LaserView _view;

    public Laser(LaserModel model, LaserView view)
    {
      _model = model;
      _view = view;
    }

    public event Action<Laser> OnDestroy;

    public void Enable()
    {
      _view.Activator.Enable();
    }
    
    public void Disable()
    {
      _view.Activator.Disable();
    }
    
    public void SetPosition(Vector2 value)
    {
      _model.Transformation.Position.Value = value;
    }

    public void SetRotation(Quaternion value)
    {
      _model.Transformation.Rotation.Value = value;
    }
    
    public void SetDistance(float value)
    {
      Vector3 scale = _view.transform.localScale;
      scale.y = value;
      _view.transform.localScale = scale;
    }

    public void StartDestroyTimer(float time)
    {
      _model.DestroyTimer.Run(time, Destroy);
    }

    private void Destroy()
    {
      OnDestroy?.Invoke(this);
    }
  }
}