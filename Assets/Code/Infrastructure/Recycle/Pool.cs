using System.Collections.Generic;
using System.Linq;

namespace Code.Infrastructure.Recycle
{
  public class Pool<T>
  {
    private readonly HashSet<T> _freeInstances = new HashSet<T>();

    public bool TryGet(out T instance)
    {
      if (_freeInstances.Count == 0)
      {
        instance = default;
        return false;
      }
      
      instance = _freeInstances.First();
      _freeInstances.Remove(instance);
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