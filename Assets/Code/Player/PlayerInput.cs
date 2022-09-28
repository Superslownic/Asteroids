using UnityEngine;

namespace Code.Player
{
  public class PlayerInput
  {
    public float Movement => Input.GetAxisRaw("Vertical");
    public float Rotation => Input.GetAxisRaw("Horizontal");
    public bool PrimaryAttack => Input.GetMouseButton(0);
    public bool SecondaryAttack => Input.GetMouseButtonDown(1);
  }
}