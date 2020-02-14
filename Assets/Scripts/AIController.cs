using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIController : MonoBehaviour
{
    public float MaxHitPoints = 20;
    public float CurrentHitPoints = 20;
    bool DoRotation = true;
    bool DoMovement = true;
    public float rotationSpeed = 300;
    public int rotationOffset = 0;
    GameObject playerToFollow;
    bool PlayerIsClose;
    Vector2 RightVector;
    float MoveNorthSpeed = 0;
    public float MoveSpeedMax = 1.5f;
    public float MoveAcceleration = 1f;
    public float MoveDeceleration = 0.5f;

    public LayerMask FarLayerMask;

    Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        playerToFollow = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        RightVector = transform.right;
        RaycastHit2D hit = Physics2D.Linecast(transform.position, playerToFollow.transform.position, FarLayerMask);
        float distanceToPlayer2 = Vector2.Distance(playerToFollow.transform.position, transform.position);

        if (DoRotation)
        {
            if(hit.collider.gameObject.tag == "Player" || PlayerIsClose)
            {
                if(distanceToPlayer2 < 16)
                {
                    Vector3 difference = playerToFollow.transform.position - transform.position; // This will calculate the distance between the mouse in the game and the position of the tank turret
                    difference.Normalize();    // This returns simplified values which makes it easier to work with


                    float angle = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;    // This calculates the angle between the mouse and the turret by using the values derives from the difference calculation.

                    transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(0f, 0f, angle + rotationOffset), rotationSpeed * Time.deltaTime); // This will rotate the turret towards the calculated angle over time. Tweaking the multiplication value will state how quickly or slowly it will rotate.
                }
            }
        }

        if(DoMovement)
        {
            if (hit.collider.gameObject.tag == "Player" || PlayerIsClose)
            {
                if (distanceToPlayer2 < 12)
                {
                    MoveNorthSpeed = (MoveNorthSpeed < MoveSpeedMax) ? MoveNorthSpeed + MoveAcceleration : MoveSpeedMax;
                    rb.AddForce(RightVector * MoveNorthSpeed * Time.deltaTime);
                }
                else
                {
                    MoveNorthSpeed = (MoveNorthSpeed > 0) ? MoveNorthSpeed - MoveDeceleration : 0;
                }
            }
        }

        if(CurrentHitPoints <= 0)
        {
            UnityEngine.Object.Destroy(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D hitInfo)
    {
        if (hitInfo.gameObject.tag == "Player")
        {
            playerToFollow = hitInfo.gameObject;
            PlayerIsClose = true;
        }
    }

    void OnTriggerExit2D(Collider2D hitInfo)
    {
        if (hitInfo.gameObject.tag == "Player")
        {
            PlayerIsClose = false;
        }
    }
}
