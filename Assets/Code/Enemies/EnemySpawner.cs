using System;
using System.Collections.Generic;
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
    private readonly Updater _updater;
    private readonly List<Asteroid> _asteroids = new List<Asteroid>();
    private readonly List<UFO> _ufos = new List<UFO>();

    public EnemySpawner(EnemySpawnerData data, AsteroidsCollection asteroidsCollection, EnemyFactory enemyFactory,
      ScreenLimits screenLimits, Updater updater)
    {
      _data = data;
      _asteroidsCollection = asteroidsCollection;
      _enemyFactory = enemyFactory;
      _screenLimits = screenLimits;
      _updater = updater;
    }

    public event Action<int> OnEnemyDestroyed;

    private Vector2 RandomDirection =>
      Random.insideUnitCircle.normalized;
    
    private bool ShouldSpawnAsteroid =>
      Random.Range(0f, 1f) < _data.AsteroidSpawnProbability;

    public void Enable()
    {
      _updater.AddListener(this);
    }
    
    public void Disable()
    {
      _updater.RemoveListener(this);
    }

    public void Clear()
    {
      _asteroids.ForEach(DisposeAsteroid);
      _asteroids.Clear();
      _ufos.ForEach(DisposeUFO);
      _ufos.Clear();
    }

    public void ResetTimer()
    {
      _data.Timer.Run(_data.Cooldown.GetRandom());
    }

    public void Update(float deltaTime)
    {
      if (_data.Timer.IsOver == false)
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
      
      ResetTimer();
    }

    private void SpawnAsteroid(AsteroidConfig config, Vector2 position)
    {
      var asteroid = _enemyFactory.CreateAsteroid(config, position, RandomDirection);
      asteroid.OnDestroy += HandleAsteroidDestroy;
      _asteroids.Add(asteroid);
    }
    
    private void SpawnUFO(UFOConfig config, Vector2 position)
    {
      var ufo = _enemyFactory.CreateUFO(config, position);
      ufo.OnDestroy += HandleUFODestroy;
      _ufos.Add(ufo);
    }

    private void HandleAsteroidDestroy(Asteroid asteroid)
    {
      DisposeAsteroid(asteroid);
      OnEnemyDestroyed?.Invoke(asteroid.Points);
      
      if(_asteroidsCollection.TryGetFragmentConfig(asteroid.Type, out AsteroidConfig fragmentConfig) == false)
        return;
      
      for (int i = 0; i < asteroid.FragmentCount; i++)
        SpawnAsteroid(fragmentConfig, asteroid.Position);
    }
    
    private void HandleUFODestroy(UFO ufo)
    {
      DisposeUFO(ufo);
      OnEnemyDestroyed?.Invoke(ufo.Points);
    }

    private void DisposeAsteroid(Asteroid asteroid)
    {
      asteroid.OnDestroy -= HandleAsteroidDestroy;
      _enemyFactory.Recycle(asteroid);
    }
    
    private void DisposeUFO(UFO ufo)
    {
      ufo.OnDestroy -= HandleUFODestroy;
      _enemyFactory.Recycle(ufo);
    }
  }
}