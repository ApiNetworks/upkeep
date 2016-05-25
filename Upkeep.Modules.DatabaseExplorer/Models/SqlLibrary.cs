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
            SqlSnippets = new List<SqlSnippet>();
            LoadSnippets();
            LoadResources();
        }

        public List<SqlSnippet> SqlSnippets { get; set; }

        private void LoadSnippets()
        {
            SqlSnippets.Add(new SqlSnippet { Name = "Select Database Table Names ", Sql = "SELECT TABLE_NAME FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_TYPE = 'BASE TABLE' AND TABLE_CATALOG='RxDMDb' AND TABLE_NAME NOT LIKE '%MigrationHistory%'" });
        }

        private void LoadResources()
        {
            string[] snippetLine = Properties.Resources.sqlsnippets.Split(new string[] { "\n", "\r\n" }, StringSplitOptions.RemoveEmptyEntries);
            foreach (string snippet in snippetLine)
            {
                string[] splitLine = snippet.Split('\t');
                if (splitLine.Length == 2 && !String.IsNullOrEmpty(splitLine[0].ToString()) && !String.IsNullOrEmpty(splitLine[1].ToString()))
                {
                    var snip = new SqlSnippet { Name= splitLine[0].ToString().Trim('\"'), Sql= splitLine[1].ToString().Trim('\"') };
                    SqlSnippets.Add(snip);
                }
            }
        }
    }
}
