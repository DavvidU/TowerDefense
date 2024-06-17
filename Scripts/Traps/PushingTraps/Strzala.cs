using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.UIElements;

/**
 * Klasa definiuj¹ca obiekt bêd¹cy strza³¹ wystrzeliwany z dispensera
 * 
 * @author Artur Leszczak
 * @version 1.0.0
 */
public class Strzala : MonoBehaviour
{
    private float speed; //arrow speed (default 16f)
    private float range; //arrow range (default 5f)
    private float time; 
    private float przyspieszenie; //arrow velocity (default 0.001f)
    private float predkoscOpadania; //arrow gravity speed (default 0)
    private GameObject thisArrow;
    private float damage; //defines how much damage arrow give (default 15f)
    private Rigidbody rb;
    private float timer;
    private float timeToDestroy; //time to destroy the arrow in seconds (default 3)
    private Vector3 kierunek;

    // Start is called before the first frame update
    void Start()
    {
        this.thisArrow = gameObject;
        this.damage = 15f;
        speed = 16.0f;
        range = 5f;
        time = range / speed; 
        predkoscOpadania = 0f;
        przyspieszenie = 0.001f;
        rb = GetComponent<Rigidbody>();
        timer = 0f;
        timeToDestroy = 3f; 
        kierunek = new Vector3(1f,0.2f,0f);
    }

    /*Defines speed of arrow*/
    public void setSpeed(float speed)
    {
        this.speed = speed;
    }

    /*Defines how far arrow fligt*/
    public void setRange(float range)
    {
        this.range = range;
    }

    /*Returns this arrow*/
    public GameObject getObject()
    {
        return this.thisArrow;
    }

    /*Returns damage set for this entity*/
    public float getDamage()
    {
        return this.damage;
    }

    /**
     * This metod sets a direction of arrow flight.
     * @param Vector3 kierunek - takes a Vector3 variable to defines a position to flight.
     * @author Artur Leszczak
     * @version 1.0.0
     */
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
