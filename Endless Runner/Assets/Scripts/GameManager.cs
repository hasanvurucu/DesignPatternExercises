using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
    [SerializeField]
    private GameObject pointPrefab;
    [SerializeField]
    private Text scoreText;

    private void Start()
    {
        score = 0;
        pointSpawnTimer = 0f;
    }

    private void Update()
    {
        PointSpawnCountdown();
        VelocityOfPrefabs();
        SetDynamicTexts();
    }

    public void IncreaseScore(int score)
    {
        this.score += score;
    }

    private void PointSpawnCountdown()
    {
        pointSpawnTimer += Time.deltaTime;
        if(pointSpawnTimer >= 2.5f)
        {
            float rand = Random.Range(0, 2);
            if(rand < 1)
            {
                Instantiate(pointPrefab, spawnPoint1.transform.position, spawnPoint1.transform.rotation);
            }
            else
            {
                Instantiate(pointPrefab, spawnPoint2.transform.position, spawnPoint2.transform.rotation);
            }
            pointSpawnTimer = 0f;
        }
    }
    
    private void VelocityOfPrefabs()
    {
        //Sets the velocity of points in the scene
        GameObject []pointPrefabs = GameObject.FindGameObjectsWithTag(Tags.POINT_TAG);

        if (pointPrefabs.Length < 1)
            return;
        
        for(int i = 0; i < pointPrefabs.Length; i++)
        {
            Rigidbody2D rbTemp = pointPrefabs[i].GetComponent<Rigidbody2D>();
            rbTemp.velocity = Vector2.right * (-1) * 10f;
        }
    }

    private void SetDynamicTexts()
    {
        scoreText.text = "Score: " + score.ToString();
    }
}
