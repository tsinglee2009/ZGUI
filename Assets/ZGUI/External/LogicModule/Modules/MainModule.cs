//---------------------------------------
// Author: Lee
// Date: 3/19/2017
// Desc: MainModule
//---------------------------------------
using UnityEngine;
using System.Collections.Generic;
using System;

namespace ZGUI
{
    public class MainModule : LogicModule
    {
        public ZUIMain m_ui;
        protected override ZUI UI
        {
            get
            {
                return m_ui;
            }
            set
            {
                m_ui = (ZUIMain)value;
            }
        }
    }
}
