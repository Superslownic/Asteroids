using Code.Common;
using UnityEngine;

namespace Code.Weapons
{
  [CreateAssetMenu]
  public class BulletConfig : ScriptableObject
  {
    [field: SerializeField] public BulletView Prefab { get; private set; }
    [field: SerializeField] public float MovementSpeed { get; private set; }
    [field: SerializeField] public float DeactivateTime { get; private set; }
    [field: SerializeField] public SharedContactFilter ContactFilter { get; private set; }
  }
}