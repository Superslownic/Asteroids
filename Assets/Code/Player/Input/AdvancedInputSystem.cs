using UnityEngine;
using UnityEngine.InputSystem;

namespace Code.Player.Input
{
  public class AdvancedInputSystem : IInput
  {
    private readonly InputAction _accelerate;
    private readonly InputAction _rotate;
    private readonly InputAction _primaryAttack;
    private readonly InputAction _secondaryAttack;

    public AdvancedInputSystem(InputActionAsset inputActions)
    {
      _accelerate = inputActions["Accelerate"];
      _rotate = inputActions["Rotate"];
      _primaryAttack = inputActions["Primary Attack"];
      _secondaryAttack = inputActions["Secondary Attack"];
    }
    
    public float Movement => _accelerate.ReadValue<float>();
    public float Rotation => _rotate.ReadValue<Vector2>().x;
    public bool PrimaryAttack => _primaryAttack.ReadValue<float>() > 0;
    public bool SecondaryAttack => _secondaryAttack.ReadValue<float>() > 0;
  }
}