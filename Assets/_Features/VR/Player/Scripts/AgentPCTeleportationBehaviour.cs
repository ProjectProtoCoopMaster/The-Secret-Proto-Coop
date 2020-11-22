﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gameplay.VR.Player
{
    public class AgentPCTeleportationBehaviour : MonoBehaviour
    {
        Transform playerHead;
        public Transform mouseAndKeyboardController;
        AgentTeleportationManager manager;
        [SerializeField] float mouseSensitivity;
        float xRotation = 0f;

        private void Awake()
        {
            manager = GetComponent<AgentTeleportationManager>();
            playerHead = manager.playerHead;
        }

        // Start is called before the first frame update
        void Start()
        {
            Cursor.lockState = CursorLockMode.Locked;
        }

        // Update is called once per frame
        void Update()
        {
            float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
            float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

            xRotation -= mouseY;
            xRotation = Mathf.Clamp(xRotation, -90f, 90f);

            playerHead.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
            playerHead.transform.parent.Rotate(Vector3.up, mouseX);

            manager.pointerOrigin = mouseAndKeyboardController;

            if (Input.GetKey(KeyCode.Space)) manager.TallRayPointer(null);
            if (Input.GetKeyUp(KeyCode.Space)) manager.TryTeleporting();
            Debug.DrawRay(playerHead.position, playerHead.forward * 500);
        }
    }
}