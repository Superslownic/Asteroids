using Code.Core.DependencyInjection;
using Code.Core.MonoEventProviders;
using Code.Core.Unit;
using Code.Core.Unit.Player;
using UnityEngine;

namespace Code
{
  public class Bootstrap : MonoBehaviour
  {
    [SerializeField] private PlayerConfig _playerConfig;
    
    private void Awake()
    {
      var diContainer = new DiContainer();
      
      var updater = gameObject.AddComponent<Updater>();
      var fixedUpdater = gameObject.AddComponent<FixedUpdater>();
      var unitFactory = new UnitFactory(diContainer);
      
      diContainer.Register(updater);
      diContainer.Register(fixedUpdater);
      diContainer.Register(unitFactory);
      
      unitFactory.CreatePlayer(_playerConfig);
    }
  }
}