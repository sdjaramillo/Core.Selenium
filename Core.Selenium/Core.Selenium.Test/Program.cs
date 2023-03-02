using Core.Selenium.Helpers;
using Core.Selenium.Logic;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using SeleniumExtras.WaitHelpers;


List<int> list1 = new List<int> { 1, 1, 2, 3, 4 };
List<int> list2 = new List<int> { 3, 4, 5, 6 };
List<int> firstNotSecond = list1.Except(list2).ToList();
var secondNotFirst = list2.Except(list1).ToList();
Console.WriteLine("Present in List1 But not in List2");
foreach (var x in firstNotSecond)
{
    Console.WriteLine(x);
}
Console.WriteLine("Present in List2 But not in List1");
foreach (var y in secondNotFirst)
{
    Console.WriteLine(y);
}

Console.ReadLine();