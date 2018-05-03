using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour {

    public bool attacking = false;

    void Start () {
	}
	
	void Update () {
		
	}

    private void OnCollisionStay(Collision collision) {
        Debug.Log("report something");

        if (attacking && gameObject.tag == "Pickaxe") {
            List<string> rocksToDestroy = new List<string>();
            foreach (ContactPoint contact in collision.contacts) {
                string tag = contact.otherCollider.tag;
                Debug.Log("name " + contact.otherCollider);
                if (tag == "Rock") {
                    rocksToDestroy.Add(contact.otherCollider.name);
                }
            }
            MapManager.Instance.DestroyRocks(rocksToDestroy);
        }
    }

    private void OnCollisionEnter(Collision collision) {
        Debug.Log("report something");
        if (attacking && gameObject.tag == "Pickaxe") {
            List<string> rocksToDestroy = new List<string>();
            foreach (ContactPoint contact in collision.contacts) {
                string tag = contact.otherCollider.tag;
                Debug.Log("name " + contact.otherCollider);
                if (tag == "Rock") {
                    rocksToDestroy.Add(contact.otherCollider.name);
                }
            }
            MapManager.Instance.DestroyRocks(rocksToDestroy);
        }
    }

    private void OnTriggerEnter(Collider other) {
        if (attacking && gameObject.tag == "Pickaxe") {
            if (other.tag == "Rock") {
                MapManager.Instance.DestroyRocks(other.name);
            }
        }
    }

    private void OnTriggerStay(Collider other) {
        if (attacking && gameObject.tag == "Pickaxe") {

            if (other.tag == "Rock") {
                MapManager.Instance.DestroyRocks(other.name);
            }
        }
    }
}
