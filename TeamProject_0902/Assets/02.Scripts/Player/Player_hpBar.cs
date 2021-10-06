using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player_hpBar : MonoBehaviour
{
    public Slider playerSlider3D;
    Slider playerSlider2D;

    Stats statsScript;
        
    void Start()
    {

        statsScript = GameObject.FindGameObjectWithTag("Player").GetComponent<Stats>();


        playerSlider2D = GetComponent<Slider>();

        playerSlider3D.maxValue = statsScript.maxHealth;
        playerSlider2D.maxValue = statsScript.maxHealth;
        statsScript.health = statsScript.maxHealth;

    }

    void Update()
    {
        playerSlider2D.value = statsScript.health;
        playerSlider3D.value = playerSlider2D.value;
    }

    private void LateUpdate()
    {
        transform.LookAt(Camera.main.transform);
        transform.Rotate(0, 180, 0);
    }
}
