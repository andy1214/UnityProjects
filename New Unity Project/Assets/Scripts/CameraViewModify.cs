using UnityEngine;
using System.Collections;

public class CameraViewModify : MonoBehaviour
{
	public float Width = 16.0f;
    public float Height = 9.0f;
    private float targetaspect = 0.0f;
	private int pre_width, pre_height;

	// Use this for initialization
	void Start()
	{

	}
	
	// Update is called once per frame
    void FixedUpdate()
	{
		if (pre_width != Screen.width || pre_height != Screen.height)
		{
			pre_width = Screen.width;
			pre_height = Screen.height;

			float windowaspect = (float)Screen.width / (float)Screen.height;

            targetaspect = Width / Height;

            float scaleheight = windowaspect / targetaspect;
		
			Camera camera = GetComponent<Camera>();
		
			if (scaleheight < 1.0f)
			{  
				Rect rect = camera.rect;
			
				rect.width = 1.0f;
				rect.height = scaleheight;
				rect.x = 0;
				rect.y = (1.0f - scaleheight) / 2.0f;
			
				camera.rect = rect;
			} else
			{ 
				float scalewidth = 1.0f / scaleheight;
			
				Rect rect = camera.rect;
			
				rect.width = scalewidth;
				rect.height = 1.0f;
				rect.x = (1.0f - scalewidth) / 2.0f;
				rect.y = 0;
			
				camera.rect = rect;
			}
		}
	}
}
