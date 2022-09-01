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
      var updater = gameObject.AddComponent<Updater>();
      var fixedUpdater = gameObject.AddComponent<FixedUpdater>();

      var transform = new UnitTransform(Vector3.zero, Quaternion.identity);
      var input = new PlayerInput();
      var movement = new PlayerMovement(transform, input, _playerConfig);
      var repeater = new UnitPositionRepeater(transform, new FloatRange(-5, 5), new FloatRange(-5, 5));
      var view = Instantiate(_playerConfig.Prefab);
      var model = new PlayerModel(transform);
      var controller = new PlayerController(model, view);
      
      updater.AddListener(input);
      updater.AddListener(movement);
      updater.AddListener(repeater);
      
      controller.Enable();
    }
  }
}