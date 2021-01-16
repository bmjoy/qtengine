using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SamplePlayer : BaseQTObjectComponent {

    public SamplePlayerUI playerUI;
    public Camera playerCamera;
    public Animator playerAnimator;
    public Text namePlate;

    public Transform playerBody;
    public CharacterController controller;
    public Transform playerGround;

    public float groundDistance = 0.4f;
    public LayerMask groundMask;

    //Server variables
    private float xRotation = 0f;
    private Vector3 velocity;

    private float mouseSensitivity = 500f;
    private float speed = 12f;
    private float gravity = -15f;
    private float jumpHeight = 3f;
    private bool isGrounded;

    //Variables
    [QTSynced]
    public float maxHealth = 100f;
    [QTSynced]
    public float health = 100f;

    public override void handleClientObjectSpawn() {
        handleOwnerChange("", "");
    }

    public override void handleServerObjectSpawn() {
        playerCamera.gameObject.SetActive(false);
        playerUI.gameObject.SetActive(false);
    }

    public override void handleClientOwnerChange(string oldOwnerID, string newOwnerID) {
        bool active = clientComponent.isOwner();

        playerCamera.gameObject.SetActive(active);
        playerUI.gameObject.SetActive(active);
        if(active) {
            Cursor.lockState = CursorLockMode.Locked;
        }
    }

    public override void handleServerUpdate() {
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

            //Animation
            playerAnimator.SetFloat("forward", moveZ);
        }
    }

    public override void handleClientUpdate() {
        string username = SyncedRoomInfo.instance != null && SyncedRoomInfo.instance.players.ContainsKey(obj.objectID) ? SyncedRoomInfo.instance.players[obj.objectID] : "Loading";

        playerUI.healthSlider.value = health / maxHealth;
        playerUI.healthText.text = Mathf.Round(health).ToString();
        namePlate.text = username;
    }
}
