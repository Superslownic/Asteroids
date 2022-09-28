using Code.Common;
using Code.Infrastructure.MonoEventProviders;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Code.Enemies
{
  public class EnemySpawner : IUpdateListener
  {
    private readonly EnemySpawnerData _data;
    private readonly AsteroidsCollection _asteroidsCollection;
    private readonly EnemyFactory _enemyFactory;
    private readonly ScreenLimits _screenLimits;

    public EnemySpawner(EnemySpawnerData data, AsteroidsCollection asteroidsCollection, EnemyFactory enemyFactory, ScreenLimits screenLimits)
    {
      _data = data;
      _asteroidsCollection = asteroidsCollection;
      _enemyFactory = enemyFactory;
      _screenLimits = screenLimits;
      RunCooldown();
    }

    private Vector2 RandomDirection =>
      Random.insideUnitCircle.normalized;
    
    private bool ShouldSpawnAsteroid =>
      Random.Range(0, 100) < _data.AsteroidSpawnProbability;

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

      if (ShouldSpawnAsteroid) SpawnAsteroid(_data.BaseAsteroidConfig, position);
      else SpawnUFO(_data.UFOConfig, position);
      
      RunCooldown();
    }

    private void RunCooldown()
    {
      _data.Countdown.Run(_data.Cooldown.GetRandom());
    }

    private void SpawnAsteroid(AsteroidConfig config, Vector2 position)
    {
      var asteroid = _enemyFactory.CreateAsteroid(config, position, RandomDirection);
      asteroid.OnDestroy += HandleAsteroidDestroy;
    }
    
    private void SpawnUFO(UFOConfig config, Vector2 position)
    {
      var asteroid = _enemyFactory.CreateUFO(config, position);
      asteroid.OnDestroy += HandleUFODestroy;
    }

    private void HandleAsteroidDestroy(Asteroid asteroid)
    {
      asteroid.OnDestroy -= HandleAsteroidDestroy;
      asteroid.Disable();
      
      _enemyFactory.Recycle(asteroid);
      
      if(_asteroidsCollection.TryGetFragmentConfig(asteroid.Type, out AsteroidConfig fragmentConfig) == false)
        return;
      
      for (int i = 0; i < asteroid.FragmentCount; i++)
        SpawnAsteroid(fragmentConfig, asteroid.Position);
    }
    
    private void HandleUFODestroy(UFO ufo)
    {
      ufo.OnDestroy -= HandleUFODestroy;
      ufo.Disable();
      
      _enemyFactory.Recycle(ufo);
    }
  }
}