using Code.Common;
using UnityEngine;

namespace Code.Weapons
{
  public class LaserGun : IWeapon
  {
    public readonly LaserGunModel Model;
    
    private readonly LaserFactory _laserFactory;
    private readonly RaycastHit2D[] _buffer;

    public LaserGun(LaserGunModel model, LaserFactory laserFactory)
    {
      Model = model;
      _laserFactory = laserFactory;
      _buffer = new RaycastHit2D[10];
      RunCooldown();
    }
    
    public bool TryShoot()
    {
      if (Model.ShotCount.Value == 0)
        return false;
      
      var laser = _laserFactory.Create(Model.LaserConfig, Model.LaserAnchor);
      laser.OnDestroy += HandleLaserDestroy;

      int targetCount =
        Physics2D.RaycastNonAlloc(Model.LaserAnchor.Position.Value, Model.LaserAnchor.Forward * Model.LaserConfig.Distance, _buffer);
      
      for (int i = 0; i < targetCount; i++)
        if(_buffer[i].transform.TryGetComponent(out IContactHandler contactHandler))
          contactHandler.OnHit();

      Model.ShotCount.Value--;
      
      if (Model.CooldownTimer.IsOver)
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
      Model.CooldownTimer.Run(Model.CooldownTime, AddLaser);
    }

    private void AddLaser()
    {
      Model.ShotCount.Value++;

      if (Model.ShotCount.Value < Model.MaxShotCount)
        RunCooldown();
    }
  }
}