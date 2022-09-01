using UnityEngine;

namespace Code.Unit.Enemies.Asteroids
{
  public class AsteroidView : MonoBehaviour
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