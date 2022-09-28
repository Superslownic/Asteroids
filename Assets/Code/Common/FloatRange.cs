using System;

namespace Code.Common
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

    public readonly float GetRandom()
    {
      return UnityEngine.Random.Range(Min, Max);
    }
  }
}