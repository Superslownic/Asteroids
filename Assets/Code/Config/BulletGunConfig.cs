using UnityEngine;

namespace Code.Config
{
  [CreateAssetMenu]
  public class BulletGunConfig : ScriptableObject
  {
    [field: SerializeField] public float Cooldown { get; private set; }
    [field: SerializeField] public BulletConfig BulletConfig { get; private set; }
  }
}