using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingMain : BuildingBase
{
    protected override void Destroy()
    {
        base.Destroy();

        UIManager.instance.LoseGame();
    }

}
