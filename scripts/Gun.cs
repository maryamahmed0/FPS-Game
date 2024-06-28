using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class Gun : MonoBehaviour
{
    public float damage = 10f;
    public float range = 100f;
    public Camera cam;
    public ParticleSystem muzzleFlash;
    public float fireRate = 30f;
    float NextTimetoFire = 0f;
    public float MaxAmmo = 30f;
    public float currAmmo =-1f;
    public float ReloadTime = 1f;
    bool isReloading = false;
    public Animator animator;
    public Vector3 normallocalpos;
    public Vector3 aiminglocalpos;
    public float aimSmothing = 10f;
    public void Shoot()
    {
        currAmmo--;
        muzzleFlash.Play();
        RaycastHit hit;
        bool Hit=Physics.Raycast(cam.transform.position,cam.transform.forward,out hit,range);
        if(Hit)
        {
            Enemy enemy = hit.transform.GetComponent<Enemy>();
            if (enemy != null)
            {
                enemy.TakeDamage(damage);
            }
        }
    }
    void Aiming()
    {
        Vector3 target = normallocalpos;
            if (Input.GetButton("Fire2") )
                target = aiminglocalpos;
            Vector3 DesiredPos = Vector3.Lerp(transform.localPosition, target, Time.deltaTime * aimSmothing);
            transform.localPosition = DesiredPos;
    }
    void Start()
    {
        currAmmo = MaxAmmo;
    }
    void isEnable()
    {
        isReloading = false ;
        animator.SetBool("Reloading", false);
    }
    IEnumerator Reload()
    {
        isReloading = true;
        yield return new WaitForSeconds(0.25f);
        animator.SetBool("Reloading", true);
        yield return new WaitForSeconds(ReloadTime-0.25f);
        animator.SetBool("Reloading", false);
        currAmmo = MaxAmmo;
        isReloading =false;
        
    }
    void Update()
    {
        Aiming();
        if (isReloading)
        { return; }
        if (currAmmo <= 0 || (Input.GetKeyDown(KeyCode.R) && currAmmo < MaxAmmo))
        {
            StartCoroutine(Reload());
            return;
        }
        if (Input.GetButton("Fire1") && Time.time >= NextTimetoFire)
        {
            NextTimetoFire = Time.time + 1/fireRate;
            Shoot();
        }
    }
}
