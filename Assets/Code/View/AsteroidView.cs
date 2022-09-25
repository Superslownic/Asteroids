using Code.View.Common;
using UnityEngine;

namespace Code.View
{
  public class AsteroidView : MonoBehaviour
  {
    [field: SerializeField] public ContactProxy ContactProxy { get; private set; }
    [field: SerializeField] public Transformation Transformation { get; private set; }
    [field: SerializeField] public Activator Activator { get; private set; }
  }
}