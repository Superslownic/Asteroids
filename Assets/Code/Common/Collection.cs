using System.Collections.Generic;
using UnityEngine;

namespace Code.Common
{
  public class Collection<T> : ScriptableObject
  {
    [SerializeField] protected T[] _collection;
  }
}