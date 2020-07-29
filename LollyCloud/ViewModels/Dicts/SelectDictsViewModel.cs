﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LollyCloud
{
    public class SelectDictsViewModel
    {
        SettingsViewModel vmSettings;
        public ObservableCollection<MDictionary> DictsAvailable { get; }
        public ObservableCollection<MDictionary> DictsSelected { get; }
        public SelectDictsViewModel(SettingsViewModel vmSettings)
        {
            this.vmSettings = vmSettings;
            DictsSelected = new ObservableCollection<MDictionary>(vmSettings.SelectedDictsReference);
            DictsAvailable = new ObservableCollection<MDictionary>(vmSettings.DictsReference.Except(vmSettings.SelectedDictsReference));
        }
        public async void OnOK() =>
            await vmSettings.UpdateDictsReference(DictsSelected.ToList());
    }
}