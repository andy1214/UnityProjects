using UnityEngine;
using System.Collections;

public class TextController : MonoBehaviour
{
	public TextMesh m_Text = null; //連結到傷害數字的變數
	public float m_ScaleTime = 1.0f; //縮放效果持續時間
	public float m_MoveTime = 1.5f; //移動效果持續時間
	public float m_MoveAmount = 1.0f; //移動量
	public AnimationCurve m_ScaleX = AnimationCurve.Linear (0, 0, 1, 1); //控制數字X軸縮放的動畫曲線
	public AnimationCurve m_ScaleY = AnimationCurve.Linear (0, 0, 1, 1); //控制數字Y軸縮放的動畫曲線
	public AnimationCurve m_MoveY = AnimationCurve.Linear (0, 0, 1, 1); //控制數字移動的動畫曲線
	private float m_CurrentTime = 0.0f; //目前累計時間
	private float m_CurrentMoveTime = 0.0f; //目前移動累計時間
	private float m_ScaleFactor = 0.0f; //縮放因子
	private float m_MoveFactor = 0.0f; //移動因子
	private float m_Offset = 0.5f; //位置偏移值
	private Vector3 m_HealthPosition; //血條座標
    
	// Use this for initialization
	void Start ()
	{
		m_ScaleFactor = 1.0f / m_ScaleTime; //取得縮放因子
		m_MoveFactor = 1.0f / m_MoveTime; //取得移動因子
	}
	//設定要顯示的文字
	public void SetText (string text) 
	{
		m_Text.text = text;
		m_CurrentTime = 0.0f; //把計時歸零
		m_CurrentMoveTime = 0.0f;
		m_Text.transform.position = Vector3.zero; //初始化文字位置
		m_Text.transform.localScale = Vector3.one; //初始化文字縮放
		StartCoroutine ("TextEffect"); //執行文字動畫特效
		StartCoroutine ("TextMove"); //執行文字移動協程     
	}
	//傳入血條座標
	public void SetHealthPosition (Vector3 pos)
	{
		m_HealthPosition = pos;
	}
	//文字特效協程
	IEnumerator TextEffect ()
	{
		m_Text.gameObject.SetActive(true);
		while (m_CurrentTime < m_ScaleTime) { //當累計時間小於動畫持續時間就不斷執行
			m_CurrentTime += Time.deltaTime;
			if (m_CurrentTime > m_ScaleTime)
				m_CurrentTime = m_ScaleTime;
         
			float temp = m_CurrentTime * m_ScaleFactor; //取得素計時間在總持總時間裡的比例
        
			float x = m_ScaleX.Evaluate (temp); 
			float y = m_ScaleY.Evaluate (temp); //取得在動畫曲線的系數
         
			m_Text.transform.localScale = new Vector3 (x, y, 1.0f); //變更縮放值
			yield return null; //交回控制權
		}
	}
	//文字移動協程  
	IEnumerator TextMove ()
	{
		while (m_CurrentMoveTime < m_MoveTime) { //當累計時間小於動畫持續時間就不斷執行
			m_CurrentMoveTime += Time.deltaTime;
			if (m_CurrentMoveTime > m_MoveTime)
				m_CurrentMoveTime = m_MoveTime;
            
			float temp = m_CurrentMoveTime * m_MoveFactor; 
			float Y = m_MoveY.Evaluate (temp); //取得在動畫曲線的系數
			
			temp = m_MoveAmount * Y;

			m_Text.transform.position = new Vector3 (m_HealthPosition.x, m_HealthPosition.y + temp + m_Offset, m_HealthPosition.z); //設定文字座標
			yield return null;
		}
		m_Text.gameObject.SetActive (false);
	}
 
	// Update is called once per frame
	void Update ()
	{
        
	}
}
