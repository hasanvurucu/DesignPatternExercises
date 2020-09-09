using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolScript : MonoBehaviour
{
    [SerializeField] private ShotScript shotPrefab;

    private Queue<ShotScript> shots = new Queue<ShotScript>();

    public static PoolScript Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
    }

    public ShotScript Get()
    {
        if(shots.Count == 0)
        {
            AddShots(1);
        }

        return shots.Dequeue();
    }

    private void AddShots(int count)
    {
        for(int i = 0; i < count; i++)
        {
            ShotScript shot = Instantiate(shotPrefab);
            shot.gameObject.SetActive(false);
            shots.Enqueue(shot);
        }
    }

    public void ReturnToPool(ShotScript shot)
    {
        shot.gameObject.SetActive(false);
        shots.Enqueue(shot);
    }
}
