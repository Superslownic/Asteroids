using Code.Enemies;
using Code.Player;
using Code.UI;
using UnityEngine;

namespace Code
{
  public class Level
  {
    private readonly Ship _ship;
    private readonly EnemySpawner _enemySpawner;
    private readonly PlayerData _playerData;
    private readonly GameOverWindow _gameOverWindow;

    public Level(Ship ship, EnemySpawner enemySpawner, PlayerData playerData, GameOverWindow gameOverWindow)
    {
      _ship = ship;
      _enemySpawner = enemySpawner;
      _playerData = playerData;
      _gameOverWindow = gameOverWindow;
    }

    public void Start()
    {
      _gameOverWindow.Close();
      
      _playerData.Score.Value = 0;
      
      _ship.Enable();
      _ship.SetPosition(Vector2.zero);
      _ship.SetVelocity(Vector2.zero);
      _ship.SetRotation(Quaternion.identity);
      _ship.OnDestroy += HandleShipDestroyed;

      _enemySpawner.Clear();
      _enemySpawner.ResetTimer();
      _enemySpawner.Enable();
      _enemySpawner.OnEnemyDestroyed += HandleEnemyDestroyed;
    }

    private void HandleEnemyDestroyed(int points)
    {
      _playerData.Score.Value += points;
    }

    private void HandleShipDestroyed()
    {
      _ship.Disable();
      
      _enemySpawner.Disable();
      _enemySpawner.OnEnemyDestroyed -= HandleEnemyDestroyed;
      
      _gameOverWindow.Open(_playerData.Score.Value, Start);
    }
  }
}