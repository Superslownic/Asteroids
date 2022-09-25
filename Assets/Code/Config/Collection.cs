using System.Collections.Generic;
using UnityEngine;

namespace Code.Config
{
  public class Collection<T> : ScriptableObject
  {
    [SerializeField] protected List<T> _collection;
  }
}