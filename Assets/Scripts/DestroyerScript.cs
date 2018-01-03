using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DestroyerScript : MonoBehaviour {
    
    void OnTriggerEnter2D(Collider2D other) {
        if(other.tag == "Player") {
            SceneManager.LoadScene(1);
            return;
        }

        if(other.gameObject.transform.parent) {
            Destroy (other.gameObject.transform.parent.gameObject);
        } else {
            Destroy(other.gameObject);
        }
    }
}
