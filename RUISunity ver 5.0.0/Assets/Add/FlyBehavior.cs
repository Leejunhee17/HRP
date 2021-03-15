using UnityEngine;
using System.Collections;

public class FlyBehavior : MonoBehaviour {
	
	
	public static Vector3 displacement;
	public float desplVel = 1f;
	public static Vector3 spread;
	

	//////////////
	//private var target:GameObject;
	public GameObject target;
	private Transform targetTransform;
	//////////////
	

	private float iniRndX;
	private float iniRndY;
	private float iniRndZ;

	private float yyy;
	
	// Use this for initialization
	void Awake () {
		
		iniRndX = Random.Range(Random.Range(0,200), 500);
		iniRndY = Random.Range(Random.Range(0,200), 500);
		iniRndZ = Random.Range(Random.Range(0,200), 500);
		
		desplVel *= Random.Range(0.5f, 2f);

		/////////////////////////////////////////////
		spread = new Vector3(0.1f,0.1f,0.1f);
		displacement = new Vector3 (10f, 10f, 10f);
		/////////////////////////////////////////////
		
		
		float tempx = transform.localEulerAngles.x;
		float tempy = Random.Range(1f,360f);
		float tempz = transform.localEulerAngles.z;
		
		transform.localEulerAngles = new Vector3 (tempx, tempy, tempz);

		yyy = Random.Range (0f, 4f);
	}

	

	void FixedUpdate () {
		//UpdateDespl();
		float x = (Mathf.PerlinNoise(Time.time*desplVel +iniRndX, iniRndX) -0.46525f) *displacement.x;
		float y = (Mathf.PerlinNoise(iniRndY, iniRndY+ Time.time*desplVel) -0.46525f) *displacement.y;
		float z = (Mathf.PerlinNoise(iniRndZ, iniRndZ+ Time.time*desplVel) -0.46525f) *displacement.z;
		
		
		Vector3 despl;
		despl = new Vector3(x,y,z);
		Quaternion rot;
		rot = Quaternion.LookRotation(despl, Vector3.up);
		
		float temp = rot.eulerAngles.y;
		temp += 90f;
		rot.eulerAngles = new Vector3 (270f, temp, 0f);
		
		Rigidbody rgbd;
		rgbd = GetComponent<Rigidbody>();
		rgbd.AddForce(despl, ForceMode.Force);
		transform.rotation = Quaternion.Lerp(transform.rotation, rot, 0.09f);

		targetTransform = target.transform;
		Vector3 vvv = new Vector3 (targetTransform.localPosition.x, targetTransform.localPosition.y + yyy, targetTransform.localPosition.z + 1.5f);

		if ((transform.localPosition-vvv).sqrMagnitude > spread.sqrMagnitude) {
			rgbd.AddForce((vvv -transform.localPosition)*1, ForceMode.Force);
		}
		
	}

}