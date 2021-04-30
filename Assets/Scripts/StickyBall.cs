using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StickyBall : MonoBehaviour
{
    public float facingAngle = 0;
    float x = 0;
    float z = 0;
    Vector2 unitV2;
    float sizeOfBall;

    public GameObject cameraReference;
    float distanceToCamera = 5;

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
    }

    //pick up sticky objects
    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.CompareTag("Sticky"))
        {
            //grow the sticky ball
            transform.localScale += new Vector3(0.01f, 0.01f, 0.01f);
            sizeOfBall += 0.01f;
            distanceToCamera += 0.08f;
            other.enabled = false;  //collected sticky items can't pick up new items

            //becomes child so it stays with the sticky ball
            other.transform.SetParent(this.transform);
        }
    }
}
