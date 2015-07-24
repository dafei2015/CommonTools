using UnityEngine;
using System.Collections;

public class FollowHanderMove : MonoBehaviour {

    public Vector3 worldCoordinates; //世界坐标
    public Vector3 screenCoordinates;
    public Vector3 mousePosition;
    public float speed = 0; //物体移动速度

	// Use this for initialization
	void Start () 
    {
	
	}
	
	// Update is called once per frame
	void Update () 
    {
        screenCoordinates = Camera.main.WorldToScreenPoint(transform.position); //将物体的世界坐标转换为屏幕坐标
        mousePosition = Input.mousePosition; //鼠标位置

        if(Input.GetMouseButton(0)) //点击鼠标左键
        {
            mousePosition.z = screenCoordinates.z;
            //因为鼠标Z坐标默认值为0，所以需要一个Z坐标；
            worldCoordinates.x = Camera.main.ScreenToWorldPoint(mousePosition).x;
            worldCoordinates.z = Camera.main.ScreenToWorldPoint(mousePosition).z;
            //worldCoordinates.y = transform.position.y;
            worldCoordinates.y = Camera.main.ScreenToWorldPoint(mousePosition).y;
            
            //开始移动
            speed = 1;
        }

        if (transform.position == worldCoordinates)
        {
            speed = 0;
        }

        transform.LookAt(worldCoordinates);//物体朝向鼠标
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
	}
}
