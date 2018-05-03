using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmController : MonoBehaviour {


    public bool attacking;
    public GameObject weapon;
    private Animation armWaving;
    private WeaponController weaponCtrl;

    void Start () {
        armWaving =  GetComponent<Animation>();
        weaponCtrl =  weapon.GetComponent<WeaponController>();
    }

    void Update() {

        if (weapon != null) {
            if (attacking) {
                weaponCtrl.attacking = true;
                if (weapon.tag == "Melee" || weapon.tag == "Pickaxe") {
                    armWaving.Play();
                }
            } else {
                armWaving.Stop();
                transform.rotation = transform.parent.rotation;
                weaponCtrl.attacking = false;
            }
        } else {
            armWaving.Stop();
            transform.rotation = transform.parent.rotation;
        }

    }


}
