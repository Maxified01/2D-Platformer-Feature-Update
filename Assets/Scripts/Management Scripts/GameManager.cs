using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [Header("UI")]
    public GameObject endScreenUI;

    [Header("Respawn")]
    public Transform[] respawnPoints;  // This is now an array — drag ALL respawn points here

    private GameObject player;
    private bool isRespawning = false;

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");

        if (endScreenUI != null)
            endScreenUI.SetActive(false);

        Time.timeScale = 1f;
    }

    public void PlayerFellInWater(Vector3 waterPosition)
    {
        if (isRespawning) return;
        isRespawning = true;

        PlayerDamage pd = player.GetComponent<PlayerDamage>();
        pd.DealDamage();

        if (pd.LifeCount > 0)
        {
            StartCoroutine(RespawnPlayer(waterPosition));
        }
        else
        {
            ShowEndScreen();
        }
    }

    // This Finds the closest respawn point to where the player died
    Transform GetClosestRespawnPoint(Vector3 waterPosition)
    {
        Transform closest = respawnPoints[0];
        float shortestDistance = Mathf.Infinity;

        foreach (Transform point in respawnPoints)
        {
            float distance = Vector3.Distance(waterPosition, point.position);
            if (distance < shortestDistance)
            {
                shortestDistance = distance;
                closest = point;
            }
        }

        return closest;
    }

    IEnumerator RespawnPlayer(Vector3 waterPosition)
    {
        yield return new WaitForSeconds(0.5f);

        // This Automatically picks the closest respawn point to the water
        Transform spawnPoint = GetClosestRespawnPoint(waterPosition);
        player.transform.position = spawnPoint.position;

        Rigidbody2D rb = player.GetComponent<Rigidbody2D>();
        if (rb != null) rb.linearVelocity = Vector2.zero;

        yield return new WaitForSeconds(0.5f);
        isRespawning = false;
    }

    public void ShowEndScreen()
    {
        Time.timeScale = 0f;
        if (endScreenUI != null)
            endScreenUI.SetActive(true);
    }

    public void Replay()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void Quit()
    {
        Application.Quit();
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}