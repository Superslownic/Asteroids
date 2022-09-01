using UnityEngine;

namespace Code.Core.Unit.Player
{
  public class PlayerView : MonoBehaviour
  {
    public void SetPosition(Vector3 value)
    {
      transform.position = value;
    }
    
    public void SetRotation(Quaternion value)
    {
      transform.rotation = value;
    }
  }
}