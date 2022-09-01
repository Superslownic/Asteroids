using System;
using System.Collections.Generic;

namespace Code.Core.DependencyInjection
{
  public class DiContainer
  {
    private readonly DiContainer _parent;
    private readonly Dictionary<Type, object> _dictionary = new Dictionary<Type, object>();

    public DiContainer(DiContainer parent = null)
    {
      _parent = parent;
    }

    public void Register<T>(T service)
    {
      _dictionary.Add(typeof(T), service);
    }

    public T Resolve<T>()
    {
      if (_dictionary.TryGetValue(typeof(T), out object service))
        return (T) service;

      if(_parent != null)
        return _parent.Resolve<T>();
      
      throw new InvalidOperationException("Service not found");
    }
  }
}