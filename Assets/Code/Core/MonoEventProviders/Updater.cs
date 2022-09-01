using System;
using System.Collections.Generic;
using UnityEngine;

namespace Code
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
      _listeners.ForEach(x => x.Update(Time.deltaTime));
    }
  }
}