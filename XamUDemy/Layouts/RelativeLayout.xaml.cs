﻿using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace XamUDemy.Layouts
{
    public partial class RelativeLayout : ContentPage
    {
        public RelativeLayout()
        {
            InitializeComponent();

            var layout = new Xamarin.Forms.RelativeLayout();
            Content = layout;

            var aquaBox = new BoxView { Color = Color.Aqua };
            layout.Children.Add
                  (aquaBox,
                   widthConstraint: Constraint.RelativeToParent(parent => parent.Width),
                   heightConstraint: Constraint.RelativeToParent(parent => parent.Height * 0.3));

            var silverBox = new BoxView { Color = Color.Silver };
            layout.Children.Add
                  (silverBox,
                   yConstraint: Constraint.RelativeToView(aquaBox, (RelativeLayout, element) => element.Height + 20));
             
        }
    }
}
