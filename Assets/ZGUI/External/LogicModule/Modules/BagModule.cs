//---------------------------------------
// Author: Lee
// Date: 3/19/2017
// Desc: BagModule
//---------------------------------------
using UnityEngine;
using System.Collections.Generic;
using System;

namespace ZGUI
{
    public class BagModule : LogicModule
    {
        public ZUIBag m_ui;
        protected override ZUI UI
        {
            get
            {
                return m_ui;
            }

            set
            {
                m_ui = (ZUIBag)value;
            }
        }
    }
}
