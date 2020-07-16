
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Android;
using OpenQA.Selenium.Remote;
using System;
using Applitools;
using Applitools.Selenium;
using OpenQA.Selenium.Appium.Enums;
using UnityEngine;


public class TestThings
{
    /*
     * NOTES:
     * eyes is a third party application which facilitates finding things on a page
     *
     * For the below to work, you need to start Appium Desktop and start the server
     *
     * Unity built apps do not have regular UI so you cannot find elements in the regular Appium way
     * You need to have a server exist in the application which opens a socket and leaves it open.
     * the server will serve information about the gameobjects and game state when a client (which
     * is embedded in your test) connects to it and queries it
     */
    private AppiumDriver<IWebElement> driver;
    //private Eyes eyes;
    
    [Test]
    public void MyTest()
    {
        //eyes = new Eyes();
        AppiumOptions options = new AppiumOptions();
        options.AddAdditionalCapability(MobileCapabilityType.DeviceName, "Pixel 4");
        options.AddAdditionalCapability(MobileCapabilityType.PlatformVersion, "10.0");
        options.AddAdditionalCapability(MobileCapabilityType.PlatformName, "Android");
        options.AddAdditionalCapability(MobileCapabilityType.App, "/Users/jasons/Desktop/unitylabs-mars1-default-android-248.apk");
        //options.AddAdditionalCapability("appActivity", "com.Unity.MARS Companion/com.unity3d.player.UnityPlayerNativeActivity");
        options.AddAdditionalCapability("deviceOrientation", "portrait");
        //options.AddAdditionalCapability("appPackage", "com.unity.mars_companion");
        //options.AddAdditionalCapability("appWaitActivity", "com.unity3d.player.*");
        options.AddAdditionalCapability(AndroidMobileCapabilityType.AutoGrantPermissions, "true"); //valid from Android 6.0
        
        IWebDriver driver = new RemoteWebDriver(new Uri("http://localhost:4723/wd/hub"), options);
        driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(60);
        
        var linkAccountButton = driver.FindElement(By.LinkText("Link Account"));
        linkAccountButton.Click();

        var asdf = "";
        // Start visual UI testing.
        //eyes.Open(driver, "MARS Companion", "My first Appium native C# test!");


        // Visual UI testing.
        //eyes.CheckWindow("Contact list!");
    }
}
