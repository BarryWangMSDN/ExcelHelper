using SpreadSheetWorking.Helper;
using SpreadSheetWorking.Model;
using SpreadSheetWorking.View;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;

namespace SpreadSheetWorking.ViewModel
{
  

    public class DaysOffListViewModel
    {
        private ObservableCollection<MemberInfo> mymemlist;

        public ObservableCollection<MemberInfo> MyMemberList
        {
            get { return mymemlist; }
            set { mymemlist = value; }
        }


        public DaysOffListViewModel()
        {
            //mymemlist = Settings.MyMemCollection;
            mymemlist = SqlDBHelper.CommonMemList;
        }

        public void AutoSuggestBox_QuerySubmitted(AutoSuggestBox sender, AutoSuggestBoxQuerySubmittedEventArgs args)
        {
            if (args.ChosenSuggestion != null)
            {
                // User selected an item from the suggestion list, take an action on it here.
                // Meet the problem. To filter data a view of the collection is needed. 
                // So need to go back to binding and set collectionview source instead. Need some time. stop it right now.
               
            }
            else
            {
                // Use args.QueryText to determine what to do.
            }
        }


    }
}
