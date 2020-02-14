using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerController : MonoBehaviour
{
    //Movement Vars

    bool MoveNorth;
    bool MoveSouth;
    bool MoveWest;
    bool MoveEast;
    float MoveNorthSpeed = 0;
    float MoveSouthSpeed = 0;
    float MoveWestSpeed = 0;
    float MoveEastSpeed = 0;
    public float MoveAcceleration = 100f;
    public float MoveDeceleration = 100f;
    public float MoveSpeedMax = 150f;
    float ActualMaxNorthSpeed;
    float ActualMaxSouthSpeed;
    float ActualMaxWestSpeed;
    float ActualMaxEastSpeed;
    Rigidbody2D rb;
    public bool DisableControls = false;
    public bool DoRotation = true;
    public int rotationOffset = 0;    // This will offset the rotation of the object as it tracks the mouse. This is required to correctly set rotation values
    public int rotationSpeed = 300;

    //Health Vars

    public float MaxHitPoints = 100;
    public float CurrentHitPoints = 100;

    //Sound Vars

    AudioSource SourceSound;
    public AudioClip WalkingSound;

    //Animation Vars

    Animator SourceAnimator;
    float VelocityXY;

    //Weapom Vars

    public Transform FirePoint;
    public GameObject BulletPrefab;
    public int MaxAmmo = 8;
    public int CurrentAmmo = 8;
    public float ReloadTime = 4.0f;
    public float ReloadTimeLeft;
    public float BulletVelocity = 8.0f;
    public int BulletDamage = 10;
    public float TimeBetweenShots = 0.5f;
    private bool IsReloading = false;
    private float TimeSinceLastShot;
    public AudioClip WeaponSound;

    // Misc Vars

    Vector2 UpVector;
    Vector2 RightVector;
    private PlayerHUDController PlayerHud;

    void Start()
    {
        SourceSound = GetComponent<AudioSource>();
        SourceAnimator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        PlayerHud = GameObject.FindGameObjectWithTag("HUD").GetComponent<PlayerHUDController>();
    }

    void Update()
    {
        UpVector = transform.up;
        RightVector = transform.right;

        ActualMaxNorthSpeed = MoveSpeedMax * Input.GetAxis("Vertical");
        ActualMaxSouthSpeed = MoveSpeedMax * Input.GetAxis("Vertical");
        ActualMaxWestSpeed = MoveSpeedMax * Input.GetAxis("Horizontal");
        ActualMaxEastSpeed = MoveSpeedMax * Input.GetAxis("Horizontal");

        if (!DisableControls)
        {
            MoveNorth = (Input.GetAxis("Vertical") > 0) ? true : MoveNorth;
            MoveNorth = (Input.GetAxis("Vertical") <= 0) ? false : MoveNorth;
            if(MoveNorth)
            {
                MoveNorthSpeed = (MoveNorthSpeed < ActualMaxNorthSpeed) ? MoveNorthSpeed + MoveAcceleration : ActualMaxNorthSpeed;
                rb.AddForce(RightVector * MoveNorthSpeed * Time.deltaTime);
            }
            else
            {
                MoveNorthSpeed = (MoveNorthSpeed > 0) ? MoveNorthSpeed - MoveDeceleration : 0;
            }

            MoveSouth = (Input.GetAxis("Vertical") < 0) ? true : MoveSouth;
            MoveSouth = (Input.GetAxis("Vertical") >= 0) ? false : MoveSouth;
            if(MoveSouth)
            {
                MoveSouthSpeed = (MoveSouthSpeed < ActualMaxSouthSpeed) ? MoveSouthSpeed + MoveAcceleration : ActualMaxSouthSpeed;
                rb.AddForce(RightVector * MoveSouthSpeed * Time.deltaTime);
            }
            else
            {
                MoveSouthSpeed = (MoveSouthSpeed > 0) ? MoveSouthSpeed - MoveDeceleration : 0;
            }

            MoveWest = (Input.GetAxis("Horizontal") > 0) ? true : MoveWest;
            MoveWest = (Input.GetAxis("Horizontal") <= 0) ? false : MoveWest;
            if (MoveWest)
            {
                MoveWestSpeed = (MoveWestSpeed < ActualMaxWestSpeed) ? MoveWestSpeed + MoveAcceleration : ActualMaxWestSpeed;
                rb.AddForce(UpVector * MoveWestSpeed * Time.deltaTime * -1);
            }
            else
            {
                MoveWestSpeed = (MoveWestSpeed > 0) ? MoveWestSpeed - MoveDeceleration : 0;
            }

            MoveEast = (Input.GetAxis("Horizontal") < 0) ? true : MoveEast;
            MoveEast = (Input.GetAxis("Horizontal") >= 0) ? false : MoveEast;
            if (MoveEast)
            {
                MoveEastSpeed = (MoveEastSpeed < ActualMaxEastSpeed) ? MoveEastSpeed + MoveAcceleration : ActualMaxEastSpeed;
                rb.AddForce(UpVector * MoveEastSpeed * Time.deltaTime * -1);
            }
            else
            {
                MoveEastSpeed = (MoveEastSpeed > 0) ? MoveEastSpeed - MoveDeceleration : 0;
            }

            if (MoveNorth || MoveSouth || MoveWest || MoveEast)
            {
                VelocityXY = 1.0f;
                SourceAnimator.SetFloat("Speed", VelocityXY);
            }
            else
            {
                VelocityXY = 0.0f;
                SourceAnimator.SetFloat("Speed", VelocityXY);
            }

            if (CurrentAmmo <= 0 && !IsReloading && !DisableControls)
            {
                StartCoroutine(Reload());
            }

            if (IsReloading)
            {
                ReloadTimeLeft -= Time.deltaTime;
                PlayerHud.ReloadText.text = string.Format("{0}: {1}", "Reloading", (ReloadTimeLeft).ToString("0.0"));
            }

            if (Input.GetButtonDown("Fire1") && !IsReloading && Time.timeScale == 1 && !DisableControls && TimeSinceLastShot < Time.time)
            {
               // SourceAnimator.SetBool("FiredGun", true);
                Shoot();
                SourceSound.Play();
                TimeSinceLastShot = Time.time + TimeBetweenShots;
            }
        }

        if(DoRotation)
        {
            Vector3 difference = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position; // This will calculate the distance between the mouse in the game and the position of the tank turret
            difference.Normalize();    // This returns simplified values which makes it easier to work with


            float angle = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;    // This calculates the angle between the mouse and the turret by using the values derives from the difference calculation.

            transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(0f, 0f, angle + rotationOffset), rotationSpeed * Time.deltaTime); // This will rotate the turret towards the calculated angle over time. Tweaking the multiplication value will state how quickly or slowly it will rotate.
        }

        IEnumerator Reload()
        {
            IsReloading = true;
            SourceAnimator.SetBool("Reloading", true);
            ReloadTimeLeft = ReloadTime;
            PlayerHud.SetReloading(true);
            yield return new WaitForSeconds(ReloadTime);

            CurrentAmmo = MaxAmmo;
            IsReloading = false;
            PlayerHud.SetReloading(false);
            SourceAnimator.SetBool("Reloading", false);
        }

        void Shoot()
        {
            CurrentAmmo--;

            //Shooting Logic
            GameObject activeBullet = Instantiate(BulletPrefab, FirePoint.position, FirePoint.rotation) as GameObject;

            Rigidbody2D rb = activeBullet.GetComponent<Rigidbody2D>();
            rb.velocity = activeBullet.transform.right * BulletVelocity;
            Bullet script = activeBullet.GetComponent<Bullet>();
            script.Damage = BulletDamage;
            //SourceAnimator.SetBool("FiredGun", false);
        }

        if(CurrentHitPoints <= 0)
        {
            Die();
        }
    }

    public void TakeDamage(float Damage)
    {

        CurrentHitPoints = (Damage > 0) ? CurrentHitPoints - Damage : 0;
    }

    void Die()
    {
        PlayerHud.ShowFailedPanel();
        UnityEngine.Object.Destroy(gameObject);
    }
}
