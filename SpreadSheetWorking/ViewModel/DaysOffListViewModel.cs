using SpreadSheetWorking.Helper;
using SpreadSheetWorking.Model;
using SpreadSheetWorking.View;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Data;

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

        private ObservableCollection<MemberInfo> basiclist;

        public ObservableCollection<MemberInfo> BasicList
        {
            get { return basiclist; }
            set { basiclist = value; }
        }

        private List<string> tempdataset;

        public List<string> TempDataset
        {
            get { return tempdataset; }
            set { tempdataset = value; }
        }


        public DaysOffListViewModel()
        {
            mymemlist = SqlDBHelper.CommonMemList;
            basiclist = mymemlist;
            tempdataset = new List<string>();
            foreach (var i in mymemlist)
            {
                tempdataset.Add(i.UserName);
            };
        }

        public void AutoSuggestBox_QuerySubmitted(AutoSuggestBox sender, AutoSuggestBoxQuerySubmittedEventArgs args)
        {
            if (args.ChosenSuggestion != null)
            {
                // User selected an item from the suggestion list, take an action on it here.
                // Meet the problem. 
                // Stop this: To filter data a view of the collection is needed. 
                // So need to go back to binding and set collectionview source instead. Need some time. stop it right now.
              
            }
            else
            {
                var querydata = args.QueryText;
                var result = mymemlist.Where(c => c.Alias == querydata);
                // Use args.QueryText to determine what to do.
            }
        }

        private void AutoSuggestBox_TextChanged(AutoSuggestBox sender, AutoSuggestBoxTextChangedEventArgs args)
        {
            // Only get results when it was a user typing,
            // otherwise assume the value got filled in by TextMemberPath
            // or the handler for SuggestionChosen.
            if (args.Reason == AutoSuggestionBoxTextChangeReason.UserInput)
            {
                //Set the ItemsSource to be your filtered dataset
                //sender.ItemsSource = mymemlist;                        
                sender.ItemsSource = tempdataset;               

            }
           
        }

    }
}
