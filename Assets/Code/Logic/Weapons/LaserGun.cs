using Code.Factory;
using Code.Model;

namespace Code.Logic.Weapons
{
  public class LaserGun : IWeapon
  {
    private readonly LaserGunData _data;
    private readonly LaserFactory _laserFactory;

    public LaserGun(LaserGunData data, LaserFactory laserFactory)
    {
      _data = data;
      _laserFactory = laserFactory;
      RunCooldown();
    }
    
    public bool TryShoot()
    {
      if (_data.ShotCount.Value == 0)
        return false;
      
      var laser = _laserFactory.Create(_data.LaserConfig, _data.LaserAnchor);
      laser.OnDestroy += HandleLaserDestroy;
      
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