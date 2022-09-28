using UnityEngine;

namespace Code.Weapons
{
  [CreateAssetMenu]
  public class LaserGunConfig : ScriptableObject
  {
    [field: SerializeField] public float Cooldown { get; private set; }
    [field: SerializeField] public int MaxShotCount { get; private set; }
    [field: SerializeField] public LaserConfig LaserConfig { get; private set; }
  }
}