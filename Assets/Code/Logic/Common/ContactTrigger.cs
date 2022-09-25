using UnityEngine;

namespace Code.Logic.Common
{
  public class ContactTrigger
  {
    private readonly Collider2D _collider;
    private readonly ContactFilter2D _contactFilter;
    private readonly Collider2D[] _buffer;

    public ContactTrigger(Collider2D collider, ContactFilter2D contactFilter, int bufferSize)
    {
      _collider = collider;
      _contactFilter = contactFilter;
      _buffer = new Collider2D[bufferSize];
    }

    public bool HasContact()
    {
      if (Physics2D.OverlapCollider(_collider, _contactFilter, _buffer) == 0)
        return false;
      
      if (_buffer[0].TryGetComponent(out IContactHandler contactHandler) == false)
        return false;
      
      contactHandler.OnHit();
      return true;
    }
  }
}