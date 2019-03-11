using DocumentFormat.OpenXml.Packaging;
using SpreadSheetWorking.Model;
using SpreadSheetWorking.View;
using SpreadSheetWorking.ViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;


// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace SpreadSheetWorking
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        private MainViewModel mymainvm;

        public MainViewModel MyMainVM
        {
            get { return mymainvm; }
            set { mymainvm = value; }
        }

        public MainPage()
        {
            this.InitializeComponent();
            MainViewModel mainvm = new MainViewModel();
            mymainvm = mainvm;        
            
        }

        private void NavView_Loaded(object sender, RoutedEventArgs e)
        {
            // NavView doesn't load any page by default, so load home page.
            NavView.SelectedItem = NavView.MenuItems[0];
            ContentFrame.Navigate(typeof(DayOffListPage));
        }

        private void NavView_ItemInvoked(NavigationView sender, NavigationViewItemInvokedEventArgs args)
        {
            if (args.IsSettingsInvoked == true)
            {
                ContentFrame.Navigate(typeof(Settings), args.RecommendedNavigationTransitionInfo);
            }
            else if (args.InvokedItemContainer != null)
            {
                var navItemTag = args.InvokedItemContainer.Tag.ToString();
                if(navItemTag=="Default")
                {
                    ContentFrame.Navigate(typeof(DayOffListPage), args.RecommendedNavigationTransitionInfo);
                }
            }

        }



        //private async void Button_Click(object sender, RoutedEventArgs e)
        //{

        //    MemberInfo mymember = new MemberInfo();
        //    ObservableCollection<MemberInfo> memcoll = new ObservableCollection<MemberInfo>();

        //    try
        //    {
        //        Stream finalstream = await SpreadsheetHelper.filepathhelper();
        //        SpreadsheetHelper.ReadDataFromExcel(finalstream, "Sheet1", "A2", "F29", memcoll);
        //    }
        //    catch (Exception ex)
        //    {
        //        Debug.WriteLine(ex.Message.ToString());
        //    }
        //}
    }
}
