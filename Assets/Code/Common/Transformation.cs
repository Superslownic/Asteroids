using UnityEngine;

namespace Code.Common
{
  public class Transformation : MonoBehaviour
  {
    private ITransformable _transformable;
    
    public void Construct(ITransformable transformable)
    {
      _transformable = transformable;
      
      _transformable.Position.OnChanged += SetPosition;
      _transformable.Rotation.OnChanged += SetRotation;
      
      SetPosition(_transformable.Position.Value);
      SetRotation(_transformable.Rotation.Value);
    }

    private void OnEnable()
    {
      if(_transformable == null)
        return;
      
      SetPosition(_transformable.Position.Value);
      SetRotation(_transformable.Rotation.Value);
    }

    private void OnDestroy()
    {
      if(_transformable == null)
        return;
      
      _transformable.Position.OnChanged -= SetPosition;
      _transformable.Rotation.OnChanged -= SetRotation;
    }

    public void SetPosition(Vector2 value)
    {
      transform.position = value;
    }
    
    public void SetRotation(Quaternion value)
    {
      transform.rotation = value;
    }
  }
}