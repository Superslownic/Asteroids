using TMPro;
using UnityEngine;

namespace Code.View
{
  public class ShipInfoView : MonoBehaviour
  {
    [field: SerializeField] public TextMeshProUGUI PositionLabel { get; private set; }
    [field: SerializeField] public TextMeshProUGUI SpeedLabel { get; private set; }
    [field: SerializeField] public TextMeshProUGUI AngleLabel { get; private set; }
  }
}