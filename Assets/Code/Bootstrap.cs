using Code.Common;
using Code.Enemies;
using Code.Infrastructure.DependencyInjection;
using Code.Infrastructure.MonoEventProviders;
using Code.Infrastructure.Timers;
using Code.Player;
using Code.UI;
using Code.Weapons;
using UnityEngine;

namespace Code
{
  public class Bootstrap : MonoBehaviour
  {
    [SerializeField] private Camera _camera;
    [SerializeField] private ShipConfig _shipConfig;
    [SerializeField] private AsteroidsCollection _asteroidsCollection;
    [SerializeField] private EnemySpawnerConfig _enemySpawnerConfig;
    [SerializeField] private ShipInfoView _shipInfoView;
    [SerializeField] private LaserGunAmmunitionView _laserGunAmmunitionView;

    private void Awake()
    {
      var diContainer = new DiContainer();
      
      var updater = gameObject.AddComponent<Updater>();
      var fixedUpdater = gameObject.AddComponent<FixedUpdater>();
      
      var input = new PlayerInput();
      
      var screenLimits = new ScreenLimits(_camera);
      
      var timerFactory = new TimerFactory(this);
      var playerFactory = new ShipFactory(diContainer);
      var asteroidFactory = new EnemyFactory(diContainer);
      var projectileParent = new GameObject("Projectiles").transform;
      var enemyParent = new GameObject("Enemies").transform;
      var bulletFactory = new BulletFactory(diContainer, projectileParent);
      var laserFactory = new LaserFactory(diContainer, projectileParent);
      
      diContainer.Register(updater);
      diContainer.Register(fixedUpdater);
      diContainer.Register(input);
      diContainer.Register(screenLimits);
      diContainer.Register(enemyParent, DependencyKey.EnemyParent);
      diContainer.Register(timerFactory);
      diContainer.Register(asteroidFactory);
      diContainer.Register(bulletFactory);
      diContainer.Register(laserFactory);

      var ship = playerFactory.Create(_shipConfig);
      
      diContainer.Register(ship);
      
      var enemySpawnerData = new EnemySpawnerData(_enemySpawnerConfig.Cooldown, _enemySpawnerConfig.BaseAsteroidConfig,
        _enemySpawnerConfig.UFOConfig, _enemySpawnerConfig.AsteroidSpawnProbability);
      var enemySpawner = new EnemySpawner(enemySpawnerData, _asteroidsCollection, asteroidFactory, screenLimits);
      
      updater.AddListener(enemySpawner);
      
      var shipInfo = new ShipInfo(ship.Data, _shipInfoView);
      shipInfo.Enable();
      
      var laserAmmunition = new LaserGunAmmunition(((LaserGun) ship.SecondaryWeapon).Data, _laserGunAmmunitionView);
      laserAmmunition.Enable();
    }
  }
}