namespace Code.Infrastructure.MonoEventProviders
{
  public interface IFixedUpdateListener
  {
    void FixedUpdate(float deltaTime);
  }
}