using Code.Weapons;

namespace Code.UI
{
  public class LaserGunAmmunition
  {
    private readonly LaserGunData _laserData;
    private readonly LaserGunAmmunitionView _view;
    private readonly string _laserCountFormat;

    public LaserGunAmmunition(LaserGunData laserData, LaserGunAmmunitionView view)
    {
      _laserData = laserData;
      _view = view;
      _laserCountFormat = _view.Label.text;
    }

    public void Enable()
    {
      
      _laserData.CooldownTimer.RemainingTime.OnChanged += UpdateFill;
      _laserData.ShotCount.OnChanged += UpdateLabel;
      
      UpdateFill(_laserData.CooldownTimer.RemainingTime.Value);
      UpdateLabel(_laserData.ShotCount.Value);
    }

    public void Disable()
    {
      _laserData.CooldownTimer.RemainingTime.OnChanged -= UpdateFill;
    }

    private void UpdateFill(float remainingTime)
    {
      _view.Fill.fillAmount = 1 - remainingTime / _laserData.CooldownTime;
    }

    private void UpdateLabel(int shotCount)
    {
      _view.Label.text = string.Format(_laserCountFormat, shotCount, _laserData.MaxShotCount);
    }
  }
}