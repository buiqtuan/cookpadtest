using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft;
using Newtonsoft.Json;

namespace cookpad
{
    public class User
    {
        public int id { get; set; }
        public string name { get; set; }
        public int[] friends { get; set; }
    }

    public class FriendRetriever
    {
        //used to check if this friend is alredy on the list
        private HashSet<int> _userCheck = new HashSet<int>();

        private async Task<T> GetApi<T>(string url)
        {
            using (var client = new HttpClient())
            {
                var result = await client.GetStringAsync(url);
                return JsonConvert.DeserializeObject<T>(result);
            }
        }

        private async Task<User> GetUser(int id)
        {
            try
            {
                var url = "http://fg-69c8cbcd.herokuapp.com/user/" + id;
                var user = await GetApi<User>(url);
                return user;
            }
            catch
            {
                return null;
            }
        }

        private async Task<IEnumerable<User>> GetFriendsNamesTask(int id)
        {
            //add comment
            List<User> users = new List<User>();

            var firstUser = await GetUser(id);
            if (firstUser == null) return users;

            var firstFriends = new User[firstUser.friends.Length];

            foreach (var friendId in firstUser.friends)
            {
                var f = await GetUser(friendId);
                if (f == null || _userCheck.Contains(f.id)) continue;
                users.Add(f);
                _userCheck.Add(f.id);
                foreach (var fId2 in f.friends)
                {
                    var f2 = await GetUser(fId2);
                    if (f2 == null || _userCheck.Contains(f2.id)) continue;
                    users.Add(f2);
                    _userCheck.Add(f2.id);
                }

            }

            return users;
        }

        public IEnumerable<User> GetFriendsNames(int id)
        {
            var task = GetFriendsNamesTask(id);
            task.Wait();
            return task.Result;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            int id = Convert.ToInt32(args[0]);
            var service = new FriendRetriever();
            var result = service.GetFriendsNames(id);
            foreach (var user in result)
            {
                Console.WriteLine(user.name);
            }
        }
    }
}
