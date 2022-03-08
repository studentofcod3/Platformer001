using UnityEngine;

public class GenerateLevelBlocks : MonoBehaviour
{
    [SerializeField] private GameObject[] levelBlock;
    private GameObject randomLevelBlock;

    private GameObject generatedBlock;
    private GameObject player;

    private int numberOfBlocks = 30;
    private int xPlacement;
    private int yPlacement;

    private bool shouldBeFlat;
    private bool shouldBeConnected;

    public int lengthOfFlatPlane = 0;
    public int lengthOfConnectedPlane = 0;

    private Vector3 lastObjectPosition = new Vector3(0, 0, 0);
    private Vector3 objectPosition;
    

    // Start is called before the first frame update
    void Start()
    {
        generateBlocks();
    }

    private void generateBlocks()
    {
        for (int i=0; i <= numberOfBlocks; i++)
        {
            setBlockSeperationAndFlatness();
            
            randomLevelBlock = levelBlock[Random.Range(0, levelBlock.Length - 1)];

            objectPosition = lastObjectPosition + new Vector3(xPlacement, yPlacement, 0);
            generatedBlock = Instantiate(randomLevelBlock, objectPosition, Quaternion.identity);

            generatedBlock.transform.Rotate(-90, 0, 90);

            lastObjectPosition = objectPosition;
        }
    }

    private void setBlockSeperationAndFlatness()
    {
        _ = lengthOfFlatPlane == 0 ? setShouldBeFlat() : lengthOfFlatPlane--;
        _ = lengthOfConnectedPlane == 0 ? setShouldBeConnected() : lengthOfConnectedPlane--;


        _ = shouldBeFlat && generatedBlock ? yPlacement = 0 : yPlacement = Random.Range(-2, 2);
        _ = shouldBeConnected && generatedBlock ? xPlacement = 2 : xPlacement = Random.Range(2, 6);
    }

    private int setShouldBeConnected()
    {
        shouldBeConnected = Random.Range(0, 2) == 0;
        if (shouldBeConnected)
        {
            lengthOfConnectedPlane = Random.Range(1, 5);
        }
        return lengthOfConnectedPlane;
    }

    private int setShouldBeFlat()
    {
        shouldBeFlat = Random.Range(0, 2) == 0;
        if (shouldBeFlat)
        {
            lengthOfFlatPlane = Random.Range(1, 5);
        }
        return lengthOfFlatPlane;
    }

    // Update is called once per frame
    void Update()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        
        //TODO get lastObjectPosition position relative to player position. Generate new blocks if needed
        if ((lastObjectPosition.x - player.transform.position.x) <= 10)
        {
            print("generating new blocks...");
            generateBlocks();
        }
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            print("Up arrow pressed");

        }
        
    }
}
