using UnityEngine;

namespace Code
{
  public class PlayerInput : IUpdateListener
  {
    public float Vertical { get; private set; }
    public float Horizontal { get; private set; }

    public void Update(float deltaTime)
    {
      Vertical = Input.GetAxisRaw("Vertical");
      Horizontal = Input.GetAxisRaw("Horizontal");
    }
  }
}