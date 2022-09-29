using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Code
{
  public class GameOverWindowView : MonoBehaviour
  {
    [field: SerializeField] public TextMeshProUGUI ScoreLabel { get; private set; }
    [field: SerializeField] public Button RestartButton { get; private set; }
  }
}