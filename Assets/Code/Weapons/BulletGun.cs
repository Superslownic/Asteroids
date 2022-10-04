using UnityEngine;

namespace Code.Weapons
{
  public class BulletGun : IWeapon
  {
    private readonly BulletGunModel _model;
    private readonly BulletFactory _bulletFactory;
    
    public BulletGun(BulletGunModel model, BulletFactory bulletFactory)
    {
      _model = model;
      _bulletFactory = bulletFactory;
    }
    
    public bool TryShoot()
    {
      if (_model.Cooldown.IsOver == false)
        return false;
      
      Bullet bullet = _bulletFactory.Create
      (
        config: _model.BulletConfig,
        position: _model.BulletAnchor.Position.Value,
        rotation: Quaternion.LookRotation(Vector3.forward, _model.BulletAnchor.Forward),
        direction: _model.BulletAnchor.Forward
      );
      
      bullet.OnDestroy += HandleBulletDestroy;
      
      _model.Cooldown.Run(_model.CooldownTime);
      return true;
    }

    private void HandleBulletDestroy(Bullet bullet)
    {
      bullet.OnDestroy -= HandleBulletDestroy;
      _bulletFactory.Recycle(bullet);
    }
  }
}