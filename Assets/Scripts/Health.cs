using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Health : MonoBehaviour
{
    public TextMeshProUGUI text;
    public static Action<int> damage;
    public static Action death;
    public static int health;
    public GameObject pause;
    public Transform spawn;
    GameObject[] traps;
    [Header("HP")]
    public int healthStart;
    public int maxHP;
    private void Start()
    {
        traps = GameObject.FindGameObjectsWithTag("Traps");
        health = healthStart;
        damage += TakeDamage;
        death += Death;
    }
    void Update()
    {
        if(health > 0)
        {
            text.text = health.ToString();
        }
        else
        {
            Death();
        }
    }
    public void TakeDamage(int count)
    {
        health -= count;
        if(health <= 0)
        {
            Death();
        }
    }
    public void Death()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        health = maxHP;
        pause.SetActive(true);
    }
    public void Restart()
    {
        Movement.key = 0;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        this.GetComponent<Movement>().enabled = false;
        this.transform.position = spawn.position;
        StartCoroutine(on());
        pause.SetActive(false);
        foreach(GameObject i in traps)
        {
            i.SetActive(true);
        }
    }
    public IEnumerator on()
    {
        yield return new WaitForSeconds(0.2f);
        this.GetComponent<Movement>().enabled = true;
    }
}
