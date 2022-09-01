﻿using System;

namespace Code.Core.Unit
{
  [Serializable]
  public struct FloatRange
  {
    public float Min;
    public float Max;

    public FloatRange(float min, float max)
    {
      Min = min;
      Max = max;
    }
  }
}