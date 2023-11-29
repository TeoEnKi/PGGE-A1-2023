using UnityEngine;

public class FootstepSounds : MonoBehaviour
{
    //list of sounds
    [SerializeField] AudioClip[] footstepSounds;
    AudioClip currentTextureSound;
    [SerializeField] AudioSource audioSource;

    public string textureType;

    [SerializeField][Range(0f, 1f)] float walkingVol;
    [SerializeField][Range(0f, 1f)] float runningVol;
    [SerializeField][Range(0f, 1f)] float jumpVol;

    [SerializeField][Range(0f, 1f)] float walkingPitch;
    [SerializeField][Range(0f, 1f)] float runningPitch;
    [SerializeField][Range(0f, 1f)] float jumpPitch;

    Vector3 groundCheck;
    [SerializeField] Vector3 offset = new Vector3(0, 0.2f, 0);

    [SerializeField] LayerMask ground;
    private RaycastHit hit;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }
    private void Start()
    {
        offset.z = gameObject.GetComponentInParent<CharacterController>().radius;
    }
    private void Update()
    {
        groundCheck = transform.position;
        //get the info based on the foot step that is infront of the other
        groundCheck.z += offset.z;
        groundCheck.y += offset.y;

        //check ground before updating the tag
        Debug.DrawLine(groundCheck, Vector3.down * 2, Color.red);
        if (Physics.Raycast(groundCheck, Vector3.down, out hit, ground))
        {
            Debug.Log("Hit");
            if (textureType == "" || textureType != hit.transform.tag)
            {
                Debug.Log(textureType);
                textureType = hit.transform.tag;
            }
        }
        UpdateTextureSound();
    }
    void WalkStep()
    {
        audioSource.volume = walkingVol;
        audioSource.pitch = walkingPitch;
        audioSource.PlayOneShot(currentTextureSound);
    }
    void RunStep()
    {
        audioSource.volume = runningVol;
        audioSource.pitch = runningPitch;
        audioSource.PlayOneShot(currentTextureSound);
    }
    void JumpLand()
    {
        audioSource.volume = jumpVol;
        audioSource.pitch = jumpPitch;
        audioSource.PlayOneShot(currentTextureSound);
    }
    void UpdateTextureSound()
    {
        if (currentTextureSound == null)
        {
            currentTextureSound = footstepSounds[0];
        }
        if (!currentTextureSound.name.ToLower().Contains(textureType.ToLower()))
        {
            foreach (AudioClip clip in footstepSounds)
            {
                if (clip.name.ToLower().Contains(textureType.ToLower()))
                {
                    currentTextureSound = clip;
                    break;
                }
            }
        }
    }
}
