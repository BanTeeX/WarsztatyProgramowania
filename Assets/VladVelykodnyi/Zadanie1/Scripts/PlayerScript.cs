//TODO-Vlad Velykodnyi: niepotrzebne usingi do usunięcia
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

//TODO-Vlad Velykodnyi: dodać namespace zgodny z folderami

//TODO-Vlad Velykodnyi: warto dodać do klasy atrybut RequireComponent, w tym przypadku [RequireComponent(typeof(RigidBody))]
public class PlayerScript : MonoBehaviour //TODO-Vlad Velykodnyi: czytelniejsza nazwa klasy, PlayerScript nic nam nie mówi o tym co robi skrypt, PlayerMovement byłoby bardziej odpowiednie 
{
    private float gravity = 10f; //TODO-Vlad Velykodnyi: gravity też warto dać do inspektora poprzez [SerializeField]
    [SerializeField] private float moveSpeed = 6f;
    [SerializeField] private float turnSpeed = 90f;
    [SerializeField] private float lerpSpeed = 10f;
    
    //TODO-Vlad Velykodnyi: nazwy nieserializowanych pól prywatnych powinny zaczynać się od podkreślnika
    private Vector3 surfaceNormal;
    private Vector3 thisNormal;
    private Rigidbody thisRigidbody;

    public BoxCollider boxCollider; //TODO-Vlad Velykodnyi: usunąć niepotrzebne pole

    private void Start() //TODO-Vlad Velykodnyi: wszystkie instrukcje wykonane w tej metodzie to instrukcje inicjalizacji tego skryptu, takie instrukcje warto dać do metody Awake
    {
        thisRigidbody = gameObject.GetComponent<Rigidbody>(); //TODO-Vlad Velykodnyi: gameobject. jest nie potrzebne, samo GetComponent starczy
        thisNormal = transform.up;
        thisRigidbody.freezeRotation = true;
    }

    private void FixedUpdate()
    {
        thisRigidbody.AddForce(-gravity * thisRigidbody.mass * thisNormal);
    }

    private void Update() //TODO-Vlad Velykodnyi: instrukcje tutaj mogą przejść do FixedUpdate, gdyż obiekt jest fizyczny i wykorzystane są fizyczne raycasty
    {
        //TODO-Vlad Velykodnyi: typy lokalnych zmiennych można deklarować za pomocą var, warto to robić gdyż zmniejsza to objętość kodu, wymusza pisanie czytelniejszych nazw i ułatwia refaktoryzację
        Ray ray = new Ray(transform.position, -thisNormal);
        RaycastHit hit; //TODO-Vlad Velykodnyi: ta zmienna może być zadeklarowana inline w metodzie Raycast
        if (Physics.Raycast(ray, out hit))
        {
            surfaceNormal = hit.normal;
        }
        transform.Rotate(0, Input.GetAxis("Horizontal") * turnSpeed * Time.deltaTime, 0); //TODO-Vlad Velykodnyi: sczytywanie inputa powinniśmy dać do innego skryptu, umieszczając to tutaj naruszamy single-responsibility principle

        thisNormal = Vector3.Lerp(thisNormal, surfaceNormal, lerpSpeed * Time.deltaTime);
        Vector3 myForward = Vector3.Cross(transform.right, thisNormal);
        Quaternion targetRot = Quaternion.LookRotation(myForward, thisNormal);
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRot, lerpSpeed * Time.deltaTime);
        transform.Translate(0, 0, Input.GetAxis("Vertical") * moveSpeed * Time.deltaTime);
    }
}

//Ogólnie mechanika sama w sobie bardzo dobra z drobnymi uwagami ale szczegóły omówimy na spotkaniu
//Do poprawy jest trochę czytelność i czystość kodu, ale to estetyczne kwestie naprawiane podczas refaktoryzacji
//PS po własnych poprawkach komentarze do usunięcia
