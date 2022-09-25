using Code.View.Common;
using UnityEngine;

namespace Code.View
{
  public class ShipView : MonoBehaviour
  {
    [field: SerializeField] public Transformation Transformation { get; private set; }
    [field: SerializeField] public Activator Activator { get; private set; }
    [field: SerializeField] public Collider2D Collider { get; private set; }
  }
}