using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

public class Ammobox : MonoBehaviour
{
    public static Player _player;

    public void Update()
    {
        _player = new Player();

       _player.GetAmmo();
        

    }


    private void OnCollisionEnter(Collision collision)
    {
      

            //if the player hits a gameobject "BG" it stops all player input to the piece and
            // stops it from going further in the y direction 
            if (collision.gameObject.name == "g17" && _player.GetAmmo() == true)
            {
                Destroy(gameObject);
            }
        }

        }


    




