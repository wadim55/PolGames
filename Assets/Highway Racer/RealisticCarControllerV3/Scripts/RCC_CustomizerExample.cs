﻿//----------------------------------------------
//            Realistic Car Controller
//
// Copyright © 2014 - 2022 BoneCracker Games
// http://www.bonecrackergames.com
// Buğra Özdoğanlar
//
//----------------------------------------------

using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.EventSystems;

/// <summary>
/// A simple customizer example script used for receiving methods from UI elements and send them to RCC_Customization script. Also updates all UI elements for new spawned vehicles too.
/// </summary>
[AddComponentMenu("BoneCracker Games/Realistic Car Controller/UI/RCC Customizer Example")]
public class RCC_CustomizerExample : RCC_Singleton<RCC_CustomizerExample> {

    [Header("UI Menus")]
    public GameObject wheelsMenu;
    public GameObject configurationMenu;
    public GameObject steeringAssistancesMenu;
    public GameObject colorsMenu;

    [Header("UI Sliders")]
    public Slider frontCamber;
    public Slider rearCamber;
    public Slider frontSuspensionDistances;
    public Slider rearSuspensionDistances;
    public Slider frontSuspensionDampers;
    public Slider rearSuspensionDampers;
    public Slider frontSuspensionSprings;
    public Slider rearSuspensionSprings;
    public Slider gearShiftingThreshold;
    public Slider clutchThreshold;

    [Header("UI Toggles")]
    public Toggle TCS;
    public Toggle ABS;
    public Toggle ESP;
    public Toggle SH;
    public Toggle counterSteering;
    public Toggle steeringSensitivity;
    public Toggle NOS;
    public Toggle turbo;
    public Toggle exhaustFlame;
    public Toggle revLimiter;
    public Toggle transmission;

    [Header("UI InputFields")]
    public InputField maxSpeed;
    public InputField maxBrake;
    public InputField maxTorque;

    [Header("UI Dropdown Menus")]
    public Dropdown drivetrainMode;

    void Start() {

        CheckUIs();

    }

