using Code.Common;
using UnityEngine;

namespace Code.Enemies
{
  public class UFOView : MonoBehaviour
  {
    [field: SerializeField] public ContactProxy ContactProxy { get; private set; }
    [field: SerializeField] public Transformable Transformable { get; private set; }
    [field: SerializeField] public Activator Activator { get; private set; }
  }
}