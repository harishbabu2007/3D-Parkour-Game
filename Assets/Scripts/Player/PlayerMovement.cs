using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
  // Start is called before the first frame update
  private CharacterController controller;
  public float speed = 12, gravity = -19.6f, jumpHeight = 3f, sprintSpeed;
  private Vector3 velocity;
  public Transform feet, gunTransform, camTransform;
  public float groundDistance = 2f;
  public LayerMask groundMask;
  private bool isGrounded;

  void Start()
  {
    controller = GetComponent<CharacterController>();
    camTransform = Camera.main.transform;
  }

  // Update is called once per frame 
  void Update()
  {
    if (controller.collisionFlags == CollisionFlags.None)
    {
      gravity = -19.6f;
    }

    isGrounded = Physics.CheckSphere(feet.position, groundDistance, groundMask);

    if (isGrounded && velocity.y < 0)
    {
      velocity.y = -2f;
    }

    float x = Input.GetAxis("Horizontal");
    float z = Input.GetAxis("Vertical");

    Vector3 move = transform.right * x + transform.forward * z;
    controller.Move(move * speed * Time.deltaTime);

    if (Input.GetButtonDown("Jump") && isGrounded)
    {
      velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
    }

    velocity.y += gravity * Time.deltaTime;
    controller.Move(velocity * Time.deltaTime);

    if (Input.GetKeyDown(KeyCode.LeftShift))
    {
      speed += sprintSpeed;
    }
    else if (Input.GetKeyUp(KeyCode.LeftShift))
    {
      speed -= sprintSpeed;
    }


    if (Input.GetKeyDown(KeyCode.C))
    {
      transform.localScale = new Vector3(1, 0.5f, 1);
      gunTransform.localScale = new Vector3(gunTransform.localScale.x, gunTransform.localScale.y + 0.5f, gunTransform.localScale.z);
      camTransform.localScale = new Vector3(camTransform.localScale.x, camTransform.localScale.y + 0.5f, camTransform.localScale.z);
    }
    else if (Input.GetKeyUp(KeyCode.C))
    {
      transform.localScale = new Vector3(1, 1, 1);
      gunTransform.localScale = new Vector3(gunTransform.localScale.x, gunTransform.localScale.y - 0.5f, gunTransform.localScale.z);
      camTransform.localScale = new Vector3(camTransform.localScale.x, camTransform.localScale.y - 0.5f, camTransform.localScale.z);
    }
  }

  private void OnControllerColliderHit(ControllerColliderHit hit)
  {
    if (hit.collider.gameObject.CompareTag("WallJump"))
    {
      gravity = -1f;
      isGrounded = true;
    }

    else
    {
      gravity = -19.6f;
    }

    if (hit.collider.gameObject.CompareTag("Void"))
    {
      SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    if (hit.gameObject.CompareTag("Glass"))
    {
      Glass glass = hit.gameObject.GetComponent<Glass>();
      if (glass != null)
      {
        glass.GlassBreak();
      }
    }
  }
}
