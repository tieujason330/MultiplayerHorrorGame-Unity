using UnityEngine;
using System.Collections;

public class NetworkCharacter : Photon.MonoBehaviour {

	Vector3 realPosition = Vector3.zero;
	Quaternion realRotation = Quaternion.identity;

	Animator anim;
	
	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator> ();
		if (anim == null) {
			Debug.LogError("You forgot to put an Animator component on this character prefab");
		}
	}
	
	// Update is called once per frame
	void Update () {
		if (photonView.isMine) {
						// do nothing - the character motor/input/etc is moving us		
		}
		else {
			//updates/moves where we think we are to the real position of where we are
			transform.position = Vector3.Lerp (transform.position, realPosition, 0.1f);
			transform.rotation = Quaternion.Lerp(transform.rotation, realRotation, 0.1f);
		}
	}

	public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
	{
		Debug.Log ("OnPhotonSerializeView");

		if (stream.isWriting) {
			//this is our player. we need to send our actual position to the network

			stream.SendNext(transform.position);
			stream.SendNext(transform.rotation);
			stream.SendNext(anim.GetFloat("Speed"));
			stream.SendNext(anim.GetBool("Jumping"));
			
		}
		else {
			//this is someone else's player. need to receive their position
			//and update our version of that player
			Debug.Log ("Other player sending info");
			realPosition = (Vector3)stream.ReceiveNext();
			realRotation = (Quaternion)stream.ReceiveNext();
			anim.SetFloat ("Speed", (float)stream.ReceiveNext());
			anim.SetBool ("Jumping", (bool)stream.ReceiveNext());
			
		}
	}
}
