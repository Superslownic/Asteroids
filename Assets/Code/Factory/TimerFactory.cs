using Code.Infrastructure.Timers;
using UnityEngine;

namespace Code.Factory
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