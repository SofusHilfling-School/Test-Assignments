# Test-Assignment-1

## Computer mouse
Here is a list of possible tests that could be performed when making a new mouse:
- Test the ergonomics of using the mouse, this can include the following tests:
    - How diffrent hand sizes fit the shape of the mouse.
    - How it feels to use the mouse over a longer period of time.
    - How does the weight impact prolong used of the mouse.
- Test the actual hardware of the mouse like:
    - If the button or scoll wheel can withstand a certain number of clicks
    - If the mouse itself can withstand being droped on the floor
    - If the mouse can withstand liquied, (if its part of the requirements)
    - If the mouse matiral can withstand exposure to the human skin. 
    - The durability of the mouse feet, how fast do they wareout
    - If the mouse is wireless how long is the delay
- Test of the software for the mouse, this inclues drives or costimization software.
    - Test that the mouse driver works on spcific versions of Windows, Linux, and Mac (maybe even things like ipads or game consoles)
    - Usual software testing on the costimzation software including unit tests and user tests

## Catastrophic failure


## JUnit 5
- **@Tag** - Tells JUnit to group test methods with the same tags, this makes it possible to only run certen groups of test and exclude others. This can be used to exclude integration tests in a local dev enviornment and only run them when as part of a build pipeline.
- **@Disabled** - Tells JUnit to not run the test. Disablining test can be useful if we temporaryly want to disable some test because a feature has been pushed to a later date.
- **@RepeatedTest** - Tells JUnit to run a test multiple times, this depends on the number specified e.g. @RepeartedTest(5) will make a test that runs 5 times. This is useful if we want to test that something behaves the same way even after multiple attempts.
- **@BeforeEach** - Tells JUnit to run the test before each test, this inclueds @Test, @RepeatedTest, @ParameterizedTest, & @TestFactory. This can be useful for initalising some tests objects used by multiple tests.
- **@AfterEach** - Same as @BeforeEach but runs after each test instead of before. Instead of being used to inialize, @AfterEach can be used on tear down methods that undo the changes made by a test.
- **@BeforeAll** - Tells JUnit to run the test one time before all other test are run, this includes @Test, @RepeatedTest, @ParameterizedTest, & @TestFactory. @BeforeAll can be useful when making integration tests, if we need the application we a integrating with to be in a spcific state before all tests a run. @BeforeAll can be used for that.
- **@AfterAll** - Same as @BeforeAll but runs after all test have completed instead of before. @AfterAll can be used to clean up the changes made by our tests or to close a db connection.
- **@DisplayName** - Set a display name of when the test reports it result back and is shown by the test runner and the IDEs. A display name makes it possible to write names with spaces and special charecters like !@$ or emojis 🤯 
- **@Nested** - Is used when making test classed instead other test classed. This can be used to give the test a hierarchical output when displayed in a IDE. ![Picture of test results shown in a IDE](https://junit.org/junit5/docs/current/user-guide/images/writing-tests_nested_test_ide.png)s
- **assumeFalse() & assumeTrue()** - Are both part of JUnit 5's [Assumption class](https://junit.org/junit5/docs/5.0.0/api/org/junit/jupiter/api/Assumptions.html). Compared to normal assertions, assumptions do not result in a test failure. Instead they results in the test being aborted. This can be useful in instances where continuing the execution of a test dosnt make sence if a condition isnt true or false. E.g. if the network is down it wouldnt make sense to contiue an integration test because it would fail regardless of the actual test.

## Mocking framework comparison

My preferred programming language is C# I have theirfore chosen to compare [FakeItEasy](https://fakeiteasy.github.io/) and [Moq](https://www.moqthis.com/moq4/). Moq is the most popular mocking framework used throught the C# world. FakeItEasy is a lesser known mocking framework that I have been using for a couple of years.

### Similarities

FakeItEasy is based on the fluent syntax from Moq which makes the very similar in nature. See the example below of how to create a mock, mock its behavior, use it as a reference, and check if the mock has been called.

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

Their syntax are very simalar as the code examples show. However, there are some diffrences between the two frameworks that I will elaborate a little on. 

### Differenceses

The main diffrence is that Moq uses its `Mock` type as a base for creating the mocks functionallity. This isnt an issue when initializing the mocks, but when the actual object are needed `.Object` has to be called to get the true object. FakeItEasy handles this by treating all objects as mocks, and then at runtime figures out if its an actual mock object. This obviously makes FakeItEasy less type safe than Moq.

### Which one do I prefer & why?

The two frameworks are so similar that I dont have any strong preferences towards the one or the other. I would however choose FakeItEasy over Moq because of its syntax. This is mostly because of the way FakeItEasy handles its natrual mock objects compared to Moq's `Mock` objects. I find it more natural when writing, and reading, my tests to have the actual types compared to calling `.Object` everytime I have to use the object.

