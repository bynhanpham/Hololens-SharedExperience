//
// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.
//

using UnityEngine;
using HoloToolkit.Sharing.Spawning;
using HoloToolkit.Unity.InputModule;
using UnityEngine.SceneManagement;

namespace HoloToolkit.Sharing.Tests
{
    /// <summary>
    /// Class that handles spawning sync objects on keyboard presses, for the SpawningTest scene.
    /// </summary>
    public class SyncObjectSpawner : MonoBehaviour
    {
        [SerializeField]
        private PrefabSpawnManager spawnManager;

        [SerializeField]
        [Tooltip("Optional transform target, for when you want to spawn the object on a specific parent.  If this value is not set, then the spawned objects will be spawned on this game object.")]
        private Transform spawnParentTransform;

        private void Awake()
        {
            if (spawnManager == null)
            {
                Debug.LogError("You need to reference the spawn manager on SyncObjectSpawner.");
            }

            // If we don't have a spawn parent transform, then spawn the object on this transform.
            if (spawnParentTransform == null)
            {
                spawnParentTransform = transform;
            }
        }

        public Vector3 PositionCalibrator(Vector3 pos, float height = 0, bool cameraHeight = false)
        {
            GameObject obj = GameObject.FindGameObjectWithTag("pos");
            obj.transform.parent = null;
            obj.transform.position = pos;
            obj.transform.parent = GameObject.FindGameObjectWithTag("spawner").transform;
            var temp = obj.transform.localPosition;
            if (!cameraHeight)
            {
                temp.y = height;
            }
            obj.transform.localPosition = temp;
            return obj.transform.localPosition;
        }

        //How much you want it rotated
        //0 is facing the same direction as camera.
        public Quaternion RotationCalibrator(int angle = 0)
        {
            var rot = Camera.main.transform.eulerAngles;
            rot.y = rot.y + angle - GameObject.FindGameObjectWithTag("spawner").transform.eulerAngles.y; //rotate it 180 to face you
            rot.x = 0;
            rot.z = 0;
            return Quaternion.Euler(rot);
        }

        public Vector3 UserRelativeLocation(int offset = 0)
        {
            Vector3 spawnerPos = GameObject.FindGameObjectWithTag("spawner").transform.position;
            Vector3 userPos = (Camera.main.transform.position + (Camera.main.transform.forward * offset));

            return (userPos - spawnerPos);
        }

        public void SpawnBasicSyncObject()
        {
            Quaternion rotation = Random.rotation;

            var spawnedObject = new SyncSpawnedObject();

            spawnManager.Spawn(spawnedObject, PositionCalibrator(Camera.main.transform.position, cameraHeight: true), rotation, spawnParentTransform.gameObject, "SpawnedObject", false);
        }

        public void SpawnCustomSyncObject()
        {
            Vector3 position = PositionCalibrator((Camera.main.transform.position + Camera.main.transform.forward), cameraHeight: true);
            Quaternion rotation = Random.rotation;

            var spawnedObject = new SyncSpawnTestSphere();
            spawnedObject.TestFloat.Value = Random.Range(0f, 100f);

            spawnManager.Spawn(spawnedObject, position, rotation, spawnParentTransform.gameObject, "SpawnTestSphere", false);
        }

        public void SpawnUnityChan()
        {
            Vector3 position = PositionCalibrator(Camera.main.transform.position + Camera.main.transform.forward * 2);

            Quaternion rotation = RotationCalibrator(180);

            var spawnObject = new SyncSpawnUnityChan();
            spawnObject.TestFloat.Value = Random.Range(0f, 100f);

            spawnManager.Spawn(spawnObject, position, rotation, spawnParentTransform.gameObject, "SpawnUnityChan", false);
        }


        /// <summary>
        /// Deletes any sync object that inherits from SyncSpawnObject.
        /// </summary>
        public void DeleteSyncObject()
        {
            GameObject hitObject = GazeManager.Instance.HitObject;
            if (hitObject != null)
            {
                var syncModelAccessor = hitObject.GetComponent<DefaultSyncModelAccessor>();
                if (syncModelAccessor != null)
                {
                    var syncSpawnObject = (SyncSpawnedObject)syncModelAccessor.SyncModel;
                    spawnManager.Delete(syncSpawnObject);
                }
            }
        }


        //Doesnt actually reset the world, but keeps the anchor position and deletes all objects spawned so far
        public void ResetWorld()
        {
            GameObject spawner = GameObject.FindGameObjectWithTag("spawner");
            for(int i = 0; i < spawner.transform.childCount; i++)
            {
                Debug.Log(spawner.transform.GetChild(i).tag);
                if (spawner.transform.GetChild(i).tag != "nodelete" && spawner.transform.GetChild(i).tag != "pos")
                {
                    var model = spawner.transform.GetChild(i).GetComponent<DefaultSyncModelAccessor>();
                    if (model != null)
                    {
                        spawnManager.Delete(((SyncSpawnedObject)(model.SyncModel)));
                    }
                    else
                    {
                        //GameObject.Destroy(spawner.transform.GetChild(i).gameObject);
                    }
                }    
            }
        }

        //Deletes everything and then reloads scene
        public void ResetScene()
        {
            ResetWorld();
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}
