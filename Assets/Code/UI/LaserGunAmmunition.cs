using Code.Weapons;

namespace Code.UI
{
  public class LaserGunAmmunition
  {
    private readonly LaserGunModel _laserGunModel;
    private readonly LaserGunAmmunitionView _view;
    private readonly string _laserCountFormat;

    public LaserGunAmmunition(LaserGunModel laserGunModel, LaserGunAmmunitionView view)
    {
      _laserGunModel = laserGunModel;
      _view = view;
      _laserCountFormat = _view.Label.text;
    }

    public void Enable()
    {
      
      _laserGunModel.CooldownTimer.RemainingTime.OnChanged += UpdateFill;
      _laserGunModel.ShotCount.OnChanged += UpdateLabel;
      
      UpdateFill(_laserGunModel.CooldownTimer.RemainingTime.Value);
      UpdateLabel(_laserGunModel.ShotCount.Value);
    }

    public void Disable()
    {
      _laserGunModel.CooldownTimer.RemainingTime.OnChanged -= UpdateFill;
    }

    private void UpdateFill(float remainingTime)
    {
      _view.Fill.fillAmount = 1 - remainingTime / _laserGunModel.CooldownTime;
    }

    private void UpdateLabel(int shotCount)
    {
      _view.Label.text = string.Format(_laserCountFormat, shotCount, _laserGunModel.MaxShotCount);
    }
  }
}