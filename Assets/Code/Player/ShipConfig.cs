using Code.Common;
using Code.Weapons;
using UnityEngine;

namespace Code.Player
{
  [CreateAssetMenu]
  public class ShipConfig : ScriptableObject
  {
    [field: SerializeField] public ShipView Prefab { get; private set; }
    [field: SerializeField] public float Acceleration { get; private set; }
    [field: SerializeField] public float Deceleration { get; private set; }
    [field: SerializeField] public float MaxSpeed { get; private set; }
    [field: SerializeField] public float RotationSpeed { get; private set; }
    [field: SerializeField] public SharedContactFilter ContactFilter { get; private set; }
    [field: SerializeField] public BulletGunConfig BulletGunConfig { get; private set; }
    [field: SerializeField] public LaserGunConfig LaserGunConfig { get; private set; }
  }
}