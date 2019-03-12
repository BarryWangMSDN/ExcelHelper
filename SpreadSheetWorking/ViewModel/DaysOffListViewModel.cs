using Microsoft.Toolkit.Uwp.UI.Controls;
using SpreadSheetWorking.Helper;
using SpreadSheetWorking.Model;
using SpreadSheetWorking.View;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Data;

namespace SpreadSheetWorking.ViewModel
{
  

    public class DaysOffListViewModel:INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged = delegate { };

        public void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            // Raise the PropertyChanged event, passing the name of the property whose value has changed.
            this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        private ObservableCollection<MemberInfo> mymemlist;

        public ObservableCollection<MemberInfo> MyMemberList
        {
            get { return mymemlist; }
            set { mymemlist = value;
                OnPropertyChanged();
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

        private string requesthour;

        public string RequestHour
        {
            get { return requesthour; }
            set
            {
                requesthour = value;
                this.OnPropertyChanged();
            }
        }

        //private string hoursleft;

        //public string HoursLeft
        //{
        //    get { return hoursleft; }
        //    set { hoursleft = value;
        //        this.OnPropertyChanged();
        //    }
        //}

        private int currentindex;

        public int CurrentIndex
        {
            get { return currentindex; }
            set { currentindex = value; }
        }



        private List<MemberInfo> tempdataset;

        

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
            requesthour = "0";
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

        public void AutoSuggestBox_TextChanged(AutoSuggestBox sender, AutoSuggestBoxTextChangedEventArgs args)
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

        public void Memdatagrid_SelectionChanged(object sender, SelectionChangedEventArgs args)
        {
            currentindex = (sender as DataGrid).SelectedIndex;
        }

        public async void RequestLeft_Click(object sender, RoutedEventArgs e)
        {
            bool WhetherInt=int.TryParse(requesthour, out int result);
            if (requesthour == "0" || requesthour == null|| WhetherInt==false)
            {
                MessageDialog popup = new MessageDialog("Please input a valid request hour");
                await popup.ShowAsync();
            }
            else if(currentindex<0)
            {
                MessageDialog popup = new MessageDialog("Please select a member that you want to request leave");
                await popup.ShowAsync();
            }
            else
            {
                SqlDBHelper.UpdateItemFromDB(mymemlist[currentindex].Alias, result);
                SqlDBHelper.UpdateItemFromCollection(currentindex, result, mymemlist);
            }
           
        }


        public void Groupselection_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string selectedteam = (sender as ComboBox).SelectedItem.ToString();         
                var teamquery = from item in basiclist
                                where item.Group == selectedteam
                                select item;
                mymemlist.Clear();
                foreach (var item in teamquery)
                {
                    mymemlist.Add(item);
                }
            
        }
    }
}
