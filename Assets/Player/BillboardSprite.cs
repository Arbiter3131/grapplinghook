using UnityEngine;

public class BillboardSprite : MonoBehaviour
{

    // script used to make objects always face the player camera, includes up and down.
    GameObject cameraPos;

    void Start()
    {
        cameraPos = GameObject.FindWithTag("MainCamera");
    }

    void Update()
    {
        Vector3 dir = cameraPos.transform.position - transform.position;
        transform.LookAt(transform.position + dir);
    }
}
