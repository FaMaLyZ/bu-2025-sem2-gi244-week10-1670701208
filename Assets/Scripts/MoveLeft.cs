using UnityEngine;

public class MoveLeft : MonoBehaviour
{
    public float startSpeed = 10f;
    public float curretSpeed;
    public bool isRunning = false;

    private float leftBound = -15;

    [SerializeField]private PlayerController playerController;

    private void Start()
    {
        playerController = GameObject.Find("Player").GetComponent<PlayerController>();
        curretSpeed = startSpeed;
    }
    // Update is called once per frame
    void Update()
    {
        if (!playerController.gameOver)
        {
            transform.Translate(Vector3.left * Time.deltaTime * curretSpeed);
        }

        if (transform.position.x < leftBound && gameObject.CompareTag("Obstacle"))
        {
            Destroy(gameObject);
        }

        if(playerController.isDash && !isRunning)
        {
            curretSpeed *=2;
            isRunning = true;
        }
        else if (!playerController.isDash)
        {
            curretSpeed = startSpeed;
            isRunning = false;
        }

    }
    
}
