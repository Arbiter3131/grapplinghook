using UnityEngine;
using UnityEngine.SceneManagement;
public class ReturnBox : MonoBehaviour
{

    void OnTriggerEnter(Collider collision)
    {
        
        if(collision.transform.tag == "Player")
        {
            SceneManager.LoadScene("MainMenu");
        }
    }
}
