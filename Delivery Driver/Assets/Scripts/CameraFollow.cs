using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {
    public GameObject objectToFollow;

    // Update is called once per frame
    void LateUpdate() {
        transform.position = objectToFollow.transform.position + new Vector3(0, 0, -10);
    }
}
