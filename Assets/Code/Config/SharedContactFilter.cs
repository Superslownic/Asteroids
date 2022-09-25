﻿using UnityEngine;

namespace Code.Config
{
  [CreateAssetMenu]
  public class SharedContactFilter : ScriptableObject
  {
    [field: SerializeField] public ContactFilter2D Value { get; private set; }

    public static implicit operator ContactFilter2D(SharedContactFilter shared) =>
      shared.Value;
  }
}