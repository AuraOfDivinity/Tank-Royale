using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviourPun, IPunObservable
{
    public float moveSpeed = 3;
    Vector2 movement;
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
            movement.x += Input.GetAxisRaw("Horizontal");
            movement.y += Input.GetAxisRaw("Vertical");

            movement = Vector3.Normalize(movement);

            animator.SetFloat("Horizontal", movement.x);
            animator.SetFloat("Vertical", movement.y);
            //animator.SetFloat("Speed", lastMovement.sqrMagnitude);

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
            stream.SendNext(movement);
        }
        //Reading pccurs when we are reading others tank position
        else if (stream.IsReading)
        {
            smoothMove = (Vector3)stream.ReceiveNext();
            receivedMovement = (Vector2)stream.ReceiveNext();
        }
    }
}
