using UnityEngine;

namespace Code.Core.Unit.Player
{
  [CreateAssetMenu]
  public class PlayerConfig : ScriptableObject
  {
    [field: SerializeField] public PlayerView Prefab { get; private set; }
    [field: SerializeField] public float Acceleration { get; private set; }
    [field: SerializeField] public float Deceleration { get; private set; }
    [field: SerializeField] public float MaxSpeed { get; private set; }
    [field: SerializeField] public float RotationSpeed { get; private set; }
  }
}