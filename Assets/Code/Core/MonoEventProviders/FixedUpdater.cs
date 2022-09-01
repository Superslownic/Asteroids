using System;
using System.Collections.Generic;
using UnityEngine;

namespace Code.Core.MonoEventProviders
{
  public class FixedUpdater : MonoBehaviour
  {
    private readonly List<IFixedUpdateListener> _listeners = new List<IFixedUpdateListener>();
    
    public void AddListener(IFixedUpdateListener listener)
    {
      if(_listeners.Contains(listener))
        throw new InvalidOperationException();
      
      _listeners.Add(listener);
    }
    
    public void RemoveListener(IFixedUpdateListener listener)
    {
      if(_listeners.Contains(listener) == false)
        throw new InvalidOperationException();
      
      _listeners.Remove(listener);
    }

    private void FixedUpdate()
    {
      for (int i = 0; i < _listeners.Count; i++)
        _listeners[i].FixedUpdate(Time.fixedDeltaTime);
    }
  }
}