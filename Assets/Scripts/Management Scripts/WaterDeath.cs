using UnityEngine;

public class WaterDeath : MonoBehaviour
{

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // First deal the damage (reduces life count)
            other.GetComponent<PlayerDamage>().DealDamage();

            // Then tell GameManager to handle respawn or end screen
            GameManager.Instance.PlayerFellInWater(transform.position);
        }
    }

} // class