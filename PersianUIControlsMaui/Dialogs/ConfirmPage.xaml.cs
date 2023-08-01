﻿using CommunityToolkit.Maui.Views;
using PersianUIControlsMaui.Models;

namespace PersianUIControlsMaui.Dialogs;

[XamlCompilation(XamlCompilationOptions.Compile)]
public partial class ConfirmPage : Popup
{
    ConfirmConfig config;
    public ConfirmPage(ConfirmConfig _config)
    {
        InitializeComponent();
        config = _config;
        BindingContext = config;

        SetDialogProperties();
    }

    private void SetDialogProperties()
    {
        double width = DeviceDisplay.MainDisplayInfo.Width / DeviceDisplay.MainDisplayInfo.Density;
        container.MaximumWidthRequest = width;
    }

    private void btnCancel_Clicked(object sender, EventArgs e)
    {
        if (config.OnAction != null)
            config.OnAction.Invoke(false);
        this.Close();
    }

    private void btnAccept_Clicked(object sender, EventArgs e)
    {
        if (config.OnAction != null)
            config.OnAction.Invoke(true);
        this.Close();
    }
}