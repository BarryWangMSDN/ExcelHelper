using SpreadSheetWorking.Helper;
using SpreadSheetWorking.Model;
using SpreadSheetWorking.View;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Data;

namespace SpreadSheetWorking.ViewModel
{
  

    public class DaysOffListViewModel:INotifyPropertyChanged
    {
        private ObservableCollection<MemberInfo> mymemlist;

        public ObservableCollection<MemberInfo> MyMemberList
        {
            get { return mymemlist; }
            set { mymemlist = value;
                NotifyPropertyChanged("MyMemberList");
            }
        }

        private ObservableCollection<MemberInfo> basiclist;

        public ObservableCollection<MemberInfo> BasicList
        {
            get { return basiclist; }
            set { basiclist = value; }
        }


        private ObservableCollection<MemberInfo> templist;

        public ObservableCollection<MemberInfo> TempList
        {
            get { return templist; }
            set { templist = value; }
        }

        private List<MemberInfo> tempdataset;

        public event PropertyChangedEventHandler PropertyChanged;

        protected void NotifyPropertyChanged(String info)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(info));
            }
        }

        public List<MemberInfo> TempDataset
        {
            get { return tempdataset; }
            set { tempdataset = value; }
        }


        public DaysOffListViewModel()
        {          
            basiclist = SqlDBHelper.CommonMemList;
            mymemlist = new ObservableCollection<MemberInfo>();
            foreach (var item in basiclist)
            {
                mymemlist.Add(item);
            }         
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
                if(args.QueryText!=null)
                {
                    var query = from item in basiclist
                                where item.Alias.Contains(args.QueryText)
                                select item;
                    mymemlist.Clear();
                    foreach (var item in query)
                    {
                        mymemlist.Add(item);
                    }
                }
                else
                {
                    mymemlist.Clear();
                    mymemlist = basiclist;
                }
               
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
                             

            }
          
        }

    }
}
