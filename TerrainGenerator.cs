using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainGenerator : MonoBehaviour
{

    public int width = 256;
    public int height = 256;
    public int depth = 20;

    public float offsetX = 100.0f;
    public float offsetY = 100.0f;
    public float scale = 20.0f;

    public int terrainGenerated = 0;


    private AccountSystem accountScript;

    private WorldGen worldScript; 

    private int specialaccountNumber = 0;
	
	
	private int randomCo;


    public int objectsSpawned = 0;
    private int totalObjectAmount = 0;


    public GameObject [] treePrefabs;


    public int terrainSeed;

    private bool done = false;


    public List<float> xPos = new List<float>();
    public List<float> zPos = new List<float>();


    private int spawnCounter=0;

   





    // Start is called before the first frame update
    void Start()
    {

	Cursor.lockState= CursorLockMode.None;
       

        accountScript = GameObject.Find("Main Camera").GetComponent<AccountSystem>();
        worldScript = GameObject.Find("current world").GetComponent<WorldGen>();

        specialaccountNumber = worldScript.specialAccountNumber; 

        Random.InitState(terrainSeed);
		
		
		randomCo = Random.Range(1,25);
    
		

		
        offsetX=Random.value * randomCo;
        offsetY=Random.value * randomCo;

       
            scale = Random.Range(1,6);

            depth = Random.Range(3,50);
      
		
		Debug.Log("The Scale is " + scale);
        Debug.Log("The Depth is "+ depth);

        GenerateTerrainData();


		
    Random.InitState(terrainSeed);
       

     
  	        totalObjectAmount = Random.Range(500,1000);    
        

        for(int i=0;i<totalObjectAmount;i++)
        {
             float xCo = CalculateCoefficient(transform.position.x);
             float zCo= CalculateCoefficient(transform.position.z);

             if(PlayerPrefs.GetInt("Terrain: "+ terrainSeed+ " tree "+ i)==1)
             {
                //Don't add it to the list
             }
             else 
             {
Debug.Log("FIlling up the float listss!!!!!");

                 xPos.Add(xCo);
                 zPos.Add(zCo);
             }
        }

       



        totalObjectAmount = xPos.Count;
    
       
    }


  

    // Update is called once per frame
    void Update()
    {
        if(spawnCounter< totalObjectAmount && done ==false)
        {
             SpawnObjects(gameObject.name);
        }
        else
        {
            done= true; 
        }

       

    }


    public void SpawnObjects(string name)
    {
        
            GameObject tree = treePrefabs[Random.Range(0,treePrefabs.Length)];

           

             if(spawnCounter< totalObjectAmount)
        {
            SpawnObject(tree);

        } 
        if(spawnCounter< totalObjectAmount)
        {
            SpawnObject(tree);

        }
         if(spawnCounter< totalObjectAmount)
        {
            SpawnObject(tree);

        }
         if(spawnCounter< totalObjectAmount)
        {
            SpawnObject(tree);

        } 
        if(spawnCounter< totalObjectAmount)
        {
            SpawnObject(tree);

        }


          



            PlayerPrefs.SetInt("Terrain: "+ terrainSeed + " total objects", objectsSpawned);
             

        
    }


    public void SpawnObject(GameObject theObject)
    {
        float xCo = CalculateCoefficient(transform.position.x);
        float zCo= CalculateCoefficient(transform.position.z);

        float yPos= 0f;

        float yVal = Random.value;

        if(PlayerPrefs.HasKey("ySeed: "+ yVal))
    {
                    yPos= PlayerPrefs.GetFloat("ySeed: "+yVal);
    }
        else
        {
            yPos = 100.0f;
        }
          
              
       
            var spawnedObject= Instantiate(theObject, new Vector3(xPos[spawnCounter] , yPos + 5.0f, zPos[spawnCounter]), Quaternion.identity);
		

          
            spawnedObject. GetComponent<terrainitems>().ySeed = yVal;


        

		    spawnedObject.transform.SetParent(gameObject.transform, true);
    
      
           
            spawnedObject.name= "Terrain Item " + spawnCounter;
            spawnedObject.GetComponent<terrainitems>().treeNum = spawnCounter; 

            spawnCounter ++;
        
       
        

    }


    public float CalculateCoefficient(float pos)
    {
        if(pos>=0)
		{
			 return Random.Range(pos, pos+ (width-5f));
		}
		else
		{
			return Random.Range(pos, pos + (width-5f));
		}
    }


  

    public void GenerateTerrainData()
    {




      
        Terrain terrain = GetComponent<Terrain>();

        terrain.terrainData= GenerateTerrain(terrain.terrainData);

        terrainGenerated = 1; 

        PlayerPrefs.SetInt("Seed: "+ worldScript.specialAccountNumber+" - terrain generated bool", terrainGenerated);

        
      


    }

     TerrainData GenerateTerrain(TerrainData terrainData)
    {
        terrainData.heightmapResolution=width+1;

        terrainData.size= new Vector3(width,depth,height);

        terrainData.SetHeights(0,0,GenerateHeights());

        return terrainData;
    }

    float[,] GenerateHeights()
    {
        float [,] heights= new float[width,height];

        for(int x=0;x<width;x++)
        {
            for(int y=0;y<height;y++)
            {
                heights[x,y]=CalculateHeight(x,y);
            }
        }

        return heights;
    }

    float CalculateHeight(int x, int y)
    {
       

        float xCord= (float)x/width * scale + offsetX;
        float yCord= (float)y/height * scale + offsetY;

        float noise =  Mathf.PerlinNoise(xCord , yCord);


        return noise; 

        
    }

}
