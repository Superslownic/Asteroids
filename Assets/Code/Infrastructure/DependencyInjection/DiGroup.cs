using System.Collections.Generic;

namespace Code.Infrastructure.DependencyInjection
{
  public class DiGroup
  {
    private readonly Dictionary<string, object> _dictionary = new Dictionary<string, object>();

    public bool TryResolve(string key, out object result)
    {
      return _dictionary.TryGetValue(key, out result);
    }

    public void Register(string key, object instance)
    {
      _dictionary.Add(key, instance);
    }
  }
}