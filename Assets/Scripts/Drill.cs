using Oculus.Interaction.HandGrab;
using System.Collections;
using System.Collections.Generic;
using System.Security.Permissions;
using UnityEngine;
using UnityEngine.Events;

public class Drill : MonoBehaviour
{
    [SerializeField]
    bool isScrewAttached;

    [SerializeField]
    GameObject screw;
    public bool canRotate;
    public float speed = 1000;

    public HandGrabInteractor rHandGrabInteractor;
    public HandGrabInteractor lHandGrabInteractor;
    bool screwAttachedToWall;

    void Start()
    {
        canRotate = false;
    }

    public void CanRotate(bool condition)
    {
        canRotate = condition;
    }

    public void AttachScrewToWall()
    {
        if (screw)
        {
            if (screw.GetComponent<Screw>().canAttachScrewToWall && !screwAttachedToWall)
            {
                screw.GetComponent<Screw>().AttachToWall();
                screwAttachedToWall = true;
            }
        }
    }

    void Update()
    {
        if (canRotate)
        {
            transform.Rotate(Vector3.left, speed * Time.deltaTime);
            AttachScrewToWall();        
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Screw"))
        {
            if (!isScrewAttached && lHandGrabInteractor.IsGrabbing)
            {
                screw = other.gameObject;
                screw.GetComponent<Screw>().UnparentScrew();
                screw.transform.SetParent(this.transform);
                screw.transform.localPosition = new Vector3(0.095f, 0, 0);
                screw.transform.localRotation = Quaternion.identity;
                isScrewAttached = true;
            }
        }
    }
}
