using Code.DependencyInjection;
using Code.Helpers;
using Code.MonoEventProviders;
using Code.Unit.Enemies.Asteroids;
using Code.Unit.Player;
using UnityEngine;

namespace Code.Unit
{
  public class UnitFactory
  {
    private readonly DiContainer _diContainer;
    private readonly Transform _asteroidsParent;

    public UnitFactory(DiContainer diContainer)
    {
      _diContainer = diContainer;
      _asteroidsParent = new GameObject("Asteroids").transform;
    }

    public PlayerController CreatePlayer(PlayerConfig config)
    {
      var updater = _diContainer.Resolve<Updater>();
      var screenLimits = _diContainer.Resolve<ScreenLimits>();

      var transform = new UnitTransform(Vector3.zero, Quaternion.identity);
      var view = Object.Instantiate(config.Prefab);
      var model = new PlayerModel(transform);
      var input = new PlayerInput();
      var movement = new PlayerMovement(transform, input, config);
      var positionRepeater = new UnitPositionRepeater(transform, screenLimits);
      var collisionDetector = new CollisionDetector(view.Collider, new ContactFilter2D().NoFilter(), 1);

      var controller = new PlayerController(model, input, movement, positionRepeater, collisionDetector, view);

      updater.AddListener(controller);
      model.IsAlive.OnChanged += UnsubscribeOnDestroy;

      void UnsubscribeOnDestroy(bool isAlive)
      {
        if(isAlive)
          return;
        
        model.IsAlive.OnChanged -= UnsubscribeOnDestroy;
        updater.RemoveListener(controller);
      }

      return controller;
    }

    public AsteroidController CreateAsteroid(AsteroidConfig config, Vector2 position, Vector2 direction)
    {
      var updater = _diContainer.Resolve<Updater>();
      var screenLimits = _diContainer.Resolve<ScreenLimits>();
      
      var transform = new UnitTransform(position, Quaternion.identity);
      var movement = new AsteroidMovement(transform, config, direction);
      var repeater = new UnitPositionRepeater(transform, screenLimits);
      var view = Object.Instantiate(config.Prefab, position, Quaternion.identity, _asteroidsParent);
      var model = new AsteroidModel(transform);
      var controller = new AsteroidController(model, movement, repeater, view);
      
      updater.AddListener(controller);
      model.IsAlive.OnChanged += UnsubscribeOnDestroy;

      void UnsubscribeOnDestroy(bool isAlive)
      {
        if(isAlive)
          return;
        
        model.IsAlive.OnChanged -= UnsubscribeOnDestroy;
        updater.RemoveListener(controller);
      }

      return controller;
    }
  }
}