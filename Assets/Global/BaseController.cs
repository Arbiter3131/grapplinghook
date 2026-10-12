using UnityEngine;

public abstract class BaseController : MonoBehaviour
{
    // I want this to be the main communication point between all scripts on a character

    // for example, when inputhandler detects input, it'll pass to here, which then the controller will call the specific methods.

    // this should not be directly used, this will just serve as an abstract, as in a guide to implement the specific enemy/player controllers.
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
