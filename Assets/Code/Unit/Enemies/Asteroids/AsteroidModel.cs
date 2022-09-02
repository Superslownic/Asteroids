using Code.Helpers;

namespace Code.Unit.Enemies.Asteroids
{
  public class AsteroidModel
  {
    public readonly UnitTransform Transform;
    public readonly Observable<bool> IsAlive = new Observable<bool>(true);

    public AsteroidModel(UnitTransform transform)
    {
      Transform = transform;
    }
  }
}