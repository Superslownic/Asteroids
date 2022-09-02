using UnityEngine;

namespace Code.Unit.Player
{
  public class PlayerView : MonoBehaviour
  {
    [field: SerializeField] public Collider2D Collider { get; private set; }

    public void SetPosition(Vector2 value)
    {
      transform.position = value;
    }
    
    public void SetRotation(Quaternion value)
    {
      transform.rotation = value;
    }
  }
}