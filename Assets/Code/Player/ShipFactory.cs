using Code.Common;
using Code.Infrastructure.DependencyInjection;
using Code.Infrastructure.MonoEventProviders;
using Code.Infrastructure.Timers;
using Code.Player.Input;
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
      var input = _diContainer.Resolve<IInput>();
      var updater = _diContainer.Resolve<Updater>();
      var screenLimits = _diContainer.Resolve<ScreenLimits>();
      var bulletFactory = _diContainer.Resolve<BulletFactory>();
      var laserFactory = _diContainer.Resolve<LaserFactory>();
      var timerFactory = _diContainer.Resolve<TimerFactory>();

      var view = Object.Instantiate(config.Prefab);
      var transformation = new Transformation(Vector2.zero, Quaternion.identity);
      var wrapper = new PositionWrapper(transformation, screenLimits);
      var contactTrigger = new ContactTrigger(view.Collider, config.ContactFilter, 1);

      var bulletGunModel = new BulletGunModel(config.BulletGunConfig.Cooldown, config.BulletGunConfig.BulletConfig, transformation);
      var bulletGun = new BulletGun(bulletGunModel, bulletFactory);
      
      var laserGunModel = new LaserGunModel(config.LaserGunConfig.LaserConfig, timerFactory.Create(),
        config.LaserGunConfig.Cooldown, config.LaserGunConfig.MaxShotCount, transformation);
      var laserGun = new LaserGun(laserGunModel, laserFactory);
      
      var model = new ShipModel(input, transformation, wrapper, contactTrigger, updater, config.Acceleration,
        config.Deceleration, config.MaxSpeed, config.RotationSpeed);
      var ship = new Ship(model, view);
      
      ship.SetPrimaryWeapon(bulletGun);
      ship.SetSecondaryWeapon(laserGun);
      
      view.Transformable.Construct(transformation);
      
      _diContainer.Register(transformation, DependencyKey.PlayerTransformation);
      _diContainer.Register(model);
      _diContainer.Register(laserGunModel);

      return ship;
    }
  }
}