using Code.Model;
using UnityEngine;

namespace Code.Config
{
  [CreateAssetMenu]
  public class AsteroidSpawnerConfig : ScriptableObject
  {
    [field: SerializeField] public FloatRange Cooldown { get; private set; }
    [field: SerializeField] public AsteroidConfig BaseAsteroidConfig { get; private set; }
  }
}