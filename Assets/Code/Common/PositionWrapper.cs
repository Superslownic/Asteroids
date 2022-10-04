namespace Code.Common
{
  public class PositionWrapper
  {
    private readonly Transformation _transformation;
    private readonly ScreenLimits _screenLimits;

    public PositionWrapper(Transformation transformation, ScreenLimits screenLimits)
    {
      _transformation = transformation;
      _screenLimits = screenLimits;
    }

    public void Execute()
    {
      _transformation.Position.Value = _screenLimits.Wrap(_transformation.Position.Value);
    }
  }
}