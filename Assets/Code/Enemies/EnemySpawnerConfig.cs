using Code.Common;
using UnityEngine;

namespace Code.Enemies
{
  [CreateAssetMenu]
  public class EnemySpawnerConfig : ScriptableObject
  {
    [field: SerializeField] public FloatRange Cooldown { get; private set; }
    [field: SerializeField] public AsteroidConfig BaseAsteroidConfig { get; private set; }
    [field: SerializeField] public UFOConfig UFOConfig { get; private set; }
    [field: SerializeField] public int AsteroidSpawnProbability { get; private set; }
  }
}