using Code.View;
using UnityEngine;

namespace Code.Config
{
  [CreateAssetMenu]
  public class LaserConfig : ScriptableObject
  {
    [field: SerializeField] public LaserView Prefab { get; private set; }
    [field: SerializeField] public float Distance { get; private set; }
    [field: SerializeField] public float DestroyTime { get; private set; }
  }
}