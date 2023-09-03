using Microsoft.VisualStudio.TestTools.UnitTesting;
using Reddit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedditNew.ApplicationTests
{
    public abstract class BaseTests
    {

        private TestContext TestContextInstance;

        public TestContext TestContext
        {
            get
            {
                return TestContextInstance;
            }
            set
            {
                TestContextInstance = value;
            }
        }

        protected readonly Dictionary<string, string> testData;
        protected readonly RedditClient reddit;
        protected readonly RedditClient reddit2;
        protected readonly RedditClient reddit3;

        public BaseTests()
        {
            testData = GetData();

            // Primary test user's instance.  
            reddit = new RedditClient(testData["AppId"], testData["RefreshToken"], userAgent: "RedditNew.ApplicationTests");

            try
            {
                // Secondary test user's instance.  
                reddit2 = new RedditClient(testData["AppId"], testData["RefreshToken2"], userAgent: "RedditNew.ApplicationTests");
            }
            catch (Exception) { }

            // App-only instance.  
            reddit3 = new RedditClient(testData["AppId"], userAgent: "RedditNew.ApplicationTests");
        }

        public Dictionary<string, string> GetData()
        {
            // Begin .NET Core workaround.  
            string xmlData;
            using (Stream stream = System.Reflection.Assembly.GetExecutingAssembly().GetManifestResourceStream("Reddit.NETTests.Reddit.NETTestsData.xml"))
            {
                using (StreamReader streamReader = new StreamReader(stream))
                {
                    xmlData = streamReader.ReadToEnd();
                }
            }

            System.Xml.XmlDocument xmlDocument = new System.Xml.XmlDocument();
            xmlDocument.LoadXml(xmlData);

            Dictionary<string, string> res = new Dictionary<string, string>
            {
                { "AppId", xmlDocument.GetElementsByTagName("AppId")[0].InnerText },
                { "RefreshToken", xmlDocument.GetElementsByTagName("RefreshToken")[0].InnerText },
                { "RefreshToken2", xmlDocument.GetElementsByTagName("RefreshToken2")[0].InnerText },
                { "Subreddit", xmlDocument.GetElementsByTagName("Subreddit")[0].InnerText }
            };

            if (res["AppId"].Equals("Paste Reddit App ID here")
                || res["RefreshToken"].Equals("Paste Reddit Refresh Token here")
                || res["RefreshToken2"].Equals("Paste second account's Reddit Refresh Token here (required for WorkflowTests)")
                || res["Subreddit"].Equals("Paste test subreddit (new or existing with full mod privs) here"))
            {
                Assert.Inconclusive("You must replace all default values in Reddit.NETTestsData.xml before running the tests.");
            }

            return res;

        }
    }
}
