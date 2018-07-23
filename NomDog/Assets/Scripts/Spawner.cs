using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour {

    //The gap between two spawned objects.
    public float GAP = 0.5f;
    //The minimum amount of objects to spawn.
    public int MIN_SPAWNED_OBJECTS = 0;
    public int MAXED_SPAWNED_OBJECTS = 3;
    public float SPAWN_RATE = 1.5f;
    public float INITIAL_SPAWN_RATE = 0.5f;
    public float MIN_SPEED = 0.5f;
    public float MAX_SPEED = 2f;
    public float INITIAL_MIN_SPEED = 6f;
    public float INITIAL_MAX_SPEED = 8f;
    public float HUSTLE_MODE_MIN_SPEED = 3f;
    public float HUSTLE_MODE_MAX_SPEED = 5F;
    //Since bombs are the only other object in the game, it spawns the rest of the time.
    [Range(0f, 1f)]
    public float TREAT_SPAWN_CHANCE;

    public List<GameObject> Objects;

    public Transform SpawnPoint_1;
    public Transform SpawnPoint_2;

    private GameObject LastLeftObject;
    private GameObject LastRightObject;

    private GameObject ObjectHolder;

    // Use this for initialization
    void Start () {
        ObjectHolder = new GameObject();
        ObjectHolder.name = "Object Container";
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    IEnumerator Spawn()
    {
        while(true)
        {
            if (SpawnObjects())
            {
                yield return new WaitForSeconds(SPAWN_RATE);
            }
            else
            {
                yield return new WaitForSeconds(Time.deltaTime);
            }
        }

    }

    public void Activate(bool hustle)
    {
        SPAWN_RATE = INITIAL_SPAWN_RATE;
        if (hustle)
        {
            MIN_SPEED = HUSTLE_MODE_MIN_SPEED;
            MAX_SPEED = HUSTLE_MODE_MAX_SPEED;
        }
        else
        {
            MIN_SPEED = INITIAL_MIN_SPEED;
            MAX_SPEED = INITIAL_MAX_SPEED;
        }


        StartCoroutine("Spawn");
    }

    public void DeActivate()
    {
        StopCoroutine("Spawn");
    }

    bool SpawnObjects()
    {
        int mode = Random.Range(0, 3);
        if(mode == 0)
        {
            if (LastLeftObject != null)
                return false;
            SpawnLeft();
        }
        else if(mode == 1)
        {
            if (LastRightObject != null)
                return false;
            SpawnRight();
        }
        else if(mode == 2)
        {
            if (LastLeftObject != null || LastRightObject != null)
                return false;

            SpawnLeft();
            SpawnRight();
        }

        return true;

    }

    void SpawnLeft()
    {
        //Spawn 
        int amount = Random.Range(MIN_SPAWNED_OBJECTS, MAXED_SPAWNED_OBJECTS + 1);
        float velocity = Random.Range(-MIN_SPEED, -MAX_SPEED);
        GameObject obj = null;

        for (int i = 0; i < amount; i++)
        {
            int object_index = PickObject();
            Vector3 TempSpawn1 = new Vector3(SpawnPoint_1.position.x, SpawnPoint_1.position.y + (i * GAP), SpawnPoint_1.position.z);
            obj = Instantiate(Objects[object_index], TempSpawn1, SpawnPoint_1.rotation);
            obj.GetComponent<FallingObject>().velocity = velocity;
            obj.transform.parent = ObjectHolder.transform;
        }
        LastLeftObject = obj;
    }

    void SpawnRight()
    {
        //Spawn 
        int amount = Random.Range(MIN_SPAWNED_OBJECTS, MAXED_SPAWNED_OBJECTS + 1);
        float velocity = Random.Range(-MIN_SPEED, -MAX_SPEED);
        GameObject obj = null;

        for (int i = 0; i < amount; i++)
        {
            int object_index = PickObject();
            Vector3 TempSpawn1 = new Vector3(SpawnPoint_2.position.x, SpawnPoint_2.position.y + (i * GAP), SpawnPoint_2.position.z);
            obj = Instantiate(Objects[object_index], TempSpawn1, SpawnPoint_2.rotation);
            obj.GetComponent<FallingObject>().velocity = velocity;
            obj.transform.parent = ObjectHolder.transform;
        }
        LastRightObject = obj;

    }

    int PickObject()
    {
        float rand_val = Random.value;
        if(rand_val <= TREAT_SPAWN_CHANCE)
        {
            //Spawn Treat
            return 0;
        }
        else
        {
            //Spawn Bomb
            return 1;
        }
    }

    public void ClearObjects()
    {
        foreach(Transform obj in ObjectHolder.transform)
        {
            Destroy(obj.gameObject);
        }
    }
}
