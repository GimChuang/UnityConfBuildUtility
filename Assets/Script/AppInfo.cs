using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AppInfo {

    public string ProductName;
    public bool bForceSingleInstance;

    public AppInfo(string _productName, bool _bForceInstance)
    {
        ProductName = _productName;
        bForceSingleInstance = _bForceInstance;
    }
}
