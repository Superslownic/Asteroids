using System;
using Code.Common;
using UnityEngine;

namespace Code.Enemies
{
  [CreateAssetMenu]
  public class AsteroidsCollection : Collection<AsteroidConfig>
  {
    public bool TryGetFragmentConfig(int id, out AsteroidConfig fragmentConfig)
    {
      var index = Array.FindIndex(_collection, x => x.GetInstanceID() == id);

      if (index == 0)
      {
        fragmentConfig = default;
        return false;
      }

      fragmentConfig = _collection[index - 1];
      return true;
    }
  }
}