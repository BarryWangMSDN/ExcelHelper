﻿using Microsoft.Toolkit.Uwp.UI.Controls;
using SpreadSheetWorking.Model;
using SpreadSheetWorking.ViewModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace SpreadSheetWorking.View
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class DayOffListPage : Page
    {
        private DaysOffListViewModel mydaysofflistvm;

        public DaysOffListViewModel MyDasyOffListVM
        {
            get { return mydaysofflistvm; }
            set { mydaysofflistvm = value; }
        }

        public DayOffListPage()
        {
            this.InitializeComponent();          
            DaysOffListViewModel daysofflistvm = new DaysOffListViewModel();
            mydaysofflistvm = daysofflistvm;
        }

        private void Memdatagrid_RightTapped(object sender, RightTappedRoutedEventArgs e)
        {
            var x=(e.OriginalSource as FrameworkElement).DataContext;
            var identiy = (x as MemberInfo).Alias;
           
        }
    }
}
