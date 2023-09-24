using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class StarPower : MonoBehaviour
{
    [SerializeField] private int usages = 2;
    [SerializeField] private Camera mainCamera;
    [SerializeField] private Transform myTransform;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private List<AudioClip> audioClips;
    [SerializeField] private TMP_Text starPowerLabel;
    // Start is called before the first frame update
    [SerializeField]
    private void Start()
    {
        starPowerLabel.text = $"Star power left : {usages}";
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P) && usages > 0)
        {
            usages--;
            starPowerLabel.text = $"Star power left : {usages}";
            Vector2 maxLimitPosition = mainCamera.ViewportToWorldPoint(new Vector3(1, 1, 0));
            Vector2 minLimitPosition = mainCamera.ViewportToWorldPoint(new Vector3(0, 0, 0));
            myTransform.position = new Vector2(
            Random.Range(minLimitPosition.x, maxLimitPosition.x),
            Random.Range(minLimitPosition.y, maxLimitPosition.y));
            audioSource.PlayOneShot(audioClips[Random.Range(0, audioClips.Count)]);
        }
    }
}
