using UnityEngine;

namespace Code.Config
{
  [CreateAssetMenu]
  public class LaserGunConfig : ScriptableObject
  {
    [field: SerializeField] public float Cooldown { get; private set; }
    [field: SerializeField] public int MaxShotCount { get; private set; }
    [field: SerializeField] public LaserConfig LaserConfig { get; private set; }
  }
}