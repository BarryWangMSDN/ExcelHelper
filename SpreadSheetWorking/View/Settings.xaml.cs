using SpreadSheetWorking.Helper;
using SpreadSheetWorking.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
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
    public sealed partial class Settings : Page
    {
        MemberInfo mymember = new MemberInfo();
        ObservableCollection<MemberInfo> memcoll = new ObservableCollection<MemberInfo>();

        private static ObservableCollection<MemberInfo> mycollection;

        public static ObservableCollection<MemberInfo> MyMemCollection
        {
            get { return mycollection; }
            set { mycollection = value; }
        }

        public Settings()
        {
            this.InitializeComponent();
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Stream finalstream = await SpreadsheetHelper.filepathhelper();
                SpreadsheetHelper.ReadDataFromExcel(finalstream, "Sheet1", "A2", "F29", memcoll);
                mycollection = memcoll;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message.ToString());
            }
        }

        private async void Write_DB(object sender, RoutedEventArgs e)
        {
            LoadingControl.IsLoading = true;
          //  await SqlDBHelper.insertcollection(memcoll);
           await Task.Run(() => SqlDBHelper.insertcollection(memcoll));
            LoadingControl.IsLoading = false;
        }
    }
}
