using System.Collections.Generic;

namespace Code.Infrastructure.Recycle
{
  public class Pool<T>
  {
    private readonly List<T> _freeInstances = new List<T>();

    public bool TryGet(out T instance)
    {
      if (_freeInstances.Count == 0)
      {
        instance = default;
        return false;
      }
      
      instance = _freeInstances[0];
      _freeInstances.RemoveAt(0);
      return true;
    }

    public void Return(T instance)
    {
      if(_freeInstances.Contains(instance))
        return;
      
      _freeInstances.Add(instance);
    }
  }
}