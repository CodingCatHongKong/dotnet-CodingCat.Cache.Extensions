# CodingCat.Cache.Extensions

Extends the `CodingCat.Cache` [https://github.com/CodingCatHongKong/dotnet-CodingCat.Cache] for consuming the storages by `Type` instead of only string.

Remark:

Required `CodingCat.Serializers` [https://github.com/CodingCatHongKong/dotnet-CodingCat.Serializers] for such feature.

`JsonSerializer<T>` will be used by default if no serialize is provided.

```csharp
var item = this.Storage.Get<Item>(usingKey);
if (item == null)
  item = this.GetItemById(itemId);
this.Storage.Add(usingKey, item);

// --

var item = this.Storage.Get(
  usingKey,
  () => this.GetItemById(itemId)
);

// --

var isEnabledUser = this.Storage.Get(usingKey, new BooleanSerializer());

// --

var item = this.StorageManager.Get<Item>(
  usingKey,
  FallbackPolicy.Default
);

// --

var item = this.StorageManager.Get(
  usingKey,
  FallbackPolicy.Default,
  () => this.GetItemById(itemId)
);
```


#### Target Frameworks

- .Net 4.6.1+
- .Net Standard 2.0+