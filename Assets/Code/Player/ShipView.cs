using Code.Common;
using UnityEngine;

namespace Code.Player
{
  public class ShipView : MonoBehaviour
  {
    [field: SerializeField] public Transformable Transformable { get; private set; }
    [field: SerializeField] public Activator Activator { get; private set; }
    [field: SerializeField] public Collider2D Collider { get; private set; }
  }
}