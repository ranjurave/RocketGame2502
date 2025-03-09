using System.Collections.Generic;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine;
using Unity.VisualScripting;

public class RocketControls : MonoBehaviour { 

    [SerializeField] float ThrustForce;
    [SerializeField] float RotationForce;

    [SerializeField] AudioClip EngineSound;
    [SerializeField] AudioClip GameOverSound;
    Rigidbody RocketRB;
    AudioSource RocketAudio;
    bool RocketCrashed = false;
    int Coins = 0;
    float Fuel = 1000.0f;
    [SerializeField] float FuelRate = 10;

    void Start() {
        RocketRB = GetComponent<Rigidbody>();
        RocketAudio = GetComponent<AudioSource>();
    }

    void Update() {
        // Check if the rocket is alive
        if (!RocketCrashed && Fuel > 0) { 
            RocketInputControl();
        }
    }

    //Reading input
    private void RocketInputControl() {
        if (Input.GetKey(KeyCode.Space)) {
            RocketRB.AddRelativeForce(Vector3.forward * Time.deltaTime * ThrustForce);
            Fuel -= FuelRate * Time.deltaTime;
            Debug.Log(Fuel);
            if (!RocketAudio.isPlaying) {
                RocketAudio.PlayOneShot(EngineSound);
            }
        }
        if (Input.GetKeyUp(KeyCode.Space)) {
            RocketAudio.Stop();
        }

        if (Input.GetKey(KeyCode.RightArrow)) {
            RocketRB.freezeRotation = true;
            transform.Rotate(Vector3.up * Time.deltaTime * RotationForce);
            RocketRB.freezeRotation = false;
        }
        if (Input.GetKey(KeyCode.LeftArrow)) {
            RocketRB.freezeRotation = true;
            transform.Rotate(Vector3.down * Time.deltaTime * RotationForce);
            RocketRB.freezeRotation = false;
        }
    }

    private void OnCollisionEnter(Collision collision) {
        switch (collision.gameObject.tag) {
            case "Respawn":
                //Debug.Log("Game Over");
                RocketCrashed = true; 
                RocketAudio.PlayOneShot(GameOverSound);
                StartCoroutine(ReloadGame());
                break;
            case "Finish":
                //Debug.Log("Level Complete");
                StartCoroutine(LoadNextLevel());
                break;
        }
    }

    private void OnTriggerEnter(Collider other) {
        switch (other.gameObject.tag) {
            case "Coin":
                Coins++;
                Debug.Log("Coins : " + Coins);
                Destroy(other.gameObject);
                break;
            case "Fuel":
                Fuel += 200;
                Destroy(other.gameObject);
                break;
        }
    }

    IEnumerator ReloadGame() {
        yield return new WaitForSeconds(3);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    IEnumerator LoadNextLevel() {
        yield return new WaitForSeconds(3);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+1);
    }
}
