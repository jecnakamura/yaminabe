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
      

    }
    public void OnClick()
    {
        RouletteResultHandler.SetResult(0);
        RouletteResultHandler.SetEnd(true);
    }
}
