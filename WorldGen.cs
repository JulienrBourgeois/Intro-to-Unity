using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldGen : MonoBehaviour
{

    private AccountSystem accountScript;

    private string username = "";

    public bool worldLoaded = false;

    public GameObject playerPrefab;


    ///
    private int totalSpecialAccountNums = 0; 

    public int specialAccountNumber = 0;




    public GameObject [] terrainList;







  
    // Start is called before the first frame update
    void Start()
    {
        
        // this should delay this so that the world reach is set...
      
       
    }

    public void WorldStart(int worldReach)
    {
         
        accountScript = GameObject.Find("Main Camera").GetComponent<AccountSystem>();


         username = accountScript.loggedInUsername; 
        
       

        Debug.Log("We have loaded a world under the username " + username);
        Debug.Log("The world is " + username + "'s "+(worldReach + 1)+" world...");

        SaveSpecialAccountNumber(worldReach);

        Random.InitState(specialAccountNumber);


        WorldGame();


        
        


                
    }


    





    public void WorldGame()
    {

        worldLoaded = true; 

        GameObject.Find("terrain").GetComponent<DayNight>().world = gameObject;
        GameObject.Find("terrain").GetComponent<DayNight>().worldLoaded = true;

        float yPos = PlayerPrefs.GetFloat("Seed: "+ specialAccountNumber + " yPos");
		
		
		Debug.Log("The if statement is getting reached");
        if(PlayerPrefs.HasKey("Seed: " + specialAccountNumber + " player X pos"))
        {
            //at its get variables...

            Vector3 spawnPos= new Vector3(PlayerPrefs.GetFloat("Seed: "+specialAccountNumber +" player X pos"), yPos + 2.0f, PlayerPrefs.GetFloat("Seed: "+ specialAccountNumber + " player Z pos"));

            var player0 = Instantiate(playerPrefab, spawnPos, Quaternion.identity);

            player0.name = "Player";
        }
        else 
        {
                 var player = Instantiate(playerPrefab, new Vector3(128,100,128), Quaternion.identity);

        player.name = "Player";
		
		Debug.Log("The spawn player is getting reached...");
        }

        GameObject.Find("Main Camera").GetComponent<FollowPlayer>().worldStarted = true;

        SpawnBiome(Random.Range(0,terrainList.Length), 0f, 0f);
//SpawnBiome(4,0f,0f);

    }
    





    public void SpawnBiome(int terrain, float spawnX, float spawnZ)
    {

		



        var plane = Instantiate(terrainList[terrain], transform.position, Quaternion.identity);


        plane.GetComponent<TerrainGenerator>().terrainSeed = Random.Range(0,1000000000);
		



        plane.transform.SetParent(GameObject.Find("terrain").transform, true);

        if(terrain<=3)
        {
            plane.name= "plains";
        }
        else 
        {
            plane.name ="mountains";
        }

       
       
    }

    public void SaveSpecialAccountNumber(int worldReach)
    {
        specialAccountNumber =  Random.Range(0, 2000000000);

        for(int i=0;i<PlayerPrefs.GetInt("Total Special Nums") ;i++)
        {
            if(PlayerPrefs.GetInt("Num : "+ i)== specialAccountNumber)
            {
                specialAccountNumber = Random.Range(0,2000000000);
                i--;
            }
        }

        
        if(PlayerPrefs.HasKey("User " + username + " world : " + worldReach + " specialAccountNumber"))
        {
            Debug.Log("Variables havce already been generated for this account");
            Debug.Log("The special account number for this world on this account is " + PlayerPrefs.GetInt("User "+ username +" world : "+worldReach + " specialAccountNumber"));

            specialAccountNumber = PlayerPrefs.GetInt("User " + username + " world : "+ worldReach+" specialAccountNumber");
            Debug.Log(specialAccountNumber);
        }
        else
        {
            totalSpecialAccountNums = PlayerPrefs.GetInt("Total Special Nums");

            totalSpecialAccountNums ++;
            PlayerPrefs.SetInt("User " + username + " world : " + worldReach + " specialAccountNumber", specialAccountNumber);
            PlayerPrefs.SetInt("Num : "+totalSpecialAccountNums, specialAccountNumber);
            PlayerPrefs.SetInt("Total Special Nums", totalSpecialAccountNums);
            Debug.Log("Special Nums "+ totalSpecialAccountNums);


        }



    }

    // Update is called once per frame
    void Update()
    {
       
    }



   

    
}
