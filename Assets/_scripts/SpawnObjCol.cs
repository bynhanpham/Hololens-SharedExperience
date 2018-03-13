using HoloToolkit.Unity;
using UnityEngine;
//using HoloToolkit.Unity.SpatialUnderstandingDll.Imports;

public class SpawnObjCol : MonoBehaviour {

    private GameObject spawner;
    //private Vector3 startPos;
    private int counter;
    private float lastPos;

	// Use this for initialization
	void Start () {
        spawner = GameObject.FindGameObjectWithTag("spawner");
        counter = 0;
        lastPos = -999;
        this.GetComponent<Renderer>().enabled = false;
	}
	
	// Update is called once per frame
	void Update () {
		
	}


    /*
     * should figure this out later. A more legit way. It actually detects what the surface type is rather than 
     * trying to guess where the floor is. currently the ptr just gets null values
    private void FixedUpdate()
    {

        Vector3 pos = this.transform.position;
        Vector3 vec = -this.transform.transform.up;
        SpatialUnderstandingDll.Imports.RaycastResult raycastResult;
        System.IntPtr raycastResultPtr = SpatialUnderstanding.Instance.UnderstandingDLL.GetStaticRaycastResultPtr();
        SpatialUnderstandingDll.Imports.PlayspaceRaycast(pos.x, pos.y, pos.z, vec.x, vec.y, vec.z, raycastResultPtr);
        raycastResult = SpatialUnderstanding.Instance.UnderstandingDLL.GetStaticRaycastResult();
        Debug.Log("SurfaceType is: " + raycastResult.SurfaceType);
        if(raycastResult.SurfaceType == SpatialUnderstandingDll.Imports.RaycastResult.SurfaceTypes.Floor)
        {
            Debug.Log("INSIDE");
        }
    }
    */
    void OnCollisionEnter(Collision col)
    {
        Vector3 pos = col.contacts[0].point;
        if (lastPos == -999)
        {
            lastPos = Mathf.Abs(pos.y);
        }
        else
        {

            float val = lastPos - Mathf.Abs(pos.y);
            Debug.Log("distance is: " + val);
            if (val < 0.1f )
            {
                counter++;
                Debug.Log("counter is: " + counter);
                if (counter > 1)
                {
                    Vector3 temp = spawner.transform.position;
                    temp.y = pos.y - 0.04f;
                    spawner.transform.position = temp;
                    Destroy(this.gameObject);
                }
            }
            else
            {
                counter = 0;
            }
        }

        
    }

}
