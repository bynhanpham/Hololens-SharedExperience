using UnityEngine;
using UnityEngine.VR.WSA;

public class VoiceCommands : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void ToggleMesh()
    {
        GameObject mesh = GameObject.FindGameObjectWithTag("spawner");
        if(mesh.GetComponent<SpatialMappingRenderer>().renderState == SpatialMappingRenderer.RenderState.None)
        {
            mesh.GetComponent<SpatialMappingRenderer>().renderState = SpatialMappingRenderer.RenderState.Visualization;
        }
        else
        {
            mesh.GetComponent<SpatialMappingRenderer>().renderState = SpatialMappingRenderer.RenderState.None;
        }
    }

    public void ToggleOcclusion()
    {
        GameObject mesh = GameObject.FindGameObjectWithTag("spawner");
        if (mesh.GetComponent<SpatialMappingRenderer>().renderState != SpatialMappingRenderer.RenderState.Occlusion)
        {
            mesh.GetComponent<SpatialMappingRenderer>().renderState = SpatialMappingRenderer.RenderState.Occlusion;
        }
        else
        {
            mesh.GetComponent<SpatialMappingRenderer>().renderState = SpatialMappingRenderer.RenderState.None;
        }
    }
            
}
