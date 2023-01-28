using Core.Selenium.Helpers;
using OpenQA.Selenium;
using SeleniumExtras.WaitHelpers;

var driver = SeleniumHelpers.GetDriverInstance("chrome");
string textoBuscar = "¡Hola!";
var element = driver.FindElement(By.XPath($"//*[contains(text(),'{textoBuscar}')]"));



var innerText = element.GetInnerText();

Console.ReadLine();