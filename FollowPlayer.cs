using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{

     private GameObject player;


    //Turn
    public Vector2 turn;


    // Start is called before the first frame update
    void Start()
    {
        //initializing
        
    }

    // Update is called once per frame
    void LateUpdate()
    {
        //camera will follow player 

        if(GameObject.Find("Player")!= null)
        {
            player= GameObject.Find("Player");

              transform.position= player.transform.position- player.transform.forward * 5;

             transform.LookAt(player.transform.position+player.transform.right * .5f);
		
		    transform.position= new Vector3(transform.position.x  , transform.position.y + 3.5f, transform.position.z);

              VerticalSense();
        }
      
    }


    void VerticalSense()
    {
        //user can look up and down
        turn.y+=Input.GetAxis("Mouse Y");
		float xRotes= 18-turn.y;
		
		if(xRotes>45)
		{
			xRotes=45;
		}else if(xRotes<-10)
		{
			xRotes=-10;
		}
		transform.Rotate(xRotes,0,0);
    }
}
