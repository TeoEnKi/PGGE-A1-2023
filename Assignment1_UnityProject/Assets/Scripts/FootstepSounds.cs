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
        //check for ground before updating the type of texture that player stepped on
        Debug.DrawLine(startRayGroundCheck, startRayGroundCheck + Vector3.down, Color.red);
        if (Physics.Raycast(startRayGroundCheck, Vector3.down, out hit, ground))
        {
            if (textureType == "" || textureType != hit.transform.tag)
            {
                Debug.Log(textureType);
                textureType = hit.transform.tag;
                audioArrayType = textureType + "Sounds";
            }
        }
    }
    //randomize the sound of the texture, the vol and pitch for every step
    void RandomizeSound(string motionState, string audioArrayType, out float volReturn, out float pitchReturn, out AudioClip audioClipReturn)
    {
        int randomClipIndex = 0;
        audioClipReturn = SandSounds[randomClipIndex];

        //base of the type of texture, 
        switch (audioArrayType)
        {
            case "SandSounds":
                randomClipIndex = Random.Range(0, SandSounds.Length);
                audioClipReturn = SandSounds[randomClipIndex];
                break;
            case "GrassSounds":
                randomClipIndex = Random.Range(0, GrassSounds.Length);
                audioClipReturn = GrassSounds[randomClipIndex];
                break;
            case "MetalSounds":
                randomClipIndex = Random.Range(0, MetalSounds.Length);
                audioClipReturn = MetalSounds[randomClipIndex];
                break;
            case "ConcreteSounds":
                randomClipIndex = Random.Range(0, ConcreteSounds.Length);
                audioClipReturn = ConcreteSounds[randomClipIndex];
                break;
            case "WoodSounds":
                randomClipIndex = Random.Range(0, WoodSounds.Length);
                audioClipReturn = WoodSounds[randomClipIndex];
                break;
        }

        volReturn = 0;
        pitchReturn = 0;
        switch (motionState)
        {
            //base on motion state, give the random value for pitch and volume
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
    //for walk animations
    void WalkStep()
    {
        //change motion state, randomise sound type volume and pitch and update the audio source using those values before playing the chosen audio
        motionState = "Walk";
        RandomizeSound(motionState, audioArrayType, out walkingVol, out walkingPitch, out currentTextureSound);
        audioSource.volume = walkingVol;
        audioSource.pitch = walkingPitch;
        audioSource.PlayOneShot(currentTextureSound);
    }
    //for run animations

    void RunStep()
    {
        //change motion state, randomise sound type volume and pitch and update the audio source using those values before playing the chosen audio
        motionState = "Run";
        RandomizeSound(motionState, audioArrayType, out runningVol, out runningPitch, out currentTextureSound);
        audioSource.volume = runningVol;
        audioSource.pitch = runningPitch;
        audioSource.PlayOneShot(currentTextureSound);
    }
    //for Jump animations
    void JumpLand()
    {
        //change motion state, randomise sound type volume and pitch and update the audio source using those values before playing the chosen audio
        motionState = "Jump";
        RandomizeSound(motionState, audioArrayType, out jumpVol, out jumpPitch, out currentTextureSound);
        audioSource.volume = jumpVol;
        audioSource.pitch = jumpPitch;
        audioSource.PlayOneShot(currentTextureSound);
    }
    
}
