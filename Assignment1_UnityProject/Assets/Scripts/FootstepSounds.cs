using UnityEngine;

public class FootstepSounds : MonoBehaviour
{
    //list of sounds
    [SerializeField] AudioClip[] SandSounds;
    [SerializeField] AudioClip[] GrassSounds;
    [SerializeField] AudioClip[] MetalSounds;
    [SerializeField] AudioClip[] ConcreteSounds;
    [SerializeField] AudioClip[] WoodSounds;

    AudioClip currentTextureSound;
    [SerializeField] AudioSource audioSource;

    string textureType;
    string motionState;
    string audioArrayType;

    [SerializeField][Range(0, 1f)] float walkingVol;
    [SerializeField][Range(0, 1f)] float runningVol;
    [SerializeField][Range(0, 1f)] float jumpVol;

    [SerializeField][Range(0, 3f)] float walkingPitch;
    [SerializeField][Range(0, 3f)] float runningPitch;
    [SerializeField][Range(0, 3f)] float jumpPitch;

    [SerializeField] Transform groundCheck;
    Vector3 startRayGroundCheck;
    [SerializeField] Vector3 offset = new Vector3(0, 0, 0);

    [SerializeField] LayerMask ground;
    private RaycastHit hit;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }
    private void Update()
    {
        //if player walk backwards, the start point of ground check will be behind player
        if(Input.GetAxis("Vertical") < 0)
        {
            Vector3 backwards = -transform.forward;
            backwards *= GetComponentInParent<CharacterController>().radius * 2;
            startRayGroundCheck = groundCheck.position + backwards;
        }
        else
        {
            startRayGroundCheck = groundCheck.position;
        }
        //check ground before updating the type of texture that player stepped on
        Debug.DrawLine(startRayGroundCheck, startRayGroundCheck + Vector3.down, Color.red);
        if (Physics.Raycast(startRayGroundCheck, Vector3.down, out hit, ground))
        {
            Debug.Log("Hit");
            if (textureType == "" || textureType != hit.transform.tag)
            {
                Debug.Log(textureType);
                textureType = hit.transform.tag;
                audioArrayType = textureType + "Sounds";
            }
        }
    }
    void RandomizeSound(string motionState, string audioArrayType, out float volReturn, out float pitchReturn, out AudioClip audioClipReturn)
    {
        int randomClipIndex = 0;
        audioClipReturn = SandSounds[randomClipIndex];

        switch (audioArrayType)
        {
            case "SandSounds":
                randomClipIndex = Random.Range(0, SandSounds.Length - 1);
                audioClipReturn = SandSounds[randomClipIndex];
                break;
            case "GrassSounds":
                randomClipIndex = Random.Range(0, GrassSounds.Length - 1);
                audioClipReturn = GrassSounds[randomClipIndex];
                break;
            case "MetalSounds":
                randomClipIndex = Random.Range(0, MetalSounds.Length - 1);
                audioClipReturn = MetalSounds[randomClipIndex];
                break;
            case "ConcreteSounds":
                randomClipIndex = Random.Range(0, ConcreteSounds.Length - 1);
                audioClipReturn = ConcreteSounds[randomClipIndex];
                break;
            case "WoodSounds":
                randomClipIndex = Random.Range(0, WoodSounds.Length - 1);
                audioClipReturn = WoodSounds[randomClipIndex];
                break;
        }

        volReturn = 0;
        pitchReturn = 0;
        switch (motionState)
        {
            case "Walk":

                volReturn = Random.Range(0.1f, 0.32f);
                pitchReturn = Random.Range(2f, 3f);

                break;

            case "Run":

                volReturn = Random.Range(0.33f, 0.62f);
                pitchReturn = Random.Range(1f, 1.9f);

                break;

            case "Jump":

                volReturn = Random.Range(0.63f, 1f);
                pitchReturn = Random.Range(0f, 0.9f);

                break;
        }

    }
    void WalkStep()
    {
        motionState = "Walk";
        RandomizeSound(motionState, audioArrayType, out walkingVol, out walkingPitch, out currentTextureSound);
        audioSource.volume = walkingVol;
        audioSource.pitch = walkingPitch;
        audioSource.PlayOneShot(currentTextureSound);
    }
    void RunStep()
    {
        motionState = "Run";
        RandomizeSound(motionState, audioArrayType, out runningVol, out runningPitch, out currentTextureSound);
        audioSource.volume = runningVol;
        audioSource.pitch = runningPitch;
        audioSource.PlayOneShot(currentTextureSound);
    }
    void JumpLand()
    {
        motionState = "Jump";
        RandomizeSound(motionState, audioArrayType, out jumpVol, out jumpPitch, out currentTextureSound);
        audioSource.volume = jumpVol;
        audioSource.pitch = jumpPitch;
        audioSource.PlayOneShot(currentTextureSound);
    }
    
}
