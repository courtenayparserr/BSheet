using RestSharp;
using RestSharp.Authenticators;
using RestSharp.Serializers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Saastrack.DAL.Helper
{
    public class CustomerIo
    {
        private readonly string _siteId;
        private readonly string _apiKey;
        private const string Endpoint = "https://track.customer.io/api/v1/";
        private const string MethodCustomer = "customers/{customer_id}";
        private const string MethodTrack = "customers/{customer_id}/events";
        private readonly RestClient _client;

        public CustomerIo(string siteId, string apiKey)
        {
            _siteId = siteId;
            _apiKey = apiKey;
            _client = new RestClient(Endpoint)
            {
                Authenticator = new HttpBasicAuthenticator(_siteId, _apiKey)
            };
        }
        public bool RemoveUser(string databaseName, int id)
        {
            // do not transmit events if we do not have a customer id
            if (string.IsNullOrEmpty(databaseName)) return false;
            var request = new RestRequest(MethodCustomer)
            {
                Method = Method.DELETE,
                RequestFormat = DataFormat.Json,
                JsonSerializer = new JsonSerializer()
            };
            request.AddUrlSegment(@"customer_id", databaseName + "-" + id);            
            var response = _client.Execute(request);
            if (response.StatusCode != HttpStatusCode.OK)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        public bool UpdateUserPlan(string databaseName, int id, string email, string plan)
        {
            // do not transmit events if we do not have a customer id
            if (string.IsNullOrEmpty(databaseName)) return false;
            var request = new RestRequest(MethodCustomer)
            {
                Method = Method.PUT,
                RequestFormat = DataFormat.Json,
                JsonSerializer = new JsonSerializer()
            };
            request.AddUrlSegment(@"customer_id", databaseName + "-" + id);
            request.AddParameter("email", email);
            request.AddParameter("plan", plan);
            var response = _client.Execute(request);
            if (response.StatusCode != HttpStatusCode.OK)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        public bool AddUser(string databaseName, string confirmUrl, int id, string email, bool overwriteCreatedBy)
        {
            // do not transmit events if we do not have a customer id
            if (string.IsNullOrEmpty(databaseName)) return false;
            var request = new RestRequest(MethodCustomer)
            {
                Method = Method.PUT,
                RequestFormat = DataFormat.Json,
                JsonSerializer = new JsonSerializer()
            };
            request.AddUrlSegment(@"customer_id", databaseName + "-" + id);
            request.AddParameter("confirmUrl", confirmUrl);
            request.AddParameter("id", id);
            request.AddParameter("email", email);
            if (overwriteCreatedBy)
                request.AddParameter("created_at", GetEpochTime().ToString());
            var response = _client.Execute(request);
            if (response.StatusCode != HttpStatusCode.OK)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        public bool AddEmployeeUser(string databaseName, int id, string email, bool overwriteCreatedBy)
        {
            // do not transmit events if we do not have a customer id
            if (string.IsNullOrEmpty(databaseName)) return false;
            var request = new RestRequest(MethodCustomer)
            {
                Method = Method.PUT,
                RequestFormat = DataFormat.Json,
                JsonSerializer = new JsonSerializer()
            };
            request.AddUrlSegment(@"customer_id", databaseName + "-" + id);
            request.AddParameter("id", id);
            request.AddParameter("email", email);
            request.AddParameter("userType", "employee");
            if (overwriteCreatedBy)
                request.AddParameter("created_at", GetEpochTime().ToString());
            var response = _client.Execute(request);
            if (response.StatusCode != HttpStatusCode.OK)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        public string AddPublicUser(string firstName, string lastName, string email)
        {
            string userId = Guid.NewGuid().ToString();
            var request = new RestRequest(MethodCustomer)
            {
                Method = Method.PUT,
                RequestFormat = DataFormat.Json,
                JsonSerializer = new JsonSerializer()
            };

            request.AddUrlSegment(@"customer_id", userId);
            request.AddParameter("id", userId);
            request.AddParameter("email", email);
            request.AddParameter("created_at", GetEpochTime().ToString());
            request.AddParameter("first_name", firstName);
            request.AddParameter("last_name", lastName);
            var response = _client.Execute(request);
            return userId;
        }

        public bool AddTrackActionForPublicUser(string userid, string action, object data)
        {
            string userId = Guid.NewGuid().ToString();
            var wrappedData = new TrackedEvent
            {
                name = action,
                data = data,
                timestamp = DateTime.Now
            };

            var request = new RestRequest(MethodTrack)
            {
                Method = Method.POST,
                RequestFormat = DataFormat.Json,
                JsonSerializer = new JsonSerializer()
            };
            request.AddUrlSegment(@"customer_id", userid);
            request.AddBody(wrappedData);
            var response = _client.Execute(request);
            if (response.StatusCode != HttpStatusCode.OK)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public bool AddTrackAction(int id, string databaseName, string action, object stuffData)
        {
            var wrappedData = new TrackedEvent
            {
                name = action,
                data = stuffData,
                timestamp = DateTime.Now
            };

            // do not transmit events if we do not have a customer id
            if (string.IsNullOrEmpty(databaseName)) return false;
            var request = new RestRequest(MethodTrack)
            {
                Method = Method.POST,
                RequestFormat = DataFormat.Json,
                JsonSerializer = new JsonSerializer()
            };
            request.AddUrlSegment(@"customer_id", databaseName + "-" + id);
            request.AddBody(wrappedData);
            var response = _client.Execute(request);
            if (response.StatusCode != HttpStatusCode.OK)
            {
                return false;
                //throw new CustomerIoApiException(response.StatusCode);
            }
            else
            {
                return true;
            }

        }
        private int GetEpochTime()
        {
            TimeSpan t = DateTime.UtcNow - new DateTime(1970, 1, 1);
            return (int)t.TotalSeconds;
        }
    }

    [SerializeAs]
    public class TrackedEvent
    {
        public string name { get; set; }
        public object data { get; set; }
        public DateTime? timestamp { get; set; }
    }
}
