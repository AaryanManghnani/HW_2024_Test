using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class ExtractJson : MonoBehaviour
{
    [System.Serializable]
    public class PlayerData
    {
        public int speed;
    }

    [System.Serializable]
    public class PulpitData
    {
        public float min_pulpit_destroy_time;
        public float max_pulpit_destroy_time;
        public float pulpit_spawn_time;
    }

    [System.Serializable]
    public class GameData
    {
        public PlayerData player_data;
        public PulpitData pulpit_data;
    }

    public static int playerSpeed;
    public static float minPulpitDestroyTime;
    public static float maxPulpitDestroyTime;
    public static float pulpitSpawnTime;

    private string url = "https://s3.ap-south-1.amazonaws.com/superstars.assetbundles.testbuild/doofus_game/doofus_diary.json";

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(GetData());
    }

    // Update is called once per frame
    IEnumerator GetData()
    {
        UnityWebRequest request = UnityWebRequest.Get(url);
        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.ConnectionError || request.result == UnityWebRequest.Result.ProtocolError)
        {
            Debug.LogError(request.error);
        }
        else
        {
            string json = request.downloadHandler.text;
            GameData gameData = JsonUtility.FromJson<GameData>(json);

            // Store the variables in static fields
            playerSpeed = gameData.player_data.speed;
            minPulpitDestroyTime = gameData.pulpit_data.min_pulpit_destroy_time;
            maxPulpitDestroyTime = gameData.pulpit_data.max_pulpit_destroy_time;
            pulpitSpawnTime = gameData.pulpit_data.pulpit_spawn_time;

            Debug.Log("Extracted speed: " + playerSpeed);
            Debug.Log("Extracted minPulpitDestroyTime: " + minPulpitDestroyTime);
            Debug.Log("Extracted maxPulpitDestroyTime: " + maxPulpitDestroyTime);
            Debug.Log("Extracted pulpitSpawnTime: " + pulpitSpawnTime);
        }
    }
}
