using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace TechnoRely.Views
{
    public partial class PopularArtistsView : BaseContentView
    {
        public PopularArtistsView()
        {
            InitializeComponent();
        }

        protected override bool OnBackButtonPressed()
        {
            return false;
        }
    }
}
