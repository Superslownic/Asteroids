using Code.Factory;
using Code.Infrastructure.Extensions;
using Code.Logic.Common;
using Code.Model;
using UnityEngine;

namespace Code.Logic.Weapons
{
  public class LaserGun : IWeapon
  {
    private readonly LaserGunData _data;
    private readonly LaserFactory _laserFactory;
    private readonly RaycastHit2D[] _buffer;

    public LaserGun(LaserGunData data, LaserFactory laserFactory)
    {
      _data = data;
      _laserFactory = laserFactory;
      _buffer = new RaycastHit2D[10];
      RunCooldown();
    }
    
    public bool TryShoot()
    {
      if (_data.ShotCount.Value == 0)
        return false;
      
      var laser = _laserFactory.Create(_data.LaserConfig, _data.LaserAnchor);
      laser.OnDestroy += HandleLaserDestroy;

      int targetCount =
        Physics2D.RaycastNonAlloc(_data.LaserAnchor.Position.Value, _data.LaserAnchor.Forward() * _data.LaserConfig.Distance, _buffer);
      
      for (int i = 0; i < targetCount; i++)
        if(_buffer[i].transform.TryGetComponent(out IContactHandler contactHandler))
          contactHandler.OnHit();

      _data.ShotCount.Value--;
      
      if (_data.CooldownTimer.IsOver)
        RunCooldown();
      
      return true;
    }

    private void HandleLaserDestroy(Laser laser)
    {
      laser.OnDestroy -= HandleLaserDestroy;
      _laserFactory.Recycle(laser);
    }

    private void RunCooldown()
    {
      _data.CooldownTimer.Run(_data.CooldownTime, AddLaser);
    }

    private void AddLaser()
    {
      _data.ShotCount.Value++;

      if (_data.ShotCount.Value < _data.MaxShotCount)
        RunCooldown();
    }
  }
}