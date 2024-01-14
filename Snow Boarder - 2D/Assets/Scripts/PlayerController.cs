using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{

    public ParticleSystem deadEffect;
    public ParticleSystem snowEffect;
    public AudioClip crash;

    private Rigidbody2D rb2d;
    private float torqueAmount = 5f;
    private int reloadDelayTime = 1;
    private float boostSpeed = 40f;
    private float defaultSpeed = 20f;
    private SurfaceEffector2D surfaceEffector2D;
    private bool canMove = true;
    private bool hasCrashed = false;

    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        surfaceEffector2D = FindObjectOfType<SurfaceEffector2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (canMove)
        {
            RotatePlayer();
            RespondToBoost();
        }
    }

    public void DisableControl()
    {
        canMove = false;
    }

    private void RotatePlayer()
    {
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            rb2d.AddTorque(torqueAmount);
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            rb2d.AddTorque(-torqueAmount);
        }
    }

    private void RespondToBoost()
    {
        if (Input.GetKey(KeyCode.UpArrow))
        {
            surfaceEffector2D.speed = boostSpeed;
        }
        else
        {
            surfaceEffector2D.speed = defaultSpeed;
        }
    }

    /// <summary>
    /// Kill the player when the player's head hits the snow surface
    /// </summary>
    /// <param name="other">The other Collider2D involved in this collision.</param>
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "SnowSurface" && !hasCrashed)
        {
            deadEffect.Play();
            GetComponent<AudioSource>().PlayOneShot(crash);
            hasCrashed = true;
            DisableControl();
            Invoke("ReloadScene", reloadDelayTime);
        }
    }

    void ReloadScene()
    {
        SceneManager.LoadScene(0);
    }

    /// <summary>
    /// Show snow dust effect when the player hits the snow surface
    /// </summary>
    /// <param name="other">The Collision2D data associated with this collision.</param>
    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "SnowSurface")
        {
            snowEffect.Play();
        }

    }

    /// <summary>
    /// Stop snow dust effect when the player leaves the snow surface
    /// </summary>
    /// <param name="other">The Collision2D data associated with this collision.</param>
    void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.tag == "SnowSurface")
        {
            snowEffect.Stop();
        }
    }
}
