namespace Code.Infrastructure.Timers
{
  public class Countdown
  {
    private readonly Stopwatch _stopwatch = new Stopwatch();
    
    private float _time;

    public bool IsOver =>
      _stopwatch.CurrentTime > _time;
    
    public float RemainingTime =>
      _time - _stopwatch.CurrentTime;

    public void Run(float time)
    {
      _time = time;
      _stopwatch.Run();
    }
  }
}