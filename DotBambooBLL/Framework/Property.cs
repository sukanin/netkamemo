﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotBambooBLL.Framework
{
    [Serializable]
    internal class Property
    {
        public string Name { get; set; }
        public object Value { get; set; }
    }

    [Serializable]
    internal class PropertyList : List<Property>
    {

    }
}
