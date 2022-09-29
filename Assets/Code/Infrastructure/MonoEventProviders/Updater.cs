using System.Collections.Generic;
using UnityEngine;

namespace Code.Infrastructure.MonoEventProviders
{
  public class Updater : MonoBehaviour
  {
    private readonly List<IUpdateListener> _listeners = new List<IUpdateListener>();
    
    public void AddListener(params IUpdateListener[] listeners)
    {
      foreach (IUpdateListener listener in listeners)
        if(_listeners.Contains(listener) == false)
          _listeners.Add(listener);
    }
    
    public void RemoveListener(params IUpdateListener[] listeners)
    {
      foreach (IUpdateListener listener in listeners)
        if(_listeners.Contains(listener))
          _listeners.Remove(listener);
    }

    private void Update()
    {
      for (int i = 0; i < _listeners.Count; i++)
        _listeners[i].Update(Time.deltaTime);
    }
  }
}