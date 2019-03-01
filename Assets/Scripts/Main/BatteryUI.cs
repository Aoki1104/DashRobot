using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//左上のバッテリー表示処理のスクリプト
public class BatteryUI : MonoBehaviour
{
    public int maxbattery;
    private float nowbattery;
    public Slider battery;
    public Image battery_color;

    public void batteryInitialize(int max)
    {
        maxbattery = max;
        battery.maxValue = maxbattery;
        battery_color.GetComponent<Image>().color = Color.green;
    }
   
    public void battery_update(float now)
    {
        nowbattery = now;
        battery.value = nowbattery;

        if(0.2 > nowbattery/maxbattery)
        {
            battery_color.GetComponent<Image>().color = Color.red;
        }
    }
}
