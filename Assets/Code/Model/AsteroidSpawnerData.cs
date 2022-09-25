using Code.Config;
using Code.Infrastructure.Timers;

namespace Code.Model
{
  public class AsteroidSpawnerData
  {
    public readonly Countdown Countdown = new Countdown();
    public readonly FloatRange Cooldown;
    public readonly AsteroidConfig BaseAsteroidConfig;

    public AsteroidSpawnerData(FloatRange cooldown, AsteroidConfig baseAsteroidConfig)
    {
      Cooldown = cooldown;
      BaseAsteroidConfig = baseAsteroidConfig;
    }
  }
}