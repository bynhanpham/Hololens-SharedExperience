/*==============================================================================
Copyright (c) 2010-2014 Qualcomm Connected Experiences, Inc.
All Rights Reserved.
Confidential and Proprietary - Protected under copyright and other laws.
==============================================================================*/

using UnityEngine;
using UnityEngine.SceneManagement;

namespace Vuforia
{
    /// <summary>
    /// A custom handler that implements the ITrackableEventHandler interface.
    /// </summary>
    public class DefaultTrackableEventHandler : MonoBehaviour,
                                                ITrackableEventHandler
    {
        public float worldHeight;

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
            ChangeAnchorLocation();
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
        }

        private void ChangeAnchorLocation()
        {
            Debug.Log("inside ChangeAnchorLocation");
            GameObject spawner = GameObject.FindGameObjectWithTag("spawner");
            //SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            Vector3 temp;
            temp = (GameObject.FindGameObjectWithTag("anchor").transform.position); // - new Vector3(0,worldHeight,0));
            temp.y = 0.0f; //(Camera.main.transform.position.y - (float)1.25);
            spawner.transform.position = temp;

            //float adjustedY = SceneManager.LoadScene(SceneManager.GetActiveScene().name); (GameObject.FindGameObjectWithTag("anchor").transform.position.y);
            //spawner.transform.position = (GameObject.FindGameObjectWithTag("anchor").transform.position + new Vector3(0, adjustedY, 0));

            //Vector3 resetPos = GameObject.FindGameObjectWithTag("anchor").transform.position;
            //resetPos.y = (Camera.main.transform.position.y - (float)1.77);
            //spawner.transform.position = resetPos;

            //spawner.transform.rotation = GameObject.FindGameObjectWithTag("anchor").transform.rotation;
            //spawner.transform.rotation = Camera.main.transform.position;
        }
        #endregion // PRIVATE_METHODS
    }

}
