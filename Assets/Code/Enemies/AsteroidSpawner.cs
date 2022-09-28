using Code.Common;
using Code.Infrastructure.MonoEventProviders;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Code.Enemies
{
  public class AsteroidSpawner : IUpdateListener
  {
    private readonly AsteroidSpawnerData _data;
    private readonly AsteroidsCollection _asteroidsCollection;
    private readonly AsteroidFactory _asteroidFactory;
    private readonly ScreenLimits _screenLimits;

    public AsteroidSpawner(AsteroidSpawnerData data, AsteroidsCollection asteroidsCollection, AsteroidFactory asteroidFactory, ScreenLimits screenLimits)
    {
      _data = data;
      _asteroidsCollection = asteroidsCollection;
      _asteroidFactory = asteroidFactory;
      _screenLimits = screenLimits;
      RunCooldown();
    }

    private static Vector2 RandomDirection =>
      Random.insideUnitCircle.normalized;

    public void Update(float deltaTime)
    {
      if (_data.Countdown.IsOver == false)
        return;

      bool onTop = Random.value > 0.5f;
      float delta = Random.value;
      float rounded = Mathf.Round(Random.value);
      
      Vector2 position = new Vector2
      {
        x = Mathf.Lerp(_screenLimits.Horizontal.Min, _screenLimits.Horizontal.Max, onTop ? rounded : delta),
        y = Mathf.Lerp(_screenLimits.Vertical.Min, _screenLimits.Vertical.Max, onTop == false ? rounded : delta)
      };

      SpawnAsteroid(_data.BaseAsteroidConfig, position);
      RunCooldown();
    }

    private void RunCooldown()
    {
      _data.Countdown.Run(_data.Cooldown.GetRandom());
    }

    private void SpawnAsteroid(AsteroidConfig config, Vector2 position)
    {
      var asteroid = _asteroidFactory.Create(config, position, RandomDirection);
      asteroid.OnDestroy += HandleAsteroidDestroy;
    }

    private void HandleAsteroidDestroy(Asteroid asteroid)
    {
      asteroid.OnDestroy -= HandleAsteroidDestroy;
      asteroid.Disable();
      
      _asteroidFactory.Recycle(asteroid);
      
      if(_asteroidsCollection.TryGetFragmentConfig(asteroid.Type, out AsteroidConfig fragmentConfig) == false)
        return;
      
      for (int i = 0; i < asteroid.FragmentCount; i++)
        SpawnAsteroid(fragmentConfig, asteroid.Position);
    }
  }
}