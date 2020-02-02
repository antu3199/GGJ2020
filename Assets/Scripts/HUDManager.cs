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
    string tooltipHouse = "Create a house (Cost: 20 wood, 20 wheat)";

    string tooltipBuild = "Place the structure";
    string tooltipCoin = "Exchange resources for a coin (Cost: 30 ore, 30 wood, 30 wheat";

    string tooltipWheat = "Create a wheat (Cost: 10 wheat)";
    string tooltipOre = "Create a ore (Cost: 10 ore)";
    string tooltipWood = "Create a wood (Cost: 10 wood)";

    public HUDState hudState;
    public Text hudText;

    public HorizontalLayoutGroup leftRightItems;
    public RectTransform cancelButton;

    public BuildableObject wallObject;
    public BuildableObject turretObject;

    public BuildableObject houseObject;
	  MouseBehaviour mouse;


    public BuildableObject wheatObject;
    public BuildableObject woodObject;
    public BuildableObject oreObject;

    public Text timerText;

    public Chapel chapel;


    // Start is called before the first frame update
    void Start()
    {
        hudState = HUDState.BASE;
        RefreshHUDSDisplay();
		mouse = Camera.main.GetComponent<MouseBehaviour>();
    }
	
	void Build(BuildableObject obj){
		if (hudState != HUDState.BASE) {
        return;
      }

      hudState = HUDState.BUILD;
      BuildableObject planner = Instantiate(obj) as BuildableObject; // Will position itself automatically
	  if (!mouse.BeginBuild(planner)){
		  Destroy(planner);
		  ResetHUDState();
		  return;
	  }
      RefreshHUDSDisplay();
	}


    public void BuildWall() {
      Build(wallObject);
    }

    public void BuildTurret() {
      Build(turretObject);

    }

    public void PressCoin() {
      if (hudState != HUDState.BASE) {
        return;
      }

      chapel.tryMakeGold();
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

    public void BuildHouse() {
      Build(houseObject);

    }
 
    public void DisplayHouse() {
      hudText.text = tooltipHouse;
    }


    public void BuildWheat() {
      Build(wheatObject);

    }
 
    public void DisplayWheat() {
      hudText.text = tooltipWheat;
    }

    public void BuildWood() {
      Build(woodObject);

    }
 
    public void DisplayWood() {
      hudText.text = tooltipWood;
    }

    public void BuildOre() {
	  Build(oreObject);

    }
 
    public void DisplayOre() {
      hudText.text = tooltipOre;
    }

}


