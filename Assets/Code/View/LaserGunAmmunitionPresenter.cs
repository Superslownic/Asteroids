namespace Code.View
{
  /*public class LaserGunAmmunitionPresenter
  {
    private readonly LaserGunData _laserData;
    private readonly Image _fill;
    private readonly TextMeshProUGUI _label;
    
    public void Enable()
    {
      _laserData.CooldownTimer.RemainingTime.OnChanged += UpdateFill;
      _laserData.ShotCount.OnChanged += UpdateLabel;
    }

    public void Disable()
    {
      _laserData.CooldownTimer.RemainingTime.OnChanged -= UpdateFill;
    }

    private void UpdateFill(float remainingTime)
    {
      _fill.fillAmount = 1 - remainingTime / _laserData.CooldownTime;
    }

    private void UpdateLabel(int shotCount)
    {
      _label.text = $"{shotCount} / {_laserData.MaxShotCount}";
    }
  }*/
}