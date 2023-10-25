using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.Experimental.GraphView.GraphView;

public class Player : MonoBehaviour
{
    [SerializeField] private float speed;
    private Vector3 _moveDir;
    private Button _ShootButton;
    private Button _CrouchedButton;

    [Header("Camera")]
    [SerializeField, Range(1, 20)] private float mouseSensX;
    [SerializeField, Range(1, 20)] private float mouseSensy;
    [SerializeField, Range(-90, 0)] private float minViewAngle;
    [SerializeField, Range(0, 90)] private float maxViewAngle;
    [SerializeField] private Transform followTarget;

    [Header("Ammo and Reloading")]
    [SerializeField] public int maxAmmo;
    [SerializeField] private int magSize;
    private Boolean HasAmmo;
    private int ammoLeft;
    [SerializeField] public int AmmoRefillMax;
    [SerializeField] GameObject _AmmoBox;




    [Header("Shooting")]
    [SerializeField] private Rigidbody _bulletPrefab;
    [SerializeField] private float projectileForce;

    [Header("Player UI")]
    [SerializeField] private Image healthBar;
    [SerializeField] private TextMeshProUGUI shotsFired;
    [SerializeField] private TextMeshProUGUI Ammo;
    [SerializeField] private TextMeshProUGUI ReloadAlert;



    [SerializeField] private float maxHealth;
    private float shotsFiredCounter;
    private float _health;

    private float Health 
    {
        get => _health;
        set
        {
            _health = value;
            healthBar.fillAmount = _health / maxHealth;
        }    
    }



    private Vector2 currentAngle;


    // Start is called before the first frame update
    void Start()
    {
        InputManager.Init(this);
        InputManager.setGameControls();
        Health = maxHealth;
        ammoLeft = magSize;
        Ammo.text = "Ammo: " + ammoLeft.ToString() + " / " + maxAmmo.ToString();
        HasAmmo = true;


    }

    // Update is called once per frame
    void Update()
    {
        transform.position += transform.rotation * (speed * Time.deltaTime * _moveDir);
        Health -= Time.deltaTime * 5;
    }
    public void SetMoveDirection(Vector3 NewDirection)
    {
        _moveDir = NewDirection;
    }


    public Boolean GetAmmo()
    {
        if (maxAmmo < AmmoRefillMax) 
        {
            return  true;
        }
        else
        {       
            return  false;
        }
    }
   
    public void Shoot()
    {
        if (ammoLeft > 0 )
        {
            Rigidbody currentProjectile =  Instantiate(_bulletPrefab, transform.position, Quaternion.identity); 
        currentProjectile.AddForce(followTarget.forward * projectileForce,ForceMode.Impulse);
            Destroy(currentProjectile.gameObject, 4);
            ammoLeft--;
            Ammo.text = "Ammo: " + ammoLeft.ToString() + " / " + maxAmmo.ToString();
            Debug.Log("shot");

        }

        if (maxAmmo == 0 && ammoLeft != 0)
        {
            HasAmmo = false;
        }
        

        if (ammoLeft == 0)
        {
            ReloadAlert.text = "OUT OF AMMMO, PRESS R TO RELOAD";

        }
        else
        {
            ReloadAlert.text = "";
        }
        
        if (maxAmmo == 0 && ammoLeft == 0)
        {
            ReloadAlert.text = "OUT OF AMMMO ";
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (GetAmmo() == true)
        {
            maxAmmo = AmmoRefillMax;
            Ammo.text = "Ammo: " + ammoLeft.ToString() + " / " + maxAmmo.ToString();
            Destroy(GameObject.FindWithTag("AmmoBox"));
        }
    }

public void Reload()
    {
        if (HasAmmo == true && maxAmmo >= 0)
        {
                if (ammoLeft == 0)
                {
                    maxAmmo -= magSize + ammoLeft;
                    ammoLeft = magSize;
                HasAmmo = true;
                    Ammo.text = "Ammo: " + ammoLeft.ToString() + " / " + maxAmmo.ToString();
                ReloadAlert.text = ""; 
                }
                if (maxAmmo >= (magSize - ammoLeft))
            {
                maxAmmo = (maxAmmo - magSize) + ammoLeft;
                ammoLeft = magSize;
                HasAmmo = true;
                Ammo.text = "Ammo: " + ammoLeft.ToString() + " / " + maxAmmo.ToString();
                ReloadAlert.text = "";
            }
            if (maxAmmo <= (magSize - ammoLeft))
            {
                HasAmmo = false;
                ammoLeft += maxAmmo;
                maxAmmo = 0;
                Ammo.text = "Ammo: " + ammoLeft.ToString() + " / " + maxAmmo.ToString();
                ReloadAlert.text = "";
            }
        }
    }

    public void SetLookRotation(Vector2 readValue) 
    {
        currentAngle.x += readValue.x * Time.deltaTime * mouseSensX;
        currentAngle.y += readValue.y * Time.deltaTime * mouseSensy;

        currentAngle.y = Mathf.Clamp (currentAngle.y,minViewAngle,maxViewAngle);
        
        transform.rotation = Quaternion.AngleAxis(currentAngle.x, Vector3.up);
        followTarget.localRotation = Quaternion.AngleAxis(currentAngle.y, Vector3.right);
    }
    
}


   
