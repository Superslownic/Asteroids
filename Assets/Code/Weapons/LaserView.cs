using Code.Common;
using UnityEngine;

namespace Code.Weapons
{
  public class LaserView : MonoBehaviour
  {
    [field: SerializeField] public Transformation Transformation { get; private set; }
    [field: SerializeField] public Activator Activator { get; private set; }
  }
}