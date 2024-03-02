using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [Header("Enemy Spawn Settings")]
    [SerializeField] protected EnemyWaveInfos enemyInfos;
    private PoolObjectType[] enemyPrefab;
    private int[] enemyCountsByLevel;
    private int enemyCountPerWave = 10;
    private float coolDown = 10f;
    private float delay = 2f;
    private float radius = 10f;

    [SerializeField] private Timer surviveTimer;

    private Vector3 point = Vector3.zero;
    private List<PoolObjectType> enemies = new List<PoolObjectType>();

    private ObjectPooler pooler;

    private void Start()
    {
        pooler = ObjectPooler.Instance;
    }

    public void Init()
    {
        ActionManager.AiUpdater += UpdatePlayerPos;

        SetProperties();
        FillTheList();
        StartCoroutine(InstantiateEnemies());

        //surviveTimer.Init();
    }

    public void DeInit()
    {
        ActionManager.AiUpdater -= UpdatePlayerPos;
        StopAllCoroutines();
        //surviveTimer.DeInit();
    }

    private void UpdatePlayerPos(Vector3 PlayerPos)
    {
        point = PlayerPos;
    }

    private void FillTheList()
    {
        for (int i = 0; i < enemyPrefab.Length; i++)
        {
            for (int y = 0; y < enemyCountsByLevel[i]; y++)
            {
                enemies.Add(enemyPrefab[i]);
            }
        }
    }

    public IEnumerator InstantiateEnemies()
    {
        yield return new WaitForSeconds(delay);

        while (true)
        {
            for (int i = 0; i < enemyCountPerWave; i++)
            {
                // Distance around the circle
                var radians = 2 * Mathf.PI / enemyCountPerWave * i;

                // direction
                var vertical = Mathf.Sin(radians);
                var horizontal = Mathf.Cos(radians);

                var spawnDir = new Vector3(horizontal, vertical, 0);
                var spawnPos = point + spawnDir * radius; // Radius is just the distance away from the point
                spawnPos += new Vector3(Random.Range(0, 0.5f), Random.Range(0, 0.5f), 0);

                var enemy = pooler.GetPooledObjectWithType(enemies[Random.Range(0, enemies.Count)]);
                enemy.transform.position = spawnPos;
                enemy.gameObject.SetActive(true);
                enemy.Init();
            }

            yield return new WaitForSeconds(coolDown);
        }
    }

    private void SetProperties()
    {
        EnemyWaveInfos.WavePrefs currentLevel = enemyInfos.GetWavePrefs;

        enemyPrefab = currentLevel.enemyPrefab;
        enemyCountsByLevel = currentLevel.enemyCountsByLevel;
        enemyCountPerWave = currentLevel.enemyCountPerWave;
        coolDown = currentLevel.coolDown;
        delay = currentLevel.delay;
        radius = currentLevel.radius;
    }
}
