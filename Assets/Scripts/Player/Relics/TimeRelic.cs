﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeRelic : EquipmentObj
{
    // Start is called before the first frame update
    void Start()
    {
        
    }
    public override void Circle() {
        base.Circle();
        //stop time
    }
    public override void UpCircle() {
        base.UpCircle();
        //speed up zend
    }
    public override void DownCircle() {
        base.DownCircle();
        //slow down time
    }
}
