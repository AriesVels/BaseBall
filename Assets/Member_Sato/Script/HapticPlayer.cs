using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.XR;

public class HapticPlayer
{
    public enum Hand
    {
        Both,
        Left,
        Right
    }

    public static void SendHaptic_XRController(Hand hand, float amplitude, float duration)
    {
        switch (hand)
        {
            case Hand.Both:
                SendHaptic(XRController.leftHand, amplitude, duration);
                SendHaptic(XRController.rightHand, amplitude, duration);
                break;

            case Hand.Left:
                SendHaptic(XRController.leftHand, amplitude, duration);
                break;

            case Hand.Right:
                SendHaptic(XRController.rightHand, amplitude, duration);
                break;

            default:
                break;
        }
    }

    public static void SendHaptic(InputDevice device, float amplitude, float duration, int channel = 0)
    {
        if (device != null)
        {
            var command = UnityEngine.InputSystem.XR.Haptics.SendHapticImpulseCommand.Create(channel, amplitude, duration);
            device.ExecuteCommand(ref command);
        }
        else
        {
            Debug.Log("device is misiing");
        }
    }
}