    public void CheckUIs() {

        if (!RCC_SceneManager.Instance.activePlayerVehicle)
            return;

        if(frontCamber)
            frontCamber.value = RCC_SceneManager.Instance.activePlayerVehicle.FrontLeftWheelCollider.camber;
        if(rearCamber)
            rearCamber.value = RCC_SceneManager.Instance.activePlayerVehicle.RearLeftWheelCollider.camber;
        if (frontSuspensionDistances)
            frontSuspensionDistances.value = RCC_SceneManager.Instance.activePlayerVehicle.FrontLeftWheelCollider.wheelCollider.suspensionDistance;
        if (rearSuspensionDistances)
            rearSuspensionDistances.value = RCC_SceneManager.Instance.activePlayerVehicle.RearLeftWheelCollider.wheelCollider.suspensionDistance;
        if (frontSuspensionDampers) 
            frontSuspensionDampers.value = RCC_SceneManager.Instance.activePlayerVehicle.FrontLeftWheelCollider.wheelCollider.suspensionSpring.damper;
        if (rearSuspensionDampers) 
            rearSuspensionDampers.value = RCC_SceneManager.Instance.activePlayerVehicle.RearLeftWheelCollider.wheelCollider.suspensionSpring.damper;
        if (frontSuspensionSprings) 
            frontSuspensionSprings.value = RCC_SceneManager.Instance.activePlayerVehicle.FrontLeftWheelCollider.wheelCollider.suspensionSpring.spring;
        if (rearSuspensionSprings) 
            rearSuspensionSprings.value = RCC_SceneManager.Instance.activePlayerVehicle.RearLeftWheelCollider.wheelCollider.suspensionSpring.spring;
        if (gearShiftingThreshold) 
            gearShiftingThreshold.value = RCC_SceneManager.Instance.activePlayerVehicle.gearShiftingThreshold;
        if (clutchThreshold) 
            clutchThreshold.value = RCC_SceneManager.Instance.activePlayerVehicle.clutchInertia;

        if(TCS)
            TCS.isOn = RCC_SceneManager.Instance.activePlayerVehicle.TCS;
        if(ABS)
            ABS.isOn = RCC_SceneManager.Instance.activePlayerVehicle.ABS;
        if(ESP)
            ESP.isOn = RCC_SceneManager.Instance.activePlayerVehicle.ESP;
        if(SH)
            SH.isOn = RCC_SceneManager.Instance.activePlayerVehicle.steeringHelper;
       if(counterSteering)
            counterSteering.isOn = RCC_SceneManager.Instance.activePlayerVehicle.useCounterSteering;
        if(NOS)
            NOS.isOn = RCC_SceneManager.Instance.activePlayerVehicle.useNOS;
        if(turbo)
            turbo.isOn = RCC_SceneManager.Instance.activePlayerVehicle.useTurbo;
        if(exhaustFlame)
            exhaustFlame.isOn = RCC_SceneManager.Instance.activePlayerVehicle.useExhaustFlame;
        if(revLimiter)
            revLimiter.isOn = RCC_SceneManager.Instance.activePlayerVehicle.useRevLimiter;
        if(transmission)
            transmission.isOn = RCC_Settings.Instance.useAutomaticGear;

        if(maxSpeed)
            maxSpeed.text = RCC_SceneManager.Instance.activePlayerVehicle.maxspeed.ToString();

        if(maxBrake)
            maxBrake.text = RCC_SceneManager.Instance.activePlayerVehicle.brakeTorque.ToString();

        if(maxTorque)
            maxTorque.text = RCC_SceneManager.Instance.activePlayerVehicle.maxEngineTorque.ToString();

        switch (RCC_SceneManager.Instance.activePlayerVehicle.wheelTypeChoise) {

            case RCC_CarControllerV3.WheelType.FWD:
                drivetrainMode.value = 0;
                break;

            case RCC_CarControllerV3.WheelType.RWD:
                drivetrainMode.value = 1;
                break;

            case RCC_CarControllerV3.WheelType.AWD:
                drivetrainMode.value = 2;
                break;

            case RCC_CarControllerV3.WheelType.BIASED:
                drivetrainMode.value = 3;
                break;

        }

    }

    public void OpenMenu(GameObject activeMenu) {

        if (activeMenu.activeInHierarchy) {

            activeMenu.SetActive(false);
            return;

        }

        wheelsMenu.SetActive(false);
        configurationMenu.SetActive(false);
        steeringAssistancesMenu.SetActive(false);
        colorsMenu.SetActive(false);

        activeMenu.SetActive(true);

    }

    public void CloseAllMenus() {

        wheelsMenu.SetActive(false);
        configurationMenu.SetActive(false);
        steeringAssistancesMenu.SetActive(false);
        colorsMenu.SetActive(false);

    }

    public void SetCustomizationMode(bool state) {

        if (!RCC_SceneManager.Instance.activePlayerVehicle)
            return;

        RCC_Customization.SetCustomizationMode(RCC_SceneManager.Instance.activePlayerVehicle, state);

        if (state)
            CheckUIs();

    }

    public void SetFrontCambersBySlider(Slider slider) {

        RCC_Customization.SetFrontCambers(RCC_SceneManager.Instance.activePlayerVehicle, slider.value);

    }

    public void SetRearCambersBySlider(Slider slider) {

        RCC_Customization.SetRearCambers(RCC_SceneManager.Instance.activePlayerVehicle, slider.value);

    }

    public void TogglePreviewSmokeByToggle(Toggle toggle) {

        RCC_Customization.SetSmokeParticle(RCC_SceneManager.Instance.activePlayerVehicle, toggle.isOn);

    }

    public void TogglePreviewExhaustFlameByToggle(Toggle toggle) {

        RCC_Customization.SetExhaustFlame(RCC_SceneManager.Instance.activePlayerVehicle, toggle.isOn);

    }

    public void SetSmokeColorByColorPicker(RCC_ColorPickerBySliders color) {

        RCC_Customization.SetSmokeColor(RCC_SceneManager.Instance.activePlayerVehicle, 0, color.color);

    }

