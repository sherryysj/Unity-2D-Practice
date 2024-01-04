using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FinishLine : MonoBehaviour {


    private int reloadDelayTime = 1;

    /// <summary>
    /// Finish game when player reach the finish line
    /// </summary>
    /// <param name="other">The other Collider2D involved in this collision.</param>
    void OnTriggerEnter2D(Collider2D other) {
        if (other.tag == "Player") {
            Invoke("ReloadScene", reloadDelayTime);
        }

    }

    void ReloadScene() {
        SceneManager.LoadScene(0);
    }
}
