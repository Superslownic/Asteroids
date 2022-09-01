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

    public void CreatePlayer(PlayerConfig config)
    {
      var updater = _diContainer.Resolve<Updater>();
      var screenLimits = _diContainer.Resolve<ScreenLimits>();

      var input = new PlayerInput();
      var transform = new UnitTransform(Vector3.zero, Quaternion.identity);
      var movement = new PlayerMovement(transform, input, config);
      var repeater = new UnitPositionRepeater(transform, screenLimits);
      var view = Object.Instantiate(config.Prefab);
      var model = new PlayerModel(transform);
      var controller = new PlayerController(model, view);
      
      updater.AddListener(input);
      updater.AddListener(movement);
      updater.AddListener(repeater);
      
      controller.Enable();
    }

    public void CreateAsteroid(AsteroidConfig config, Vector2 position, Vector2 direction)
    {
      var updater = _diContainer.Resolve<Updater>();
      var screenLimits = _diContainer.Resolve<ScreenLimits>();
      
      var transform = new UnitTransform(position, Quaternion.identity);
      var movement = new AsteroidMovement(transform, config, direction);
      var repeater = new UnitPositionRepeater(transform, screenLimits);
      var view = Object.Instantiate(config.Prefab, position, Quaternion.identity, _asteroidsParent);
      var model = new AsteroidModel(transform);
      var controller = new AsteroidController(model, view);
      
      updater.AddListener(movement);
      updater.AddListener(repeater);
      
      controller.Enable();
    }
  }
}