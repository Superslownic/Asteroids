using Code.Helpers;

namespace Code.Unit.Player
{
  public class PlayerModel
  {
    public readonly UnitTransform Transform;
    public readonly Observable<bool> IsAlive = new Observable<bool>(true);

    public PlayerModel(UnitTransform transform)
    {
      Transform = transform;
    }
  }
}
