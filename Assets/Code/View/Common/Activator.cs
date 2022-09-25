using UnityEngine;

namespace Code.View.Common
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