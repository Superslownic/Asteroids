using Code.Core.DependencyInjection;
using Code.Core.MonoEventProviders;
using Code.Core.Unit;
using Code.Core.Unit.Enemies;
using Code.Core.Unit.Player;
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
      var asteroidsSpawner = new EnemySpawner(unitFactory, _asteroidsSpawnerConfig, screenLimits);
      
      diContainer.Register(updater);
      diContainer.Register(fixedUpdater);
      diContainer.Register(screenLimits);
      diContainer.Register(unitFactory);
      
      updater.AddListener(asteroidsSpawner);
      
      unitFactory.CreatePlayer(_playerConfig);
    }
  }
}