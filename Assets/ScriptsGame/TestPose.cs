using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using UnityEngine.XR.Management;

public class TestPose : MonoBehaviour {

    public Transform leftHand;
    public Transform rightHand;

    [HideInInspector]
    public Vector3 originLeftHand;
    [HideInInspector]
    public Vector3 originRightHand;

    [HideInInspector]
    public Vector3 originLeftHandRotation;
    [HideInInspector]
    public Vector3 originRightHandRotation;

    public float multiplier;

    public ClientVRInputEmitter input;

    void Start() {
        ClientManager.instance.initializeVR(null);

        originLeftHand = leftHand.localPosition;
        originRightHand = rightHand.localPosition;
        originLeftHandRotation = leftHand.localRotation.eulerAngles;
        originRightHandRotation = rightHand.localRotation.eulerAngles;
    }

    void Update() {
        if (input.lastVRPositions.ContainsKey(SteamVR_Input_Sources.LeftHand)) {
            Vector3 newPos = input.lastVRPositions[SteamVR_Input_Sources.LeftHand];

            Debug.Log("Left:");
            Debug.Log(newPos.x);
            Debug.Log(newPos.y);
            Debug.Log(newPos.z);

            newPos = new Vector3(originLeftHand.x + (newPos.x * multiplier), originLeftHand.y + (newPos.y * multiplier), originLeftHand.z + (newPos.z * multiplier));
            leftHand.localPosition = newPos;
        }

        if (input.lastVRRotations.ContainsKey(SteamVR_Input_Sources.LeftHand)) {
            Vector3 rotL = input.lastVRRotations[SteamVR_Input_Sources.LeftHand];

            rotL = new Vector3(originLeftHandRotation.x + rotL.x, originLeftHandRotation.y + rotL.y, originLeftHandRotation.z + rotL.z);
            leftHand.localRotation.eulerAngles.Set(rotL.x, rotL.y, rotL.z);
        }

        if (input.lastVRPositions.ContainsKey(SteamVR_Input_Sources.RightHand)) {
            Vector3 newPos = input.lastVRPositions[SteamVR_Input_Sources.RightHand];

            Debug.Log("Right:");
            Debug.Log(newPos.x);
            Debug.Log(newPos.y);
            Debug.Log(newPos.z);

            newPos = new Vector3(originRightHand.x + (newPos.x * multiplier), originRightHand.y + (newPos.y * multiplier), originRightHand.z + (newPos.z * multiplier));
            rightHand.localPosition = newPos;
        }

        if (input.lastVRRotations.ContainsKey(SteamVR_Input_Sources.RightHand)) {
            Vector3 rotR = input.lastVRRotations[SteamVR_Input_Sources.RightHand];

            rotR = new Vector3(originRightHandRotation.x + rotR.x, originRightHandRotation.y + rotR.y, originRightHandRotation.z + rotR.z);
            rightHand.localRotation.eulerAngles.Set(rotR.x, rotR.y, rotR.z);
        }
    }
}
