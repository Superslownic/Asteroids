using Code.View;
using UnityEngine;

namespace Code.Config
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