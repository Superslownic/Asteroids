using UnityEngine;

namespace Code.Core.Unit.Enemies.Asteroids
{
  [CreateAssetMenu]
  public class AsteroidConfig : ScriptableObject
  {
    [field: SerializeField] public AsteroidView Prefab { get; private set; }
    [field: SerializeField] public float MovementSpeed { get; private set; }
  }
}