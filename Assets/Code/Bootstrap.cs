using Code.Common;
using Code.Enemies;
using Code.Infrastructure.DependencyInjection;
using Code.Infrastructure.MonoEventProviders;
using Code.Infrastructure.Timers;
using Code.Player;
using Code.Player.Input;
using Code.UI;
using Code.Weapons;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Code
{
  public class Bootstrap : MonoBehaviour
  {
    [SerializeField] private Camera _camera;
    [SerializeField] private ShipConfig _shipConfig;
    [SerializeField] private InputActionAsset _inputActions;
    [SerializeField] private AsteroidsCollection _asteroidsCollection;
    [SerializeField] private EnemySpawnerConfig _enemySpawnerConfig;
    [SerializeField] private ShipInfoView _shipInfoView;
    [SerializeField] private LaserGunAmmunitionView _laserGunAmmunitionView;
    [SerializeField] private ScoreView _scoreView;
    [SerializeField] private GameOverWindowView _gameOverWindowView;

    private void Awake()
    {
      var diContainer = new DiContainer();
      
      var updater = gameObject.AddComponent<Updater>();
      var fixedUpdater = gameObject.AddComponent<FixedUpdater>();
      
      var input = new AdvancedInputSystem(_inputActions);
      
      var screenLimits = new ScreenLimits(_camera);
      
      var timerFactory = new TimerFactory(this);
      var playerFactory = new ShipFactory(diContainer);
      var asteroidFactory = new EnemyFactory(diContainer);
      var projectileParent = new GameObject("Projectiles").transform;
      var enemyParent = new GameObject("Enemies").transform;
      var bulletFactory = new BulletFactory(diContainer, projectileParent);
      var laserFactory = new LaserFactory(diContainer, projectileParent);
      
      diContainer.Register(updater);
      diContainer.Register(fixedUpdater);
      diContainer.Register<IInput>(input);
      diContainer.Register(screenLimits);
      diContainer.Register(enemyParent, DependencyKey.EnemyParent);
      diContainer.Register(timerFactory);
      diContainer.Register(asteroidFactory);
      diContainer.Register(bulletFactory);
      diContainer.Register(laserFactory);

      var playerData = new PlayerData();
      var ship = playerFactory.Create(_shipConfig);
      
      diContainer.Register(ship);
      
      var enemySpawnerData = new EnemySpawnerData(_enemySpawnerConfig.Cooldown, _enemySpawnerConfig.BaseAsteroidConfig,
        _enemySpawnerConfig.UFOConfig, _enemySpawnerConfig.AsteroidSpawnProbability);
      var enemySpawner = new EnemySpawner(enemySpawnerData, _asteroidsCollection, asteroidFactory, screenLimits, updater);

      var shipInfo = new ShipInfo(diContainer.Resolve<ShipModel>(), _shipInfoView);
      shipInfo.Enable();
      
      var laserAmmunition = new LaserGunAmmunition(diContainer.Resolve<LaserGunModel>(), _laserGunAmmunitionView);
      laserAmmunition.Enable();
      
      var score = new Score(playerData, _scoreView);
      score.Enable();
      
      var gameOverWindow = new GameOverWindow(_gameOverWindowView);

      var level = new Level(ship, enemySpawner, playerData, gameOverWindow);
      level.Start();
    }
  }
}