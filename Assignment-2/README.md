# Test-Assignment-1

## Computer mouse
Here is a list of possible tests that could be performed when making a new mouse:
- Test the ergonomics of using the mouse, this can include the following tests:
    - How different hand sizes fit the shape of the mouse.
    - How it feels to use the mouse over a longer period.
    - How does the weight impact prolong used of the mouse.
- Test the actual hardware of the mouse like:
    - If the button or scroll wheel can withstand a certain number of clicks
    - If the mouse itself can withstand being dropped on the floor
    - If the mouse can withstand liquid, (if its part of the requirements)
    - If the mouse material can withstand exposure to the human skin. 
    - The durability of the mouse feet, how fast do they wear out
    - If the mouse is wireless how long is the delay
- Test of the software for the mouse, this includes drives or customization software.
    - Test that the mouse driver works on specific versions of Windows, Linux, and Mac (and maybe even things like an iPad or a game console)
    - Usual software testing on the customization software including unit tests and user tests

## Catastrophic failure


> At approximately midnight Pacific Standard Time, on December 31, 2008, Zune 30s froze. The problem was caused by a third-party driver written by Freescale for their MC13783 PMIC processor. The bug also froze up Toshiba Gigabeat S media players that shared the same Freescale device and driver. The exact cause was first discovered by itsnotabigtruck of Zune Boards. What itsnotabigtruck found out was that there was an if statement inside a while loop that was written in such a way that on that particular day, because it was a leap year and the day count was 366, the loop was destined to continuously self-execute until the 24 hours of that day were up. People who experienced the problem gave the event many names, among them Z2K9, Y2K9, Z2K, the Zune Screen of Death, Zunepocalypse, and the December 31st, 2008 Zune Meltdown.
>
> Microsoft posted a comment on the support front page stating the issue is because 2008 is a leap year, and a firmware clock driver used by the Zune 30 improperly handled the last day of a leap year, causing the player to freeze. The driver was for a part used only in the Zune 30 model, which was why the bug didn't affect other Zune models. The official fix was to drain the device battery and then recharge after midday GMT on January 1, 2009.
> 
> \- [Wikipedia](https://en.wikipedia.org/wiki/Zune_30#:~:text=Freescale%20Driver%20Issue)

This incident obviously happened because of a lack of basic testing. An obvious edge case to software that works with time/days is the occurrence of a leap year with an extra day. If a practice like TTD had been used this issue could likely have been prevented and thereby saving Microsoft potential millions in things like brand perception.

TDD and unit testing would of cause not have been a guaranteed way of preventing the issue from happening, but it would likely have minimized the possibility to such a degree that it wouldn’t happen.  

## JUnit 5
- **@Tag** - Tells JUnit to group test methods with the same tags, this makes it possible to only run certain groups of tests and exclude others. This can be used to exclude integration tests in a local dev environment and only run them when as part of a build pipeline.
- **@Disabled** - Tells JUnit to not run the test. Disabling test can be useful if we temporarily want to disable some test because a feature has been pushed to a later date.
- **@RepeatedTest** - Tells JUnit to run a test multiple times, this depends on the number specified e.g. @RepeartedTest(5) will make a test that runs 5 times. This is useful if we want to test that something behaves the same way even after multiple attempts.
- **@BeforeEach** - Tells JUnit to run the test before each test, this includes @Test, @RepeatedTest, @ParameterizedTest, & @TestFactory. This can be useful for initializing some tests objects used by multiple tests.
- **@AfterEach** - Same as @BeforeEach but runs after each test instead of before. Instead of being used to initialize, @AfterEach can be used on tear down methods that undo the changes made by a test.
- **@BeforeAll** - Tells JUnit to run the test one time before all other tests are run, this includes @Test, @RepeatedTest, @ParameterizedTest, & @TestFactory. @BeforeAll can be useful when making integration tests, if we need the application, we a integrating with to be in a specific state before all tests a run. @BeforeAll can be used for that.
- **@AfterAll** - Same as @BeforeAll but runs after all tests have completed instead of before. @AfterAll can be used to clean up the changes made by our tests or to close a db connection.
- **@DisplayName** - Set a display name of when the test reports it result back and is shown by the test runner and the IDEs. A display name makes it possible to write names with spaces and special characters like !@$ or emojis 🤯 
- **@Nested** - Is used when making test classed instead other test classed. This can be used to give the test a hierarchical output when displayed in an IDE. ![Picture of test results shown in a IDE](https://junit.org/junit5/docs/current/user-guide/images/writing-tests_nested_test_ide.png)s
- **assumeFalse() & assumeTrue()** - Are both part of JUnit 5's [Assumption class](https://junit.org/junit5/docs/5.0.0/api/org/junit/jupiter/api/Assumptions.html). Compared to normal assertions, assumptions do not result in a test failure. Instead, they result in the test being aborted. This can be useful in instances were continuing the execution of a test doesn’t make sense if a condition isn’t true or false. E.g., if the network is down, it wouldn’t make sense to continue an integration test because it would fail regardless of the actual test.

## Mocking framework comparison

My preferred programming language is C# I have therefore chosen to compare [FakeItEasy](https://fakeiteasy.github.io/) and [Moq](https://www.moqthis.com/moq4/). Moq is the most popular mocking framework used through the C# world. FakeItEasy is a lesser-known mocking framework that I have been using for a couple of years.

### Similarities

FakeItEasy is based on the fluent syntax from Moq which makes the very similar in nature. See the example below of how to create a mock, mock its behavior, use it as a reference, and check if the mock has been called.

FakeItEasy:
```C#
IBankAccount bankAccount = A.Fake<IBankAccount>();

A
    .CallTo(() => bankAccount.Withdraw(A<double>.Ignored))
    .Returns(10);

A
    .CallTo(() => bankAccount.AccountHolder)
    .Returns("John Doe");

Bank bank = new Bank();
bank.AddAccount(bankAccount);

A
    .CallTo(() => bankAccount.Withdraw(A<double>.Ignored))
    .MustHaveHappenedOnceOrMore();
```
Moq:
```C#
Mock<IBankAccount> bankAccount = new Mock<IBankAccount>();

bankAccount
    .Setup(account => account.Withdraw(It.IsAny<double>()))
    .Returns(10);

bankAccount
    .Setup(account => account.AccountHolder)
    .Returns("John Doe");

Bank bank = new Bank();
bank.AddAccount(bankAccount.Object);

bankAccount
    .Verify(account => 
        account.Withdraw(It.IsAny<double>()), 
        Moq.Times.AtLeastOnce()
    );
```

Their syntax are very similar as the code examples show. However, there are some differences between the two frameworks that I will elaborate a little on. 

### Differences

The main difference is that Moq uses its `Mock` type as a base for creating the mocks functionality. This isn’t an issue when initializing the mocks, but when the actual object are needed `.Object` has to be called to get the true object. FakeItEasy handles this by treating all objects as mocks, and then at runtime figures out if its an actual mock object. This obviously makes FakeItEasy less type safe than Moq.

### Which one do I prefer & why?

The two frameworks are so similar that I don’t have any strong preferences towards the one or the other. I would however choose FakeItEasy over Moq because of its syntax. This is mostly because of the way FakeItEasy handles its natural mock objects compared to Moq's `Mock` objects. I find it more natural when writing, and reading, my tests to have the actual types compared to calling `.Object` every time I have to use the object.