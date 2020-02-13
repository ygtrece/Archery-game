using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstView : MonoBehaviour {

	//direction sensitivity  
    public float sensitivityX = 3F;   
    public float sensitivityY = 3F;   
      
    //largest view on Y 
    public float minimumY = -60F;  
    public float maximumY = 60F;  
      
    float rotationY = 0F;  
      
    void Update ()  
    {  
        //deal with x
        float rotationX = transform.localEulerAngles.y + Input.GetAxis("Mouse X") * sensitivityX;  
          
        //deal with Y  
        rotationY += Input.GetAxis("Mouse Y") * sensitivityY;  
        //limitation of angle.if rotationY < min,return min. > max,return max. else return value   
        rotationY = Mathf.Clamp (rotationY, minimumY, maximumY);  
          
		//set the angle of camera
        transform.localEulerAngles = new Vector3(-rotationY, rotationX, 0);   
    }  
      
    void Start ()  
    {  
        // Make the rigid body not change rotation  
		if (GetComponent<Rigidbody>())  
			GetComponent<Rigidbody>().freezeRotation = true;  
    }
}
