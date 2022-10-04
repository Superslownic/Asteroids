using Code.Common;
using UnityEngine;

namespace Code.Weapons
{
  public class LaserView : MonoBehaviour
  {
    [field: SerializeField] public Transformable Transformable { get; private set; }
    [field: SerializeField] public Activator Activator { get; private set; }
  }
}