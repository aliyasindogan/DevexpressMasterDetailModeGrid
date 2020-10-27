using System.ComponentModel;

namespace DevexpressMasterDetailModeGrid.Entities
{
    public class UserSettingDto
    {
        public int Id { get; set; }
        public int CardID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public BindingList<UserSetting> UserSettings { get; set; }
    }
}