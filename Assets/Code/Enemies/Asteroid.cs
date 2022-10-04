using System;
using Code.Common;
using Code.Infrastructure.MonoEventProviders;
using UnityEngine;

namespace Code.Enemies
{
  public class Asteroid : IUpdateListener, IContactHandler
  {
    private readonly AsteroidModel _model;
    private readonly AsteroidView _view;

    public Asteroid(AsteroidModel model, AsteroidView view)
    {
      _model = model;
      _view = view;
    }

    public int Type => _model.Type;
    public int FragmentCount => _model.FragmentCount;
    public Vector2 Position => _model.Transformation.Position.Value;
    public int Points => _model.Points;

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
      _model.Transformation.Position.Value = value;
    }

    public void SetDirection(Vector2 value)
    {
      _model.Movement.Direction = value;
    }

    public void Update(float deltaTime)
    {
      _model.Movement.Execute(deltaTime);
      _model.Wrapper.Execute();
    }
    
    public void OnHit()
    {
      OnDestroy?.Invoke(this);
    }
  }
}