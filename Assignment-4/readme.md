# Test-Assignment-4

## Mockito powerups

How do ou verify that a mock was called?
```java
verify(mockedObject).methodOnObject();
```

How do you verify that a mock was NOT called?
```java
verify(mockedObject, never()).methodOnObject();
```

How do you specify how many times a mock should have been called?
```java
//exact number of invocations verification
verify(mockedObject, times(2)).methodOnObject();

//verification using atLeast()/atMost()
verify(mockedObject, atMostOnce()).methodOnObject();
verify(mockedObject, atLeastOnce()).methodOnObject();
verify(mockedObject, atLeast(2)).methodOnObject();
verify(mockedObject, atMost(5)).methodOnObject();
```

How do you verify that a mock was called with specific arguments?
```java
verify(mockedObject).methodOnObject("argument");
```

How do you use a predicate to verify the properties of the arguments given to a call to the mock?
```java
verify(mockedObject).methodOnObject(argThat(someString -> someString.length() > 5));
```

## Test Reports

- [Coverage report](https://github.com/cph-sn311/Test-Assignments/blob/main/Assignment-4/Report/TestReport.html)
- [Mutation Testing Report](https://github.com/cph-sn311/Test-Assignments/blob/main/Assignment-4/Report/mutation-report.html)