using UnityEngine;

namespace Code.Core.Unit
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
  }
}