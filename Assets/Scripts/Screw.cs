using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Oculus.Interaction;
using Oculus.Interaction.HandGrab;
using Unity.VisualScripting;

public class Screw : MonoBehaviour
{
    public GameObject HandGrabInteractor;
    public bool isScrewAttached = false;
    public bool canAttachScrewToWall;
    public GameObject collidedObject;
    void Start()
    {

    }

    public void UnparentScrew()
    {
        HandGrabInteractor.SetActive(false);
        isScrewAttached = true;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Wall"))
        {
            collidedObject = other.gameObject;
            canAttachScrewToWall = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Wall"))
        {
            collidedObject = null;
            canAttachScrewToWall = false;
        }
    }

    public void AttachToWall()
    {
        if (collidedObject)
        {
            transform.SetParent(collidedObject.transform);
            transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y, -0.5224838f);
            transform.localRotation = Quaternion.Euler(transform.localRotation.x, -90, transform.localRotation.z);
        }
    }
}