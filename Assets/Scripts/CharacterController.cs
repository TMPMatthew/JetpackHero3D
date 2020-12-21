using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.UI;
using UnityEngine.Events;

public class CharacterController : MonoBehaviour
{
    [Header("Character Parametrs")]
            
    public float runSpeed = 1f;
    public float ascentSpeed = 1f;
    public float fuelAmount = 10f;
    public float fuelConsumptionRate = 1;
    public int coinsCollected;

    [Space]
    [Header("Public Bools")]


    public bool grounded;
    public bool enteredEndgame;

    private bool playerWon;


    [Space]
    [Header("GO's")]


    [SerializeField]
    private Image fuelFill;
    [SerializeField]
    private GameObject character;
    [SerializeField]
    private Text coinsCollectedText;
    private Animator characterAnimator;
    private Rigidbody rb;
    [HideInInspector]
    public CinemachineDollyCart dollyCart;

    [Space]
    [Header("VFX")]


    public GameObject[] jetpackFireVFX;
    public GameObject[] jetpackNoFuelVFX;
    public GameObject winVFX;

    [Space]
    [Header("Events")]

    [SerializeField]
    private UnityEvent OnGameEnd;
    [SerializeField]
    private UnityEvent OnPlayerWon;




    // Start is called before the first frame update
    void Start()
    {
        characterAnimator = character.GetComponent<Animator>();
        dollyCart = gameObject.GetComponent<CinemachineDollyCart>();
        rb = character.GetComponent<Rigidbody>();
        
        //fuelRenderer = GetComponent<MeshRenderer>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        characterAnimator.SetBool("grounded", grounded);
        characterAnimator.SetFloat("speed",dollyCart.m_Speed);
        characterAnimator.SetBool("playerWon", playerWon);
        fuelFill.fillAmount = fuelAmount;


        //Fly Up/Descent
        if (Input.GetMouseButton(0) && fuelAmount >= 0f && !playerWon)
        {
            characterAnimator.SetBool("flying", true);
            rb.velocity = transform.up * ascentSpeed;
            fuelAmount -= fuelConsumptionRate * Time.deltaTime;


            for (int i = 0; i < jetpackFireVFX.Length; i++)
            {
                jetpackFireVFX[i].SetActive(true);
                jetpackNoFuelVFX[i].SetActive(false);
            }
        }
        else if (Input.GetMouseButton(0) && fuelAmount <= 0)
        {

            for (int i = 0; i < jetpackNoFuelVFX.Length; i++)
            {
                jetpackNoFuelVFX[i].SetActive(true);
                jetpackFireVFX[i].SetActive(false);
            }
        }
        else
        {
            characterAnimator.SetBool("flying", false);
            characterAnimator.SetBool("falling", true);
            for (int i = 0; i < jetpackFireVFX.Length; i++)
            {
                jetpackFireVFX[i].SetActive(false);
            }
        }

        if (enteredEndgame && dollyCart.m_Speed <= 0.01f)
        {
            PlayerWon();
        }
    }

    public void OnGameStart()
    {
        dollyCart.m_Speed = runSpeed;
    }

    public void OnGameEnded()
    {
        OnGameEnd.Invoke();   
        dollyCart.m_Speed = 0;
        runSpeed = 0;
    }

    public void PlayerWon()
    {
        OnPlayerWon.Invoke();
        playerWon = true;
        winVFX.SetActive(true);
        coinsCollectedText.text = "You've collected " + coinsCollected.ToString() + " coins";
    }
}
