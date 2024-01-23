# Grading PA2

Name: Sandu

Grade: **1**

Points: **18,5/20**

## Minimum Requirements

### Clean Coding (3/3)

- [1 / 1] No Code Smells / Code Quality Issues (Max 3 Fails) [Found 0]
- [1 / 1] No Compiler Warnings (Max 5) [Found 0]
- [1 / 1] Use Async Code where ever possible (Max 3 Fails) [Found 1]

### Requirements (7/7)

- [0,5 / 0,5] Handle Command Line Args (import / clear / help)
- [0,5 / 0,5] Create EF Core Model (Model and Properties)
- [0,5 / 0,5] Configure EF Core Relations (PK, FK, Navigation Properties)
- [0,5 / 0,5] Configure EF Core Unique Index
- [0,5 / 0,5] Configure EF Core Model Annotations (MaxLength, Range, Precision and Scale)
- [0,5 / 0,5] Setup Migrations
- [0,5 / 0,5] Create and Configure DbContext (DBSets, Options)
- [0,5 / 0,5] DbContext Factory and appsettings.json
- [0,5 / 0,5] DbContext Conventions (Singular table names, disable cascade delete)
- [0,5 / 0,5] Read Json and Deserialize
- [0,5 / 0,5] Appropriate Json deserialize model (data types, names, attributes)
- [0,5 / 0,5] Import Product and Vendor Data in Database
- [0,5 / 0,5] Use Transaction for Import (commit, rollback)
- [0,5 / 0,5] Import SpecialOffer and Availability (Handle Product & Vendor Ids)

## Additional Requirements

### Project Structure (2/2)

- [1 / 1] Clean Solution Structure (Projects, Folders, Dependencies) (Max 1 Fail) [Found 0]
- [1 / 1] Clean Code Structure (Namespaces, one class per file, etc.) (Max 1 Fail) [Found 0]

### Clear Database (2/2)

- [0,5 / 0,5] Clear Database entities in appropriate order (e.g. SpecialOffer before Products)
- [0,5 / 0,5] Use Transaction (explicit or implicit) for Clear
- [0,5 / 0,5] Use RawSql (or smiliar) for clear database (no loading of entites only to delete)
- [0,5 / 0,5] Clean Code* (code in *BestPrice.Logic*)

### Calculate Price (4,5/6)

- [0,5 / 0,5] Parse Default Shopping List (list without errors)
- [**0,0** / 0,5] Parse Faulty Shopping List (error resistant parsing)
- [0,5 / 0,5] Check availability of products (code in *BestPrice.Logic*)
- [0,5 / 0,5] Create appropriate data structure for calculation (see snippet)
- [0,5 / 0,5] Calculate summed up price without discount (code in *BestPrice.Logic*)
- [0,5 / 0,5] Performant database queries (AsNoTracking)
- [1.0 / 1.0] Choose vendor with lowest price (code in *BestPrice.Logic*)
- [**0,5** / 1.0] Handle output of calculated price (override ToString)
- [**0,5** / 1.0] Clean Code* and Code Reuse (no duplication)

### Calculate with Discount [OPTIONAL / Extra Points] (0/3)

- [**0** / 1] Create additional strategy for price with discount
- [**0** / 1] Calculate price with applied discount
- [**0** / 1] Output Code Reuse (override ToString)

## Remarks

- Watch naming conventions: `appsettings.json` without `-`
- `await using` means you are working with a resource that needs asynchronous disposal and you want to await the completion of its disposal. This is particularly useful when you deal with asynchronous operations where both the operation and the disposal of the resource are asynchronous.

  ```csharp
  await using (var resource = await   GetAsyncDisposableResource())
  {
      // Use the resource here
  }
  // The resource is automatically disposed of   asynchronously after this block
  ```

- Split BestPriceLogic in ImportService, PriceCalculationService - Hint: Single Responsibility Principle

> ### Notes
>
> Comments in your code:
> |     |                                                                                          |
> | --- | ---------------------------------------------------------------------------------------- |
> | ℹ️  | hint or comment how to code better in the future                                          |
> | ⚠️ | warning - ugly coding style that should be avoided in the future                         |
> | ❌ | code smell, error,fail - really bad code (counts against your max 3 clean coding issues) |
> |     |                                                                                          |
>
> `*` **Clean Code** points can only be earned if the requirement is mostly solved.
>
