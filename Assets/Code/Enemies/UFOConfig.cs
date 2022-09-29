using UnityEngine;

namespace Code.Enemies
{
  [CreateAssetMenu]
  public class UFOConfig : ScriptableObject
  {
    [field: SerializeField] public UFOView Prefab { get; private set; }
    [field: SerializeField] public float MovementSpeed { get; private set; }
    [field: SerializeField] public int Points { get; private set; }
  }
}