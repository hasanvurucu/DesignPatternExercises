using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    #region Singleton
    private static GameManager _instance;
    public static GameManager Instance => _instance;
    
    private void Awake()
    {
        if(_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    #endregion

    [SerializeField]
    private int score;

    private float pointSpawnTimer;
    [SerializeField]
    private Transform spawnPoint1, spawnPoint2;

    private void Start()
    {
        score = 0;
        pointSpawnTimer = 0f;
    }

    private void Update()
    {
        PointSpawnCountdown();
    }

    public void IncreaseScore(int score)
    {
        this.score += score;
    }

    private void PointSpawnCountdown()
    {
        pointSpawnTimer += Time.deltaTime;
        if(pointSpawnTimer >= 2f)
        {
            //spawn new point at spawnpoint 1 or 2
            pointSpawnTimer = 0f;
        }
    }
}
