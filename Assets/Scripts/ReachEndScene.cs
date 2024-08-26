using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ReachEndScene : MonoBehaviour
{
    public float gameOverAltitude = -10f; // Set the altitude threshold
    public string gameOverSceneName = "GameOver"; // Name of the game over scene


    // Update is called once per frame
    void Update()
    {
        if (transform.position.y < gameOverAltitude)
        {
            SceneManager.LoadScene(gameOverSceneName);
        }
    }
}
