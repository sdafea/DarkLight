using UnityEngine;
using System.Collections;

public class MovieCamera : MonoBehaviour {

    public float speed = 10;

    private float endZ = -20;

	// Use this for initialization
	void Start () {
		Invoke("ShowEnterPanel",4);
	}
	
	// Update is called once per frame
	void Update () {
		if (transform.position.z < endZ)
		{
			//还没有达到目标位置，需要移动
			transform.Translate(Vector3.forward * speed * Time.deltaTime);
		}
	}
	private void ShowEnterPanel()
	{
		UIManager.GetInstance().ShowPanel<EnterPanel>("EnterPanel");
	}
}
