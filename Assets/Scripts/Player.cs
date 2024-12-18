using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    private float distance, time, xPos;

    public float speed;

    public bool hasGameStarted;
    private bool isMobile;
   
    private void Start()
    {
        hasGameStarted = false;
        speed = distance/time;
        isMobile = Application.platform == RuntimePlatform.Android ||
                   Application.platform == RuntimePlatform.IPhonePlayer;
    }

    private void FixedUpdate()
    {
        if (!GameManager.instance.isGamerunning) return;


        Vector3 forwardMovement = Vector3.forward * speed * Time.fixedDeltaTime;

        // Handle horizontal movement
        float horizontalInput = isMobile ? GetMobileInput() : Input.GetAxis("Horizontal");
        Vector3 horizontalMovement = Vector3.right * horizontalInput * speed*2 *Time.fixedDeltaTime;

        Vector3 movement = forwardMovement + horizontalMovement;
        transform.Translate(movement);

        // Clamp position to allowed x range
        Vector3 position = transform.position;
        position.x = Mathf.Clamp(position.x, -xPos, xPos);
        transform.position = position;
    }

    private float GetMobileInput()
    {
        // Handle touch input for mobile
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Moved)
            {
                return touch.deltaPosition.x / Screen.width * 10f; // Adjust sensitivity
            }
        }
        return 0f;
    }

  
}
