using Code.Common;
using Code.Infrastructure.Timers;

namespace Code.Enemies
{
  public class EnemySpawnerData
  {
    public readonly Countdown Timer = new Countdown();
    public readonly FloatRange Cooldown;
    public readonly AsteroidConfig BaseAsteroidConfig;
    public readonly UFOConfig UFOConfig;
    public readonly float AsteroidSpawnProbability;

    public EnemySpawnerData(FloatRange cooldown, AsteroidConfig baseAsteroidConfig, UFOConfig ufoConfig, float asteroidSpawnProbability)
    {
      Cooldown = cooldown;
      BaseAsteroidConfig = baseAsteroidConfig;
      UFOConfig = ufoConfig;
      AsteroidSpawnProbability = asteroidSpawnProbability;
    }
  }
}