    public void SetHeadlightColorByColorPicker(RCC_ColorPickerBySliders color) {

        RCC_Customization.SetHeadlightsColor(RCC_SceneManager.Instance.activePlayerVehicle, color.color);

    }

    public void ChangeWheelsBySlider(Slider slider) {

        RCC_Customization.ChangeWheels(RCC_SceneManager.Instance.activePlayerVehicle, RCC_ChangableWheels.Instance.wheels[(int)slider.value].wheel, true);

    }

    public void SetFrontSuspensionTargetsBySlider(Slider slider) {

        RCC_Customization.SetFrontSuspensionsTargetPos(RCC_SceneManager.Instance.activePlayerVehicle, slider.value);

    }

    public void SetRearSuspensionTargetsBySlider(Slider slider) {

        RCC_Customization.SetRearSuspensionsTargetPos(RCC_SceneManager.Instance.activePlayerVehicle, slider.value);

    }

    public void SetAllSuspensionTargetsByButton(float strength) {

        RCC_Customization.SetAllSuspensionsTargetPos(RCC_SceneManager.Instance.activePlayerVehicle, strength);

    }

    public void SetFrontSuspensionDistancesBySlider(Slider slider) {

        RCC_Customization.SetFrontSuspensionsDistances(RCC_SceneManager.Instance.activePlayerVehicle, slider.value);

    }

    public void SetRearSuspensionDistancesBySlider(Slider slider) {

        RCC_Customization.SetRearSuspensionsDistances(RCC_SceneManager.Instance.activePlayerVehicle, slider.value);

    }

    public void SetGearShiftingThresholdBySlider(Slider slider) {

        RCC_Customization.SetGearShiftingThreshold(RCC_SceneManager.Instance.activePlayerVehicle, Mathf.Clamp(slider.value, .5f, .95f));

    }

    public void SetClutchThresholdBySlider(Slider slider) {

        RCC_Customization.SetClutchThreshold(RCC_SceneManager.Instance.activePlayerVehicle, Mathf.Clamp(slider.value, .1f, .9f));

    }

    public void SetDriveTrainModeByDropdown(Dropdown dropdown) {

        switch (dropdown.value) {

            case 0:
                RCC_Customization.SetDrivetrainMode(RCC_SceneManager.Instance.activePlayerVehicle, RCC_CarControllerV3.WheelType.FWD);
                break;

            case 1:
                RCC_Customization.SetDrivetrainMode(RCC_SceneManager.Instance.activePlayerVehicle, RCC_CarControllerV3.WheelType.RWD);
                break;

            case 2:
                RCC_Customization.SetDrivetrainMode(RCC_SceneManager.Instance.activePlayerVehicle, RCC_CarControllerV3.WheelType.AWD);
                break;

        }

    }

    public void SetCounterSteeringByToggle(Toggle toggle) {

        RCC_Customization.SetCounterSteering(RCC_SceneManager.Instance.activePlayerVehicle, toggle.isOn);

    }

    public void SetSteeringLimitByToggle(Toggle toggle) {

        RCC_Customization.SetSteeringLimit(RCC_SceneManager.Instance.activePlayerVehicle, toggle.isOn);

    }

    public void SetNOSByToggle(Toggle toggle) {

        RCC_Customization.SetNOS(RCC_SceneManager.Instance.activePlayerVehicle, toggle.isOn);

    }

    public void SetTurboByToggle(Toggle toggle) {

        RCC_Customization.SetTurbo(RCC_SceneManager.Instance.activePlayerVehicle, toggle.isOn);

    }

    public void SetExhaustFlameByToggle(Toggle toggle) {

        RCC_Customization.SetUseExhaustFlame(RCC_SceneManager.Instance.activePlayerVehicle, toggle.isOn);

    }

    public void SetRevLimiterByToggle(Toggle toggle) {

        RCC_Customization.SetRevLimiter(RCC_SceneManager.Instance.activePlayerVehicle, toggle.isOn);

    }

