using UnityEngine;

namespace Code.Common
{
  public class Transformable : MonoBehaviour
  {
    private Transformation _transformation;
    
    public void Construct(Transformation transformation)
    {
      _transformation = transformation;
      
      _transformation.Position.OnChanged += SetPosition;
      _transformation.Rotation.OnChanged += SetRotation;
      
      SetPosition(_transformation.Position.Value);
      SetRotation(_transformation.Rotation.Value);
    }

    private void OnEnable()
    {
      if(_transformation == null)
        return;
      
      SetPosition(_transformation.Position.Value);
      SetRotation(_transformation.Rotation.Value);
    }

    private void OnDestroy()
    {
      if(_transformation == null)
        return;
      
      _transformation.Position.OnChanged -= SetPosition;
      _transformation.Rotation.OnChanged -= SetRotation;
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