using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpreadSheetWorking.Model
{
    public class MemberInfo
    {
        private string username;

        public string UserName
        {
            get { return username; }
            set { username = value; }
        }

        private string alias;

        public string Alias
        {
            get { return alias; }
            set { alias = value; }
        }

        private string wsalias;

        public string WsAlias
        {
            get { return wsalias; }
            set { wsalias = value; }
        }

        private string technology;

        public string Technology
        {
            get { return technology; }
            set { technology = value; }
        }

        private string group;

        public string Group
        {
            get { return group; }
            set { group = value; }
        }

        private int vacationhour;

        public int VacationHour
        {
            get { return vacationhour; }
            set { vacationhour = value; }
        }

    }
}
