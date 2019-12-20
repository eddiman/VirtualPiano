using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK.Prefabs.Locomotion.Teleporters;
using Zinnia.Data.Type;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class PauseMenuLogic : MonoBehaviour
{
    public TeleporterFacade teleporter;
    public Transform playArea;
    public Transform headOrientation;
    public Transform pauseLocation;
    public Transform gameLocation;

    protected bool inPauseMenu = false;

    public List<GameObject> pauseItems;
    public List<GameObject> gameItems;

    public GameObject teleportationRelease;
    public GameObject teleportationPress;
    public void SwitchRooms() {
        TransformData teleportDestination = new TransformData(gameLocation);
        if (!inPauseMenu) {
            gameLocation.position = new Vector3(headOrientation.position.x, playArea.position.y, headOrientation.position.z);

            Vector3 right = Vector3.Cross(playArea.up, headOrientation.forward);
            Vector3 forward = Vector3.Cross(right, playArea.up);

            gameLocation.rotation = Quaternion.LookRotation(forward, playArea.up);

            teleportDestination = new TransformData(pauseLocation);
        }

        teleporter.Teleport(teleportDestination);
        inPauseMenu = !inPauseMenu;

        foreach (GameObject item in pauseItems) {
            item.SetActive(inPauseMenu);
        }

        foreach (GameObject item in gameItems) {
            item.SetActive(!inPauseMenu);
        }
    }
    public void ResetGame() {
        SceneManager.LoadScene("Final", LoadSceneMode.Single);
    }

    public void SwitchTeleportationToPress(bool value) {
        teleportationRelease.SetActive(!value);
        teleportationPress.SetActive(value);
    }
    public void SwitchTeleportationToRelease(bool value) {
        SwitchTeleportationToPress(!value);
    }
}
