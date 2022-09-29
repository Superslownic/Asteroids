namespace Code.UI
{
  public class Score
  {
    private readonly PlayerData _playerData;
    private readonly ScoreView _view;
    private readonly string _format;
    
    public Score(PlayerData playerData, ScoreView view)
    {
      _playerData = playerData;
      _view = view;
      _format = _view.Label.text;
    }

    public void Enable()
    {
      _playerData.Score.OnChanged += UpdateScore;
    }

    public void Disable()
    {
      _playerData.Score.OnChanged -= UpdateScore;
    }

    private void UpdateScore(int score)
    {
      _view.Label.text = string.Format(_format, score);
    }
  }
}