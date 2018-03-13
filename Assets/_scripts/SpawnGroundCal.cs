using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnGroundCal : MonoBehaviour {
    private bool on;
    private GameObject spawner;
	// Use this for initialization
	void Start () {
        on = false;
        spawner = GameObject.FindGameObjectWithTag("spawner");
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void SetOn(bool tf)
    {
        on = tf;
    }

    void OnCollisionEnter(Collision col)
    {
        if (on)
        {
            Vector3 pos = col.contacts[0].point;
            Vector3 temp = spawner.transform.position;
            temp.y = pos.y;
            spawner.transform.position = temp;
            on = false;
        }
    }
}
