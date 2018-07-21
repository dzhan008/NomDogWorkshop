using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour {

    public GameObject Bone;
    public GameObject Bomb;

    public float gap = 1.5f;
    public int min_spawn_amount = 0;
    public int max_spawn_amount = 1;
    public float min_speed = 1f;
    public float max_speed = 2f;
    public float spawn_rate = 0.5f;

    public List<GameObject> Objects;

    public Transform SpawnPoint_1;
    public Transform SpawnPoint_2;

	// Use this for initialization
	void Start () {
	}

	
	// Update is called once per frame
	void Update () {
    }

    public void Activate()
    {
        //TODO: Look up Coroutines
        InvokeRepeating("Spawn", 1f, spawn_rate);
    }

    public void DeActivate()
    {
        CancelInvoke();
    }

    public void Spawn()
    {
        int spawnType = Random.Range(0, 3);
        if(spawnType == 0)
        {
            SpawnLeft();
        }
        else if(spawnType == 1)
        {
            SpawnRight();
        }
        else if(spawnType == 2)
        {
            SpawnLeft();
            SpawnRight();
        }
    }

    public void SpawnLeft()
    {
        int spawnIndex = Random.Range(0, Objects.Count);
        int amount = Random.Range(min_spawn_amount, max_spawn_amount + 1);

        for(int i = 0; i < amount; i++)
        {
            Vector3 spawnPoint = new Vector3(SpawnPoint_1.position.x, SpawnPoint_1.position.y + (i * gap), SpawnPoint_1.position.z);
            GameObject obj = Instantiate(Objects[spawnIndex], spawnPoint, SpawnPoint_1.rotation);
            obj.GetComponent<Rigidbody2D>().gravityScale = Random.Range(min_speed, max_speed);
        }
    }

    public void SpawnRight()
    {
        int spawnIndex = Random.Range(0, Objects.Count);
        int amount = Random.Range(min_spawn_amount, max_spawn_amount + 1);

        for (int i = 0; i < amount; i++)
        {
            Vector3 spawnPoint = new Vector3(SpawnPoint_2.position.x, SpawnPoint_2.position.y + (i * gap), SpawnPoint_2.position.z);
            GameObject obj = Instantiate(Objects[spawnIndex], spawnPoint, SpawnPoint_2.rotation);
            obj.GetComponent<Rigidbody2D>().gravityScale = Random.Range(min_speed, max_speed);
        }
    }
}
