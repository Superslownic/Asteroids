using UnityEngine;

namespace Code.Unit
{
  public class CollisionDetector
  {
    private readonly Collider2D _collider;
    private readonly ContactFilter2D _contactFilter;
    private readonly Collider2D[] _buffer;

    public CollisionDetector(Collider2D collider, ContactFilter2D contactFilter, int bufferSize)
    {
      _collider = collider;
      _contactFilter = contactFilter;
      _buffer = new Collider2D[bufferSize];
    }

    public int Update()
    {
      return Physics2D.OverlapCollider(_collider, _contactFilter, _buffer);
    }
  }
}