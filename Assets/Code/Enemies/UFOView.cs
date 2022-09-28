using Code.Common;
using UnityEngine;

namespace Code.Enemies
{
  public class UFOView : MonoBehaviour
  {
    [field: SerializeField] public ContactProxy ContactProxy { get; private set; }
    [field: SerializeField] public Transformation Transformation { get; private set; }
    [field: SerializeField] public Activator Activator { get; private set; }
  }
}