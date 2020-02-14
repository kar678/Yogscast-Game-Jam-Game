using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamaraFollow : MonoBehaviour
{
    //What we are following
    private GameObject Player;

    //Zeros out the velocity
    Vector3 Velocity = Vector3.zero;

    //Time to follow target
    public float SmoothTime = .15f;

    //Enable and set the maximum Y value
    public bool YMaxEnabled = false;
    public float YMaxValue = 0f;

    //Enable and set the min Y value
    public bool YMinEnabled = false;
    public float YMinValue = 0f;

    //Enable and set the maximum X value
    public bool XMaxEnabled = false;
    public float XMaxValue = 0f;

    //Enable and set the min X value
    public bool XMinEnabled = false;
    public float XMinValue = 0f;

    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        //target position
        Vector3 targetPos = Player.transform.position;

        //Vertical Clamp
        if (YMinEnabled && YMaxEnabled)
            targetPos.y = Mathf.Clamp(Player.transform.position.y, YMinValue, YMaxValue);

        else if (YMinEnabled)
            targetPos.y = Mathf.Clamp(Player.transform.position.y, YMinValue, Player.transform.position.y);

        else if (YMaxEnabled)
            targetPos.y = Mathf.Clamp(Player.transform.position.y, Player.transform.position.y, YMaxValue);

        //Horizontal Clamp

        if (XMinEnabled && XMaxEnabled)
            targetPos.x = Mathf.Clamp(Player.transform.position.x, XMinValue, XMaxValue);

        else if (XMinEnabled)
            targetPos.x = Mathf.Clamp(Player.transform.position.x, XMinValue, Player.transform.position.x);

        else if (XMaxEnabled)
            targetPos.x = Mathf.Clamp(Player.transform.position.x, Player.transform.position.x, XMaxValue);




        targetPos.z = Player.transform.position.z + -10;

        transform.position = Vector3.SmoothDamp(transform.position, targetPos, ref Velocity, SmoothTime);
    }
}
