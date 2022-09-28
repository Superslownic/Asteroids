using Code.Common;
using Code.Infrastructure.Timers;

namespace Code.Weapons
{
  public class LaserGunData
  {
    public readonly LaserConfig LaserConfig;
    public readonly Timer CooldownTimer;
    public readonly float CooldownTime;
    public readonly int MaxShotCount;
    public readonly Observable<int> ShotCount;
    public readonly ITransformable LaserAnchor;

    public LaserGunData(LaserConfig laserConfig, Timer cooldownTimer, float cooldownTime, int maxShotCount, ITransformable laserAnchor)
    {
      LaserConfig = laserConfig;
      CooldownTimer = cooldownTimer;
      CooldownTime = cooldownTime;
      MaxShotCount = maxShotCount;
      LaserAnchor = laserAnchor;
      ShotCount = new Observable<int>();
    }
  }
}