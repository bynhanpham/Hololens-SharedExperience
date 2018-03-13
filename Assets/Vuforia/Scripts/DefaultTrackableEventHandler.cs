/*==============================================================================
Copyright (c) 2010-2014 Qualcomm Connected Experiences, Inc.
All Rights Reserved.
Confidential and Proprietary - Protected under copyright and other laws.
==============================================================================*/

using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.VR.WSA;

namespace Vuforia
{
    /// <summary>
    /// A custom handler that implements the ITrackableEventHandler interface.
    /// </summary>
    public class DefaultTrackableEventHandler : MonoBehaviour,
                                                ITrackableEventHandler
    {
        public Text positionText;
        public GameObject obj;

        #region PRIVATE_MEMBER_VARIABLES

        private TrackableBehaviour mTrackableBehaviour;

        #endregion // PRIVATE_MEMBER_VARIABLES



        #region UNTIY_MONOBEHAVIOUR_METHODS

        void Start()
        {
            mTrackableBehaviour = GetComponent<TrackableBehaviour>();
            if (mTrackableBehaviour)
            {
                mTrackableBehaviour.RegisterTrackableEventHandler(this);
            }
            DigitalEyewearARController.Instance.EnableWorldAnchorUsage(false);
        }

        #endregion // UNTIY_MONOBEHAVIOUR_METHODS



        #region PUBLIC_METHODS

        /// <summary>
        /// Implementation of the ITrackableEventHandler function called when the
        /// tracking state changes.
        /// </summary>
        public void OnTrackableStateChanged(
                                        TrackableBehaviour.Status previousStatus,
                                        TrackableBehaviour.Status newStatus)
        {
            if (newStatus == TrackableBehaviour.Status.DETECTED ||
                newStatus == TrackableBehaviour.Status.TRACKED ||
                newStatus == TrackableBehaviour.Status.EXTENDED_TRACKED)
            {
                OnTrackingFound();
            }
            else
            {
                OnTrackingLost();
            }
        }

        #endregion // PUBLIC_METHODS



        #region PRIVATE_METHODS


        private void OnTrackingFound()
        {
            Renderer[] rendererComponents = GetComponentsInChildren<Renderer>(true);
            Collider[] colliderComponents = GetComponentsInChildren<Collider>(true);

            // Enable rendering:
            foreach (Renderer component in rendererComponents)
            {
                component.enabled = true;
            }

            // Enable colliders:
            foreach (Collider component in colliderComponents)
            {
                component.enabled = true;
            }

            Debug.Log("Trackable " + mTrackableBehaviour.TrackableName + " found");
            ChangeAnchorRotation();
            ChangeAnchorLocationCursor();
            SetGroundUsingObj();
            positionText.text = "FOUND !!";
            GameObject.FindGameObjectWithTag("spawner").GetComponent<SpatialMappingRenderer>().renderState = SpatialMappingRenderer.RenderState.None;

        }


        private void OnTrackingLost()
        {
            Renderer[] rendererComponents = GetComponentsInChildren<Renderer>(true);
            Collider[] colliderComponents = GetComponentsInChildren<Collider>(true);

            // Disable rendering:
            foreach (Renderer component in rendererComponents)
            {
                component.enabled = false;
            }

            // Disable colliders:
            foreach (Collider component in colliderComponents)
            {
                component.enabled = false;
            }

            Debug.Log("Trackable " + mTrackableBehaviour.TrackableName + " lost");
            positionText.text = "";

        }

        private void ChangeAnchorLocation()
        {
            GameObject spawner = GameObject.FindGameObjectWithTag("spawner");
            //SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            Vector3 temp;
            temp = (GameObject.FindGameObjectWithTag("anchor").transform.position); // - new Vector3(0,worldHeight,0));

            temp.y = spawner.transform.position.y;
            spawner.transform.position = temp;
            //spawner.transform.rotation = GameObject.FindGameObjectWithTag("anchor").transform.rotation;

        }

        private void ChangeAnchorLocationCam()
        {
            GameObject spawner = GameObject.FindGameObjectWithTag("spawner");
            //SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            Vector3 temp;
            temp = Camera.main.transform.position; // - new Vector3(0,worldHeight,0));

            /*
            // Update Text object and log position
            string position = "Position: " + temp.ToString();

            Vector3 playerPos = GameObject.FindGameObjectWithTag("cursor").transform.position;
            float distanceVal = Vector3.Distance(playerPos, temp);
            string marker = "";
            if (distanceVal > 0.0) marker = " m";
            string distance = "Distance: " + distanceVal + marker;
            positionText.text = position + ", " + distance;
            Debug.Log(position + ", " + distance);
            */
            /*
            temp.y = spawner.transform.position.y;
            temp = temp + Camera.main.transform.forward;
            spawner.transform.position = temp;
            */
            spawner.transform.position = this.transform.position;
            //spawner.transform.rotation = GameObject.FindGameObjectWithTag("anchor").transform.rotation;

        }

        private void ChangeAnchorLocationCursor()
        {
            GameObject spawner = GameObject.FindGameObjectWithTag("spawner");
            GameObject cursor = GameObject.FindGameObjectWithTag("cursor");
            //var temp = cursor.transform.position;
           // temp.y = spawner.transform.position.y;
            spawner.transform.position = cursor.transform.position;
        }

        private void SetGroundUsingObj()
        {
            Vector3 temp = Camera.main.transform.position;
            temp = temp + new Vector3(0,0,1.5f);
            Instantiate(obj, temp, Quaternion.identity);
        }

        private void ChangeAnchorRotation()
        {
            GameObject spawner = GameObject.FindGameObjectWithTag("spawner");
            var temp = spawner.transform.eulerAngles;
            temp.y = this.transform.eulerAngles.y;
            spawner.transform.eulerAngles = temp;
        }
        #endregion // PRIVATE_METHODS
    }

}
