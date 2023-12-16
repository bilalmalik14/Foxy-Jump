using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float speed;
    public Animator animator;

    private Rigidbody2D body;
    // Start is called before the first frame update
    int JumpCount = 0;
    public int MaxJumps = 1; //Maximum amount of jumps (i.e. 2 for double jumps)

    private void Start()
    {
        body = GetComponent<Rigidbody2D>();

        JumpCount = MaxJumps;
    }

    // Update is called once per frame

    private void Update()
    {
        body.velocity = new Vector2(Input.GetAxis("Horizontal") * speed, body.velocity.y);

        Flip();

        animator.SetFloat("Speed", Mathf.Abs(body.velocity.x));
        animator.SetFloat("JumpSpeed", (body.velocity.y));

        if(Input.GetKeyDown(KeyCode.UpArrow))
        {
            if (JumpCount > 0) 
            {
                body.velocity = new Vector2(body.velocity.x, speed*2);
                JumpCount =  JumpCount - 1;
                animator.SetFloat("JumpSpeed", (body.velocity.y));
            }
        }
    }

    public void Flip()
    {
        Vector3 currentScale = transform.localScale;
        if (Input.GetAxis("Horizontal") < 0)
        {
            currentScale.x = -10;
        }
        else
        {
            currentScale.x = 10;
        }
        transform.localScale = currentScale;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.gameObject.tag == "KILLZONE")
        {
            Scene CS = SceneManager.GetActiveScene();
            SceneManager.LoadScene(CS.name);
        }

        if (collision.gameObject.tag == "Enemy")
        {
            Scene CS = SceneManager.GetActiveScene();
            SceneManager.LoadScene(CS.name);
        }

        if (collision.gameObject.tag == "Ground")
        {
            JumpCount = MaxJumps;
        }
    }
}
