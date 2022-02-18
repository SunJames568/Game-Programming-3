using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    public float lookspeed = 3;
    public Transform camTrans;
    private Vector2 rotation = Vector2.zero;
    public float walkSpeed = 2.8f;
    public float runSpeed = 5.6f;
    private Rigidbody _rigidbody;
    public AudioSource footstep = default;
    public AudioClip[] steps = default;
    private float footstepTime = 0f;
    bool sprint = false;
    

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        _rigidbody = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.LeftShift)) {
            sprint = true;
        }
        else {
            sprint = false;
        }
        rotation.y += Input.GetAxis("Mouse X") * 2;
        rotation.x -= Input.GetAxis("Mouse Y");
        rotation.x = Mathf.Clamp(rotation.x, -30, 30);
        camTrans.localEulerAngles = new Vector3(rotation.x, 0, 0) * lookspeed;
        transform.eulerAngles = new Vector3(0, rotation.y, 0);
        Handle_Footstep();
        
    }

    private void FixedUpdate() {
        Vector3 moveDir = transform.forward * Input.GetAxis("Vertical") + transform.right * Input.GetAxis("Horizontal");
        if (sprint) {
            moveDir *= runSpeed;
        }
        else {
            moveDir *= walkSpeed;
        }      
        moveDir.y = _rigidbody.velocity.y;
        _rigidbody.velocity = moveDir;
        
    }

    private void Handle_Footstep() {
        if (!Input.anyKey) return;
        footstepTime -= Time.deltaTime;

        if (footstepTime <= 0) {
            if (Physics.Raycast(camTrans.transform.position, Vector3.down, out RaycastHit hit, 5)) {
               footstep.PlayOneShot(steps[0]);
            }
            if (sprint) {
               footstepTime = 0.55f;
            }
            else {
               footstepTime = 0.8f; 
            }
        }
    }
}
