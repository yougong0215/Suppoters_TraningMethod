using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ClearExi : MonoBehaviour
{
    // Start is called before the first frame update


    float t = 0;
    void Update()
    {
        t += Time.deltaTime;

        if(t > 1f && Input.GetMouseButtonDown(0))
        {
            SceneManager.LoadScene("Lobby");
        }
    }
}
