
using UnityEngine;

public class Ball : MonoBehaviour
{
    [SerializeField]
    private float distance, time;

    [SerializeField]
    private GameObject explosionPrefab;

    private float speed, startSpeed, acceleration;

   

    private void Start()
    {
       

        startSpeed = 2 * distance / time;
        acceleration = -0.995f * startSpeed / time;
        speed = startSpeed;
    }

    private void FixedUpdate()
    {

        if (!GameManager.instance.isGamerunning) return;

        speed += acceleration * Time.fixedDeltaTime;
        Vector3 temp = new Vector3(0, speed * Time.fixedDeltaTime, 0);
        transform.localPosition += temp;
        temp = transform.localPosition;

        if(temp.y < -2f)
        {
            GameManager.instance.GameOver();
            Instantiate(explosionPrefab, transform.position, Quaternion.identity);
            Destroy(transform.parent.gameObject);
            
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Diamond"))
        {
            Destroy(other.gameObject);
            GameManager.instance.UpdateDiamond();
            GameObject temp = Instantiate(explosionPrefab);
            temp.transform.position = other.gameObject.transform.position;
            temp.GetComponent<Explosion>().SetAsDiamond();
        }

        if(other.CompareTag("Block"))
        {
            GameManager.instance.UpdateScore();
            speed = startSpeed;
            GameManager.instance.SpawnBlock();
        }
    }


}
