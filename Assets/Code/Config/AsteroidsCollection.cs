using UnityEngine;

namespace Code.Config
{
  [CreateAssetMenu]
  public class AsteroidsCollection : Collection<AsteroidConfig>
  {
    public bool TryGetFragmentConfig(int id, out AsteroidConfig fragmentConfig)
    {
      var index = _collection.FindIndex(x => x.GetInstanceID() == id);

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