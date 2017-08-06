using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace MyRPG {

	public interface IDescription {
        
        Texture2D Icon { get; set; }
        string Name { get; set; }
        string Description { get; set; }

    }

}