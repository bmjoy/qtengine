using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SamplePlayer : BaseQTObjectComponent {

    public Camera playerCamera;
    public Transform playerBody;
    public CharacterController controller;
    public Transform playerGround;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;

    private float xRotation = 0f;
    private Vector3 velocity;

    private float mouseSensitivity = 500f;
    private float speed = 12f;
    private float gravity = -15f;
    private float jumpHeight = 3f;
    private bool isGrounded;

    void Start() {
        handleClientStart();
    }

    void Update() {
        handleServerLogic();
    }

    public override void handleObjectSpawn() {
        handleOwnerChange("", "");

        if (obj.objectType != BaseQTObject.type.CLIENT) { return; }
        obj.onOwnerChanged += handleOwnerChange;
    }

    public void handleOwnerChange(string oldOwnerID, string newOwnerID) {
        if (ClientManager.instance.workerClient != null && ClientManager.instance.workerClient.session != null) {
            playerCamera.gameObject.SetActive(obj.ownerID == ClientManager.instance.workerClient.session.id);
        } else {
            playerCamera.gameObject.SetActive(false);
        }
    }

    public void handleClientStart() {
        if (obj.objectType != BaseQTObject.type.CLIENT) { return; }
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void handleServerLogic() {
        if (obj.objectType != BaseQTObject.type.SERVER) { return; }
        WorkerServerQTClient qtRemoteClient = (WorkerServerQTClient)WorkerServerManager.instance.connections.clients.Find(c => c.session.id == obj.ownerID);

        if (qtRemoteClient != null) {
            Dictionary<KeyCode, bool> keys = qtRemoteClient.syncedKeys;
            Dictionary<string, float> axis = qtRemoteClient.syncedAxis;

            bool keySpace = keys.ContainsKey(KeyCode.Space) ? keys[KeyCode.Space] : false;

            float axisX = axis.ContainsKey("Mouse X") ? axis["Mouse X"] : 0;
            float axisY = axis.ContainsKey("Mouse Y") ? axis["Mouse Y"] : 0;
            float moveX = axis.ContainsKey("Horizontal") ? axis["Horizontal"] : 0;
            float moveZ = axis.ContainsKey("Vertical") ? axis["Vertical"] : 0;

            //Rotation
            float mouseX = axisX * mouseSensitivity * Time.deltaTime;
            float mouseY = axisY * mouseSensitivity * Time.deltaTime;

            xRotation -= mouseY;
            xRotation = Mathf.Clamp(xRotation, -90f, 90f);

            playerCamera.gameObject.transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
            playerBody.Rotate(Vector3.up * mouseX);

            //Position
            Vector3 move = playerBody.transform.right * moveX + playerBody.forward * moveZ;
            controller.Move(move * speed * Time.deltaTime);

            //Gravity
            isGrounded = Physics.CheckSphere(playerGround.position, groundDistance, groundMask);
            if(isGrounded && velocity.y < 0f) {
                velocity.y = -2f;
            }

            if(isGrounded && keySpace == true) {
                velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
            }

            velocity.y += gravity * Time.deltaTime;
            controller.Move(velocity * Time.deltaTime);
        }
    }
}
