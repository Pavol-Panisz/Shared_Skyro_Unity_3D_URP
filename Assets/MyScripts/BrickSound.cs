using System.Collections;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class BrickSound : MonoBehaviour
{
	[SerializeField] private AudioClip clip;
	[SerializeField] private float volume = 1f;
	[SerializeField] private float minImpactVelocity = 1.0f;
	[SerializeField] private string groundTag = "Ground";
	[SerializeField] private float cooldown = 0.1f;

	private AudioSource audioSource;
	private float lastPlayTime;

	private void Awake()
	{
		audioSource = GetComponent<AudioSource>();
		audioSource.playOnAwake = false;
		audioSource.spatialBlend = 1f; // 3D sound
	}

	private void OnCollisionEnter(Collision collision)
	{
		if (clip == null)
			return;

		if (!IsGroundContact(collision))
			return;

		float impactSpeed = collision.relativeVelocity.magnitude;
		if (impactSpeed < minImpactVelocity)
			return;

		if (Time.time - lastPlayTime < cooldown)
			return;

		audioSource.PlayOneShot(clip, volume);
		lastPlayTime = Time.time;
	}

	private bool IsGroundContact(Collision collision)
	{
		// Prefer tag match if provided
		bool tagMatches = string.IsNullOrEmpty(groundTag) || collision.collider.CompareTag(groundTag);

		// Confirm the contact is with a surface below the brick (upward normal)
		bool normalUp = false;
		foreach (var contact in collision.contacts)
		{
			if (contact.normal.y > 0.5f)
			{
				normalUp = true;
				break;
			}
		}

		// Require upward normal, and either tag matches or no tag specified
		return normalUp && tagMatches;
	}
}

