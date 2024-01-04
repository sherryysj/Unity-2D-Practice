using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour {

    public ParticleSystem deadEffect;

    private Rigidbody2D rb2d;
    private float torqueAmount = 5f;
    private int reloadDelayTime = 1;

    // Start is called before the first frame update
    void Start() {
        rb2d = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update() {
        if (Input.GetKey(KeyCode.LeftArrow)) {
            rb2d.AddTorque(torqueAmount);
        } else if (Input.GetKey(KeyCode.RightArrow)) {
            rb2d.AddTorque(-torqueAmount);
        }
    }

    /// <summary>
    /// Kill the player when the player's head hit the snow surface
    /// </summary>
    /// <param name="other">The other Collider2D involved in this collision.</param>
    void OnTriggerEnter2D(Collider2D other) {
        if (other.tag == "SnowSurface") {
            deadEffect.Play();
            Invoke("ReloadScene", reloadDelayTime);
        }
    }

    void ReloadScene() {
        SceneManager.LoadScene(0);
    }
}
