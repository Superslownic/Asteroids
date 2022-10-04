using Code.Common;
using Code.Infrastructure.DependencyInjection;
using Code.Infrastructure.MonoEventProviders;
using Code.Infrastructure.Recycle;
using Code.Infrastructure.Timers;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Code.Weapons
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
        var transformation = new Transformation(position, rotation);
        var movement = new StraightMovement(transformation)
        {
          Direction = direction,
          Speed = config.MovementSpeed
        };
        var contactTrigger = new ContactTrigger(view.Collider, config.ContactFilter, 1);
        var model = new BulletModel(transformation, timerFactory.Create(), movement, contactTrigger);
        
        bullet = new Bullet(model, view);
        
        view.Transformable.Construct(transformation);
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