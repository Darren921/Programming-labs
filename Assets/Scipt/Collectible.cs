using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Collectible : MonoBehaviour
{
    private static int totalCoins = 0;
    [SerializeField] private Text _text;


    private void OnTriggerEnter(Collider other)
    {

        if (other.transform.tag == "Player")
        {
            totalCoins++;
            Destroy(gameObject);
            _text.text = "Total amount of coins: " + totalCoins.ToString();
           
        }


    }
}
