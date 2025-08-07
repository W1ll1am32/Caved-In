using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

interface IInteractable {
    public void Interact();
}

public class Interactor : MonoBehaviour
{
    public Transform InteractSource;
    public float InteractDistance;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E)) {

            Ray r = new Ray(InteractSource.position, InteractSource.forward);
            Debug.DrawRay(r.origin, r.direction * 100f, Color.red, 3f);

            if (Physics.Raycast(r, out RaycastHit hitInfo, InteractDistance)) {
                print(hitInfo.collider.gameObject);
                if (hitInfo.collider.gameObject.TryGetComponent(out IInteractable InteractObj)) {
                    print(3);
                    InteractObj.Interact();
                }
            }
        }
    }
}
