using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Grab : MonoBehaviour
{
    public Transform camTrans;
    //private float launchForce = 10;
    private float raycastDist = 2;
    public Image reticle;
    public Transform holdPoint;
    private Transform heldObject = null;
    private Rigidbody heldRigidBody = null;
    public LayerMask grabbableLayers;
    private int ignorePlayerLayer;
    void Start()
    {
        ignorePlayerLayer = LayerMask.NameToLayer("IgnorePlayer");
    }

    void Update()
    {
        if (Physics.Raycast(camTrans.position, camTrans.forward, out RaycastHit hit, raycastDist, grabbableLayers)) {
            reticle.color = UnityEngine.Color.blue;
        } else {
            reticle.color = UnityEngine.Color.white;
        }
        if (Input.GetKeyDown(KeyCode.E)) {
            if (heldObject == null) {
                checkForPickup();
            }
        }
    }

    void checkForPickup() {
        RaycastHit hit;
        if (Physics.Raycast(camTrans.position, camTrans.forward, out hit, raycastDist, grabbableLayers)) {
            StartCoroutine(PickUpObject(hit.transform));
       
        }
    }

    IEnumerator PickUpObject(Transform _tranform) {
        heldObject = _tranform;
        heldObject.gameObject.layer = ignorePlayerLayer;
        heldRigidBody = heldObject.GetComponent<Rigidbody>();
        heldRigidBody.isKinematic = true;

        float t = 0f;
        while (t < .4f) {
            heldRigidBody.position = Vector3.Lerp(heldRigidBody.position, holdPoint.position, t);
            t += Time.deltaTime;
            yield return null;
        }
        SnapToHand();
    }
    void SnapToHand() {
        heldObject.position = holdPoint.position;
        heldObject.parent = holdPoint;
    }
}
