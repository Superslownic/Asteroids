using Code.Common;
using Code.Infrastructure.Timers;

namespace Code.Enemies
{
  public class EnemySpawnerData
  {
    public readonly Countdown Countdown = new Countdown();
    public readonly FloatRange Cooldown;
    public readonly AsteroidConfig BaseAsteroidConfig;
    public readonly UFOConfig UFOConfig;
    public readonly int AsteroidSpawnProbability;

    public EnemySpawnerData(FloatRange cooldown, AsteroidConfig baseAsteroidConfig, UFOConfig ufoConfig, int asteroidSpawnProbability)
    {
      Cooldown = cooldown;
      BaseAsteroidConfig = baseAsteroidConfig;
      UFOConfig = ufoConfig;
      AsteroidSpawnProbability = asteroidSpawnProbability;
    }
  }
}