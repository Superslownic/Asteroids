﻿using Code.Config;
using Code.Infrastructure.DependencyInjection;
using Code.Infrastructure.MonoEventProviders;
using Code.Infrastructure.Recycle;
using Code.Logic.Common;
using Code.Logic.Weapons;
using Code.Model;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Code.Factory
{
  public class BulletFactory
  {
    private readonly DiContainer _diContainer;
    private readonly Transform _projectileParent;
    private readonly Pool<Bullet> _pool = new Pool<Bullet>();

    public BulletFactory(DiContainer diContainer, Transform projectileParent)
    {
      _diContainer = diContainer;
      _projectileParent = projectileParent;
    }

    public Bullet Create(BulletConfig config, Vector2 position, Quaternion rotation, Vector2 direction)
    {
      if (_pool.TryGet(out Bullet bullet) == false)
      {
        var timerFactory = _diContainer.Resolve<TimerFactory>();
        
        var view = Object.Instantiate(config.Prefab, position, Quaternion.identity, _projectileParent);
        var data = new BulletData(config.MovementSpeed, timerFactory.Create());
        var contactTrigger = new ContactTrigger(view.Collider, config.ContactFilter, 1);

        bullet = new Bullet(data, view, contactTrigger);
        
        view.Transformation.Construct(data);
      }

      bullet.SetPosition(position);
      bullet.SetRotation(rotation);
      bullet.SetMovementDirection(direction);
      bullet.StartDestroyTimer(config.DeactivateTime);
      bullet.Enable();

      _diContainer.Resolve<Updater>().AddListener(bullet);

      return bullet;
    }

    public void Recycle(Bullet bullet)
    {
      bullet.Disable();
      _diContainer.Resolve<Updater>().RemoveListener(bullet);
      _pool.Return(bullet);
    }
  }
}