# Grading PA4

Name: Sandu

Grade: **1**

Points: **18.25/20**

## Clean Coding (3.75/4)

- [0.5 / 0.5] No Compiler Warnings (Max 3) [Found 0]
- [0.5 / 0.5] Clean Code & Structure (Namespaces, one class per file, Format, etc.) (Max 1 Fail) [Found 0]
- [0.25 / 0.5] No Code Smells / Code Quality Issues (Max 1 Fail) [Found ~1]
- [0.5 / 0.5] Clean XAML (Max 1 Fail) [Found 0]
- [0.5 / 0.5] Minimal Code Behind Usage
- [0.5 / 0.5] Dependency Injection Setup and Usage (App CodeBehind, Services and ViewModels configured)
- [1.0 / 1.0] MVVM Usage Issues (Missing OnPropertyChanged, Wrong Binding, Only simple Models)

## 1. UI Design XAML (1.75/2)

- [0.5 / 0.5] MainWindow designed/created & ActivityWindow designed/created
- [0.5 / 0.5] Use appropriate Panels and Controls (Grid, DataGrid, ComboBox)
- [0.25 / 0.5] Window resize / scaling behavior (incl. MinHeight, MinWidth)
- [0.5 / 0.5] Appropriate usage of Styles (TextBlocks, Header, ...)

## 2. Start Activity (3/3)

- [0.5 / 0.5] User can select Sport Type
- [0.5 / 0.5] MainViewModel StartStop Command implementation & Binding
- [0.5 / 0.5] User can Start Tracking
- [0.5 / 0.5] Activity window created and shown (non modal)
- [0.5 / 0.5] Tracking hint shown
- [0.5 / 0.5] Button text is changed Start / Stop

## 3. Activity Window (2.25/3)

- [0.5 / 0.5] Activity data handover (e.g. LoadActivity)
- [0.5 / 0.5] ViewModel implementation and Binding usage
- [0.25 / 0.5] Updates duration (timer tick, difference calculation, TimeSpan)
- [0.5 / 0.5] Updates burned calories (with injected service)
- [0.5 / 0.5] Activity window can not be closed directly
- [0 / 0.5] Output Data Formats (DateTime dd.MM.yyyy hh:mm, TimeSpan hh:mm:ss, Energy 2 digits)

## 4. Stop Activity (2/2)

- [0.5 / 0.5] ActivityWindow gets closed (clean closing logic flag logic)
- [0.5 / 0.5] Activity is saved to (in-memory) list (with Updated burned calories and duration)
- [0.5 / 0.5] Button Start / Stop is switched to Start
- [0.5 / 0.5] Tracking hint hidden

## 5. History (1.5/2)

- [0.5 / 0.5] DataGrid ItemSource Binding & IsReadOnly
- [0.5 / 0.5] DataGrid Columns (Headers, Binding), Disable AutoGenerateColumns
- [0.5 / 0.5] ObservableCollection usage for Activity History List
- [0 / 0.5] Output Data Formats (DateTime dd.MM.yyyy hh:mm, TimeSpan hh:mm:ss, Energy 2 digits)

## 6. Delete Activity (2/2)

- [0.5 / 0.5] MainViewModel Delete Command implementation & Binding
- [0.5 / 0.5] DataGrid SelectedItem (or SelectedIndex) Handling & Binding
- [0.5 / 0.5] Delete Command CanExecute handled
- [0.5 / 0.5] Activity is deleted from history (+ UI updated)

## 7. Calculate Burned Calories (2/2)

- [0.5 / 0.5] Burned Calories Service (DI, Interface)
- [0.5 / 0.5] No Code Duplication (Service used in MainWindow and ActivityWindow)
- [1.0 / 1.0] Valid burned calories calculation

## Remarks

- 😎👍🏻

> ### Notes
>
> Comments in your code:
> |     |                                                                                          |
> | --- | ---------------------------------------------------------------------------------------- |
> |  ℹ️  | hint or comment how to code better in the future                                          |
> | ⚠️ | warning - ugly coding style that should be avoided in the future                          |
> | 💥 | code smell, error, fail -  bad code (counts against your coding issues)                   |
> | ❌ | implementation error requirement not implemented correctly                                |
> |    |                                                                                            |
>
> `*` **Clean Code** points can only be earned if the requirement is mostly solved.
> &nbsp;
