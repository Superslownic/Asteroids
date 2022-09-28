using UnityEngine;

namespace Code.Common
{
  public class Activator : MonoBehaviour
  {
    public void Enable()
    {
      gameObject.SetActive(true);
    }
    
    public void Disable()
    {
      gameObject.SetActive(false);
    }
  }
}