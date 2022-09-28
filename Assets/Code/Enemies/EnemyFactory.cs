using System.Collections.Generic;
using Code.Common;
using Code.Infrastructure.DependencyInjection;
using Code.Infrastructure.MonoEventProviders;
using Code.Infrastructure.Recycle;
using UnityEngine;

namespace Code.Enemies
{
  public class EnemyFactory
  {
    private readonly DiContainer _diContainer;
    private readonly Dictionary<int, Pool<Asteroid>> _asteroidPools = new Dictionary<int, Pool<Asteroid>>();
    private readonly Pool<UFO> _ufoPool = new Pool<UFO>();

    public EnemyFactory(DiContainer diContainer)
    {
      _diContainer = diContainer;
    }

    public Asteroid CreateAsteroid(AsteroidConfig config, Vector2 position, Vector2 direction)
    {
      var id = config.GetInstanceID();
      
      if(_asteroidPools.ContainsKey(id) == false)
        _asteroidPools.Add(id, new Pool<Asteroid>());
      
      if (_asteroidPools[id].TryGet(out Asteroid asteroid) == false)
      {
        var parent = _diContainer.Resolve<Transform>(DependencyKey.EnemyParent);
        var screenLimits = _diContainer.Resolve<ScreenLimits>();
        
        var view = Object.Instantiate(config.Prefab, position, Quaternion.identity, parent);
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
    
    public UFO CreateUFO(UFOConfig config, Vector2 position)
    {
      if (_ufoPool.TryGet(out UFO ufo) == false)
      {
        var parent = _diContainer.Resolve<Transform>(DependencyKey.EnemyParent);
        var screenLimits = _diContainer.Resolve<ScreenLimits>();
        
        var view = Object.Instantiate(config.Prefab, position, Quaternion.identity, parent);
        var data = new UFOData(config.MovementSpeed, _diContainer.Resolve<ITransformable>(DependencyKey.PlayerTransformable));

        ufo = new UFO(data, view, screenLimits);
        
        view.ContactProxy.Construct(ufo);
        view.Transformation.Construct(data);
      }

      ufo.SetPosition(position);
      ufo.Enable();
      
      _diContainer.Resolve<Updater>().AddListener(ufo);

      return ufo;
    }

    public void Recycle(Asteroid asteroid)
    {
      asteroid.Disable();
      _diContainer.Resolve<Updater>().RemoveListener(asteroid);
      _asteroidPools[asteroid.Type].Return(asteroid);
    }

    public void Recycle(UFO ufo)
    {
      ufo.Disable();
      _diContainer.Resolve<Updater>().RemoveListener(ufo);
      _ufoPool.Return(ufo);
    }
  }
}