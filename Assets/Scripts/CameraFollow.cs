
using UnityEngine;

public class CameraFollow : MonoBehaviour
{

    [SerializeField]
    private Transform player;
    private Vector3 offset;
    public bool canFollow;

    // Start is called before the first frame update
    void Start()
    {
        offset = transform.position - player.transform.position;
    }

    private void FixedUpdate()
    {
        if (!GameManager.instance.isGamerunning) return;

        transform.position = new Vector3(transform.position.x,transform.position.y, player.position.z + offset.z);
    }
}
