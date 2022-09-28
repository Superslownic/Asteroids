using Code.Config;
using Code.Factory;
using Code.Infrastructure.DependencyInjection;
using Code.Infrastructure.MonoEventProviders;
using Code.Logic.Enemies.Asteroids;
using Code.Logic.Player;
using Code.Logic.Weapons;
using Code.Model;
using Code.View;
using UnityEngine;

namespace Code
{
  public class Bootstrap : MonoBehaviour
  {
    [SerializeField] private Camera _camera;
    [SerializeField] private ShipConfig _shipConfig;
    [SerializeField] private AsteroidsCollection _asteroidsCollection;
    [SerializeField] private AsteroidSpawnerConfig _asteroidsSpawnerConfig;
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
      var asteroidFactory = new AsteroidFactory(diContainer);
      var projectileParent = new GameObject("Projectiles").transform;
      var bulletFactory = new BulletFactory(diContainer, projectileParent);
      var laserFactory = new LaserFactory(diContainer, projectileParent);
      
      diContainer.Register(updater);
      diContainer.Register(fixedUpdater);
      diContainer.Register(input);
      diContainer.Register(screenLimits);
      diContainer.Register(timerFactory);
      diContainer.Register(asteroidFactory);
      diContainer.Register(bulletFactory);
      diContainer.Register(laserFactory);

      var ship = playerFactory.Create(_shipConfig);
      
      diContainer.Register(ship);
      
      var asteroidSpawnerData = new AsteroidSpawnerData(_asteroidsSpawnerConfig.Cooldown, _asteroidsSpawnerConfig.BaseAsteroidConfig);
      var asteroidSpawner = new AsteroidSpawner(asteroidSpawnerData, _asteroidsCollection, asteroidFactory, screenLimits);
      
      updater.AddListener(asteroidSpawner);
      
      var shipInfo = new ShipInfo(ship.Data, _shipInfoView);
      shipInfo.Enable();
      
      var laserAmmunition = new LaserGunAmmunition(((LaserGun) ship.SecondaryWeapon).Data, _laserGunAmmunitionView);
      laserAmmunition.Enable();
    }
  }
}