// using UnityEngine;
// using System;
// using System.Collections.Generic;


// public class CameraController : MonoBehaviour
// {
//     #region Fields
//     float currentXAngle;
//     float currentYAngle;

//     [Range(0f, 90f)] public float upperVerticalLimit = 35f;
//     [Range(0f, 90f)] public float lowerVerticalLimit = 35f;

//     public float cameraSpeed = 50f;
//     public bool smoothCameraRotation;
//     [Range(1f, 50f)] public float cameraSmoothingFactor = 25f;

//     Transform tr;
//     Camera cam;
//     [SerializeField, Required] InputReader input;
//     #endregion

//     public Vector3 GetUpDirection() => tr.up;
//     public Vector3 GetFacingDirection() => tr.forward;

//     void Awake()
//     {
//         tr = transform;
//         cam = GetComponentInChildren<Camera>();

//         currentXAngle = tr.localRotation.eulerAngles.x;
//         currentYAngle = tr.localRotation.eulerAngles.y;
//     }

//     void Update() {
//         RotateCamera(input.LookDirection.x, -input.LookDirection.y);
//     }
// }
