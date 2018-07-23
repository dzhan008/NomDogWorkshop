using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoRotator : MonoBehaviour {

    public float MIN_ROTATE_TIME;
    public float MAX_ROTATE_TIME;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}


    IEnumerator Rotate()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(MIN_ROTATE_TIME, MAX_ROTATE_TIME));
            ChangeOrientation();
        }

    }

    public void ChangeOrientation()
    {
        Screen.orientation = (ScreenOrientation)Random.Range(1, 5);
    }

    public void Activate()
    {
        StartCoroutine("Rotate");
    }

    public void DeActivate()
    {
        StopCoroutine("Rotate");
    }

}
