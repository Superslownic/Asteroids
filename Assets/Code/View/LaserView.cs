using Code.View.Common;
using UnityEngine;

namespace Code.View
{
  public class LaserView : MonoBehaviour
  {
    [field: SerializeField] public Transformation Transformation { get; private set; }
    [field: SerializeField] public Activator Activator { get; private set; }
  }
}