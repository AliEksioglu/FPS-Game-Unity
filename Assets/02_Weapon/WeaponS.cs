using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class WeaponS : MonoBehaviour
{
    public Transform whoFired;
    //Sound--

    [SerializeField] AudioSource shotSound;
    [SerializeField] AudioSource reloadSound;
    //--

    //Ammo----
    public AmmoChangeManager ammo;
    private List<GameObject> ammos;
    public float currentAmmo;

    private Camera fpsCamera;
    //----

    //Bullet ---

    [SerializeField] Transform firePoint;
    [SerializeField] Bullet _bulletPrefab;
    [SerializeField] private int _bulletAmount;

    List<Bullet> _bulletPool = new List<Bullet>();
    GameObject Bullets;

    //----
    private float coolDown = -1;
    [SerializeField] private Animator animWeapon;
    [SerializeField] private Animator animCamera;
    private void Awake()
    {
        fpsCamera = Camera.main;
        
        ammos = ammo.getAmmos();
        currentAmmo = 7f;
        Bullets = new GameObject("Bullets");
        animWeapon = GetComponent<Animator>();
        BulletLoading();
    }

    int reloadTime = 1;
    private void Update()
    {
        //Look
        if (!GameManager.playerIsDead)
        {
            if (coolDown > 0) coolDown -= Time.deltaTime;
            //Fire
            if ((Input.GetMouseButtonDown(0) && coolDown < 0 ) && currentAmmo > 0)
            {
                EventManager.Event_OnFirePistol();
                EventManager.Event_OnCurBulletChange();
                coolDown = 0.30f;
            }
            else if (Input.GetMouseButtonDown(0) && currentAmmo == 0)
            {
                EventManager.Event_OnFinishAmmo();
            }

            if(currentAmmo < 7 && Input.GetKeyDown(KeyCode.R))
            {
                if(reloadTime > 0 ) ReloadAmmo();
                coolDown = 1f;
            }
        }
    }

    private void BulletLoading()
    {
        for(int i = 0; i < _bulletAmount; i++)
        {
            Bullet newBullet = Instantiate(_bulletPrefab);
            newBullet.gameObject.SetActive(false);
            newBullet.transform.SetParent(Bullets.transform);
            newBullet.transform.position = firePoint.position;
            _bulletPool.Add(newBullet);
        }
        
    }
    Bullet GetBulletPool()
    {
        foreach(Bullet bullet in _bulletPool)
        {
            if (!bullet.gameObject.activeSelf) return bullet;
        }

        return null;
    }

    // veriabla from func
    private Ray ray;
    private RaycastHit _hit;
    private Vector3 targetPosition;
    private Vector3 directionWitoutspread;
    private void FireBullet()
    {
        Transform whoFired = this.transform.GetComponentInParent<PlayerMovement>().firefighterSPosition;
        ray = fpsCamera.ViewportPointToRay(new Vector3(0.5f,0.5f,0));
        if (Physics.Raycast(ray, out _hit, Mathf.Infinity , LayerMask.GetMask(Layers.DamageLayer) , QueryTriggerInteraction.Ignore))
        {
            targetPosition = _hit.point;
        }
        else
        {
            targetPosition = ray.GetPoint(70);
        }
        //--

        //Debug.DrawLine(ray.origin, ray.origin +  ray.direction * 50 , Color.red , 5f );
        //Debug.DrawLine(ray.origin, targetPosition + ray.direction * 500, Color.green , 1f );
        //Debug.DrawLine(_hit.point, firePoint.position, Color.yellow, 3f);
        //--

        directionWitoutspread = targetPosition - firePoint.position;
        animWeapon.SetTrigger("shot");
        Bullet newBullet = GetBulletPool();
        if (newBullet == null)
        {
            newBullet = Instantiate(_bulletPrefab);
            _bulletPool.Add(newBullet);
        }
        shotSound.Play();
        newBullet.transform.position = firePoint.position;
        newBullet.transform.rotation = Quaternion.Euler(new Vector3(-90, firePoint.position.y, firePoint.position.z));
        newBullet.transform.up = directionWitoutspread.normalized;
        newBullet.gameObject.SetActive(true);

        animCamera.Play("takeDamageCamera",0,0f);

    }
    private void ReloadAmmo()
    {
        reloadSound.Play();
        animWeapon.Play("GunReload");
        for (int i = 0; i < ammos.Count; i++)
        {
            if (!ammos[i].activeSelf)
            {
                currentAmmo++;
                ammos[i].SetActive(true);
            }
        }
    }
    
    private void Bulletchange()
    {
        if (currentAmmo > 0)
        {
            currentAmmo--;
            for (int i = 0; i < ammos.Count; i++)
            {
                if (ammos[i].activeSelf)
                {
                    ammos[i].SetActive(false);
                    return;
                }
            }
        }
    }
    private void OnEnable()
    {
        EventManager.OnFirePistol += FireBullet;
        EventManager.OnCurBulletChange += Bulletchange;
    }
    private void OnDisable()
    {
        EventManager.OnFirePistol -= FireBullet;
        EventManager.OnCurBulletChange -= Bulletchange;
    }

}
