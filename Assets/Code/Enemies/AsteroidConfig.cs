using UnityEngine;

namespace Code.Enemies
{
  [CreateAssetMenu]
  public class AsteroidConfig : ScriptableObject
  {
    [field: SerializeField] public AsteroidView Prefab { get; private set; }
    [field: SerializeField] public int FragmentCount { get; private set; }
    [field: SerializeField] public float MovementSpeed { get; private set; }
  }
}