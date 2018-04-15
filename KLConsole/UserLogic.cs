using System;

namespace KLConsole
{
    class UserLogic
    {
        private UserInfo user;
        private ServiceReferenceUser.UsersClient db_user;
        private ServiceReferenceSystem.SystemClient db_sys;
        private ServiceReferenceActivity.ActivityClient db_act;
        public DateTime start { get; set; }
        public UserLogic(UserInfo user)
        {
            //inject user
            this.user = user;
            db_sys = new ServiceReferenceSystem.SystemClient();
            db_user = new ServiceReferenceUser.UsersClient();
            db_act = new ServiceReferenceActivity.ActivityClient();
            //user = GetInfo();

            Initialization();
        }

        /// <summary>
        /// Init UserLogic
        /// </summary>
        private void Initialization()
        {
            db_user.AddNewUser(user.MAC);
            db_user.AddUserAdress(user.IP, user.UserName, user.ID);
            db_sys.AddSysInfo(user.OS, user.PCName, user.ProcInfo + " Core " + user.ProcCount, user.ID);
        }
        /// <summary>
        /// Get user ID 
        /// </summary>
        /// <returns></returns>
        internal int GetUserID()
        {
            return user.ID;
        }

        /// <summary>
        /// Set user status
        /// </summary>
        /// <param name="status"></param>
        internal void SetStatus(string status)
        {
            db_act.UpdateuserStatus(status, user.ID);
        }

        /// <summary>
        /// Start session
        /// </summary>
        internal void StartSession()
        {
            start = DateTime.Now.ToLocalTime();//use one server time
            db_act.SessionDurationStart(user.ID, start);
        }

        /// <summary>
        /// End session
        /// </summary>
        internal void EndSession()
        {
            db_act.SessionDurationStop(start, user.ID);
        }

        public void PostInfoToWeb(UserInfo userInfo)
        {

        }
    }
}
