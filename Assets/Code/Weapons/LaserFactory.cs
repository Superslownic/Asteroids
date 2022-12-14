using Code.Common;
using Code.Infrastructure.DependencyInjection;
using Code.Infrastructure.Recycle;
using Code.Infrastructure.Timers;
using UnityEngine;

namespace Code.Weapons
{
  public class LaserFactory
  {
    private readonly DiContainer _diContainer;
    private readonly Transform _projectileParent;
    private readonly Pool<Laser> _pool = new Pool<Laser>();

    public LaserFactory(DiContainer diContainer, Transform projectileParent)
    {
      _diContainer = diContainer;
      _projectileParent = projectileParent;
    }
    
    public Laser Create(LaserConfig config, Transformation anchor)
    {
      if (_pool.TryGet(out Laser laser) == false)
      {
        var timerFactory = _diContainer.Resolve<TimerFactory>();

        var view = Object.Instantiate(config.Prefab, _projectileParent);
        var transformation = new Transformation(anchor.Position.Value, anchor.Rotation.Value);
        var model = new LaserModel(transformation, timerFactory.Create());
        
        laser = new Laser(model, view);
        
        view.Transformable.Construct(transformation);
      }

      laser.SetPosition(anchor.Position.Value);
      laser.SetRotation(anchor.Rotation.Value);
      laser.SetDistance(config.Distance);
      laser.StartDestroyTimer(config.DestroyTime);
      laser.Enable();
      
      return laser;
    }

    public void Recycle(Laser laser)
    {
      laser.Disable();
      _pool.Return(laser);
    }
  }
}