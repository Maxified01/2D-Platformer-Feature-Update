using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerDamage : MonoBehaviour
{

    private Text lifeText;
    private int lifeScoreCount;

    private bool canDamage;

    // This lets the GameManager read the current life count
    public int LifeCount { get { return lifeScoreCount; } }

    void Awake()
    {
        lifeText = GameObject.Find("LifeText").GetComponent<Text>();
        lifeScoreCount = 3;
        lifeText.text = "x" + lifeScoreCount;
        canDamage = true;
    }

    void Start()
    {
        Time.timeScale = 1f;
    }

    public void DealDamage()
    {
        if (canDamage)
        {

            lifeScoreCount--;

            if (lifeScoreCount >= 0)
            {
                lifeText.text = "x" + lifeScoreCount;
            }

            // GameManager handles end screen now — remove the old restart logic
            // so there's one single place that decides what happens on death
            if (lifeScoreCount <= 0)
            {
                GameManager.Instance.ShowEndScreen();
            }

            canDamage = false;
            StartCoroutine(WaitForDamage());
        }
    }

    IEnumerator WaitForDamage()
    {
        yield return new WaitForSeconds(2f);
        canDamage = true;
    }

} // class