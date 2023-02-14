using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarrotAnimator : MonoBehaviour
{
    private PlantStatus plantStatus;
    private Animator m_Animator;

    // Start is called before the first frame update
    void Start()
    {
        plantStatus = GetComponent<PlantStatus>();
        m_Animator = GetComponent<Animator>();
    }

    void Update()
    {
        CarrotAnimation();
    }

    public void CarrotAnimation()
    {
        if (plantStatus)
        {
            switch (plantStatus.level)
            {
                case 0:
                    m_Animator.SetInteger("carrotLevel", 1);
                    break;

                case 1:
                    m_Animator.SetInteger("carrotLevel", 2);
                    break;

                case 2:
                    m_Animator.SetInteger("carrotLevel", 3);
                    break;

                case 3:
                    m_Animator.SetInteger("carrotLevel", 4);
                    break;

                default:
                    break;
            }
        }
    }
}