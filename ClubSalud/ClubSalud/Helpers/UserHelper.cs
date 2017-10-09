using System;
using ClubSalud.Models.ClubSalud;
using Realms;
using System.Linq;

namespace ClubSalud.Helpers
{
    public class UserHelper
    {
        public static void SaveUserInfo(User user)
        {
            /**
            try
            {
                var realm = App.CurrentApp.RealmInstance;

                realm.Write(() =>
                {
                    var users = realm.All<User>().ToList();
                    if (users.Count == 0)
                    {
                        realm.Write(() =>
                        {
                            //var newUser = new User();
                            //newUser = user;

                            //realm.Add(newUser);
                        });
                    }
                    else
                    {
                        realm.Write(() =>
                        {
                            //var actualUser = users.Last();
                            //actualUser = user;
                        });

                    }
                });
                
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }
        **/
            App.CurrentUser = user;
        }

        public static User CurrentUser()
        {
            /**
            try
            {
                var realm = Realm.GetInstance(Realms.RealmConfiguration.DefaultConfiguration);

                var user = realm.All<User>().FirstOrDefault();

                return user;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);

                return null;
            }
    **/
            return App.CurrentUser;
        }

        public static void Logout()
        {
            /**
            try
            {
                var realm = Realm.GetInstance(Realms.RealmConfiguration.DefaultConfiguration);

                realm.RemoveAll<User>();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }
    **/
            App.CurrentUser = null;
        }
    }
}
