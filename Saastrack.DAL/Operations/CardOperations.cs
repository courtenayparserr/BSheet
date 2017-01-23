using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Saastrack.DAL.Operations
{
    public class CardOperations: BaseOperations
    {
        public static string GetAccessTokenForUser(string databaseName)
        {
            using (Beam db = new Beam(connectionStringWithUser(databaseName)))
            {
                if (db.Cards.Count() > 0)
                {
                    return db.Cards.First().AccessToken;
                }
                else
                {
                    return string.Empty;
                }
                
            }
        }

        public static Card InsertInitialCardData(string databaseName, string accessToken, string publicToken)
        {
            Card comp = new Card();
            using (Beam db = new Beam(connectionStringWithUser(databaseName)))
            {
                if (!db.Cards.Any(e => e.AccessToken == accessToken && e.PublicToken == publicToken))
                {
                    comp.accounts = new List<Account>();
                    comp.AccessToken = accessToken;
                    comp.PublicToken = publicToken;
                    comp = db.Cards.Add(comp);
                    db.SaveChanges();
                }
                else
                {
                    return db.Cards.First(e => e.AccessToken == accessToken && e.PublicToken == publicToken);                    
                }
            }
            return comp;
        }
        
    }
}
