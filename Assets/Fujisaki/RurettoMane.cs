using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RurettoMane : MonoBehaviour
{
    [SerializeField]
    private float seconds;
    private bool kari;

    // Start is called before the first frame update
    void Start()
    {
        seconds = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            kari = true;
        }
        if(kari == true) {
            seconds += Time.deltaTime;
            if (seconds >= 6f)
            {
                SceneManager.UnloadSceneAsync("Ruretto");
            }
        }
        
    }
    public void OnClick()
    {
        RouletteResultHandler.SetResult(0);
        RouletteResultHandler.SetEnd(true);
    }
}
