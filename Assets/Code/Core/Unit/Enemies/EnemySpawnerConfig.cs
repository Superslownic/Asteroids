﻿using Code.Core.Unit.Enemies.Asteroids;
using UnityEngine;

namespace Code.Core.Unit.Enemies
{
  [CreateAssetMenu]
  public class EnemySpawnerConfig : ScriptableObject
  {
    [field: SerializeField] public FloatRange Time { get; private set; }
    [field: SerializeField] public AsteroidConfig AsteroidConfig { get; private set; }
  }
}