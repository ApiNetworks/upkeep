using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Upkeep.Modules.DatabaseExplorer.Models
{
    public class SqlLibrary
    {
        public SqlLibrary()
        {
            SqlSnippets = GetSnippets();
        }

        public List<SqlSnippet> SqlSnippets { get; set; }

        private List<SqlSnippet> GetSnippets()
        {
            List<SqlSnippet> snippets = new List<SqlSnippet>();
            snippets.Add(new SqlSnippet { Name = "Select top 10 doctors", Sql = "SELECT TOP 10 * FROM DOCTORS" });
            snippets.Add(new SqlSnippet { Name = "Select top 10 patients", Sql = "SELECT TOP 10 * FROM PATIENTS" });
            snippets.Add(new SqlSnippet { Name = "Select top 10 perscriptions", Sql = "SELECT TOP 10 * FROM PRESCRIPTIONS" });
            snippets.Add(new SqlSnippet { Name = "Select top 10 stores", Sql = "SELECT TOP 10 * FROM STORES" });
            return snippets;
        }
    }
}
