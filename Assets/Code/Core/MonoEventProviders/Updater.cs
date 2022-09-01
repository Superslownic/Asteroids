using System;
using System.Collections.Generic;
using UnityEngine;

namespace Code.Core.MonoEventProviders
{
  public class Updater : MonoBehaviour
  {
    private readonly List<IUpdateListener> _listeners = new List<IUpdateListener>();
    
    public void AddListener(IUpdateListener listener)
    {
      if(_listeners.Contains(listener))
        throw new InvalidOperationException();
      
      _listeners.Add(listener);
    }
    
    public void RemoveListener(IUpdateListener listener)
    {
      if(_listeners.Contains(listener) == false)
        throw new InvalidOperationException();
      
      _listeners.Remove(listener);
    }

    private void Update()
    {
      for (int i = 0; i < _listeners.Count; i++)
        _listeners[i].Update(Time.deltaTime);
    }
  }
}