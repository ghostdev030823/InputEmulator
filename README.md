# InputEmulator

## Goal

The objective of this project is to emulate mouse and keyboard inputs for testing a desktop WPF application, while at the same time ensuring that it does not interfere with the physical mouse and keyboard connected to the PC.

## Context

The existing implementation unfortunately results in the primary mouse and keyboard of the tester being captured when run; that means that any use of the tester's mouse and keyboard will conflict with the automated mouse and keyboard events in the test, likely causing a test failure.

Naturally, having one's mouse and keyboard captured in this way is frustrating for the tester; in addition, it also reduces productivity as the tester is essentially not able to use his/her mouse and keyboard for any other work for the duration of the test, which could potentially be many minutes.

The challenge is to find an alternative approach to achieve the same task without affecting the operation of the PC's connected mouse and keyboard.

Solving this issue will enable the tester to be able to use his/her computer while the test is running; in addition, it will also enable the parallelization of these type of tests, as they currently have to be sequentially.

## Constraint
- The execution and testing must work on Windows 10 and above.

## Visual Studio (VS) solution structure

The Visual Studio (VS) solution contains two C# projects:
1. `InputEmulator.App`
    - This project consists of a basic WPF application containing one window and three buttons.
    - After window initialization, there is a method (commented out) called `TestClick()` that emulates mouse actions to sequentially click on the three buttons.
3. `InputEmulator.Test`
    - This is a test project with a single unit test that replicates the actions performed by the `TestClick()` method, but from an external process (the unit test runner process).

## Triggering the automated click sequence

To trigger the automated click sequence, there are two different options:
1. Uncomment the `TestClick()` method and launch the 'InputEmulator.App' application.
2. Leave the `TestClick()` method commented out and run the unit test.
  
In either scenario, you will observe the mouse automatically clicking on the three buttons in succession.

Below the buttons, there is a `TextBlock` that displays a message each time a button event is triggered.

At the end of the automated click sequence, you should see the following in the `TestBlock`:

```
Button 1 mouse enter
Button 1 is mouse directly over changed (was False, now is True)
Button 1 is mouse directly over changed (was True, now is False)
Button 1 clicked
Button 1 mouse leave
Button 2 mouse enter
Button 2 is mouse directly over changed (was False, now is True)
Button 2 is mouse directly over changed (was True, now is False)
Button 2 clicked
Button 2 mouse leave
Button 3 mouse enter
Button 3 is mouse directly over changed (was False, now is True)
Button 3 is mouse directly over changed (was True, now is False)
Button 3 clicked
```

# Definition of a successful challenge solution

The following criteria indicate whether or not a solution has successfully addressed the challenge of this project:
1. The button clicking sequence and `TextBlock` output **must remain unchanged**.
2. The primary cursor **must remain stationary during the automated clicking sequence**.
3. The solution **must operate at the input device level**.
4. The application **must continue to trigger mouse-related events such as `MouseEnter`, `MouseLeave`, etc. based on the emulated mouse actions.**
    * Any solution relying on triggering a WPF command or raising a WPF event **will not be accepted**.
