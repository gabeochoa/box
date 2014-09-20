using UnityEngine;
using System.Collections;

public class WhackAMole : MonoBehaviour {
	
	public GameObject[] moles;
	public Transform[] moles2;
	public Random rand;

	// Use this for initialization
	void Start () {
		InvokeRepeating("popup", 1f, 1.0f);
	}
	
	// Update is called once per frame
	void Update () {

	}

	public float speed = 1.0f;
	Transform orig;
	int r = 0;
	void popup()
	{
		r = Random.Range (0, moles.Length-1);

		GameObject m = moles [r] as GameObject;
		//orig = moles2[r];
		m.transform.localPosition = Vector3.Lerp(orig.transform.localPosition, new Vector3(orig.transform.localPosition.x, orig.transform.localPosition.y - 10, orig.transform.localPosition.z), speed);
		//Invoke ("setback", speed);
	}

	void setback()
	{
		GameObject m = moles [r] as GameObject;
		m.transform.localPosition = Vector3.Lerp (m.transform.localPosition, orig.transform.localPosition, speed);
	}
}
