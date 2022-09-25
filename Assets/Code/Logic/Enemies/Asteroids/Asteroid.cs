using System;
using Code.Infrastructure.MonoEventProviders;
using Code.Logic.Common;
using Code.Model;
using Code.View;
using UnityEngine;

namespace Code.Logic.Enemies.Asteroids
{
  public class Asteroid : IUpdateListener, IContactHandler
  {
    private readonly AsteroidData _data;
    private readonly AsteroidView _view;
    private readonly ScreenLimits _screenLimits;

    public Asteroid(AsteroidData data, AsteroidView view, ScreenLimits screenLimits)
    {
      _data = data;
      _view = view;
      _screenLimits = screenLimits;
    }

    public int Type => _data.Type;
    public int FragmentCount => _data.FragmentCount;
    public Vector2 Position => _data.Position.Value;

    public event Action<Asteroid> OnDestroy;

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
      _data.Position.Value = value;
    }

    public void SetDirection(Vector2 value)
    {
      _data.Direction.Value = value;
    }

    public void Update(float deltaTime)
    {
      _data.Position.Value += _data.Speed * deltaTime * _data.Direction.Value;
      _data.Position.Value = _screenLimits.Wrap(_data.Position.Value);
    }
    
    public void OnHit()
    {
      OnDestroy?.Invoke(this);
    }
  }
}