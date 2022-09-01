using Code.Core.DependencyInjection;
using Code.Core.MonoEventProviders;
using Code.Core.Unit.Player;
using UnityEngine;

namespace Code.Core.Unit
{
  public class UnitFactory
  {
    private readonly DiContainer _diContainer;

    public UnitFactory(DiContainer diContainer)
    {
      _diContainer = diContainer;
    }

    public void CreatePlayer(PlayerConfig config)
    {
      var updater = _diContainer.Resolve<Updater>();

      var input = new PlayerInput();
      var transform = new UnitTransform(Vector3.zero, Quaternion.identity);
      var movement = new PlayerMovement(transform, input, config);
      var repeater = new UnitPositionRepeater(transform, new FloatRange(-5, 5), new FloatRange(-5, 5));
      var view = Object.Instantiate(config.Prefab);
      var model = new PlayerModel(transform);
      var controller = new PlayerController(model, view);
      
      updater.AddListener(input);
      updater.AddListener(movement);
      updater.AddListener(repeater);
      
      controller.Enable();
    }
  }
}