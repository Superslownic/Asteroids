using Code.Helpers;
using Code.MonoEventProviders;
using Code.Timers;
using UnityEngine;

namespace Code.Unit.Enemies
{
  public class EnemySpawner : IUpdateListener
  {
    private readonly Stopwatch _stopwatch = new Stopwatch();
    private readonly UnitFactory _unitFactory;
    private readonly EnemySpawnerConfig _config;
    private readonly ScreenLimits _screenLimits;

    private float _time;

    public EnemySpawner(UnitFactory unitFactory, EnemySpawnerConfig config, ScreenLimits screenLimits)
    {
      _unitFactory = unitFactory;
      _config = config;
      _screenLimits = screenLimits;
      RunTimer();
    }

    public void Update(float deltaTime)
    {
      if (_stopwatch.CurrentTime < _time)
        return;

      bool vertical = Random.value < 0.5f;
      float random = Random.value;
      float rounded = Mathf.Round(Random.value);
      
      Vector2 position = new Vector2
      {
        x = Mathf.Lerp(_screenLimits.Horizontal.Min, _screenLimits.Horizontal.Max, vertical ? rounded : random),
        y = Mathf.Lerp(_screenLimits.Vertical.Min, _screenLimits.Vertical.Max, vertical == false ? rounded : random)
      };
      
      Vector2 direction = Random.onUnitSphere;

      _unitFactory.CreateAsteroid(_config.AsteroidConfig, position, direction.normalized);
      
      RunTimer();
    }

    private void RunTimer()
    {
      _time = Random.Range(_config.Time.Min, _config.Time.Max);
      _stopwatch.Run();
    }
  }
}