using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.UIElements;

public class Strzala : MonoBehaviour
{
    private float speed; 
    private float range; 
    private float time;
    private float przyspieszenie;
    private float predkoscOpadania;
    private GameObject thisArrow;
    private float damage;
    private Rigidbody rb;
    private float timer;
    private float timeToDestroy;
    private Vector3 kierunek;

    // Start is called before the first frame update
    void Start()
    {
        this.thisArrow = gameObject;
        this.damage = 15f;
        speed = 8.0f;
        range = 5f;
        time = range / speed; 
        predkoscOpadania = 0f;
        przyspieszenie = 0.001f;
        rb = GetComponent<Rigidbody>();
        timer = 0f;
        timeToDestroy = 3f; 
        kierunek = new Vector3(1f,0.2f,0f);
    }

    public void setSpeed(float speed)
    {
        this.speed = speed;
    }

    public void setRange(float range)
    {
        this.range = range;
    }

    public GameObject getObject()
    {
        return this.thisArrow;
    }

    public float getDamage()
    {
        return this.damage;
    }
    public void UstawKierunek(Vector3 kierunek)
    {
        this.kierunek = kierunek.normalized;
        transform.rotation = Quaternion.LookRotation(kierunek, Vector3.up) * Quaternion.Euler(0f, 90f, 0f);
    }

    void Update()
    {
        if (transform.position.y > 0.08f)
        {
            predkoscOpadania += przyspieszenie;

            transform.Translate(kierunek * speed * Time.deltaTime);
            transform.Translate(Vector3.down * predkoscOpadania * Time.deltaTime);


            /*obraca strza³ê pod k¹tem do ziemi, nisetety na razie psuje resztê*/
          /*  float angle = Mathf.Atan2(predkoscOpadania*20, speed) * Mathf.Rad2Deg;
            angle = Mathf.Clamp(angle, 0, 90f);
            transform.rotation = Quaternion.Euler(90f, 0f, -angle); */
        }
        else
        {
            timer += Time.deltaTime;
            if (timer >= timeToDestroy)
            {
                Destroy(gameObject);
                timer = 0f;
            }
            transform.position = new Vector3(transform.position.x,0.08f,transform.position.z);
        }
    }
}
