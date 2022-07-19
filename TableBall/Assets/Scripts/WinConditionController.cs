using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinConditionController : MonoBehaviour
{
    [Header("Get Animation win")]
    [SerializeField] LevelAnimationController levelController;
    private void OnCollisionEnter(Collision other) 
    {
        if(other.gameObject.tag == "Player")
        {
            levelController.startAnimationWin();
        }    
    }
}
