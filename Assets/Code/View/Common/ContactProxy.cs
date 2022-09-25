using Code.Logic.Common;
using UnityEngine;

namespace Code.View.Common
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