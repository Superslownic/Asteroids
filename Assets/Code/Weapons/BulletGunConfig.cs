using UnityEngine;

namespace Code.Weapons
{
  [CreateAssetMenu]
  public class BulletGunConfig : ScriptableObject
  {
    [field: SerializeField] public float Cooldown { get; private set; }
    [field: SerializeField] public BulletConfig BulletConfig { get; private set; }
  }
}