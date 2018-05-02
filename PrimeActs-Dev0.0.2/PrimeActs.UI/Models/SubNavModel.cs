namespace PrimeActs.UI.Models
{
    public class SubNavModel
    {
        public SubNavModel(string controller)
        {
            LoadSubNav(controller);
        }

        public string ParentSubNav { get; set; }
        public string ChildSubNav { get; set; }
        public string GrandChildSubNav { get; set; }

        private void LoadSubNav(string controller)
        {
            switch (controller)
            {
                case "Home":
                    ParentSubNav = "SalesHome";
                    ChildSubNav = "subsubmenu";
                    GrandChildSubNav = "one";
                    break;
                case "Company":
                case "Division":
                case "Department":
                    ParentSubNav = "Setup";
                    ChildSubNav = "Five";
                    break;
                case "Consignment":
                    ParentSubNav = "Setup1";
                    ChildSubNav = "Six";
                    break;
                case "UsersAdmin":
                case "RolesAdmin":
                case "Permissions":
                    ParentSubNav = "UserManagement";
                    ChildSubNav = "Five";
                    break;

                default:
                    break;
            }
        }
    }
}