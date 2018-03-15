# Hololens-SharedExperience

This project uses the holotoolkits spatial mapping and sharing libraries to align AR objects between users and real space (alignment with the floor). In order to align objects, this application uses Vuforia image markers to find the position in real space. 

## Getting Started

1. Launch the sharing service (HoloToolKit > Sharing Service > Launch Sharing Service). Copy the IP and in the scene under Managers > Sharing, put in the IP in the inspector. Leave port as default. 
2. Remember to launch the sharing service every time when using the app. Disable firewall if you can't connect.
3. If your IP changes then you have to set the IP in the inspector again and rebuild/deploy the application.

## Making Objects Sharable

1. Under Manager > Sharing in the game scene, in the inspector panel under the Prefab Spawn Manager script component, increase the size of the list and put in a Data Model Classname, and drag in the prefab. 
2. Make a class that matches the Data Model Classname. Use the SyncSpawnUnityChan as a reference. 
3. Make a instatiation method in SyncObjectSpawner.cs for your object. Use the PrefabSpawnManager.spawn() method. In order to get the correct relative position and rotation to pass in, use PositionCalibrator() and RotationCalibrator(). (It converts world position and rotation to the location relative to the SpawnTarget which acts as a shared anchor location between the users)
4. To instantiate objects with voice commands, add the method to Managers > Sharing > KeywordManager.
