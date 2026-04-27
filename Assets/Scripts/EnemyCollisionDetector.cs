using UnityEngine;

public class EnemyCollisionDetector : MonoBehaviour
{
    private EnemyMovement movement;

    void Start()
    {
        movement = GetComponentInParent<EnemyMovement>();
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        movement.OnChildTrigger(col);
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        movement.OnChildCollision(col);
    }
}
