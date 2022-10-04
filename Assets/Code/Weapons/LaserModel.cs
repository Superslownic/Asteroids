using Code.Common;
using Code.Infrastructure.Timers;
using UnityEngine;

namespace Code.Weapons
{
  public class LaserModel
  {
    public readonly Transformation Transformation;
    public readonly Timer DestroyTimer;

    public LaserModel(Transformation transformation, Timer destroyTimer)
    {
      Transformation = transformation;
      DestroyTimer = destroyTimer;
    }
  }
}