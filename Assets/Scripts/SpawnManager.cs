using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using TMPro;

public enum WaveType
{
    normal,
    miniboss,
    boss
}

public class Wave
{
    public WaveType waveType;
    public Dictionary<Enemy, int> waveData;
}

public class SpawnManager : Singleton<SpawnManager>
{
    public TMP_Text notification;
    public List<Wave> waveList;
    public List<Transform> spawnPoints;
    public List<GameObject> existingEnemy;
    public int currentWaveIndex;
    public bool isResting;

    public void Start()
    {
        isResting = true;
        currentWaveIndex = 0;
    }

    public void Update()
    {
        if(!isResting)
            notification.text = existingEnemy.Count + " Remaining";
    }

    public IEnumerator SpawnWave(int index)
    {
        isResting = false;

        switch (waveList[index].waveType)
        {
            case WaveType.normal:
                List<Enemy> totalEnemyToSpawn = new List<Enemy>();

                foreach (KeyValuePair<Enemy, int> entry in waveList[index].waveData)
                {
                    for (int i = 0; i < entry.Value; i++)
                    {
                        totalEnemyToSpawn.Add(entry.Key);
                    }
                }

                ShuffleThisList(totalEnemyToSpawn);

                for (int i = 0; i < totalEnemyToSpawn.Count; i++)
                {
                    Vector3 spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Count)].transform.position;
                    GameObject enemy = Instantiate(totalEnemyToSpawn[i].gameObject, spawnPoint, Quaternion.identity);
                    existingEnemy.Add(enemy);
                    yield return new WaitForSeconds(1f);
                }

                currentWaveIndex++;
                StartCoroutine(RestWave());
                break;
        }
    }

    public IEnumerator RestWave()
    {
        isResting = true;
        for(int i = 0; i < 15; i++)
        {
            notification.text = (15 - i) + " second before next wave";
            yield return new WaitForSeconds(1f);
        }
        
        if(currentWaveIndex < waveList.Count)
            StartCoroutine(SpawnWave(currentWaveIndex));
    }

    public static List<T> ShuffleThisList<T>(List<T> list)
    {
        List<T> mylist = new List<T>();

        mylist.AddRange(list);

        List<T> result = new List<T>();

        int random;

        do
        {
            random = Random.Range(0, mylist.Count);
            result.Add(mylist[random]);
            mylist.RemoveAt(random);
        }
        while (mylist.Count > 0);

        return result;
    }
}
