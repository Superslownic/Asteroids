using UnityEngine;

namespace Code.Weapons
{
  [CreateAssetMenu]
  public class LaserConfig : ScriptableObject
  {
    [field: SerializeField] public LaserView Prefab { get; private set; }
    [field: SerializeField] public float Distance { get; private set; }
    [field: SerializeField] public float DestroyTime { get; private set; }
  }
}