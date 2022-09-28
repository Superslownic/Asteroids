using UnityEngine;

namespace Code.Infrastructure.Timers
{
  public class TimerFactory
  {
    private readonly MonoBehaviour _context;

    public TimerFactory(MonoBehaviour context)
    {
      _context = context;
    }

    public Timer Create()
    {
      return new Timer(_context);
    }
  }
}