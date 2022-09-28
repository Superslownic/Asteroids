using Code.Common;
using Code.Common.Extensions;
using UnityEngine;

namespace Code.Weapons
{
  public class LaserGun : IWeapon
  {
    public readonly LaserGunData Data;
    
    private readonly LaserFactory _laserFactory;
    private readonly RaycastHit2D[] _buffer;

    public LaserGun(LaserGunData data, LaserFactory laserFactory)
    {
      Data = data;
      _laserFactory = laserFactory;
      _buffer = new RaycastHit2D[10];
      RunCooldown();
    }
    
    public bool TryShoot()
    {
      if (Data.ShotCount.Value == 0)
        return false;
      
      var laser = _laserFactory.Create(Data.LaserConfig, Data.LaserAnchor);
      laser.OnDestroy += HandleLaserDestroy;

      int targetCount =
        Physics2D.RaycastNonAlloc(Data.LaserAnchor.Position.Value, Data.LaserAnchor.Forward() * Data.LaserConfig.Distance, _buffer);
      
      for (int i = 0; i < targetCount; i++)
        if(_buffer[i].transform.TryGetComponent(out IContactHandler contactHandler))
          contactHandler.OnHit();

      Data.ShotCount.Value--;
      
      if (Data.CooldownTimer.IsOver)
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
      Data.CooldownTimer.Run(Data.CooldownTime, AddLaser);
    }

    private void AddLaser()
    {
      Data.ShotCount.Value++;

      if (Data.ShotCount.Value < Data.MaxShotCount)
        RunCooldown();
    }
  }
}