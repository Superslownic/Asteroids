using Code.Common;
using Code.Infrastructure.Timers;

namespace Code.Weapons
{
  public class BulletGunModel
  {
    public readonly float CooldownTime;
    public readonly BulletConfig BulletConfig;
    public readonly Transformation BulletAnchor;
    public readonly Countdown Cooldown;

    public BulletGunModel(float cooldownTime, BulletConfig bulletConfig, Transformation bulletAnchor)
    {
      CooldownTime = cooldownTime;
      BulletConfig = bulletConfig;
      BulletAnchor = bulletAnchor;
      Cooldown = new Countdown();
    }
  }
}