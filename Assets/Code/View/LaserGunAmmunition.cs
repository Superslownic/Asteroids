using Code.Model;

namespace Code.View
{
  public class LaserGunAmmunition
  {
    private readonly LaserGunData _laserData;
    private readonly LaserGunAmmunitionView _view;

    public LaserGunAmmunition(LaserGunData laserData, LaserGunAmmunitionView view)
    {
      _laserData = laserData;
      _view = view;
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
      _view.Label.text = $"{shotCount} / {_laserData.MaxShotCount}";
    }
  }
}