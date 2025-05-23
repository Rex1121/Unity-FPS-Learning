using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{

    public Camera playerCamera;

    //Shooting
    public bool isShooting, readyToShoot;
    bool allowReset = true;
    public float shootingDelay = 2f;

    //Burst mode
    public int bulletsPerBurst = 3;
    public int burstBulletsLeft;

    //weapon spread
    public float SpreadIntensity;

    //Bullet properties
    public GameObject bulletPrefab;
    public Transform bulletSpawn;
    public float bulletVelocity = 40f;
    public float bulletPrefabLifeTime = 2f; 
   
   public enum ShootingMode
    {
        Single,
        Burst,
        Auto
    }
    
    public ShootingMode currentShootingMode;
    private void Awake ()
    {
        readyToShoot = true;
        burstBulletsLeft = bulletsPerBurst;
    }





    void Update()
    {
        // Left Mouse Click
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            FireWeapon();
        }
        
    if (currentShootingMode == ShootingMode.Auto)
    {
    //Holding downleft Mouse button
    isShooting = (Input.GetKey(KeyCode.Mouse0));
    }
    else if (currentShootingMode == ShootingMode.Single || 
    currentShootingMode == ShootingMode.Burst)
    {
    //Cliking Left Mouse Button Once
    isShooting = Input.GetKeyDown(KeyCode.Mouse0);
    }

    if (readyToShoot && isShooting)
    {
    burstBulletsLeft = bulletsPerBurst;
    FireWeapon();
    } 
    

    }

    private void FireWeapon()
    { 
        readyToShoot = false;

        Vector3 shootingDirection = CalculateDirectionAndSpread().normalized;
        
        //Instantiate the bullet
        GameObject bullet = Instantiate(bulletPrefab, bulletSpawn.position, Quaternion.identity);
        
        // Pointing the bullet to face the shooting direction
        bullet.transform.forward = shootingDirection;
        
        //Shoot the bullet
        bullet.GetComponent<Rigidbody>().AddForce(shootingDirection * bulletVelocity, ForceMode.Impulse);
        
        //Destroy the bullet after some time
        StartCoroutine(DestroyBulletAfterTime(bullet, bulletPrefabLifeTime));
        
        //checking if we are done shooting
        if (allowReset)
        {
            Invoke("ResetShot", shootingDelay);
            allowReset = false;
        }

        //Burst Mode
        if (currentShootingMode == ShootingMode.Burst && burstBulletsLeft > 1)
        {
            burstBulletsLeft--;
            Invoke("Fireweapon", shootingDelay);
        }
    
    }

     private void ResetShot()
    {
        readyToShoot = true;
        allowReset = true;
    }
public Vector3 CalculateDirectionAndSpread()
        {
            // Shooting from the middle of the screen to check where are we pointing at
            Ray ray = playerCamera.ViewportPointToRay(new Vector3 (0.5f, 0.5f, 0));
            RaycastHit hit;

            Vector3 targetPoint;
            if (Physics.Raycast(ray, out hit))
            {
                //hitting something
                targetPoint = hit.point;
            }
            else
            {
                // hitting air
                targetPoint = ray.GetPoint(100);
            }

            Vector3 direction = targetPoint - bulletSpawn.position;

            float x = UnityEngine.Random.Range( -SpreadIntensity, SpreadIntensity);
            float y = UnityEngine.Random.Range( -SpreadIntensity, SpreadIntensity);
            
            // Returing the shooting direction and spread
            return direction + new Vector3(x, y, 0);  

        }

    private IEnumerator DestroyBulletAfterTime(GameObject bullet, float delay)
    {
        yield return new WaitForSeconds(delay);
        Destroy(bullet);
    }
}