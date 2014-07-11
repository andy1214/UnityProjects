using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour
{
	public GameObject[] m_Obstacle = null; //障礙物
	public GameObject m_Target = null; //目標點
	private float m_Probe; //探針長度
	private bool m_Aviod = false; //是否迴避障礙中
	private Vector3 m_TempPoint; //探測點
	private Vector3 m_Direction; //移動方向
	private Vector3 m_RightPoint; //迴避障礙的點
	private float m_Distance; //存距離的變數

	// Use this for initialization
	void Start ()
	{
		m_Probe = m_Obstacle [0].collider.bounds.extents.x + collider.bounds.extents.x; //取得測針長度
	}
 
	// Update is called once per frame
	void Update ()
	{
		m_TempPoint = transform.position + transform.forward * m_Probe; //取得探針點

		for (int i = 0; i < m_Obstacle.Length; i++) {
			m_Distance = Vector3.Distance (m_TempPoint, m_Obstacle [i].transform.position); //取得障礙物跟探針點的距離
            
			if (m_Distance < (collider.bounds.extents.x + m_Obstacle [i].collider.bounds.extents.x)) { //如果距離過短會碰障就避免
				Vector3 v = m_Obstacle [i].transform.position - transform.position; //取得障礙物到玩家的向量
            
				m_Aviod = true; //迴避障礙中
				Vector3 r = Vector3.Cross (transform.up, v); //取得向右的向量
				r.Normalize (); 
				Vector3 right = r * (m_Obstacle [i].collider.bounds.extents.x + collider.bounds.extents.x); //取得要轉向的向量
				m_RightPoint = m_Obstacle [i].transform.position + right; //取得轉向的點
				m_Direction = m_RightPoint - transform.position; //取得新的方向
  
				m_TempPoint = m_RightPoint; //取得新的探測點
			}
		}

		if (!m_Aviod) {
			m_Direction = m_Target.transform.position - gameObject.transform.position; //向目標點移動
		}

		m_Direction.Normalize (); 
		m_Direction.y = 0; //將y設成0
        
		Debug.DrawLine (gameObject.transform.position, m_RightPoint, Color.red);
		transform.forward = m_Direction; //改變方向
		gameObject.transform.position += transform.forward * Time.deltaTime; //向方向移動
		
		if (m_Aviod) {
			m_Distance = Vector3.Distance (transform.position, m_RightPoint); 
			if (m_Distance <= transform.forward.magnitude) { //如果跟轉向點的距離夠近, 就切回向目標點移動
				m_Aviod = false;
			}
		}
	}
}
