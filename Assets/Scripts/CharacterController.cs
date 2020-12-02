using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.UI;
using UnityEngine.Events;

public class CharacterController : MonoBehaviour
{

    public float runSpeed = 1f;
    public float ascentSpeed = 1f;
    public float fuelAmount = 10f;
    public float fuelConsumptionRate = 1;
    public bool grounded;
    public bool enteredEndgame;

    [SerializeField]
    private Image fuelFill;
   // [SerializeField]
   // private MeshRenderer fuelRenderer;


    public GameObject character;

    public GameObject[] jetpackFireVFX;
    public GameObject[] jetpackNoFuelVFX;

    private Animator characterAnimator;
    private Rigidbody rb;
    private CinemachineDollyCart dollyCart;



    [Header("Events")]

    [SerializeField]
    private UnityEvent OnGameEnd;




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
        fuelFill.fillAmount = fuelAmount;
       // fuelRenderer.material.SetFloat("_FillAmount", fuelAmount);

        //Zero fuel GAMEOVER
        if (fuelAmount < 0 && !enteredEndgame)
        {
            OnGameEnded();
        }

        //Fly UP
        if (Input.GetMouseButton(0) && fuelAmount >= 0f)
        {
            characterAnimator.SetBool("flying", true);
            rb.velocity = transform.up * ascentSpeed;
            fuelAmount -= fuelConsumptionRate * Time.deltaTime;

            for (int i = 0; i < jetpackFireVFX.Length; i++)
            {
                jetpackFireVFX[i].SetActive(true);
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
}
