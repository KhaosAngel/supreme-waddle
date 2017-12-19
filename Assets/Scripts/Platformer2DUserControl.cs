using UnityEngine;

[RequireComponent(typeof(PlatformerCharacter2D))]
public class Platformer2DUserControl : MonoBehaviour {

    private PlatformerCharacter2D character;
    private bool jump;
    //CrossPlatformInput CrossPlatformInput;

    // Use this for initialization
    void Awake() {
        character = GetComponent<PlatformerCharacter2D>();
    }

    // Update is called once per frame
    void Update() {
        //if (CrossPlatformInput.GetButtonDown("Jump")) {
        //    jump = true;
        //}
    }

    void FixedUpdate() {
        character.Move(1, false, jump);
        jump = false;
    }
}