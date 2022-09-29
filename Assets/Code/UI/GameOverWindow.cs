using System;

namespace Code.UI
{
  public class GameOverWindow
  {
    private readonly GameOverWindowView _view;
    private readonly string _scoreLabelFormat;
    
    private Action _restartButtonCallback;

    public GameOverWindow(GameOverWindowView view)
    {
      _view = view;
      _scoreLabelFormat = _view.ScoreLabel.text;
    }

    public void Open(int score, Action restartButtonCallback)
    {
      if(_view.gameObject.activeSelf)
        return;
      
      _restartButtonCallback = restartButtonCallback;
      _view.ScoreLabel.text = string.Format(_scoreLabelFormat, score);
      _view.RestartButton.onClick.AddListener(_restartButtonCallback.Invoke);
      _view.gameObject.SetActive(true);
    }

    public void Close()
    {
      if(_view.gameObject.activeSelf == false)
        return;
      
      _view.RestartButton.onClick.RemoveListener(_restartButtonCallback.Invoke);
      _view.gameObject.SetActive(false);
    }
  }
}