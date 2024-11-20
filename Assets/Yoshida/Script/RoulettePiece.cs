using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RoulettePiece : MonoBehaviour
{
    public int No;
    public GameObject Me;
    private Text textComponent;

    // Start is called before the first frame update
    void Start()
    {
        Me = this.gameObject;
        textComponent = GetComponentInChildren<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
