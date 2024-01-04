using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Car : MonoBehaviour {

    float steerSpeed = 300;
    float moveSpeed = 10;
    int carHealth = 100;
    bool hasPackage;
    Color32 hasPackageColor = new Color32(222, 100, 0, 255);
    Color32 noPackageColor = new Color32(255, 255, 255, 255);
    SpriteRenderer carSpriteRenderer;


    // Start is called before the first frame update
    void Start() {
        carSpriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update() {
        // multiply time to make the control the same at all computers, however fast or slow
        float moveAmount = Input.GetAxis("Vertical") * moveSpeed * Time.deltaTime;

        // only turn the car when car engin starts
        if (moveAmount != 0) {
            float steerAmount = Input.GetAxis("Horizontal") * steerSpeed * Time.deltaTime;
            //moveSpeed = Input.GetAxis("Shift");
            transform.Rotate(0, 0, -steerAmount);
            transform.Translate(0, moveAmount, 0);
        }
    }

    /// <summary>
    /// Minus car health when the car hit other things
    /// </summary>
    /// <param name="other">The Collision2D data associated with this collision.</param>
    void OnCollisionEnter2D(Collision2D other) {
        if (carHealth != 0) {
            carHealth--;
            moveSpeed = 10;
        } else if (carHealth == 0) {
            moveSpeed = 0;
        }
    }

    /// <summary>
    /// Sent when another object enters a trigger collider attached to this
    /// object (2D physics only).
    /// </summary>
    /// <param name="other">The other Collider2D involved in this collision.</param>
    void OnTriggerEnter2D(Collider2D other) {

        if (other.tag == "Parcel" && !hasPackage) {
            hasPackage = true;
            carSpriteRenderer.color = hasPackageColor;
        }

        if (other.tag == "Customer" && hasPackage) {
            hasPackage = false;
            carSpriteRenderer.color = noPackageColor;
        }

        if(other.tag == "Booster"){
            moveSpeed = 20;
        }
        Destroy(other.gameObject, 0.5f);
    }
}
