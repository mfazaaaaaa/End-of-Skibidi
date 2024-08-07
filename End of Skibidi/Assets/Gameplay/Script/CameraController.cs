using UnityEngine;

public class CameraController : MonoBehaviour
{
    //Follow player
    [SerializeField] private Transform player;

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(player.position.x, transform.position.y, transform.position.z);
    }
}
