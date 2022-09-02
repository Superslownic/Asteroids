using UnityEngine;

namespace Code.Unit.Player
{
  public class PlayerInput
  {
    public float Vertical { get; private set; }
    public float Horizontal { get; private set; }

    public void Update()
    {
      Vertical = Input.GetAxisRaw("Vertical");
      Horizontal = Input.GetAxisRaw("Horizontal");
    }
  }
}