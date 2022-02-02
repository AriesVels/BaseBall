using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.OpenXR.Input;
using UnityEngine.XR;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.XR;
using UnityEngine.XR.Interaction.Toolkit;

public class BatAction : MonoBehaviour
{
    [SerializeField] HapticPlayer.Hand hand;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Ball")
        {
            HapticPlayer.SendHaptic_XRController(hand, 0.2f, 0.5f);

            Debug.Log("Ball‚É“–‚½‚Á‚½");
        }
    }
}
