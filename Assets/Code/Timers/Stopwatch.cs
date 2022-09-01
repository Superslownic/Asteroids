namespace Code.Timers
{
  public class Stopwatch
  {
    private float _startTime;

    public float CurrentTime =>
      UnityEngine.Time.time - _startTime;

    public void Run() =>
      _startTime = UnityEngine.Time.time;
  }
}