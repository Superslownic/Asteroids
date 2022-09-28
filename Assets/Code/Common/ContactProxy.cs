using UnityEngine;

namespace Code.Common
{
  public class ContactProxy : MonoBehaviour, IContactHandler
  {
    private IContactHandler _contactHandler;

    public void Construct(IContactHandler contactHandler)
    {
      _contactHandler = contactHandler;
    }

    public void OnHit()
    {
      _contactHandler.OnHit();
    }
  }
}