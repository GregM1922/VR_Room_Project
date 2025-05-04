using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class SpaceshipInteract : MonoBehaviour
{
    public AudioSource spaceshipWhine;

    private XRGrabInteractable grabInteractable;

    private void Awake()
    {
        grabInteractable = GetComponent<XRGrabInteractable>();
        grabInteractable.onSelectEntered.AddListener(OnSelectEntered);
    }

    private void OnSelectEntered(XRBaseInteractor interactor)
    {
        if (spaceshipWhine != null)
        {
            spaceshipWhine.Play();
        }
    }
}


