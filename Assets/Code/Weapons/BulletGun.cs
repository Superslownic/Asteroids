using Code.Common.Extensions;
using UnityEngine;

namespace Code.Weapons
{
  public class BulletGun : IWeapon
  {
    private readonly BulletGunData _data;
    private readonly BulletFactory _bulletFactory;
    
    public BulletGun(BulletGunData data, BulletFactory bulletFactory)
    {
      _data = data;
      _bulletFactory = bulletFactory;
    }
    
    public bool TryShoot()
    {
      if (_data.Cooldown.IsOver == false)
        return false;
      
      Bullet bullet = _bulletFactory.Create
      (
        config: _data.BulletConfig,
        position: _data.BulletAnchor.Position.Value,
        rotation: Quaternion.LookRotation(Vector3.forward, _data.BulletAnchor.Forward()),
        direction: _data.BulletAnchor.Forward()
      );
      
      bullet.OnDestroy += HandleBulletDestroy;
      
      _data.Cooldown.Run(_data.CooldownTime);
      return true;
    }

    private void HandleBulletDestroy(Bullet bullet)
    {
      bullet.OnDestroy -= HandleBulletDestroy;
      _bulletFactory.Recycle(bullet);
    }
  }
}