using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gameplay.VR.Player
{
    public class AgentPCTeleportationBehaviour : MonoBehaviour
    {
        Transform playerHead;
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

            transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
            playerHead.Rotate(Vector3.up, mouseX);
        }

        void shit()
        {
            //skeleton.GetBonePosition();

            /*if (isMouseAndKey)
            {
                if (Physics.Raycast(Camera.main.transform.position, Vector3.forward, out RaycastHit hit))
                {
                    pointer.transform.position = hit.point;
                }

                bezierVisualization.SetPosition(0, playerPosition);
                bezierVisualization.SetPosition(1, pointer.transform.position);
            }*/
        }
    }

}