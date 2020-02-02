using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum HUDState {
  BASE,
  BUILD,

};

public class HUDManager : Singleton<HUDManager>
{
    string tooltipCreateWall = "Create a wall (Cost: 15 ore)";
    string tooltipCreateTurret = "Create a turret (Cost: 30 ore, 10 wood, 10 wheat)";

    string tooltipBuild = "Place the structure";
    string tooltipCoin = "Exchange resources for a coin (Cost: 30 ore, 30 wood, 30 wheat";

    public HUDState hudState;
    public Text hudText;

    public HorizontalLayoutGroup leftRightItems;
    public RectTransform cancelButton;

    public BuildableObject wallObject;
    public BuildableObject turretObject;
	
	MouseBehaviour mouse;



    // Start is called before the first frame update
    void Start()
    {
        hudState = HUDState.BASE;
        RefreshHUDSDisplay();
		mouse = Camera.main.GetComponent<MouseBehaviour>();
    }


    public void BuildWall() {
      if (hudState != HUDState.BASE) {
        return;
      }

      hudState = HUDState.BUILD;
      BuildableObject planner = Instantiate(wallObject) as BuildableObject; // Will position itself automatically
	  if (!mouse.BeginBuild(planner)){
		  Destroy(planner);
		  return;
	  }
      RefreshHUDSDisplay();
    }

    public void BuildTurret() {
      if (hudState != HUDState.BASE) {
        return;
      }
      
      hudState = HUDState.BUILD;

      BuildableObject planner = Instantiate(turretObject) as BuildableObject; // Will position itself automatically
	  if (!mouse.BeginBuild(planner)){
		  Destroy(planner);
		  return;
	  }
      RefreshHUDSDisplay();

    }

    public void PressCoin() {
      if (hudState != HUDState.BASE) {
        return;
      }

      //TODO: if can buy a coin
      // ...
    }

    public void DisplayBuildWallTooltip() {
      hudText.text = tooltipCreateWall;
    }

    public void DisplayBuildTurretTooltip() {
      hudText.text = tooltipCreateTurret;
    }

    public void DisplayCoinToolTip() {
      hudText.text = tooltipCoin;
    }

    public void PointerExitIcon() {
      hudText.text = "";
    }

    public void CancelPressed() {
      ResetHUDState();
    }

    public void ResetHUDState() {
		mouse.EndBuild();
      hudState = HUDState.BASE;
      hudText.text = "";
      RefreshHUDSDisplay();
    }

    public void RefreshHUDSDisplay() {
      switch (hudState) {
        case HUDState.BASE:
          cancelButton.gameObject.SetActive(false);
          leftRightItems.gameObject.SetActive(true);
          hudText.text = "";
          break;
        case HUDState.BUILD:
          cancelButton.gameObject.SetActive(true);
          leftRightItems.gameObject.SetActive(false);
          hudText.text = tooltipBuild;
          break;
        default:
          break;
      }
    }
}
