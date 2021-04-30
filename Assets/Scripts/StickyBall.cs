using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StickyBall : MonoBehaviour
{
    public float facingAngle = 0;
    float x = 0;
    float z = 0;
    Vector2 unitV2;
    float sizeOfBall = 1;

    public GameObject cameraReference;
    float distanceToCamera = 5;

    public GameObject tiny;
    bool tinyUnlocked = false;
    public GameObject small;
    bool smallUnlocked = false;
    public GameObject medium;
    bool mediumUnlocked = false;
    public GameObject big;
    bool bigUnlocked = false;
    public GameObject huge;
    bool hugeUnlocked = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //user control - WILL LIKELY CHANGE FOR VR
        x = Input.GetAxis("Horizontal") * Time.deltaTime * -100;
        z = Input.GetAxis("Vertical") * Time.deltaTime * 500;

        //facing angle
        facingAngle += x;
        unitV2 = new Vector2(Mathf.Cos(facingAngle * Mathf.Deg2Rad), Mathf.Sin(facingAngle * Mathf.Deg2Rad));
    }

    private void FixedUpdate()
    {
        //apply force behind the ball
        this.transform.GetComponent<Rigidbody>().AddForce(new Vector3(unitV2.x, 0, unitV2.y) * z * 3);

        //set camera position behind the ball based on rotation
        cameraReference.transform.position = new Vector3(-unitV2.x * distanceToCamera, distanceToCamera, -unitV2.y * distanceToCamera) + this.transform.position;

        UnlockPickupSizes();
    }

    void UnlockPickupSizes()
    {
        if(tinyUnlocked == false)
        {
            if (sizeOfBall >= 1)
            {
                tinyUnlocked = true;
                //iterate through all children in the size category
                for (int i = 0; i < tiny.transform.childCount; i++)
                {
                    //allow all tiny objects to be able to be collected by the sticky ball
                    tiny.transform.GetChild(i).GetComponent<Collider>().isTrigger = true;
                }
            }
        }
        else if (smallUnlocked == false)
        {
            if (sizeOfBall >= 1.5f) //CAN CHANGE VALUES IS NEEDED
            {
                smallUnlocked = true;
                //iterate through all children in the size category
                for (int i = 0; i < small.transform.childCount; i++)
                {
                    //allow all small objects to be able to be collected by the sticky ball
                    small.transform.GetChild(i).GetComponent<Collider>().isTrigger = true;
                }
            }
        }
        else if (mediumUnlocked == false)
        {
            if (sizeOfBall >= 4.0f) //CAN CHANGE VALUES IF NEEDED
            {
                mediumUnlocked = true;
                //iterate through all children in the size category
                for (int i = 0; i < medium.transform.childCount; i++)
                {
                    //allow all medium objects to be able to be collected by the sticky ball
                    medium.transform.GetChild(i).GetComponent<Collider>().isTrigger = true;
                }
            }
        }
        else if (bigUnlocked == false)
        {
            if (sizeOfBall >= 8.0f) //CAN CHANGE VALUES IF NEEDED
            {
                bigUnlocked = true;
                //iterate through all children in the size category
                for (int i = 0; i < big.transform.childCount; i++)
                {
                    //allow all big objects to be able to be collected by the sticky ball
                    big.transform.GetChild(i).GetComponent<Collider>().isTrigger = true;
                }
            }
        }
        else if (hugeUnlocked == false)
        {
            if (sizeOfBall >= 10.0f)    //CAN CHANGE VALUES IF NEEDED
            {
                hugeUnlocked = true;
                //iterate through all children in the size category
                for (int i = 0; i < huge.transform.childCount; i++)
                {
                    //allow all huge objects to be able to be collected by the sticky ball
                    huge.transform.GetChild(i).GetComponent<Collider>().isTrigger = true;
                }
            }
        }
    }

    //pick up sticky objects
    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.CompareTag("Sticky"))
        {
            //grow the sticky ball
            transform.localScale += new Vector3(0.01f, 0.01f, 0.01f);

            //increase size of ball + distance from camera by general size of sticky object
            if (other.gameObject.transform.parent == tiny.transform)
            {
                sizeOfBall += 0.01f;
                distanceToCamera += 0.08f;
            }
            else if (other.gameObject.transform.parent == small.transform)
            {
                sizeOfBall += 0.3f; //CAN CHANGE THESE VALUES IF NEEDED
                distanceToCamera += 0.1f;
            }
            else if (other.gameObject.transform.parent == medium.transform)
            {
                sizeOfBall += 1.0f; //CAN CHANGE THESE VALUES IF NEEDED
                distanceToCamera += 0.5f;
            }
            else if (other.gameObject.transform.parent == big.transform)
            {
                sizeOfBall += 6.0f; //CAN CHANGE THESE VALUES IF NEEDED
                distanceToCamera += 2.0f;
            }
            else if (other.gameObject.transform.parent == huge.transform)
            {
                sizeOfBall += 12.0f; //CAN CHANGE THESE VALUES IF NEEDED
                distanceToCamera += 8.0f;
            }

            //sizeOfBall += 0.01f;
            //distanceToCamera += 0.08f;
            other.enabled = false;  //collected sticky items can't pick up new items

            //becomes child so it stays with the sticky ball
            other.transform.SetParent(this.transform);

            print("Size of ball: " + sizeOfBall.ToString());   //FOR DEBUGGING PURPOSES
        }
    }
}
