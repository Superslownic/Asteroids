using UnityEngine;

namespace Code.Common
{
  public class ScreenLimits
  {
    public readonly FloatRange Vertical;
    public readonly FloatRange Horizontal;

    public ScreenLimits(Camera camera)
    {
      Vector2 leftBottom = camera.ScreenToWorldPoint(new Vector3(0, 0));
      Vector2 rightTop = camera.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height));
      
      Vertical = new FloatRange(leftBottom.y, rightTop.y);
      Horizontal = new FloatRange(leftBottom.x, rightTop.x);
    }
    
    public Vector2 Wrap(Vector2 position)
    {
      if (position.x < Horizontal.Min) position.x = Horizontal.Max;
      if (position.x > Horizontal.Max) position.x = Horizontal.Min;
      
      if (position.y < Vertical.Min) position.y = Vertical.Max;
      if (position.y > Vertical.Max) position.y = Vertical.Min;
      
      return position;
    }
  }
}