using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    [SerializeField] private GameObject respawnPoint;
    [SerializeField] private float speed = 5;
    [SerializeField] private float gravity = 1;
    [SerializeField] private float jumpHeight = 20;
    [SerializeField] private float deadZoneY = -100;
    [SerializeField] private int lives = 3;
    private UIManager uiManager;
    private CharacterController controller;
    private float yVelocity;
    private int coins;
    private bool canDoubleJump;

    // Start is called before the first frame update
    void Start()
    {
        uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();
        if(uiManager == null)
        {
            Debug.LogError("Player: UIManager is NULL");
        }

        uiManager.UpdateLivesDisplay(lives);

        controller = GetComponent<CharacterController>();
        if(controller == null)
        {
            Debug.LogError("Player: CharacterController is NULL");
        }
    }

    // Update is called once per frame
    void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        Vector3 direction = new Vector3(horizontalInput, 0, 0);
        Vector3 velocity = direction * speed;

        if(controller.isGrounded)
        {
            if(Input.GetKeyDown(KeyCode.Space))
            {
                yVelocity = jumpHeight;
                canDoubleJump = true;
            }
        }
        else
        {
            if(canDoubleJump)
            {
                if(Input.GetKeyDown(KeyCode.Space))
                {
                    yVelocity += jumpHeight;
                    canDoubleJump = false;
                }
            }

            yVelocity -= gravity;
        }

        velocity.y = yVelocity;

        controller.Move(velocity * Time.deltaTime);

        if(transform.position.y <= deadZoneY)
        {
            Damage();
        }
    }

    void Damage()
    {
        controller.enabled = false;

        lives--;

        if(lives <= 0)
        {
            SceneManager.LoadScene(0);
        }
        else
        {
            uiManager.UpdateLivesDisplay(lives);
            
            transform.position = respawnPoint.transform.position;

            controller.enabled = true;
        }
    }

    public void AddCoins()
    {
        coins++;

        uiManager.UpdateCoinsDisplay(coins);
    }
}
