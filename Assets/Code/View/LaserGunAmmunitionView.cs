﻿using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Code.View
{
  public class LaserGunAmmunitionView : MonoBehaviour
  {
    [field: SerializeField] public Image Fill { get; private set; }
    [field: SerializeField] public TextMeshProUGUI Label { get; private set; }
  }
}