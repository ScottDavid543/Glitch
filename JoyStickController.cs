using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JoyStickController : PlayerController
{

    public float speed = 6.0F;
    public float jumpSpeed = 8.0F;
    public float gravity = 20.0F;

    private Vector3 moveDirection = Vector3.zero;
    private Transform faceDirection;

    public GameObject body;
    public bool canMove = false;

    public GameObject Trail;
  
    private bool bounce = false;
    private bool pistol = true;
    private bool MiniGun = false;
    private bool RanCannon = false;
    public bool canShoot = true;
    public int miniAmmo = 0;
    public int bounammo = 0;
    public float randCount = 0;

    AudioSource audioSource;
    public AudioClip pew;
    public AudioClip miniPew;
    public AudioClip bouncePew;
    public AudioClip randPew;

    public CharacterController controller;

    void Start()
    {
        CharacterController controller = GetComponent<CharacterController>();
        audioSource = GetComponent<AudioSource>();
    }
    float fireRate = 0.5f;
    // Update is called once per frame
    void Update()
    {       
        if (RanCannon == true)
        {
            randCount -= Time.deltaTime;
        }
        if (canMove == true)
        {
            if (gameObject.layer == LayerMask.NameToLayer("Player1"))
            {            
                if (controller.isGrounded)
                {                             
                    // Handles the Left joystick movement
                    moveDirection = new Vector3(Input.GetAxis("Hoz1"), 0, Input.GetAxis("Vert1"));
                    moveDirection = transform.TransformDirection(moveDirection);
                    moveDirection *= speed;
                    if (Input.GetAxis("Hoz1") != 0 || Input.GetAxis("Vert1") != 0)
                    {
                        Trail.SetActive(true);
                    }
                    else
                    {
                        Trail.SetActive(false);
                    }
                    if (Input.GetAxis("Hoz2") != 0 || Input.GetAxis("Vert2") != 0)
                    {
                        // Handles body rotation with right joystick
                        body.transform.eulerAngles = new Vector3(0, Mathf.Atan2(Input.GetAxisRaw("Hoz2"), Input.GetAxisRaw("Vert2")) * 180 / Mathf.PI, 0);
                        if (canShoot == true)
                        {
                            if (!MiniGun && !RanCannon)
                            {
                                if (fireRate >= 0)
                                {
                                    fireRate -= Time.deltaTime;
                                }
                                else
                                {
                                    shoot();
                                    fireRate = 0.2f;
                                }
                            }
                            if (MiniGun)
                            {
                                shoot();
                            }
                            if (RanCannon)
                            {
                                if (fireRate >= 0)
                                {
                                    fireRate -= Time.deltaTime;
                                }
                                else
                                {
                                    shoot();
                                    fireRate = 0.1f;
                                }
                            }
                        }
                    }
                }
                moveDirection.y -= gravity * Time.deltaTime;
                controller.Move(moveDirection * Time.deltaTime);
            }
            if (gameObject.layer == LayerMask.NameToLayer("Player2"))
            {

                if (controller.isGrounded)
                {
                    moveDirection = new Vector3(Input.GetAxis("Hoz3"), 0, Input.GetAxis("Vert3"));
                    moveDirection = transform.TransformDirection(moveDirection);
                    moveDirection *= speed;

                    if (Input.GetAxis("Hoz3") != 0 || Input.GetAxis("Vert4") != 0)
                    {
                        Trail.SetActive(true);
                    }
                    else
                    {
                        Trail.SetActive(false);
                    }


                    if (Input.GetAxis("Hoz4") != 0 || Input.GetAxis("Vert4") != 0)
                    {
                        body.transform.eulerAngles = new Vector3(0, Mathf.Atan2(Input.GetAxisRaw("Hoz4"), Input.GetAxisRaw("Vert4")) * 180 / Mathf.PI, 0);
                        if (canShoot == true)
                        {
                            if (!MiniGun && !RanCannon)
                            {
                                if (fireRate >= 0)
                                {
                                    fireRate -= Time.deltaTime;

                                }
                                else
                                {
                                    shoot();
                                    fireRate = 0.2f;
                                }
                            }
                            if (MiniGun)
                            {
                                shoot();
                            }
                            if (RanCannon)
                            {
                                if (fireRate >= 0)
                                {
                                    fireRate -= Time.deltaTime;

                                }
                                else
                                {
                                    shoot();
                                    fireRate = 0.1f;
                                }
                            }
                        }
                    }                       
                }
                moveDirection.y -= gravity * Time.deltaTime;
                controller.Move(moveDirection * Time.deltaTime);
            }
        }

    }



    void shoot()
    {
        if (MiniGun) ShootMini();

        if (pistol)
        {
            audioSource.PlayOneShot(pew, 0.2f);
            Rigidbody bulletInstance;
            bulletInstance = Instantiate(PistolBullet, barrelend[0].position, barrelend[0].rotation);
            bulletInstance.AddForce(barrelend[0].forward * Pistolbulletforce);
        }
        if (bounce)
        {
       
            //StartCoroutine(BounceShoot());
            if (bounammo > 0)
            {
                audioSource.PlayOneShot(bouncePew, 0.2f);
                Rigidbody bulletInstance;
                bulletInstance = Instantiate(BounceBullet, barrelend[0].position, barrelend[0].rotation) as Rigidbody;
                bulletInstance.AddForce(barrelend[0].forward * shotgunbulletforce);
                bounammo -= 1;

                if (bounammo <= 0)
                {
                    bounce = false;
                    pistol = true;
                    MiniGun = false;
                    RanCannon = false;
                }
            }
        }
         if (RanCannon)
        {
            audioSource.PlayOneShot(randPew, 0.2f);
            Rigidbody bulletInstance;
            int rand = Random.Range(0, barrelend.Length);
            bulletInstance = Instantiate(PistolBullet, barrelend[rand].position, barrelend[rand].rotation) as Rigidbody;
            bulletInstance.AddForce(barrelend[rand].forward * Pistolbulletforce);
            if (randCount <= 0)
            {
                bounce = false;
                pistol = true;
                MiniGun = false;
                RanCannon = false;
            }
        }
    }
    void ShootMini()
    {
        if (miniAmmo > 0)
        {
            audioSource.PlayOneShot(miniPew, 0.2f);
            Rigidbody bulletInstance;
            bulletInstance = Instantiate(MiniBullet, barrelend[0].position, barrelend[0].rotation) as Rigidbody;
            miniAmmo -= 1;
            bulletInstance.AddForce(barrelend[0].forward * MiniGunBulletforce);
            if (miniAmmo <= 0)
            {
                bounce = false;
                pistol = true;
                MiniGun = false;
                RanCannon = false;
            }
        }
    }
    public void MiniPickup()
    {
        miniAmmo = 200;
        bounce = false;
        pistol = false;
        MiniGun = true;
        RanCannon = false;
    }
    public void RanPickup()
    {
        randCount = 10;
        MiniGun = false;
        bounce = false;
        pistol = false;
        RanCannon = true;
    }
    public void BouncePickup()
    {
        bounammo = 10;
        MiniGun = false;
        bounce = true;
        pistol = false;
        RanCannon = false;

    }
    //IEnumerator BounceShoot()
    //{
    //    if (bounammo > 0)
    //    {
    //        yield return new WaitForSeconds(0.5f);
    //        Rigidbody bulletInstance;
    //        bulletInstance = Instantiate(BounceBullet, barrelend[0].position, barrelend[0].rotation) as Rigidbody;
    //        bulletInstance.AddForce(barrelend[0].forward * shotgunbulletforce);
    //        bounammo -= 1;

    //        if (bounammo <= 0)
    //        {
    //            bounce = false;
    //            pistol = true;
    //            MiniGun = false;
    //            RanCannon = false;
    //        }
    //    }
        
    //}

}
