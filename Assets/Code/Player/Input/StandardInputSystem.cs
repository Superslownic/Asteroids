namespace Code.Player.Input
{
  public class StandardInputSystem : IInput
  {
    public float Movement => UnityEngine.Input.GetAxisRaw("Vertical");
    public float Rotation => UnityEngine.Input.GetAxisRaw("Horizontal");
    public bool PrimaryAttack => UnityEngine.Input.GetMouseButton(0);
    public bool SecondaryAttack => UnityEngine.Input.GetMouseButtonDown(1);
  }
}