using UnityEngine;

public class TopDownCharacterController : MonoBehaviour
{
    public float speed;
    private Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        Vector2 dir = Vector2.zero;
        
        if (Input.GetKey(KeyCode.A))
        {
            dir.x = CameraState.IsFlipped ? 1 : -1;  // Flip direction if camera is flipped
            animator.SetInteger("Direction", CameraState.IsFlipped ? 2 : 3);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            dir.x = CameraState.IsFlipped ? -1 : 1;  // Flip direction if camera is flipped
            animator.SetInteger("Direction", CameraState.IsFlipped ? 3 : 2);
        }

        if (Input.GetKey(KeyCode.W))
        {
            dir.y = 1;
            animator.SetInteger("Direction", 1);
        }
        else if (Input.GetKey(KeyCode.S))
        {
            dir.y = -1;
            animator.SetInteger("Direction", 0);
        }

        dir.Normalize();
        if (dir.magnitude == 0) animator.SetInteger("Direction", 4);  // Set direction to idle

        GetComponent<Rigidbody2D>().velocity = speed * dir;
    }
}