    public void SetFrontSuspensionsSpringForceBySlider(Slider slider) {

        RCC_Customization.SetFrontSuspensionsSpringForce(RCC_SceneManager.Instance.activePlayerVehicle, Mathf.Clamp(slider.value, 10000f, 100000f));

    }

    public void SetRearSuspensionsSpringForceBySlider(Slider slider) {

        RCC_Customization.SetRearSuspensionsSpringForce(RCC_SceneManager.Instance.activePlayerVehicle, Mathf.Clamp(slider.value, 10000f, 100000f));

    }

    public void SetFrontSuspensionsSpringDamperBySlider(Slider slider) {

        RCC_Customization.SetFrontSuspensionsSpringDamper(RCC_SceneManager.Instance.activePlayerVehicle, Mathf.Clamp(slider.value, 1000f, 10000f));

    }

    public void SetRearSuspensionsSpringDamperBySlider(Slider slider) {

        RCC_Customization.SetRearSuspensionsSpringDamper(RCC_SceneManager.Instance.activePlayerVehicle, Mathf.Clamp(slider.value, 1000f, 10000f));

    }

    public void SetMaximumSpeedByInputField(InputField inputField) {

        RCC_Customization.SetMaximumSpeed(RCC_SceneManager.Instance.activePlayerVehicle, StringToFloat(inputField.text, 200f));
        inputField.text = RCC_SceneManager.Instance.activePlayerVehicle.maxspeed.ToString();

    }

    public void SetMaximumTorqueByInputField(InputField inputField) {

        RCC_Customization.SetMaximumTorque(RCC_SceneManager.Instance.activePlayerVehicle, StringToFloat(inputField.text, 2000f));
        inputField.text = RCC_SceneManager.Instance.activePlayerVehicle.maxEngineTorque.ToString();

    }

    public void SetMaximumBrakeByInputField(InputField inputField) {

        RCC_Customization.SetMaximumBrake(RCC_SceneManager.Instance.activePlayerVehicle, StringToFloat(inputField.text, 2000f));
        inputField.text = RCC_SceneManager.Instance.activePlayerVehicle.brakeTorque.ToString();

    }

    public void RepairCar() {

        RCC_Customization.Repair(RCC_SceneManager.Instance.activePlayerVehicle);

    }

    public void SetESP(Toggle toggle) {

        RCC_Customization.SetESP(RCC_SceneManager.Instance.activePlayerVehicle, toggle.isOn);

    }

    public void SetABS(Toggle toggle) {

        RCC_Customization.SetABS(RCC_SceneManager.Instance.activePlayerVehicle, toggle.isOn);

    }

    public void SetTCS(Toggle toggle) {

        RCC_Customization.SetTCS(RCC_SceneManager.Instance.activePlayerVehicle, toggle.isOn);

    }

    public void SetSH(Toggle toggle) {

        RCC_Customization.SetSH(RCC_SceneManager.Instance.activePlayerVehicle, toggle.isOn);

    }

    public void SetSHStrength(Slider slider) {

        RCC_Customization.SetSHStrength(RCC_SceneManager.Instance.activePlayerVehicle, slider.value);

    }

    public void SetTransmission(Toggle toggle) {

        RCC_Customization.SetTransmission(toggle.isOn);

    }

    public void SaveStats() {

        RCC_Customization.SaveStats(RCC_SceneManager.Instance.activePlayerVehicle);

    }

    public void LoadStats() {

        RCC_Customization.LoadStats(RCC_SceneManager.Instance.activePlayerVehicle);
        CheckUIs();

    }

    public void ResetStats() {

        int selectedVehicleIndex = HR_MainMenuHandler.Instance.carIndex;

        RCC_Customization.ResetStats(RCC_SceneManager.Instance.activePlayerVehicle, HR_PlayerCars.Instance.cars[selectedVehicleIndex].playerCar.GetComponent<RCC_CarControllerV3>());

        CheckUIs();

    }

    private float StringToFloat(string stringValue, float defaultValue) {

        float result = defaultValue;
        float.TryParse(stringValue, out result);
        return result;

    }

}
