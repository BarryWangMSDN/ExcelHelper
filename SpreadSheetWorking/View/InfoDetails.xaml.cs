using SpreadSheetWorking.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using SpreadSheetWorking.Helper;
using SpreadSheetWorking.ViewModel;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace SpreadSheetWorking
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class InfoDetails : Page
    {
        public InfoDetails()
        {
            this.InitializeComponent();
            DaysOffListViewModel daysofflistvm = new DaysOffListViewModel();
            mydaysofflistvm = daysofflistvm;
        }
        private DaysOffListViewModel mydaysofflistvm;

        public DaysOffListViewModel MyDasyOffListVM
        {
            get { return mydaysofflistvm; }
            set { mydaysofflistvm = value; }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Frame root = Window.Current.Content as Frame;
            root.Navigate(typeof(View.video));
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {

        }
    }
}
