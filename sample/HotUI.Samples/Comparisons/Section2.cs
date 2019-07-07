﻿using System;
using System.Collections.Generic;
using System.Text;


/*
 
import SwiftUI

struct ContentView: View {
    var body: some View {
        Text("Turtle Rock")
            .font(.title)
            .color(.green)
    }
}

 */

namespace HotUI.Samples.Comparisons
{
    public class Section2 : View
    {
        public Section2()
        {
            Body = () => 
                new Text("Turtle Rock")
                    .Font(Font.Title)
                    .Color(Colors.Green);
        }
    }
}