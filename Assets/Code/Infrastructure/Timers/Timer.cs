using System;
using System.Collections;
using Code.Model;
using UnityEngine;

namespace Code.Infrastructure.Timers
{
  public class Timer
  {
    private readonly Observable<float> _remainingTime = new Observable<float>();
    private readonly MonoBehaviour _context;

    private Coroutine _current;

    public Timer(MonoBehaviour context)
    {
      _context = context;
    }

    public IReadOnlyObservable<float> RemainingTime =>
      _remainingTime;

    public bool IsOver =>
      _remainingTime.Value <= 0;

    public Coroutine Run(float time, Action callback)
    {
      if(_current != null)
        _context.StopCoroutine(_current);
      
      _current = _context.StartCoroutine(Routine(time, callback));
      return _current;
    }

    public void Stop()
    {
      if(_current != null)
        _context.StopCoroutine(_current);
    }

    private IEnumerator Routine(float time, Action callback)
    {
      _remainingTime.Value = time;

      while (_remainingTime.Value > 0)
      {
        _remainingTime.Value -= Time.deltaTime;
        yield return null;
      }

      _remainingTime.Value = 0;
      callback?.Invoke();
    }
  }
}