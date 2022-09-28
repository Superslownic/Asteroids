﻿using System;
using UnityEngine;

namespace Code.Weapons
{
  public class Laser
  {
    private readonly LaserData _data;
    private readonly LaserView _view;

    public Laser(LaserData data, LaserView view)
    {
      _data = data;
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
      _view.Transformation.SetPosition(value);
    }

    public void SetRotation(Quaternion value)
    {
      _view.Transformation.SetRotation(value);
    }
    
    public void SetDistance(float value)
    {
      Vector3 scale = _view.transform.localScale;
      scale.y = value;
      _view.transform.localScale = scale;
    }

    public void StartDestroyTimer(float time)
    {
      _data.DestroyTimer.Run(time, Destroy);
    }

    private void Destroy()
    {
      OnDestroy?.Invoke(this);
    }
  }
}