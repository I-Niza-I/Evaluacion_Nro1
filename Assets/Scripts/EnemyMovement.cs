using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] private Transform startPoint;
    [SerializeField] private Transform endPoint;
    [SerializeField] private GameObject objectToMove = null;
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private Animator animator;
    [SerializeField] private float pushForce = 5f;

    private float speed = 0.8f;
    private Vector3 moveTo;

    private Rigidbody2D rb;
    private bool isAlive = true;
    private bool isPushed = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

        if (objectToMove != null)
        {
            spriteRenderer = objectToMove.GetComponent<SpriteRenderer>();
        }
        moveTo = endPoint.position;
        rb = objectToMove.GetComponent<Rigidbody2D>();

    }

    // Update is called once per frame
    void Update()
    {
        // si esta muerto no hace nada
        if (!isAlive) return;

        // Si fue empujado, intercambia la "prioridad" de las fisicas
        if (isPushed) return;


        objectToMove.transform.position = Vector3.MoveTowards(objectToMove.transform.position, moveTo, speed * Time.deltaTime);
        if (objectToMove.transform.position == endPoint.position)
        {
            moveTo = startPoint.position;
            spriteRenderer.flipX = true;
        }
        else if (objectToMove.transform.position == startPoint.position)
        {
            moveTo = endPoint.position;
            spriteRenderer.flipX = false;
        }
    }

    public void OnChildCollision(Collision2D col)
    {
        if (!isAlive) return;

        if (col.gameObject.tag == "Player")
        {
            // Obtiene la direccion del empuje dependiendo de la posicion del jugador
            float direction = objectToMove.transform.position.x - col.transform.position.x;
            // Normaliza la direccion a -1 o 1, osea , izquierda o derecha
            direction = Mathf.Sign(direction);

            isPushed = true;
            rb.linearVelocity = Vector2.zero;  // Resetea la velocidad para que el empuje sea consistente
            rb.AddForce(new Vector2(direction * pushForce, 0), ForceMode2D.Impulse); // Empuja al enemigo en la direccion opuesta al jugador
        }
    }

    // Si el enemigo choca con una espina, muere
    public void OnChildTrigger(Collider2D col)
    {
        if (!isAlive) return;

        if (col.tag == "Spikes")
        {
            Morir();
        }
    }

    private void Morir()
    {
        // Cambia el estado a muerto, detiene el movimiento y reproduce la animacion de muerte
        isAlive = false;
        isPushed = false;
        rb.linearVelocity = Vector2.zero;
        rb.bodyType = RigidbodyType2D.Kinematic;

        animator.SetTrigger("Death"); // Reproduce la animacion de muerte
        GameController.GetInstance().AddDinos(1); // Agrega un dino al contador del jugador

        Destroy(objectToMove, 1.5f); // Espera a que termine la animacion
    }
}
