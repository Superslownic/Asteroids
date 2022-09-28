using Code.Common;
using Code.Infrastructure.Timers;

namespace Code.Weapons
{
  public class BulletGunData
  {
    public readonly float CooldownTime;
    public readonly BulletConfig BulletConfig;
    public readonly ITransformable BulletAnchor;
    public readonly Countdown Cooldown;

    public BulletGunData(float cooldownTime, BulletConfig bulletConfig, ITransformable bulletAnchor)
    {
      CooldownTime = cooldownTime;
      BulletConfig = bulletConfig;
      BulletAnchor = bulletAnchor;
      Cooldown = new Countdown();
    }
  }
}