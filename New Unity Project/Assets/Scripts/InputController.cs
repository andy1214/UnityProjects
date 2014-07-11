using UnityEngine;
using System.Collections;

public class InputController : MonoBehaviour {
	public GameObject m_Cube = null;
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.UpArrow) == true)
		{
			Debug.Log("按了上");
			m_Cube.transform.position = m_Cube.transform.position + Vector3.up;
		}
		if (Input.GetKeyDown(KeyCode.DownArrow) == true)
		{
			Debug.Log("按了下");
			m_Cube.transform.position = m_Cube.transform.position + Vector3.down;
		}
		if (Input.GetKeyDown(KeyCode.LeftArrow) == true)
		{
			Debug.Log("按了左");
			m_Cube.transform.position = m_Cube.transform.position + Vector3.left;
		}
		if (Input.GetKeyDown(KeyCode.RightArrow) == true)
		{
			Debug.Log("按了右");
			m_Cube.transform.position = m_Cube.transform.position + Vector3.right;
		}
	}
}
