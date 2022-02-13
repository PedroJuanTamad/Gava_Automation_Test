using Generic_Test_Framework;
using SeleniumExtras.PageObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gava_Automation_Test_Framework
{
    public class Pages
    {
        public static HomePage HomePage
        {
            get
            {
                var homePage = new HomePage();
                PageFactory.InitElements(Browser.Driver, homePage);
                return homePage;
            }
        }

        public static CampaignPage CampaignPage
        {
            get
            {
                var campaignPage = new CampaignPage();
                PageFactory.InitElements(Browser.Driver, campaignPage);
                return campaignPage;
            }
        }

        public static GiveNowPage GiveNowPage
        {
            get
            {
                var giveNowPage = new GiveNowPage();
                PageFactory.InitElements(Browser.Driver, giveNowPage);
                return giveNowPage;
            }
        }

        public static CheckoutPage CheckoutPage
        {
            get
            {
                var checkoutPage = new CheckoutPage();
                PageFactory.InitElements(Browser.Driver, checkoutPage);
                return checkoutPage;
            }
        }

        public static AppCoinsPage AppCoinsPage
        {
            get
            {
                var appCoinsPage = new AppCoinsPage();
                PageFactory.InitElements(Browser.Driver, appCoinsPage);
                return appCoinsPage;
            }
        }
    }
}
