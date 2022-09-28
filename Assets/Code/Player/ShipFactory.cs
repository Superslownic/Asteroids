using Code.Common;
using Code.Infrastructure.DependencyInjection;
using Code.Infrastructure.MonoEventProviders;
using Code.Infrastructure.Timers;
using Code.Weapons;
using UnityEngine;

namespace Code.Player
{
  public class ShipFactory
  {
    private readonly DiContainer _diContainer;

    public ShipFactory(DiContainer diContainer)
    {
      _diContainer = diContainer;
    }

    public Ship Create(ShipConfig config)
    {
      var input = _diContainer.Resolve<PlayerInput>();
      var updater = _diContainer.Resolve<Updater>();
      var screenLimits = _diContainer.Resolve<ScreenLimits>();
      var bulletFactory = _diContainer.Resolve<BulletFactory>();
      var laserFactory = _diContainer.Resolve<LaserFactory>();
      var timerFactory = _diContainer.Resolve<TimerFactory>();

      var view = Object.Instantiate(config.Prefab);
      var data = new ShipData(config.Acceleration, config.Deceleration, config.MaxSpeed, config.RotationSpeed);
      var contactTrigger = new ContactTrigger(view.Collider, config.ContactFilter, 1);
      
      var bulletGunData = new BulletGunData(config.BulletGunConfig.Cooldown, config.BulletGunConfig.BulletConfig, data);
      var bulletGun = new BulletGun(bulletGunData, bulletFactory);
      
      var laserGunData = new LaserGunData(config.LaserGunConfig.LaserConfig, timerFactory.Create(), config.LaserGunConfig.Cooldown, config.LaserGunConfig.MaxShotCount, data);
      var laserGun = new LaserGun(laserGunData, laserFactory);
      
      var ship = new Ship(data, input, screenLimits, contactTrigger, view);
      ship.SetPrimaryWeapon(bulletGun);
      ship.SetSecondaryWeapon(laserGun);
      ship.Enable();
      
      view.Transformation.Construct(data);
      
      updater.AddListener(ship);
      
      _diContainer.Register<ITransformable>(data, DependencyKey.PlayerTransformable);

      return ship;
    }
  }
}