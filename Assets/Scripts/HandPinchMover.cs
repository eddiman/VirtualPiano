using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class HandPinchMover : MonoBehaviour
{
    // Start is called before the first frame update
    public Transform objectToMove;
    public Transform objectToMoveParent;
    public Transform objectToMoveAnchor;
    private GameObject HandsManager;
    public TextMeshPro tmText;
    public float pinchTreshold = .9f;

    private string _pinchString;
    private OVRHand _hand;
    private OVRSkeleton _skeleton;
    private Transform _thumbTipTransform;
    private int noOfCaps;

    public Color currentColor;
    public Color positioningColor;

    public bool isLeftPinching;
    protected void Start()
    {
        _hand = GetComponent<OVRHand>();
        _skeleton = GetComponent<OVRSkeleton>();
        HandsManager = GameObject.Find("HandsManager");
        GetComponent<SkinnedMeshRenderer>().material.color = currentColor;

        foreach (var capsule in GetComponent<OVRSkeleton>().Capsules)
        {
            capsule.CapsuleCollider.transform.gameObject.tag = "HandCapsule";
        }

        foreach (var bone in _skeleton.Bones)
        {
            if (bone.Id == OVRSkeleton.BoneId.Hand_ThumbTip)
            {
                _thumbTipTransform = bone.Transform;
            }
        }
    }

    // Update is called once per frame
    public void Update()
    {
        if (HandsManager.GetComponent<PianoPositionController>().canPositionPiano)
        {
            CheckIndexPinch();
        }
    }

    public void ChangeColor()
    {
        StartCoroutine(HandColorCoroutine());


    }

    private void CheckIndexPinch()
    {
        float indexPinchStrength = _hand.GetFingerPinchStrength(OVRHand.HandFinger.Index);
        float middlePinchStrength = _hand.GetFingerPinchStrength(OVRHand.HandFinger.Middle);

        bool isIndexPinching = indexPinchStrength > pinchTreshold;
        bool isMiddlePinching = middlePinchStrength > pinchTreshold;

        OVRHand.TrackingConfidence conf = _hand.HandConfidence;
        tmText.text = "Index Pinch Strength: " + indexPinchStrength + " - isPinching: " + isIndexPinching + "\n" +
                      "Middle Pinch Strength: " + middlePinchStrength + " - isPinching: " + isMiddlePinching + "\n" +
                      "HandCapsule Tags: " + noOfCaps;

        if (isIndexPinching && !isMiddlePinching && conf == OVRHand.TrackingConfidence.High &&
            _skeleton.GetSkeletonType() == OVRSkeleton.SkeletonType.HandRight /*&& objectToMove.localPosition.y < -0.264f && objectToMove.localPosition.y > -.802f*/)
        {
            tmText.text = string.Concat(tmText.text, " in the indexpinchong");
            float offsetY = objectToMoveAnchor.position.y - objectToMove.position.y;

            objectToMove.position = Vector3.Lerp(objectToMove.position,
                new Vector3(objectToMove.position.x, _thumbTipTransform.position.y - offsetY, objectToMove.position.z),
                Time.deltaTime);
        }
        else if (isMiddlePinching && !isIndexPinching && conf == OVRHand.TrackingConfidence.High &&
                 _skeleton.GetSkeletonType() == OVRSkeleton.SkeletonType.HandRight)
        {
            float offsetX = objectToMoveAnchor.position.x - objectToMoveParent.position.x;
            objectToMoveParent.position = Vector3.Lerp(objectToMoveParent.position,
                new Vector3( _thumbTipTransform.position.x - offsetX, objectToMoveParent.position.y, objectToMoveParent.position.z),
                Time.deltaTime);
        }
        else if (isMiddlePinching && isIndexPinching && conf == OVRHand.TrackingConfidence.High &&
                 _skeleton.GetSkeletonType() == OVRSkeleton.SkeletonType.HandRight)
        {
            float offsetZ = objectToMoveAnchor.position.z - objectToMoveParent.position.z;
            objectToMoveParent.position = Vector3.Lerp(objectToMoveParent.position,
                new Vector3( objectToMoveParent.position.x, objectToMoveParent.position.y, _thumbTipTransform.position.z - offsetZ),
                Time.deltaTime);
        }

        if (isIndexPinching && conf == OVRHand.TrackingConfidence.High &&
            _skeleton.GetSkeletonType() == OVRSkeleton.SkeletonType.HandLeft)
        {
            isLeftPinching = true;
            Vector3 relativePos = _thumbTipTransform.position - objectToMoveParent.position;
            Quaternion toRotation = Quaternion.LookRotation(relativePos * -1);
            objectToMoveParent.rotation = Quaternion.Lerp( objectToMoveParent.rotation, new Quaternion(objectToMoveParent.rotation.x, toRotation.y, objectToMoveParent.rotation.z, objectToMoveParent.rotation.w), 1 * Time.deltaTime );
        }
        else
        {
            isLeftPinching = false;
        }
    }

    public IEnumerator HandColorCoroutine()
    {
        if (HandsManager.GetComponent<PianoPositionController>().canPositionPiano)
        {
            float t = 0.0f;
            while ( t < 3f )
            {
                t += Time.deltaTime;
                GetComponent<SkinnedMeshRenderer>().material.color =
                    Color.Lerp(GetComponent<SkinnedMeshRenderer>().material.color, positioningColor,t / 3f);
                yield return null;
            }
        }
        else
        {
            float t = 0.0f;
            while ( t < 3f )
            {
                t += Time.deltaTime;
                GetComponent<SkinnedMeshRenderer>().material.color =
                    Color.Lerp(GetComponent<SkinnedMeshRenderer>().material.color, currentColor, t / 3f);
                yield return null;
            }
        }
    }
}
