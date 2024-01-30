using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    NewPlayerController player;
    Text distanceText;
    private void Awake()
    {
        player = GameObject.Find("Player").GetComponent<NewPlayerController>();
        distanceText = GameObject.Find("DistanceText").GetComponent<Text>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        int distance = (int)player.distance; 
        distanceText.text = distance.ToString() + "m";
    }
}
