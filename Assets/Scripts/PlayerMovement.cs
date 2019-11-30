using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviourPun, IPunObservable
{
    public float moveSpeed = 3;
    Vector2 lastMovement;
    public float direction;
    Vector2 receivedMovement;

    public Animator animator;
    private Vector3 smoothMove;
    private GameObject sceneCamera;
    public GameObject playerCamera;

    public Text nameText;

    //PhotonViews are used to sync the player animations of multiple players
    public PhotonView pv;

    // Start is called before the first frame update
    void Start()
    {
        if (pv.IsMine)
        {
            nameText.text = PhotonNetwork.NickName;
            sceneCamera = GameObject.Find("Main Camera");
            sceneCamera.SetActive(false);
            playerCamera.SetActive(true);
        }
        else
        {
            nameText.text = pv.Owner.NickName;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (pv.IsMine)
        {
            if (Input.GetAxis("Horizontal") > Input.GetAxis("Vertical"))
            {
                direction = (Input.GetAxis("Horizontal") > 0) ? 3 : 2;
            }
            else
            {
                direction = (Input.GetAxis("Vertical") > 0) ? 0 : 1;
            }
            //lastMovement.x = Mathf.Clamp(lastMovement.x + Input.GetAxisRaw("Horizontal"), -1, 1);
            //lastMovement.y = Mathf.Clamp(lastMovement.y + Input.GetAxisRaw("Vertical"), -1, 1);

            //animator.SetFloat("Horizontal", lastMovement.x);
            //animator.SetFloat("Vertical", lastMovement.y);
            //animator.SetFloat("Speed", lastMovement.sqrMagnitude);
            animator.SetFloat("Direction", direction);

            //rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);

            var move = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
            transform.position += move * moveSpeed * Time.deltaTime;

        }
        else
        {
            animator.SetFloat("Horizontal", receivedMovement.x);
            animator.SetFloat("Vertical", receivedMovement.y);
            animator.SetFloat("Speed", receivedMovement.sqrMagnitude);
            transform.position = Vector3.Lerp(transform.position, smoothMove, Time.deltaTime * 10);
        }
    }


    //The OnPhotonSerialize function sends and receives positions of the other tank instances
    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        //Writing occurs when we are sending our tank position to others.
        if (stream.IsWriting)
        {
            //Send the transform position to all the other instances
            //SendNext is only called when the a change occurs
            stream.SendNext(transform.position);
            stream.SendNext(lastMovement);
        }
        //Reading pccurs when we are reading others tank position
        else if (stream.IsReading)
        {
            smoothMove = (Vector3)stream.ReceiveNext();
            receivedMovement = (Vector2)stream.ReceiveNext();
        }
    }
}
