using System.Collections.Generic;
using UnityEngine;

public class roulette : MonoBehaviour
{
    public GameObject Roulette;
    public GameObject Arow;
    // Start is called before the first frame update
    void Start()
    {
        Roulette = GameObject.Find("Roulette");
        Arow = GameObject.Find("Arow");
    }

    // Update is called once per frame
    void Update()
    {

    }
}
