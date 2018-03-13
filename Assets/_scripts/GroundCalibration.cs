using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GroundCalibration : MonoBehaviour {

    GameObject spawner;
    public Text text;

    private void Start()
    {
        spawner = GameObject.FindGameObjectWithTag("spawner");
    }

    //raises ground level for testing purposes
    public void RaiseGround()
    {
        spawner.transform.position = spawner.transform.position + new Vector3(0, 5, 0);
    }

    //Fires ray cast from camera and ideally it would hit the ground mesh and set the height to that
    public void FindGround()
    {
        RaycastHit hit;
        if(Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit))
        {
            Vector3 pos = spawner.transform.position;
            pos.y = hit.point.y;
            spawner.transform.position = pos;
            Debug.Log("Distance to ground is" + hit.distance + "Location: " + hit.point);
        }
    }

    //Cursor goes to nearest wall and so if looking at the ground itll set ground level to be that height
    public void CursorGround()
    {
        float y = GameObject.FindGameObjectWithTag("cursor").transform.position.y;
        Vector3 pos = spawner.transform.position;
        pos.y = y;
        spawner.transform.position = pos;
        text.text = text.text + " GroundSetC!";
    }

    public void SpawnCubeCal()
    {
        GameObject.FindGameObjectWithTag("groundcal").GetComponent<SpawnGroundCal>().SetOn(true);
    }
}
