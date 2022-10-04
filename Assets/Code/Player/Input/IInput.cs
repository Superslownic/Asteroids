namespace Code.Player.Input
{
  public interface IInput
  {
    float Movement { get; }
    float Rotation { get; }
    bool PrimaryAttack { get; }
    bool SecondaryAttack { get; }
  }
}