using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileMover : MonoBehaviour
{
    public float speed = 15f;
    public float hitOffset = 0f;
    public bool UseFirePointRotation;
    public GameObject hit;
    public bool IsEnemy;
    public float lifeTime = 6.0f;

    void Start()
    {
        StartCoroutine(WaitToDestroy());
    }

    void FixedUpdate()
    {
        if (speed != 0)
        {
            transform.position += transform.forward * (speed * Time.deltaTime);
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag != "Player" && IsEnemy == false)
        {
            speed = 0;
            ContactPoint contact = collision.contacts[0];
            Quaternion rot = Quaternion.FromToRotation(Vector3.up, contact.normal);
            Vector3 pos = contact.point + contact.normal * hitOffset;

            if (hit != null)
            {
                var hitInstance = Instantiate(hit, pos, rot);
                if (UseFirePointRotation)
                { hitInstance.transform.rotation = gameObject.transform.rotation * Quaternion.Euler(0, 180f, 0); }
                else
                { hitInstance.transform.LookAt(contact.point + contact.normal); }

                var hitPs = hitInstance.GetComponent<ParticleSystem>();
                if (hitPs == null)
                {
                    Destroy(hitInstance, hitPs.main.duration);
                }
                else
                {
                    var hitPsParts = hitInstance.transform.GetChild(0).GetComponent<ParticleSystem>();
                    Destroy(hitInstance, hitPsParts.main.duration);
                }
                Destroy(gameObject);
            }
        }
        else if (collision.transform.tag != "Enemy" && IsEnemy == true)
        {
            speed = 0;
            ContactPoint contact = collision.contacts[0];
            Quaternion rot = Quaternion.FromToRotation(Vector3.up, contact.normal);
            Vector3 pos = contact.point + contact.normal * hitOffset;

            if (hit != null)
            {
                var hitInstance = Instantiate(hit, pos, rot);
                if (UseFirePointRotation)
                { hitInstance.transform.rotation = gameObject.transform.rotation * Quaternion.Euler(0, 180f, 0); }
                else
                { hitInstance.transform.LookAt(contact.point + contact.normal); }

                var hitPs = hitInstance.GetComponent<ParticleSystem>();
                if (hitPs == null)
                {
                    Destroy(hitInstance, hitPs.main.duration);
                }
                else
                {
                    var hitPsParts = hitInstance.transform.GetChild(0).GetComponent<ParticleSystem>();
                    Destroy(hitInstance, hitPsParts.main.duration);
                }
            }
            Destroy(gameObject);
        }

    }

    IEnumerator WaitToDestroy()
    {
        yield return new WaitForSeconds(lifeTime);
        Destroy(gameObject);
    }
}
