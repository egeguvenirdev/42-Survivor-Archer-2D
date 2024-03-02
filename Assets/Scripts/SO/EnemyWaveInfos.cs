using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Wave Settings", menuName = "New Wave")]

public class EnemyWaveInfos : ScriptableObject
{
    [SerializeField] private WavePrefs wavePrefs;

    public WavePrefs GetWavePrefs { get => wavePrefs; set => wavePrefs = value; }

    [System.Serializable]
    public class WavePrefs
    {
        [Header("Stats")]
        public PoolObjectType[] enemyPrefab;
        public int[] enemyCountsByLevel;
        public int enemyCountPerWave = 10;
        public float coolDown = 10f;
        public float delay = 2f;
        public float radius = 10f;
    }
}
