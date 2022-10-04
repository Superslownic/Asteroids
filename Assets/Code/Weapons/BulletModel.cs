using Code.Common;
using Code.Infrastructure.Timers;

namespace Code.Weapons
{
  public class BulletModel
  {
    public readonly Timer DisableTimer;
    public readonly Transformation Transformation;
    public readonly StraightMovement Movement;
    public readonly ContactTrigger ContactTrigger;

    public BulletModel(Transformation transformation, Timer disableTimer, StraightMovement movement, ContactTrigger contactTrigger)
    {
      DisableTimer = disableTimer;
      Movement = movement;
      ContactTrigger = contactTrigger;
      Transformation = transformation;
    }
  }
}