using SpreadSheetWorking.Helper;
using SpreadSheetWorking.Model;
using SpreadSheetWorking.View;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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


    }
}
