using System.Collections.Generic;
using Code.Config;
using Code.Infrastructure.DependencyInjection;
using Code.Infrastructure.MonoEventProviders;
using Code.Infrastructure.Recycle;
using Code.Logic.Enemies.Asteroids;
using Code.Model;
using UnityEngine;

namespace Code.Factory
{
  public class AsteroidFactory
  {
    private readonly DiContainer _diContainer;
    private readonly Dictionary<int, Pool<Asteroid>> _pools;
    private readonly Transform _parent;

    public AsteroidFactory(DiContainer diContainer)
    {
      _diContainer = diContainer;
      _pools = new Dictionary<int, Pool<Asteroid>>();
      _parent = new GameObject("Asteroids").transform;
    }

    public Asteroid Create(AsteroidConfig config, Vector2 position, Vector2 direction)
    {
      var id = config.GetInstanceID();
      
      if(_pools.ContainsKey(id) == false)
        _pools.Add(id, new Pool<Asteroid>());
      
      if (_pools[id].TryGet(out Asteroid asteroid) == false)
      {
        var screenLimits = _diContainer.Resolve<ScreenLimits>();
        
        var view = Object.Instantiate(config.Prefab, position, Quaternion.identity, _parent);
        var data = new AsteroidData(id, config.FragmentCount, config.MovementSpeed);

        asteroid = new Asteroid(data, view, screenLimits);
        
        view.ContactProxy.Construct(asteroid);
        view.Transformation.Construct(data);
      }

      asteroid.SetPosition(position);
      asteroid.SetDirection(direction);
      asteroid.Enable();
      
      _diContainer.Resolve<Updater>().AddListener(asteroid);

      return asteroid;
    }

    public void Recycle(Asteroid asteroid)
    {
      asteroid.Disable();
      _diContainer.Resolve<Updater>().RemoveListener(asteroid);
      _pools[asteroid.Type].Return(asteroid);
    }
  }
}