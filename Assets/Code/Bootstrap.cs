using Code.DependencyInjection;
using Code.Helpers;
using Code.MonoEventProviders;
using Code.Unit;
using Code.Unit.Enemies;
using Code.Unit.Player;
using UnityEngine;

namespace Code
{
  public class Bootstrap : MonoBehaviour
  {
    [SerializeField] private Camera _camera;
    [SerializeField] private PlayerConfig _playerConfig;
    [SerializeField] private EnemySpawnerConfig _asteroidsSpawnerConfig;

    private void Awake()
    {
      var diContainer = new DiContainer();
      
      var updater = gameObject.AddComponent<Updater>();
      var fixedUpdater = gameObject.AddComponent<FixedUpdater>();
      var screenLimits = new ScreenLimits(_camera);
      var unitFactory = new UnitFactory(diContainer);
      
      diContainer.Register(updater);
      diContainer.Register(fixedUpdater);
      diContainer.Register(screenLimits);
      diContainer.Register(unitFactory);
      
      var player = unitFactory.CreatePlayer(_playerConfig);
      var asteroidsSpawner = new EnemySpawner(unitFactory, _asteroidsSpawnerConfig, screenLimits);
      
      diContainer.Register(player);
      
      updater.AddListener(asteroidsSpawner);
    }
  }
}