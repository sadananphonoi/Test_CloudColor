using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Implement the following class. This class manages the game objects used as bullets in a game.
/// Assume that there will be a need to spawn a lot of bullets as efficiently as possible.
/// You have 1 day to work on it.
/// </summary>
public class BulletManagerTest : MonoBehaviour
{
	//Prefab for a single bullet
	public GameObject BulletPrefab;

    //TODO - declar the neccessary member variables
    public Camera Cam; 

    private float speedH = 2.0f;
    private float speedV = 2.0f;
    private float yaw = 0.0f;
    private float pitch = 0.0f;
     
    void Start()
	{
        //TODO - system initailization 
        Cam = GameObject.Find("Main Camera").GetComponent<Camera>();
    }

	/// <summary>
	/// Spawn bullets
	/// </summary>
	/// <param name="count">number of bullets to spawn</param>
	/// <param name="aim">starting position and direction of the bullets</param>
	/// <param name="lifeTime">life time of bullets in seconds</param>
	/// <param name="force">how fast the bullets should travel per second</param>
	/// <param name="maxSpreadAngle">how wide the bullets can spread in degree</param>
    /// 
	public void SpawnBullets(int count, Ray aim, float lifeTime, float force, float maxSpreadAngle)
	{
        //TODO - spawn the bullets according to the input parameters
         
        for (int i = 0; i < count; i++)
        {
            GameObject Bullet = Instantiate(BulletPrefab, transform.position, transform.rotation) as GameObject;


            var randomNumberX = Random.Range(-maxSpreadAngle, maxSpreadAngle);
            var randomNumberY = Random.Range(-maxSpreadAngle, maxSpreadAngle);
            var randomNumberZ = Random.Range(-maxSpreadAngle, maxSpreadAngle); 
            Vector3 direction = aim.direction;
            direction = Quaternion.AngleAxis(randomNumberX, Vector3.up) * direction;
            direction = Quaternion.AngleAxis(randomNumberY, Vector3.forward) * direction;
            direction = Quaternion.AngleAxis(randomNumberZ, Vector3.right) * direction;
             

            Rigidbody Bullet_RigidBody;
            Bullet_RigidBody = Bullet.GetComponent<Rigidbody>();  
            Bullet_RigidBody.AddForce(direction * force , ForceMode.Impulse);
            
            Destroy(Bullet, lifeTime);
        }
       
    }

	private void UpdateBullets()
	{
        //TODO - update the position of the bullets

        yaw += speedH * Input.GetAxis("Mouse X");
        pitch -= speedV * Input.GetAxis("Mouse Y"); 
        Cam.transform.eulerAngles = new Vector3(pitch, yaw, 0.0f); 
        this.transform.forward = Cam.transform.forward;
    }

	private void DrawBullets()
	{
        //TODO - visualize the bullets
        Vector3 rayOrigin = Cam.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 0.0f));
        Debug.DrawRay(rayOrigin, Cam.transform.forward * 100, Color.green);
    }

	void Update()
	{
		UpdateBullets();
		DrawBullets();

        if (Input.GetKeyDown(KeyCode.A))
        {
            SpawnBullets(1, new Ray(this.transform.position, this.transform.forward), 5, 100, 0.7f);
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            SpawnBullets(100, new Ray(this.transform.position, this.transform.forward), 5, 100, 3);
        }

        //For testing
        if (Input.GetKeyDown(KeyCode.W))
		{
			SpawnBullets(50, new Ray(Vector3.zero, Vector3.forward), 3, 100, 10);
		}
		if (Input.GetKeyDown(KeyCode.S))
		{
			SpawnBullets(50, new Ray(Vector3.zero, Vector3.back), 3, 100, 10);
		}
	}
}



