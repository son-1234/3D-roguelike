using System;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    private Rigidbody rb;
    [SerializeField] public float speed = 10f;
    [SerializeField] public float jumpHeight = 3f;
    [SerializeField] public float dash =  5f;
    [SerializeField] public float rotSpeed =  3f;
    [SerializeField] public float mouseSensitivity = 1000f;

    private Vector3 dir = Vector3.zero;

    private bool Ground = false;
    [SerializeField] public LayerMask layer;
    
    [SerializeField] private Transform playerCamera; // 카메라 Transform

    private float xRotation = 0f; // 카메라 X축 회전값
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = this.GetComponent<Rigidbody>();
        Cursor.lockState = CursorLockMode.Locked;//마우스 고정
        Cursor.visible = false;
    }
    
    // Update is called once per frame
    void Update()
    {
        dir.x = Input.GetAxis("Horizontal");
        dir.z = Input.GetAxis("Vertical");
        dir.Normalize();
        
        checkGround();
        
        if (Input.GetButtonDown("Jump") && Ground)
        {
            Vector3 jumpPower = Vector3.up * jumpHeight;
            rb.AddForce(jumpPower,ForceMode.VelocityChange);
        }
        if (Input.GetButtonDown("Dash"))
        {
            Vector3 dashPower = this.transform.forward * dash;
            rb.AddForce(dashPower, ForceMode.VelocityChange);
        }
        
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;
        
        transform.Rotate(0f, mouseX, 0f);
        
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f); // 상하 회전 제한

        playerCamera.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
    }

    private void FixedUpdate()
    {
        if (dir != Vector3.zero)
        {
            Vector3 moveDir = transform.TransformDirection(dir);
            rb.MovePosition(transform.position + moveDir * speed * Time.deltaTime);
        }
    }

    void checkGround()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position + (Vector3.up * 0.2f), Vector3.down, out hit, 0.4f, layer))
        {
            Ground = true;
        }
        else
        {
            Ground = false;
        }
        
    }
}
