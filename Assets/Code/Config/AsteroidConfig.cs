using Code.View;
using UnityEngine;

namespace Code.Config
{
  [CreateAssetMenu]
  public class AsteroidConfig : ScriptableObject
  {
    [field: SerializeField] public AsteroidView Prefab { get; private set; }
    [field: SerializeField] public int FragmentCount { get; private set; }
    [field: SerializeField] public float MovementSpeed { get; private set; }
  }
}