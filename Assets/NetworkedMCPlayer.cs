using UnityEngine;
using BeardedManStudios.Forge.Networking.Generated;
using BeardedManStudios.Forge.Networking;
using BeardedManStudios.Forge.Networking.Unity;
using UnityStandardAssets.Characters.FirstPerson;

public class NetworkedMCPlayer : PlayerBehavior
{
    private Animator mAnimator = null;

    public float _SyncFactor = 0.2f;
    public int _NetworkSendRate = 11;
    private float mSendDelay = 1f / 11f; // Time in seconds to delay before sending again
    private float mLastServerParamTime = 0f; // Last time of the server parameter that was processed
    private float mSendElapsedTime = 0f; // Time since the last send
    private float mPhaseElapsedTime = 0f; // Time before we'll change the motion phase
    private bool isSetup;
    private bool isPlayerSetup;

    protected override void NetworkStart()
    {
        base.NetworkStart();

        //Enable Camera for Owned Player
        if (!networkObject.IsOwner)
        {
            Debug.Log("Disable Remote Player's camera");
            this.transform.GetComponent<FirstPersonController>().enabled = false;
            this.transform.Find("FirstPersonCharacter").GetComponent<AudioListener>().enabled = false;
            this.transform.Find("FirstPersonCharacter").GetComponent<Camera>().enabled = false;
        }

        Debug.Log("NetworkStart setup for Player!");
        isSetup = true;
        isPlayerSetup = false;
    }

    private void Update()
    {
        if (!isSetup) return;

        if (networkObject.IsOwner) //Send Pos/Rot/Anims to the Clients
        {
            //Sync Position/Rotation on the Network
            networkObject.position = transform.position;
            networkObject.rotation = transform.rotation;

            //Sync Animations
            //SyncAnimations();

            //Enable Camera for Owned Player
            if (networkObject.IsOwner && isPlayerSetup == false)
            {
                Debug.Log("Enable Local Player's camera");
                this.transform.GetComponent<FirstPersonController>().enabled = true;
                this.transform.Find("FirstPersonCharacter").GetComponent<AudioListener>().enabled = true;
                this.transform.Find("FirstPersonCharacter").GetComponent<Camera>().enabled = true;
                isPlayerSetup = true;
            }
        }
        else //Receive Data on Remote Players
        {
            // Assign the position of this player to the position/rotation sent on the network
            transform.position = Vector3.Lerp(transform.position, networkObject.position, _SyncFactor);
            transform.rotation = Quaternion.Lerp(transform.rotation, networkObject.rotation, _SyncFactor);
            //transform.position = networkObject.position;
            //transform.rotation = networkObject.rotation;
        }
    }

    private void SyncAnimations()
    {
    }

    public override void UpdateInfo(RpcArgs args)
    {
        string charName = args.GetNext<string>();
        int level = args.GetNext<int>();
        int race = args.GetNext<int>();
        int gender = args.GetNext<int>();
        string guildName = args.GetNext<string>();
    }

    public override void UpdateAnim(RpcArgs args)
    {
        bool IsGrounded = args.GetNext<bool>();
        int Stance = args.GetNext<int>();
        float InputX = args.GetNext<float>();
        float InputY = args.GetNext<float>();
        float InputMagnitude = args.GetNext<float>();
        float InputMagnitudeAvg = args.GetNext<float>();
        float InputAngleFromAvatar = args.GetNext<float>();
        float InputAngleFromCamera = args.GetNext<float>();
        int L0MotionParameter = args.GetNext<int>();
        float L0MotionStateTime = args.GetNext<float>();
        int L0MotionPhase = args.GetNext<int>();
        int L0MotionForm = args.GetNext<int>();
        int L1MotionParameter = args.GetNext<int>();
        float L1MotionStateTime = args.GetNext<float>();
        int L1MotionPhase = args.GetNext<int>();
        int L1MotionForm = args.GetNext<int>();
    }

    public override void UpdateAnimStates(RpcArgs args)
    {
        //UpdateAnimBool
    }

}