using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))   //number 1
        {
            SceneManager.LoadScene("inventory");
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))   //number 2
        {
            SceneManager.LoadScene("game");
        }
    }
}
